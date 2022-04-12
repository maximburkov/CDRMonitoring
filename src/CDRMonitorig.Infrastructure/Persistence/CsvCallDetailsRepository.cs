using CDRMonitorig.Domain;
using CDRMonitorig.Domain.Entities;
using CDRMonitorig.Infrastructure.Persistence.Maps;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Text;

namespace CDRMonitorig.Infrastructure.Persistence
{
    public class CsvCallDetailsRepository : ICallDetailsRepository
    {
        private readonly bool _useCache;
        private bool _isCacheActual = false;
        private List<Call>? _cachedValues = null;
        private readonly FileService _fileService;

        private readonly CsvConfiguration _configuration = new(CultureInfo.InvariantCulture)
        {
            Encoding = Encoding.UTF8, 
            Delimiter = "," 
        };

        public CsvCallDetailsRepository(FileService fileService, bool useCache = true)
        {
            _useCache = useCache;
            _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
        }

        public async Task<IEnumerable<Call>> GetAll()
        {
            if (_isCacheActual && _cachedValues != null) return _cachedValues;

            string? filename = _fileService.Filename;

            if (filename is null) throw new Exception("Source file should be initialized before using repository.");

            List<Call> calls = new();

            try
            {
                await using var fs = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
                using var textReader = new StreamReader(fs, Encoding.UTF8);
                using var csv = new CsvReader(textReader, _configuration);
                csv.Context.RegisterClassMap<CallByNameMap>();
                calls = csv.GetRecords<Call>().Where(call => call is not null).ToList();
            }
            catch (Exception ex)
            {
                //TODO: handle csv exceptions.
            }

            if (_useCache)
            {
                // TODO: need to test in concurrent scenarios.
                _cachedValues = calls;
                _isCacheActual = true;
            }

            return calls;
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
