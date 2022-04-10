using System.Linq.Expressions;

namespace CDRMonitorig.Domain.Specification
{
    public class BaseSpecification<T> : ISpecification<T>
    {

        protected void ApplyFiltering(Expression<Func<T, bool>> isSatisfiedBy)
        {
            IsSatisfiedBy = isSatisfiedBy;
        }

        protected void ApplyGroupBy(Expression<Func<T, object>> groupByExpression, int? count = null)
        {
            GroupBy = groupByExpression;
        }

        protected void ApplyGroupByHavingCount(Expression<Func<T, object>> groupByExpression, int count)
        {
            GroupBy = groupByExpression;
            HavingCount = count;
        }

        public Expression<Func<T, bool>>? IsSatisfiedBy { get; private set; }
        public Expression<Func<T, object>>? GroupBy { get; private set; }
        public int? HavingCount { get; private set; }
    }
}
