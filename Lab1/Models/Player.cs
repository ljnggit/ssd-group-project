using Lab1.Attribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lab1.Models
{
    public class Player
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }


        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Team Name")]
        public string TeamName { get; set; }


        [DataType(DataType.Date)]
        [DateValidationAttribute]
        [Display(Name = "Date of Birth")]
        public DateTime? BirthDate { get; set; }
    }
}
