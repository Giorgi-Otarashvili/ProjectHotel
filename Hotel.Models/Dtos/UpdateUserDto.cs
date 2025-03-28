using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Models.Dtos
{
    public class UpdateUserDto
    {
        [Required, MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required, MaxLength(11)]
        public string PersonalId { get; set; } = string.Empty;

        [Required, Phone, MaxLength(20)]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
