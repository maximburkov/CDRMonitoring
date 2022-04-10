using System.Linq.Expressions;

namespace CDRMonitorig.Domain
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>>? IsSatisfiedBy { get; }

        Expression<Func<T, object>>? GroupBy { get; }

        int? HavingCount { get; }
    }
}
