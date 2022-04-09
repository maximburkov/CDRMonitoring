namespace CDRMonitorig.Domain
{
    public class InformationService
    {
        private readonly ICallDetailsRepository _repository;

        public InformationService(ICallDetailsRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<TotalInformation> GetTotalInfo()
        {
            return await _repository.GetTotalInfo();
        }
    }
}
