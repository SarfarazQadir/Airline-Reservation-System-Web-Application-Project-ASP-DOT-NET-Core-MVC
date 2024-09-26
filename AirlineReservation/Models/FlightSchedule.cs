using System.ComponentModel.DataAnnotations;

namespace AirlineReservation.Models
{
    public class FlightSchedule
    {
        [Key]
        public int ScheduleId { get; set; }

        [Required]
        public int FlightId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public DateTime DepartureTime { get; set; }

        [Required]
        public DateTime ArrivalTime { get; set; }

        [Required, Range(0, int.MaxValue)]
        public int AvailableSeats { get; set; }
    }
}
