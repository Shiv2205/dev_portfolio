using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using dev_portfolio.Components.Data;

namespace Utilities;

public static class Util
{
  public static void SeedTable<T>(DevDbContext db, DbSet<T> table, string filepath, bool isList = false) 
    where T : class
  {
    if (!table.Any())
    {
      var jsonText = File.ReadAllText(filepath);
      if (isList)
      {
        var seedData = JsonSerializer.Deserialize<List<T>>(jsonText);

        if (seedData is not null)
          table.AddRange(seedData);
      }
      else
      {
        var seedData = JsonSerializer.Deserialize<T>(jsonText);

        if (seedData is not null)
          table.Add(seedData);
      }

      db.SaveChanges();
    }
  }
}