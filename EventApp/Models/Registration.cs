using System.ComponentModel.DataAnnotations;

namespace EventApp.Models;

public class Registration
{
    // A property to link this registration to a specific event.
    public int EventId { get; set; }

    [Required(ErrorMessage = "Your name is required.")]
    [StringLength(100, ErrorMessage = "Name is too long.")]
    public string AttendeeName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Your email is required.")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    public string AttendeeEmail { get; set; } = string.Empty;
}