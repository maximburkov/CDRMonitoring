using CDRMonitorig.Domain.Entities;
using CDRMonitorig.Domain.Specification;

namespace CDRMonitorig.Domain.Rules.FromSameCaller
{
    internal class GroupByCallerNumberSpec : BaseSpecification<Call>
    {
        public GroupByCallerNumberSpec(int threshold)
        {
            ApplyGroupByHavingCount(i => i.Caller, threshold);
        }
    }
}
