namespace dev_portfolio.Components.Models;

public class Project
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public List<string>? Technologies { get; set; }
    public string? ImageUrl { get; set; }
    public string? RepoUrl { get; set; }
    public string? LiveUrl { get; set; }
    public string? DemoUrl { get; set; }
}
