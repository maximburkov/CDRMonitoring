using CDRMonitorig.Domain;
using CDRMonitorig.Domain.ValueObjects;

namespace CDRMonitorig.Infrastructure
{
    public class SampleRepository : ICallDetailsRepository
    {
        private readonly Call[] _calls = new[]
        {
            new Call
            {
                Id = Guid.NewGuid().ToString(),
                Dialed = PhoneNumber.From("888"),
                Caller = PhoneNumber.From("777"),
                SalesPrice = 100
            },
            new Call
            {
                Id = Guid.NewGuid().ToString(),
                Dialed = PhoneNumber.From("888"),
                Caller = PhoneNumber.From("111"),
                SalesPrice = 100
            },
            new Call
            {
                Id = Guid.NewGuid().ToString(),
                Dialed = PhoneNumber.From("222"),
                Caller = PhoneNumber.From("555"),
                SalesPrice = 100
            },
            new Call
            {
                Id = Guid.NewGuid().ToString(),
                Dialed = PhoneNumber.From("333"),
                Caller = PhoneNumber.From("555"),
                SalesPrice = 100
            }
        };


        public Task<TotalInformation> GetTotalInfo()
        {
            return Task.FromResult(new TotalInformation
            {
                Cost = 5,
                Count = 10,
                Minutes = 60
            });
        }

        //public Task<>

        public Task<Call> GetCallBySpec(ISpecification<Call> specification)
        {
            throw new NotImplementedException();
        }
    }
}
