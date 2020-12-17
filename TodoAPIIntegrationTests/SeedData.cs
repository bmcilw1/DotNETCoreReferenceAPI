using System;
using TodoAPI.Models;
using TodoAPI.Repositories;

namespace TodoAPIIntegrationTests
{
    public static class SeedData
    {
        public static void PopulateTestData(AppDbContext dbContext)
        {
            dbContext.Todos.Add(new Todo() { Id = 1, Name = "Feed the dog", IsComplete = false });
            dbContext.Todos.Add(new Todo() { Id = 2, Name = "Do things", IsComplete = true });
            dbContext.Todos.Add(new Todo() { Id = 3, Name = "The Todo to be updated", IsComplete = false });
            dbContext.Todos.Add(new Todo() { Id = 4, Name = "The Todo to be deleted", IsComplete = false });
            dbContext.SaveChanges();
        }
    }
}