using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Hotel.Models.Entities
{
    public class Manager : IdentityUser
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required, MaxLength(11)]
        public string PersonalId { get; set; } = string.Empty; // უნიკალური

        [Required, EmailAddress, MaxLength(100)]
        public string Email { get; set; } = string.Empty; // უნიკალური

        [Required, Phone, MaxLength(20)]
        public string PhoneNumber { get; set; } = string.Empty;

        // Relationships
        [ForeignKey("Hotel")]
        public int HotelId { get; set; }
        public hotel Hotel { get; set; }
    }
}
