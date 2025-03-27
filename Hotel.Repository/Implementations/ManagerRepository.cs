using Hotel.Models.Entities;
using Hotel.Repository.Data;
using Hotel.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Repository.Implementations
{
    public class ManagerRepository : RepositoryBase<Manager>, IManagerRepository
    {
        public ManagerRepository(ApplicationDbContext context) : base(context) { }

        public async Task<Manager?> GetByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(m => m.Email == email);
        }

        public async Task<bool> ExistsByPersonalIdAsync(string personalId)
        {
            return await _dbSet.AnyAsync(m => m.PersonalId == personalId);
        }
    }
}
