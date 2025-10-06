using System.ComponentModel.DataAnnotations;

namespace DayCareProject.Models
{
    public class Facility
    {
        [Key]
        public int FacilityId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        public string ImagePath { get; set; } // store image file path
    }
}
