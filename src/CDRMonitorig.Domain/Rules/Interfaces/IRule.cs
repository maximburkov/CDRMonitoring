namespace CDRMonitorig.Domain.Rules.Interfaces
{
    public interface IRule<T> where T : IReport
    {
        public Task<T> Apply(ICallDetailsRepository repository);
    }
}
