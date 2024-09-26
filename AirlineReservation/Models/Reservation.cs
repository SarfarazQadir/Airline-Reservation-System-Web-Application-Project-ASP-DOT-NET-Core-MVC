using System.ComponentModel.DataAnnotations;

namespace AirlineReservation.Models
{
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int FlightId { get; set; }

        [Required]
        public DateTime DepartureDate { get; set; }

        [Required]
        public string TripType { get; set; } // One-way, Round-trip

        [Required]
        public string Status { get; set; } // Confirmed, Blocked, Canceled

        [MaxLength(50)]
        public string BlockingNumber { get; set; }

        [Required, MaxLength(50)]
        public string ConfirmationNumber { get; set; }

        [Required, Range(0, double.MaxValue)]
        public decimal TotalPrice { get; set; }

        [Range(0, double.MaxValue)]
        public int Mileage { get; set; }
    }
}
