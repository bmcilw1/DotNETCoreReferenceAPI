using System.Collections.Generic;
using System.Threading.Tasks;
using TodoAPI.Models;

namespace TodoAPI.Services
{
    public interface ITodoService
    {
        Task<Todo> GetByIdAsync(long id);
        Task<List<Todo>> GetAllAsync();
        Task AddAsync(Todo todo);
        Task<bool> UpdateAsync(Todo todo);
        Task<bool> DeleteAsync(long id);
        Task<bool> ExistsAsync(long id);
    }
}