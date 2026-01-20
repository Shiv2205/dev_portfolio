using System.ComponentModel.DataAnnotations;

namespace blzrTest.Components.Data
{
    public class ContactMessage
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Message { get; set; }
    }
}
