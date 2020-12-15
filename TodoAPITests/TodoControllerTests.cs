using Moq;
using System.Threading.Tasks;
using System.Collections.Generic;
using TodoAPI.Controllers;
using TodoAPI.Services;
using TodoAPI.Models;
using Xunit;
using Microsoft.AspNetCore.Mvc;

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

            var todos = new List<Todo>();
            todoServiceMock.Setup(s => s.GetAllAsync())
                .Returns(Task.FromResult(todos));

            // Act
            var result = await todoController.GetTodos();

            // Assert
            todoServiceMock.Verify(s => s.GetAllAsync(), Times.Once());
            Assert.IsAssignableFrom<List<Todo>>(result.Value);
            Assert.Equal(todos, result.Value);
        }

        [Fact]
        public async Task GetTodoItems_ReturnsTodos()
        {
            // Arrange
            var todoServiceMock = new Mock<ITodoService>();
            var todoController = new TodoController(todoServiceMock.Object);

            var todos = GetTodos();
            todoServiceMock.Setup(s => s.GetAllAsync())
                .Returns(Task.FromResult(todos));

            // Act
            var result = await todoController.GetTodos();

            // Assert
            todoServiceMock.Verify(s => s.GetAllAsync(), Times.Once());
            Assert.IsAssignableFrom<List<Todo>>(result.Value);
            Assert.Equal(todos, result.Value);
        }

        private Todo GetTodo() =>
            new Todo { Id = 1, Name = "Hi", IsComplete = false };

        private List<Todo> GetTodos() => new List<Todo> {
            new Todo { Id = 1, Name = "Hi", IsComplete = false },
            new Todo { Id = 2, Name = "Bye", IsComplete = true }
         };
    }
}
