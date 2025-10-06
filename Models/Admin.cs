using System.ComponentModel.DataAnnotations;

namespace DayCareProject.Models
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        // This is important! There should NOT be a "Password" property
        // Only PasswordHash is needed for hashed password storage
        [Required]
        public string PasswordHash { get; set; }
    }
}
