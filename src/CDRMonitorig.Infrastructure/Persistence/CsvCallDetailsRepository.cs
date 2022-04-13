using CDRMonitorig.Domain;
using CDRMonitorig.Domain.Entities;
using CDRMonitorig.Infrastructure.Persistence.Maps;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Text;
using Microsoft.Extensions.Logging;

namespace CDRMonitorig.Infrastructure.Persistence
{
    public class CsvCallDetailsRepository : ICallDetailsRepository
    {
        private readonly bool _useCache;
        private bool _isCacheActual = false;
        private List<Call>? _cachedValues = null;
        private readonly FileService _fileService;
        private readonly ILogger<CsvCallDetailsRepository> _logger;

        private readonly CsvConfiguration _configuration = new(CultureInfo.InvariantCulture)
        {
            Encoding = Encoding.UTF8, 
            Delimiter = "," 
        };

        public CsvCallDetailsRepository(FileService fileService, ILogger<CsvCallDetailsRepository> logger, bool useCache = true)
        {
            _useCache = useCache;
            _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
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
            catch (ReaderException ex)
            {
                var innerMessage = ex.InnerException?.Message;
                _logger.LogError("Unable to parse data from file: {innerMessage}. \nFull error: {Message}",
                    innerMessage, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error during reading file. {Message}", ex.Message);
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

            // Specification could be extended with another methods.

            return calls;
        }
    }
}
