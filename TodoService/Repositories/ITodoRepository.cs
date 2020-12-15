using System.Collections.Generic;
using System.Threading.Tasks;
using TodoService.Models;

namespace TodoService.Repositories
{
    public interface ITodoRepository
    {
        Task<TodoDTO> GetByIdAsync(long id);
        Task<List<TodoDTO>> GetAllAsync();
        Task AddAsync(TodoDTO todo);
        Task UpdateAsync(TodoDTO todo);
        Task<bool> DeleteAsync(long id);
        Task<bool> ExistsAsync(long id);
    }
}