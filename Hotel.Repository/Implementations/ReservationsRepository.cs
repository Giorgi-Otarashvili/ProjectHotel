using Hotel.Models.Dtos;
using Hotel.Models.Entities;
using Hotel.Repository.Data;
using Hotel.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Repository.Implementations
{
    public class ReservationsRepository : RepositoryBase<Reservation>, IReservationsRepository
    {
        public ReservationsRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
