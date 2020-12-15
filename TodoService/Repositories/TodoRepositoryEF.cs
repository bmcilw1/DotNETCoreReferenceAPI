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

        public async Task<TodoDTO> GetByIdAsync(int id)
        {
            var item = await _context.Todos.FindAsync(id);

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

        public Task UpdateAsync(TodoDTO todoDTO)
        {
            var todo = new Todo()
            {
                Id = todoDTO.Id,
                Name = todoDTO.Name,
                IsComplete = todoDTO.IsComplete
            };

            _context.Entry(todo).State = EntityState.Modified;
            return _context.SaveChangesAsync();
        }


        private static TodoDTO ItemToDTO(Todo todo) => new TodoDTO
        {
            Id = todo.Id,
            Name = todo.Name,
            IsComplete = todo.IsComplete
        };
    }
}