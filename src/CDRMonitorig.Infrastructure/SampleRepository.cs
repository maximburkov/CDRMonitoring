using CDRMonitorig.Domain;

namespace CDRMonitorig.Infrastructure
{
    public class SampleRepository : ICallDetailsRepository
    {
        public Task<TotalInformation> GetTotalInfo()
        {
            return Task.FromResult(new TotalInformation
            {
                Cost = 5,
                Count = 10,
                Minutes = 60
            });
        }

        public Task<Call> GetCallBySpec(ISpecification<Call> specification)
        {
            throw new NotImplementedException();
        }
    }
}
