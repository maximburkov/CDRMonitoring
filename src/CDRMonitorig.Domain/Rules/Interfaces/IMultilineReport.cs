namespace CDRMonitorig.Domain.Rules.Interfaces
{
    public interface IMultilineReport<T> : IReport
    {
        public IEnumerable<T> Items { get; }
    }
}
