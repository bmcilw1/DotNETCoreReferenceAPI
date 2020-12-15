using System.Collections.Generic;
using System.Threading.Tasks;
using TodoService.Models;

namespace TodoService.Services
{
    public interface ITodoService
    {
        Task<Todo> GetByIdAsync(long id);
        Task<List<Todo>> GetAllAsync();
        Task AddAsync(Todo todo);
        Task UpdateAsync(Todo todo);
        Task<bool> DeleteAsync(long id);
        Task<bool> ExistsAsync(long id);
    }
}