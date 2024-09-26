﻿using System.ComponentModel.DataAnnotations;

namespace AirlineReservation.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required, MinLength(6), MaxLength(100)]
        public string Password { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string? LastName { get; set; }

        [Required, MaxLength(200)]
        public string? Address { get; set; }

        [Required, Phone]
        public string? PhoneNumber { get; set; }

        [Required, EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        public string? Sex { get; set; }
        
        [Required]
        public string? UserImage { get; set; }

        [Range(18, 100)]
        public int? Age { get; set; }

        [CreditCard]
        public string? CreditCardNumber { get; set; }

        public int? SkyMiles { get; set; } = 0;
    }
}