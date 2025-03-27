using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Models.Entities
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime CheckIn { get; set; }

        [Required]
        public DateTime CheckOut { get; set; }

        // Relationships
        [ForeignKey("Room")]
        public int RoomId { get; set; }
        public Room Room { get; set; }

        [ForeignKey("Guest")]
        public string GuestId { get; set; }
        public ApplicationUser Guest { get; set; }
    }
}
