using Microsoft.EntityFrameworkCore;
using dev_portfolio.Components.Models;

namespace dev_portfolio.Components.Data;
public class ProjectService(DevDbContext db)
{
  private readonly DevDbContext _db = db;

  public async Task<List<Project>> GetProjectsAsync() => 
    await _db.Projects.ToListAsync();
}
