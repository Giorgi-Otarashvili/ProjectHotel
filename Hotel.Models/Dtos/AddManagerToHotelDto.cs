using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Models.Dtos
{
    public class AddManagerToHotelDto
    {
        public string ManagerId { get; set; }
        public int HotelId { get; set; }
    }
}
