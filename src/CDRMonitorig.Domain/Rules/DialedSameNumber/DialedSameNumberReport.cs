using CDRMonitorig.Domain.Rules.Interfaces;
using CDRMonitorig.Domain.ValueObjects;

namespace CDRMonitorig.Domain.Rules.DialedSameNumber
{
    public class DialedSameNumberReport : IMultilineReport<DialedSameNumberReportItem>
    {
        public DialedSameNumberReport(IEnumerable<DialedSameNumberReportItem> items)
        {
            Items = items;
        }

        public IEnumerable<DialedSameNumberReportItem> Items { get; }
    }

    public class DialedSameNumberReportItem
    {
        public PhoneNumber Number { get; set; }

        public int Count { get; set; }

        public decimal Cost { get; set; }
    }
}
