using CDRMonitorig.Domain;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Text;
using CDRMonitorig.Domain.Entities;
using CDRMonitorig.Infrastructure.Persistence.Maps;

namespace CDRMonitorig.Infrastructure.Persistence
{
    public class CsvCallDetailsRepository : ICallDetailsRepository
    {
        private readonly bool _useCache;
        private List<Call>? _cachedValues = null;
        private readonly string _filename;

        private readonly CsvConfiguration _configuration = new(CultureInfo.InvariantCulture)
        {
            Encoding = Encoding.UTF8, 
            Delimiter = "," 
        };

        public CsvCallDetailsRepository(string filename, bool useCache = true)
        {
            _useCache = useCache;
            _filename = filename;
        }


        public async Task<IEnumerable<Call>> GetAll()
        {
            if (_useCache && _cachedValues != null) return _cachedValues;

            List<Call> calls = new();

            try
            {
                await using var fs = File.Open(_filename, FileMode.Open, FileAccess.Read, FileShare.Read);
                using var textReader = new StreamReader(fs, Encoding.UTF8);
                using var csv = new CsvReader(textReader, _configuration);
                csv.Context.RegisterClassMap<CallByNameMap>();
                calls = csv.GetRecords<Call>().Where(call => call is not null).ToList();
            }
            catch (Exception ex)
            {
                //TODO: handle csv exceptions.
            }

            if(_useCache)
                _cachedValues = calls;

            return calls;
        }

        public Task<TotalInformation> GetTotalInformation()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Call>> GetCallsBySpec(ISpecification<Call> specification)
        {
            var calls = await GetAll();

            if (specification.IsSatisfiedBy != null)
            {
                calls = calls.Where(specification.IsSatisfiedBy.Compile());
            }

            if (specification.GroupBy != null)
            {
                var groups = calls.GroupBy(specification.GroupBy.Compile());

                if (specification.HavingCount != null)
                {
                    groups = groups.Where(g => g.Count() >= specification.HavingCount);
                }

                calls = groups.SelectMany(i => i);
            }

            return calls;
        }
    }
}
