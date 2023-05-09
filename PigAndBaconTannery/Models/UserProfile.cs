using System;
using System.ComponentModel.DataAnnotations;

namespace PigAndBaconTannery.Models
{
    public class UserProfile
    {
        public int Id { get; set; }
        [Required]
        [StringLength(28, MinimumLength = 28)]
        public string FirebaseUserId { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(255)]
        public string Email { get; set; }
        public string ImageUrl { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
