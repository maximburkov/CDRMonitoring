namespace CDRMonitorig.Domain
{
    public interface ISpecification<in T>
    {
        Func<T, bool> IsSatisfiedBy { get; }
    }
}
