using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoService.Models;

namespace TodoService.Repositories
{
    public class TodoRepositoryEF : ITodoRepository
    {
        private readonly TodoContext _context;

        public TodoRepositoryEF(TodoContext context)
        {
            _context = context;
        }

        public Task<List<TodoDTO>> GetAllAsync()
        {
            return _context.Todos.Select(item => ItemToDTO(item)).ToListAsync();
        }

        public async Task<TodoDTO> GetByIdAsync(long id)
        {
            var item = await _context.Todos.FindAsync(id);

            if (item == null)
                return null;

            return ItemToDTO(item);
        }
        public Task AddAsync(TodoDTO todoDTO)
        {
            var todo = new Todo()
            {
                Name = todoDTO.Name,
                IsComplete = todoDTO.IsComplete
            };

            _context.Todos.Add(todo);
            return _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TodoDTO todoDTO)
        {
            var todo = await _context.Todos.FindAsync(todoDTO.Id);

            if (todo == null)
                return;

            todo.Id = todoDTO.Id;
            todo.Name = todoDTO.Name;
            todo.IsComplete = todoDTO.IsComplete;

            _context.Entry(todo).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null)
            {
                return false;
            }

            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();

            return true;
        }

        public Task<bool> ExistsAsync(long id)
        {
            return _context.Todos.AnyAsync(e => e.Id == id);
        }

        private static TodoDTO ItemToDTO(Todo todo) => new TodoDTO
        {
            Id = todo.Id,
            Name = todo.Name,
            IsComplete = todo.IsComplete
        };
    }
}