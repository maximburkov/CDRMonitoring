using CDRMonitorig.Domain.Entities;
using CDRMonitorig.Domain.Specification;

namespace CDRMonitorig.Domain.Rules.DialedSameNumber
{
    internal class GroupByDialedNumberSpec : BaseSpecification<Call>
    {
        public GroupByDialedNumberSpec(int threshold)
        {
            ApplyGroupByHavingCount(call => call.Dialed, threshold);
        }
    }
}
