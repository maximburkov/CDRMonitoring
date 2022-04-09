using CDRMonitorig.Domain.ValueObjects;

namespace CDRMonitorig.Domain
{
    public class Call
    {
        public string Id { get; set; }

        public PhoneNumber Dialed { get; set; }

        public PhoneNumber Caller { get; set; }

        public decimal SalesPrice { get; set; }
    }
}
