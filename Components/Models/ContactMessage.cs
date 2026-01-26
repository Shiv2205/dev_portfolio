using System.ComponentModel.DataAnnotations;

namespace dev_portfolio.Components.Models;

public class ContactMessage
{
    [Required(ErrorMessage = "Name is required")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Email cannot be empty")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Message field cannot be empty")]
    public string? Message { get; set; }

    public string FormatEmail()
    {
        return 
        @$"
            From: {this.Name}
            Email: {this.Email}


            {this.Message}
        ";
    }
}
