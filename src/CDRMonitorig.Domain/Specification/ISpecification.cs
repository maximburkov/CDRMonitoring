using System.Linq.Expressions;

namespace CDRMonitorig.Domain
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> IsSatisfiedBy { get; }
    }
}
