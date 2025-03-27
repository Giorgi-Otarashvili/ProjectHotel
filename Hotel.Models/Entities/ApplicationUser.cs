using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Required, MaxLength(50)]

        public string FirstName { get; set; } = string.Empty;
        [Required, MaxLength(50)]

        public string LastName {  get; set; } = string.Empty;

        [Required, MaxLength(11)]
        public string PersonalId { get; set; } = string.Empty; // უნიკალური

        [Required, Phone, MaxLength(20)]
        public string PhoneNumber { get; set; } = string.Empty;

        public RoleEnum Role { get; set; } = RoleEnum.Guest; // rolemanager-ში ძებნა რომ არ მოგვიწიოს ყოველთვის

        public List<Reservation>? Reservations { get; set; }

        // Relationships
        [ForeignKey("Hotel")]
        public int? HotelId { get; set; }
        public hotel? Hotel { get; set; }
    }
}
