using CDRMonitorig.Domain;
using CDRMonitorig.Domain.ValueObjects;

namespace CDRMonitorig.Infrastructure
{
    /// <summary>
    /// In memory repository with sample data.
    /// </summary>
    public class SampleCallDetailRepository : ICallDetailsRepository
    {
        private readonly IEnumerable<Call> _calls = new[]
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


        public Task<IEnumerable<Call>> GetAll()
        {
            return Task.FromResult(_calls);
        }

        public Task<TotalInformation> GetTotalInfo()
        {
            return Task.FromResult(new TotalInformation
            {
                Cost = 5,
                Count = 10,
                Minutes = 60
            });
        }

        public Task<IEnumerable<Call>> GetCallsBySpec(ISpecification<Call> specification)
        {
            var result = _calls;

            if (specification.IsSatisfiedBy != null)
            {
                result = result.Where(specification.IsSatisfiedBy.Compile());
            }

            if (specification.GroupBy != null)
            {
                var groups = result.GroupBy(specification.GroupBy.Compile());

                if (specification.HavingCount != null)
                {
                    groups = groups.Where(g => g.Count() >= specification.HavingCount);
                }

                result = groups.SelectMany(i => i);
            }

            return Task.FromResult(result);
        }
    }
}
