namespace CDRMonitorig.Domain.Specification
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification(Func<T, bool> isSatisfiedBy)
        {
            IsSatisfiedBy = isSatisfiedBy;
        }

        public Func<T, bool> IsSatisfiedBy { get; private set; }
    }
}
