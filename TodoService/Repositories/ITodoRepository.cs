using System.Collections.Generic;
using System.Threading.Tasks;
using TodoService.Models;

namespace TodoService.Repositories
{
    public interface ITodoRepository
    {
        Task<TodoDTO> GetByIdAsync(int id);
        Task<List<TodoDTO>> GetAllAsync();
        Task AddAsync(TodoDTO todo);
        Task UpdateAsync(TodoDTO todo);
    }
}