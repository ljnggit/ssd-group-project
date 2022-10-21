using Lab1.Attribute;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Lab1.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }


        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }


        [DataType(DataType.Date)]
        [DateValidationAttribute]
        [Display(Name = "Date of Birth")]
        public DateTime? BirthDate { get; set; }

    }
}
