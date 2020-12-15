using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public Task<Todo> GetByIdAsync(long id)
        {
            return _todoRepository.GetByIdAsync(id);
        }

        public Task AddAsync(Todo todo)
        {
            return _todoRepository.AddAsync(todo);
        }

        public Task UpdateAsync(Todo todo)
        {
            return _todoRepository.UpdateAsync(todo);
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var todo = await _todoRepository.GetByIdAsync(id);
            if (todo == null)
                return false;

            await _todoRepository.DeleteAsync(id);

            return true;
        }

        public Task<bool> ExistsAsync(long id)
        {
            return _todoRepository.ExistsAsync(id);
        }
    }
}