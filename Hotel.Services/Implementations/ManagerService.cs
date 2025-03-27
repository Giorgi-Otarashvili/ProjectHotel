using AutoMapper;
using Hotel.Models.Dtos;
using Hotel.Models.Entities;
using Hotel.Repository.Data;
using Hotel.Repository.Implementations;
using Hotel.Repository.Interfaces;
using Hotel.Services.Interfases;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services.Implementations
{
    public class ManagerService : IManagerService
    {
        private readonly IManagerRepository _managerRepository;
        private readonly IMapper _mapper;

        public ManagerService(IManagerRepository managerRepository, IMapper mapper)
        {
            _managerRepository = managerRepository;
            _mapper = mapper;
        }

        public async Task<ManagerDTO> CreateManagerAsync(ManagerDTO managerDTO)
        {
            if (await _managerRepository.ExistsByPersonalIdAsync(managerDTO.PersonalId))
                throw new Exception("Personal ID must be unique");

            var managerEntity = _mapper.Map<Manager>(managerDTO);
            await _managerRepository.AddAsync(managerEntity);
            return _mapper.Map<ManagerDTO>(managerEntity);
        }

        public async Task<ManagerDTO?> UpdateManagerAsync(int managerId, ManagerDTO managerDTO)
        {
            var existingManager = await _managerRepository.GetByIdAsync(managerId);
            if (existingManager == null) return null;

            existingManager.FirstName = managerDTO.FirstName;
            existingManager.LastName = managerDTO.LastName;
            existingManager.Email = managerDTO.Email;
            existingManager.PhoneNumber = managerDTO.PhoneNumber;

            await _managerRepository.UpdateAsync(existingManager);
            return _mapper.Map<ManagerDTO>(existingManager);
        }

        public async Task<bool> DeleteManagerAsync(int managerId)
        {
            var manager = await _managerRepository.GetByIdAsync(managerId);
            if (manager == null) return false;

            await _managerRepository.DeleteAsync(manager);
            return true;
        }

        public async Task<ManagerDTO?> GetManagerByIdAsync(int managerId)
        {
            var manager = await _managerRepository.GetByIdAsync(managerId);
            return manager == null ? null : _mapper.Map<ManagerDTO>(manager);
        }

        public async Task<IEnumerable<ManagerDTO>> GetAllManagersAsync()
        {
            var managers = await _managerRepository.GetAll().ToListAsync();
            return _mapper.Map<IEnumerable<ManagerDTO>>(managers);
        }
    }
}
