namespace CDRMonitorig.Domain.Rules
{
    public interface IMultilineReport<T> : IReport
    {
        public IEnumerable<T> Items { get; }
    }
}
