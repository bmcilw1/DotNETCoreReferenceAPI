using System;
using TodoAPI.Models;
using TodoAPI.Repositories;

namespace TodoAPIIntegrationTests
{
    public static class SeedData
    {
        public static void PopulateTestData(AppDbContext dbContext)
        {
            dbContext.Todo.Add(new Todo() { Id = 1, Name = "Feed the dog", IsComplete = false });
            dbContext.Todo.Add(new Todo() { Id = 2, Name = "Do things", IsComplete = true });
            dbContext.Todo.Add(new Todo() { Id = 3, Name = "Do things new", IsComplete = false });
            dbContext.SaveChanges();
        }
    }
}