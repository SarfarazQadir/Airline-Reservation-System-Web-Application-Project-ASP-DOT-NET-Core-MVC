using System.ComponentModel.DataAnnotations;

namespace AirlineReservation.Models
{
    public class TicketStatus
    {
        [Key]
        public int StatusId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int ReservationId { get; set; }

        [Required]
        public int FlightId { get; set; }

        [MaxLength(500)]
        public string StatusUpdate { get; set; } // Any last-minute changes or status

        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}
