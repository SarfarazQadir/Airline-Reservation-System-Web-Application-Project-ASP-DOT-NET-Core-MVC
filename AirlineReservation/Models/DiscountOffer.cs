using System.ComponentModel.DataAnnotations;

namespace AirlineReservation.Models
{
    public class DiscountOffer
    {
        [Key]
        public int OfferId { get; set; }

        [Required]
        public int FlightId { get; set; }

        [Required, Range(0, 100)]
        public double DiscountPercentage { get; set; }

        [Required]
        public DateTime ValidFrom { get; set; }

        [Required]
        public DateTime ValidTo { get; set; }
    }
}
