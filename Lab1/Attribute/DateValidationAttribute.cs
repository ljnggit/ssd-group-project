using System.ComponentModel.DataAnnotations;

namespace Lab1.Attribute
{
    public class DateValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return null;

            DateTime dt = (DateTime)value;

            if (dt <= DateTime.Now)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("The given date must not surpass today's date");
            }
        }
    }
}
