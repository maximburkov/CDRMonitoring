using System.Linq.Expressions;

namespace CDRMonitorig.Domain.Specification
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification(Expression<Func<T, bool>> isSatisfiedBy)
        {
            IsSatisfiedBy = isSatisfiedBy;
        }

        public Expression<Func<T, bool>> IsSatisfiedBy { get; private set; }
    }
}
