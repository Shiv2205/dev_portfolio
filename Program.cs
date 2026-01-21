using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using System.Text.Json;
using MudBlazor.Services;
using dev_portfolio.Components;
using dev_portfolio.Components.Models;
using dev_portfolio.Components.Data;

const string SEED_DIR = "seed";

var builder = WebApplication.CreateBuilder(args);

var connection = new SqliteConnection("DataSource=portfolio.db");
connection.Open();

// Add services to the container.
builder.Services.AddDbContext<DevDbContext>(options => options.UseSqlite(connection));
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddMudServices();
builder.Services.AddScoped<ProjectService>();
builder.Services.AddSingleton<DeveloperProfile>();

// Build App
var app = builder.Build();

using (var scoped = app.Services.CreateScope())
{
  var services = scoped.ServiceProvider;
  var context = services.GetRequiredService<DevDbContext>();
  context.Database.EnsureCreated();

  if (!context.Projects.Any())
  {
    var jsonText = File.ReadAllText($"{SEED_DIR}/projects.json");
    var seedData = JsonSerializer.Deserialize<List<Project>>(jsonText);
    if (seedData is not null)
    {
      context.Projects.AddRange(seedData);
      context.SaveChanges();
    }
  }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Start App
app.Run();
