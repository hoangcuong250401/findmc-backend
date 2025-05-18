using Application.Dtos.User;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IContractRepository:IBaseRepository<Contract, ContractDto>
    {
        /// <summary>
        /// Get contracts that need to be reviewed
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Contract>> GetContractsForReviewAsync();
    }
}
