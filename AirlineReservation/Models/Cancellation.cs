using System.ComponentModel.DataAnnotations;

namespace AirlineReservation.Models
{
    public class Cancellation
    {
        [Key]
        public int CancellationId { get; set; }

        [Required]
        public int ReservationId { get; set; }

        [Required, MaxLength(50)]
        public string CancellationNumber { get; set; }

        [Required, Range(0, double.MaxValue)]
        public decimal RefundAmount { get; set; }

        [Required]
        public DateTime CancellationDate { get; set; }
    }
}
