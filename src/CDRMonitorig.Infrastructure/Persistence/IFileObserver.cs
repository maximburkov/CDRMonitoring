namespace CDRMonitorig.Infrastructure.Persistence
{
    public interface IFileObserver
    {
        public void OnFileChanged(string filename);
    }
}
