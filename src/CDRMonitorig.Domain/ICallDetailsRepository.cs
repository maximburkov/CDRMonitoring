namespace CDRMonitorig.Domain
{
    public interface ICallDetailsRepository
    {
        Task<TotalInformation> GetTotalInfo();

        Task<Call> GetCallBySpec(ISpecification<Call> specification);
    }
}
