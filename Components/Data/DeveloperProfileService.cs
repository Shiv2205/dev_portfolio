using Microsoft.EntityFrameworkCore;
using dev_portfolio.Components.Models;

namespace dev_portfolio.Components.Data;

public class DeveloperProfileService(DevDbContext db)
{
  private readonly DevDbContext _db = db;

  public async Task<List<DeveloperProfile>> GetProfiles() =>
    await _db.Profiles.ToListAsync();

  public async Task<DeveloperProfile> GetProfile(string name) =>
    await _db.Profiles.FirstAsync(e => e.Name == name);
}