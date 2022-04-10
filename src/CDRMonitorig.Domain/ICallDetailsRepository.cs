namespace CDRMonitorig.Domain
{
    public interface ICallDetailsRepository
    {
        Task<IEnumerable<Call>> GetAll();

        Task<TotalInformation> GetTotalInfo();

        Task<IEnumerable<Call>> GetCallsBySpec(ISpecification<Call> specification);
    }
}
