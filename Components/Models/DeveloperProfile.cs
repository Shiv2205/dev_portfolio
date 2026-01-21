using System.ComponentModel.DataAnnotations;

namespace dev_portfolio.Components.Models;

public class DeveloperProfile
{
    public string Name { get; set; } = string.Empty;
    public string? Title { get; set; }
    public string? Bio { get; set; } 
    public List<string> Skills { get; set; } = [];
    public string? AvatarUrl { get; set; } 
    public Dictionary<string, string> SocialLinks { get; set; } = [];
}
