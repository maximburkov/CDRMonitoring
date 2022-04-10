﻿using CDRMonitorig.Domain.Entities;

namespace CDRMonitorig.Domain
{
    public interface ICallDetailsRepository
    {
        Task<IEnumerable<Call>> GetAll();

        Task<TotalInformation> GetTotalInformation();

        Task<IEnumerable<Call>> GetCallsBySpec(ISpecification<Call> specification);
    }
}
