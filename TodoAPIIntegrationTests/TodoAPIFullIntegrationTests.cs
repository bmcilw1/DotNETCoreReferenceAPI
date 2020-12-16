using System;
using Xunit;
using TodoAPI;
using TodoAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TodoAPIIntegrationTests
{
    public class TodoAPIFullIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public TodoAPIFullIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAllTodos_ReturnsTodosInDb()
        {
            // Arrange, See CustomWebApplicationFactory

            // Act
            var httpResponse = await _client.GetAsync("/api/Todo");

            // Assert
            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var todos = JsonConvert.DeserializeObject<IEnumerable<Todo>>(stringResponse);
            Assert.Contains(todos, t => t.Name == "Feed the dog");
            Assert.Equal(3, todos.Count());
        }

        [Fact]
        public async Task GetTodo_ReturnsSpecifiedTodo()
        {
            // Arrange, See CustomWebApplicationFactory

            // Act
            var httpResponse = await _client.GetAsync("/api/Todo/1");

            // Assert
            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var todo = JsonConvert.DeserializeObject<Todo>(stringResponse);
            Assert.Equal(1, todo.Id);
        }
    }
}
