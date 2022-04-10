using CDRMonitorig.Domain.Rules;

namespace CDRMonitorig.Domain
{
    public interface IRule<T> where T : IReport
    {
        public Task<T> Apply();
    }
}
