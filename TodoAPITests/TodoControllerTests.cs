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
        Mock<ITodoService> _todoServiceMock;
        TodoController _todoController;

        public TodoControllerTests()
        {
            _todoServiceMock = new Mock<ITodoService>();
            _todoController = new TodoController(_todoServiceMock.Object);
        }

        [Fact]
        public async Task GetTodoItems_CallsGetAllAsync()
        {
            // Arrange

            _todoServiceMock.Setup(s => s.GetAllAsync())
                .Returns(Task.FromResult(new List<Todo>() { }));

            // Act
            await _todoController.GetTodos();

            // Assert
            _todoServiceMock.Verify(s => s.GetAllAsync(), Times.Once());
        }
    }
}
