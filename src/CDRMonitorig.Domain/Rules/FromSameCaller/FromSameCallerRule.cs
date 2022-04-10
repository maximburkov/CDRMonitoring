using CDRMonitorig.Domain.Rules.DialedSameNumber;
using CDRMonitorig.Domain.Rules.Interfaces;

namespace CDRMonitorig.Domain.Rules.FromSameCaller
{
    internal class FromSameCallerRule : IRule<FromSameCallerReport>
    {
        private const int Threshold = 5;

        public async Task<FromSameCallerReport> Apply(ICallDetailsRepository repository)
        {
            var calls = await repository.GetCallsBySpec(new GroupByDialedNumberSpec(Threshold));

            var records = calls.GroupBy(c => c.Caller)
                .Select(group => new FromSameCallerReportItem
                {
                    Number = group.Key,
                    Cost = group.Sum(call => call.SalesPrice),
                    Count = group.Count()
                });

            return new FromSameCallerReport(records);
        }
    }
}
