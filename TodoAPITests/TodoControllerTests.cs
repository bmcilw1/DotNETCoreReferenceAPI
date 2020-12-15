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
                .Setup(s => s.GetByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(todo));

            // Act
            var result = await todoController.GetTodo(todo.Id);

            // Assert
            todoServiceMock.Verify(s =>
                s.GetByIdAsync(It.Is<int>(id => id == todo.Id)), Times.Once());
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
                .Setup(s => s.GetByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult<Todo>(null));

            // Act
            var result = await todoController.GetTodo(1);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result.Result);
            Assert.Null(result.Value);
        }

        [Fact]
        public async Task DeleteTodo_WhenNotFound_Returns404()
        {
            // Arrange
            var todoServiceMock = new Mock<ITodoService>();
            var todoController = new TodoController(todoServiceMock.Object);

            todoServiceMock
                .Setup(s => s.DeleteAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(false));

            // Act
            var result = await todoController.DeleteTodo(1);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteTodo_WhenFound_ReturnsOk()
        {
            // Arrange
            var todoServiceMock = new Mock<ITodoService>();
            var todoController = new TodoController(todoServiceMock.Object);

            todoServiceMock
                .Setup(s => s.DeleteAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(true));

            // Act
            var result = await todoController.DeleteTodo(1);

            // Assert
            Assert.IsAssignableFrom<OkResult>(result);
        }

        [Fact]
        public async Task PostTodo_CallsAddAsync()
        {
            // Arrange
            var todoServiceMock = new Mock<ITodoService>();
            var todoController = new TodoController(todoServiceMock.Object);

            var todo = GetTodo();
            todoServiceMock
                .Setup(s => s.AddAsync(It.IsAny<Todo>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await todoController.PostTodo(todo);

            // Assert
            todoServiceMock.Verify(s =>
                s.AddAsync(It.Is<Todo>(t =>
                    t.IsComplete == todo.IsComplete &&
                    t.Name == todo.Name
                )), Times.Once());
            Assert.IsAssignableFrom<CreatedAtActionResult>(result.Result);
            Assert.Equal(1, (result.Result as CreatedAtActionResult).RouteValues["id"]);
        }

        [Fact]
        public async Task PutTodo_WhenNotFound_Returns404()
        {
            // Arrange
            var todoServiceMock = new Mock<ITodoService>();
            var todoController = new TodoController(todoServiceMock.Object);

            var todo = GetTodo();
            todoServiceMock
                .Setup(s => s.UpdateAsync(It.IsAny<Todo>()))
                .Returns(Task.FromResult(false));

            // Act
            var result = await todoController.PutTodo(todo.Id, todo);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Fact]
        public async Task PutTodo_WhenFound_ReturnsOk()
        {
            // Arrange
            var todoServiceMock = new Mock<ITodoService>();
            var todoController = new TodoController(todoServiceMock.Object);

            var todo = GetTodo();
            todoServiceMock
                .Setup(s => s.UpdateAsync(It.IsAny<Todo>()))
                .Returns(Task.FromResult(true));

            // Act
            var result = await todoController.PutTodo(todo.Id, todo);

            // Assert
            Assert.IsAssignableFrom<OkResult>(result);
        }

        [Fact]
        public async Task PutTodo_WhenIdNotMatch_ReturnsBadRequest()
        {
            // Arrange
            var todoServiceMock = new Mock<ITodoService>();
            var todoController = new TodoController(todoServiceMock.Object);

            var todo = GetTodo();
            todo.Id = 1;

            // Act
            var result = await todoController.PutTodo(2, todo);

            // Assert
            Assert.IsAssignableFrom<BadRequestResult>(result);
        }


        private Todo GetTodo() =>
            new Todo { Id = 1, Name = "Hi", IsComplete = false };

        private List<Todo> GetTodos() => new List<Todo> {
            new Todo { Id = 1, Name = "Hi", IsComplete = false },
            new Todo { Id = 2, Name = "Bye", IsComplete = true }
         };
    }
}
