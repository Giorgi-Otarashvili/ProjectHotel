using Hotel.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Repository.Interfaces
{
    public interface IManagerRepository : IRepositoryBase<Manager>
    {
        Task<Manager?> GetByEmailAsync(string email);
        Task<bool> ExistsByPersonalIdAsync(string personalId);
    }
}
