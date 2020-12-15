using Moq;
using System.Threading.Tasks;
using System.Collections.Generic;
using TodoService.Controllers;
using TodoService.Services;
using TodoService.Models;
using Xunit;

namespace TodoServiceTests
{
    public class TodoItemsControllerTests
    {
        [Fact]
        public async Task GetTodoItems_CallsGetAllAsync()
        {
            // Arrange
            var todoServiceMock = new Mock<ITodoService>();
            var todoController = new TodoController(todoServiceMock.Object);

            todoServiceMock.Setup(s => s.GetAllAsync())
                .Returns(Task.FromResult(new List<TodoDTO>() {}));

            // Act
            await todoController.GetTodos();

            // Assert
            todoServiceMock.Verify(s => s.GetAllAsync(), Times.Once());
        }
    }
}
