using System.ComponentModel.DataAnnotations;

namespace AirlineReservation.Models
{
    public class UserAction
    {
        [Key]
        public int ActionId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required, MaxLength(100)]
        public string ActionType { get; set; } // Register, Update Profile, Cancel Ticket, etc.

        [Required]
        public DateTime ActionDate { get; set; } = DateTime.Now;
    }
}
