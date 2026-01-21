using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using dev_portfolio.Components;
using dev_portfolio.Components.Models;
using dev_portfolio.Components.Data;
using MudBlazor.Services;

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

var app = builder.Build();

using (var scoped = app.Services.CreateScope())
{
  var services = scoped.ServiceProvider;
  var context = services.GetRequiredService<DevDbContext>();
  context.Database.EnsureCreated();

  if (!context.Projects.Any())
  {
    context.Projects.AddRange(DummyProjects.GetProjects());
    context.SaveChanges();
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

app.MapGet("/api/projects", async (DevDbContext db) => 
    await db.Projects.OrderBy(p => p.Id).ToListAsync());

app.Run();
