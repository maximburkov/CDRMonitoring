using CDRMonitorig.Domain.Rules.Interfaces;

namespace CDRMonitorig.Domain.Rules.DialedSameNumber
{
    public class DialedSameNumberRule : IRule<DialedSameNumberReport>
    {
        private const int Threshold = 5;

        public async Task<DialedSameNumberReport> Apply(ICallDetailsRepository repository)
        {
            var calls = await repository.GetCallsBySpec(new GroupByDialedNumberSpec(Threshold));

            var records = calls.GroupBy(c => c.Dialed)
                .Select(group => new DialedSameNumberReportItem
                {
                    Number = group.Key,
                    Cost = group.Sum(call => call.SalesPrice),
                    Count = group.Count()
                });

            return new DialedSameNumberReport(records, this);
        }

        public string Description { get; } = $"Number dialed more than {Threshold} times";
    }
}
