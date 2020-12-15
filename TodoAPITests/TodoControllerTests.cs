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
        public async Task GetTodos_CallsGetAllAsync()
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
        public async Task GetTodos_ReturnsTodos()
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

        [Fact]
        public async Task GetTodo_CallsGetByIdAsync()
        {
            // Arrange
            var todoServiceMock = new Mock<ITodoService>();
            var todoController = new TodoController(todoServiceMock.Object);

            var todo = GetTodo();
            todoServiceMock
                .Setup(s => s.GetByIdAsync(It.IsAny<long>()))
                .Returns(Task.FromResult(todo));

            // Act
            var result = await todoController.GetTodo(todo.Id);

            // Assert
            todoServiceMock.Verify(s =>
                s.GetByIdAsync(It.Is<long>(id => id == todo.Id)), Times.Once());
            Assert.IsAssignableFrom<Todo>(result.Value);
            Assert.Equal(todo, result.Value);
        }

        [Fact]
        public async Task GetTodo_WhenNotFound_Returns404()
        {
            // Arrange
            var todoServiceMock = new Mock<ITodoService>();
            var todoController = new TodoController(todoServiceMock.Object);

            todoServiceMock
                .Setup(s => s.GetByIdAsync(It.IsAny<long>()))
                .Returns(Task.FromResult<Todo>(null));

            // Act
            var result = await todoController.GetTodo(1);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result.Result);
            Assert.Equal(null, result.Value);
        }

        private Todo GetTodo() =>
            new Todo { Id = 1, Name = "Hi", IsComplete = false };

        private List<Todo> GetTodos() => new List<Todo> {
            new Todo { Id = 1, Name = "Hi", IsComplete = false },
            new Todo { Id = 2, Name = "Bye", IsComplete = true }
         };
    }
}
