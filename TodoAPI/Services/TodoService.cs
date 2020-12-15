using System.Collections.Generic;
using System.Threading.Tasks;
using TodoAPI.Models;
using TodoAPI.Repositories;

namespace TodoAPI.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;

        public TodoService(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public Task<List<Todo>> GetAllAsync()
        {
            return _todoRepository.GetAllAsync();
        }

        public Task<Todo> GetByIdAsync(int id)
        {
            return _todoRepository.GetByIdAsync(id);
        }

        public Task AddAsync(Todo todo)
        {
            return _todoRepository.AddAsync(todo);
        }

        public Task<bool> UpdateAsync(Todo todo)
        {
            return _todoRepository.UpdateAsync(todo);
        }

        public Task<bool> DeleteAsync(int id)
        {
            return _todoRepository.DeleteAsync(id);
        }


        public Task<bool> ExistsAsync(int id)
        {
            return _todoRepository.ExistsAsync(id);
        }
    }
}