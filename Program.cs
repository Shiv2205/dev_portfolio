using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using MudBlazor.Services;
using dev_portfolio.Components;
using dev_portfolio.Components.Data;
using dev_portfolio.Components.Models;
using Utilities;

const string SEED_DIR = "seed";

var builder = WebApplication.CreateBuilder(args);

var connection = new SqliteConnection(builder.Configuration["Database:Source"]);
connection.Open();


// Add services to the container.
builder.Services.AddDbContext<DevDbContext>(
    options => options.UseSqlite(connection));                                 // SQLite connection
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddMudServices();
builder.Services.AddScoped<ProjectService>();
builder.Services.AddScoped<DeveloperProfileService>();
builder.Services.Configure<SmtpSettings>(
    builder.Configuration.GetSection("SmtpSettings"));                         // SMTP Settings
builder.Services.AddTransient<IEmailService, EmailService>();                  // Email Service


// Build App
var app = builder.Build();

using (var scoped = app.Services.CreateScope())
{
  var services = scoped.ServiceProvider;
  var context = services.GetRequiredService<DevDbContext>();
  context.Database.EnsureCreated();

  Util.SeedTable(context, context.Projects, $"{SEED_DIR}/projects.json", true);
  Util.SeedTable(context, context.Profiles, $"{SEED_DIR}/dev_profile.json");
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
