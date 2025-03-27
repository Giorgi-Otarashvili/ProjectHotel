using Hotel.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services.Interfases
{
    public interface IManagerService
    {
        Task<ManagerDTO> CreateManagerAsync(ManagerDTO managerDTO);
        Task<ManagerDTO?> UpdateManagerAsync(int managerId, ManagerDTO managerDTO);
        Task<bool> DeleteManagerAsync(int managerId);
        Task<ManagerDTO?> GetManagerByIdAsync(int managerId);
        Task<IEnumerable<ManagerDTO>> GetAllManagersAsync();
    }
}
