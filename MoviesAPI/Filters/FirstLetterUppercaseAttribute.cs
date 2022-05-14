using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Filters
{
    public class FirstLetterUppercaseAttribute:ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var firstLetter = value.ToString()[0].ToString();
            if (value == null || String.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }
            if (firstLetter != firstLetter.ToUpper())
            {
                return new ValidationResult("First Letter Should Be Uppercase");
            }
            return ValidationResult.Success;
        }
    }
}
