using CDRMonitorig.Domain.Rules.Interfaces;
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
        public PhoneNumber? Number { get; set; }

        public int Count { get; set; }

        public decimal Cost { get; set; }
        public override string ToString() =>
            $"Number Dialed: {Number}\nCall count: {Count}\nCall cost: {Math.Round(Cost, 2)}";
    }
}
