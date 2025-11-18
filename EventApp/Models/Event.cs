using System.ComponentModel.DataAnnotations;

namespace EventApp.Models;

public class Event
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Event name is required.")]
    [StringLength(50, ErrorMessage = "Event name cannot be longer than 50 characters.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Event date is required.")]
    // Custom validation to ensure the date is not in the past.
    [DateNotInPast(ErrorMessage = "Event date cannot be in the past.")]
    public DateTime Date { get; set; } = DateTime.Now; // Default to today

    [Required(ErrorMessage = "Location is required.")]
    public string Location { get; set; } = string.Empty;
}


// A custom validation attribute to check the date.
public class DateNotInPastAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is DateTime dateValue)
        {
            if (dateValue.Date < DateTime.Now.Date)
            {
                return new ValidationResult(ErrorMessage);
            }
        }
        return ValidationResult.Success;
    }
}