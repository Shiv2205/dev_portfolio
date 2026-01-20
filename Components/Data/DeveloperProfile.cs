namespace blzrTest.Components.Data
{
    public class DeveloperProfile
    {
        public string Name { get; set; } = "Shivam Vee";
        public string Title { get; set; } = "Software Engineer";
        public string Bio { get; set; } = "I am a passionate software engineer with a love for creating elegant and efficient solutions. I have experience in a variety of technologies, including C#, .NET, Blazor, and Azure.";
        public List<string> Skills { get; set; } = new List<string> { "C#", ".NET", "Blazor", "Azure", "SQL", "Git" };
        public string AvatarUrl { get; set; } = "https://avatars.githubusercontent.com/u/1234567?v=4";
        public Dictionary<string, string> SocialLinks { get; set; } = new Dictionary<string, string>
        {
            { "GitHub", "https://github.com/johndoe" },
            { "LinkedIn", "https://linkedin.com/in/johndoe" }
        };
    }
}
