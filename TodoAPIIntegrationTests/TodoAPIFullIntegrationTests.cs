using System;
using Xunit;
using TodoAPI;
using TodoAPI.Models;
using System.Collections.Generic;
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
            // The endpoint or route of the controller action.
            var httpResponse = await _client.GetAsync("/api/Todo");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var todos = JsonConvert.DeserializeObject<IEnumerable<Todo>>(stringResponse);
            Assert.Contains(todos, p => p.Name == "Feed the dog");
        }
    }
}
