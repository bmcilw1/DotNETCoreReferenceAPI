using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoAPI.Models;

namespace TodoAPI.Repositories
{
    public class TodoRepositoryEF : ITodoRepository
    {
        private readonly AppDbContext _context;

        public TodoRepositoryEF(AppDbContext context)
        {
            _context = context;
        }

        public Task<List<Todo>> GetAllAsync()
        {
            return _context.Todos.ToListAsync();
        }

        public async Task<Todo> GetByIdAsync(int id)
        {
            return await _context.Todos.FindAsync(id);
        }

        public Task AddAsync(Todo todo)
        {
            _context.Todos.Add(todo);
            return _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(Todo todo)
        {
            var existingTodo = await _context.Todos.FindAsync(todo.Id);
            if (existingTodo == null)
                return false;

            var todoEntity = _context.Entry(existingTodo);

            todoEntity.CurrentValues.SetValues(todo);
            todoEntity.State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null)
                return false;

            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();

            return true;
        }

        public Task<bool> ExistsAsync(int id)
        {
            return _context.Todos.AnyAsync(e => e.Id == id);
        }
    }
}