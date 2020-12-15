using Moq;
using System.Threading.Tasks;
using System.Collections.Generic;
using TodoAPI.Controllers;
using TodoAPI.Services;
using TodoAPI.Models;
using Xunit;

namespace TodoAPITests
{
    public class TodoControllerTests
    {
        [Fact]
        public async Task GetTodoItems_CallsGetAllAsync()
        {
            // Arrange
            var todoServiceMock = new Mock<ITodoService>();
            var todoController = new TodoController(todoServiceMock.Object);

            todoServiceMock.Setup(s => s.GetAllAsync())
                .Returns(Task.FromResult(new List<Todo>() { }));

            // Act
            await todoController.GetTodos();

            // Assert
            todoServiceMock.Verify(s => s.GetAllAsync(), Times.Once());
        }
    }
}
