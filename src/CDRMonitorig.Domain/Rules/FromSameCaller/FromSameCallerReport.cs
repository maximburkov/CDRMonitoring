using CDRMonitorig.Domain.ValueObjects;

namespace CDRMonitorig.Domain.Rules.FromSameCaller
{
    public class FromSameCallerReport : IMultilineReport<FromSameCallerReportItem>
    {
        public FromSameCallerReport(IEnumerable<FromSameCallerReportItem> items)
        {
            Items = items;
        }

        public IEnumerable<FromSameCallerReportItem> Items { get; }
    }

    public class FromSameCallerReportItem
    {
        public PhoneNumber Number { get; set; }

        public int Count { get; set; }

        public decimal Cost { get; set; }
    }
}
