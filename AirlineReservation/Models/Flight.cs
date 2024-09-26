using System.ComponentModel.DataAnnotations;

namespace AirlineReservation.Models
{
    public class Flight
    {
        [Key]
        public int FlightId { get; set; }
            
        [Required]
        public string FlightNumber { get; set; }

        [Required, MaxLength(100)]
        public string OriginCity { get; set; }

        [Required, MaxLength(100)]
        public string DestinationCity { get; set; }

        [Required]
        public DateTime DepartureTime { get; set; }

        [Required]
        public DateTime ArrivalTime { get; set; }

        [Range(0, 24)]
        public double Duration { get; set; }

        [Required, Range(1, 500)]
        public int TotalSeats { get; set; }

        [Required, Range(0, 500)]
        public int AvailableSeats { get; set; }

        [Required, MaxLength(50)]
        public string Class { get; set; } // Business, First, Club, etc.
    }
}
