using System.ComponentModel.DataAnnotations;

namespace DayCareProject.Models
{
    public class Kid
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Age")]
        public int Age { get; set; }

        [Required]
        [Display(Name = "Parent Name")]
        public string ParentName { get; set; }
    }
}
