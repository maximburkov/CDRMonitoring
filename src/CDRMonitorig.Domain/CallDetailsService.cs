using CDRMonitorig.Domain.Entities;
using CDRMonitorig.Domain.Rules.Interfaces;

namespace CDRMonitorig.Domain
{
    public class CallDetailsService
    {
        private readonly ICallDetailsRepository _repository;
        public CallDetailsService(ICallDetailsRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<TotalInformation> GetTotalInformation()
        {
            var calls = (await _repository.GetAll()).ToList();

            return new TotalInformation
            {
                Count = calls.Count,
                Duration = TimeSpan.FromTicks(calls.Sum(call => call.Duration.Ticks)),
                Cost = calls.Sum(c => c.SalesPrice)
            };
        }

        public async Task<T> GetReportForRule<T>(IRule<T> rule) where T : IReport
        {
            return await rule.Apply(_repository);
        }
    }
}
