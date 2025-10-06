using System.ComponentModel.DataAnnotations;

namespace DayCareProject.Models
{
    public class ChildApplication
    {
        public int Id { get; set; }

        // Child Info
        [Required(ErrorMessage = "Child surname is required")]
        [Display(Name = "Child Surname")]
        public string ChildSurname { get; set; }

        [Required(ErrorMessage = "Child full name is required")]
        [Display(Name = "Child Full Name")]
        public string ChildFullName { get; set; }

        [Required(ErrorMessage = "Date of birth is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Home Language")]
        public string HomeLanguage { get; set; }

        [Display(Name = "Residential Address")]
        public string ResidentialAddress { get; set; }

        // Parent/Guardian Info
        [Required(ErrorMessage = "Parent full name is required")]
        [Display(Name = "Parent Full Name")]
        public string ParentFullName { get; set; }

        [Required(ErrorMessage = "Parent surname is required")]
        [Display(Name = "Parent Surname")]
        public string ParentSurname { get; set; }

        [Display(Name = "Occupation")]
        public string Occupation { get; set; }

        [Phone(ErrorMessage = "Invalid work telephone number")]
        [Display(Name = "Work Telephone")]
        public string WorkTel { get; set; }

        [Phone(ErrorMessage = "Invalid cellphone number")]
        [Required(ErrorMessage = "Cellphone number is required")]
        [Display(Name = "Cellphone Number")]
        public string Cell { get; set; }

        // Next of Kin
        [Required(ErrorMessage = "Next of kin name is required")]
        [Display(Name = "Name")]
        public string KinName { get; set; }

        [Required(ErrorMessage = "Next of kin surname is required")]
        [Display(Name = "Surname")]
        public string KinSurname { get; set; }

        [Required(ErrorMessage = "Relation to next of kin is required")]
        [Display(Name = "Relationship to Child")]
        public string KinRelation { get; set; }

        [Display(Name = "Date Submitted")]
        public DateTime DateSubmitted { get; set; } = DateTime.Now;
    }
}
