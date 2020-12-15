using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoService.Models;

namespace TodoService.Services
{
    public class TodoServiceEF : ITodoService
    {
        private readonly TodoContext _context;

        public TodoServiceEF(TodoContext context)
        {
            _context = context;
        }

        public Task<List<Todo>> GetAllAsync()
        {
            return _context.Todos.ToListAsync();
        }

        public async Task<Todo> GetByIdAsync(long id)
        {
            return await _context.Todos.FindAsync(id);
        }

        public Task AddAsync(Todo todo)
        {
            _context.Todos.Add(todo);
            return _context.SaveChangesAsync();
        }

        public Task UpdateAsync(Todo todo)
        {
            _context.Entry(todo).State = EntityState.Modified;
            return _context.SaveChangesAsync();
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
    }
}