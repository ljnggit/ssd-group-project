using Lab1.Attribute;
using System.ComponentModel.DataAnnotations;

namespace Lab1.Models
{
    public class Team
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Team Name")]
        public string TeamName { get; set; }

        [Required]
        [RegularExpression("[a-zA-Z0-9\\.]+@[a-zA-Z0-9]+\\.[a-zA-Z]{2,6}", ErrorMessage = "The Email field is not a valid e-mail address.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [DataType(DataType.Date)]
        [Display(Name = "Established Date")]
        public DateTime? EstablishedDate { get; set; }
    }
}
