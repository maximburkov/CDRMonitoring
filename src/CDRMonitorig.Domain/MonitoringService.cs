using CDRMonitorig.Domain.Rules;

namespace CDRMonitorig.Domain
{
    public class MonitoringService
    {
        private readonly ICallDetailsRepository _repository;
        public MonitoringService(ICallDetailsRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<T> ApplyRule<T>(IRule<T> rule) where T : IReport
        {
            return await rule.Apply();
        }
    }
}
