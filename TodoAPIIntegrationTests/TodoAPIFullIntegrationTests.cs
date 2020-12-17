using Xunit;
using TodoAPI;
using TodoAPI.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;

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
            Assert.Contains(todos, t => t.Name == "First Todo to be read");
            Assert.Contains(todos, t => t.Name == "Second Todo to be read");
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

        [Fact]
        public async Task PostTodo_CreatesNewTodo()
        {
            // Arrange
            var todo = new Todo() { Name = "todo to be newly created", IsComplete = false };

            // Act
            var httpResponse = await _client.PostAsJsonAsync("/api/Todo", todo);

            // Assert
            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var todoResponse = JsonConvert.DeserializeObject<Todo>(stringResponse);
            Assert.IsType<int>(todoResponse.Id);
            Assert.True(todoResponse.Id > 0);
            var httpResponseValidate = await _client.GetAsync($"/api/Todo/{todoResponse.Id}");
            httpResponseValidate.EnsureSuccessStatusCode();
            var stringResponseValidate = await httpResponseValidate.Content.ReadAsStringAsync();
            var todoResponseValidate = JsonConvert.DeserializeObject<Todo>(stringResponseValidate);
            Assert.Equal(todo.Name, todoResponseValidate.Name);
        }

        [Fact]
        public async Task PutTodo_UpdatesTodo()
        {
            // Arrange
            var todo = new Todo { Id = 3, Name = "todo to be updated - update", IsComplete = true };

            // Act
            var httpResponse = await _client.PutAsJsonAsync($"/api/Todo/{todo.Id}", todo);

            // Assert
            httpResponse.EnsureSuccessStatusCode();
            var httpResponseValidate = await _client.GetAsync($"/api/Todo/{todo.Id}");
            httpResponseValidate.EnsureSuccessStatusCode();
            var stringResponseValidate = await httpResponseValidate.Content.ReadAsStringAsync();
            var todoResponseValidate = JsonConvert.DeserializeObject<Todo>(stringResponseValidate);
            Assert.Equal(todo.Name, todoResponseValidate.Name);
        }

        [Fact]
        public async Task DeleteTodo_DeletesTodo()
        {
            // Arrange
            var id = 4;
            var httpResponseValidate = await _client.GetAsync($"/api/Todo/{id}");
            Assert.Equal(HttpStatusCode.OK, httpResponseValidate.StatusCode);

            // Act
            var httpResponse = await _client.DeleteAsync($"/api/Todo/{id}");

            // Assert
            httpResponse.EnsureSuccessStatusCode();
            httpResponseValidate = await _client.GetAsync($"/api/Todo/{id}");
            Assert.Equal(HttpStatusCode.NotFound, httpResponseValidate.StatusCode);
        }
    }
}
