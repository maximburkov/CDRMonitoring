namespace CDRMonitorig.Domain.Rules.DialedSameNumber
{
    public class DialedSameNumberRule : IRule<DialedSameNumberReport>
    {
        private const int Threshold = 5;
        private readonly ICallDetailsRepository _repository;

        public DialedSameNumberRule(ICallDetailsRepository repository)
        {
            _repository = repository;
        }

        public async Task<DialedSameNumberReport> Apply()
        {
            var calls = await _repository.GetCallsBySpec(new GroupByDialedNumberSpec(Threshold));

            var records = calls.GroupBy(c => c.Dialed)
                .Select(group => new DialedSameNumberReportItem
                {
                    Number = group.Key,
                    Cost = group.Sum(call => call.SalesPrice),
                    Count = group.Count()
                });

            return new DialedSameNumberReport(records);
        }
    }
}
