using System;
using TodoAPI.Models;
using TodoAPI.Repositories;

namespace TodoAPIIntegrationTests
{
    public static class SeedData
    {
        public static void PopulateTestData(AppDbContext dbContext)
        {
            dbContext.Todos.Add(new Todo { Id = 1, Name = "First Todo to be read", IsComplete = false });
            dbContext.Todos.Add(new Todo { Id = 2, Name = "Second Todo to be read", IsComplete = true });
            dbContext.Todos.Add(new Todo { Id = 3, Name = "The Todo to be updated", IsComplete = false });
            dbContext.Todos.Add(new Todo { Id = 4, Name = "The Todo to be deleted", IsComplete = false });
            dbContext.SaveChanges();
        }
    }
}