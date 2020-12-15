using System.Collections.Generic;
using System.Threading.Tasks;
using TodoAPI.Models;

namespace TodoAPI.Repositories
{
    public interface ITodoRepository
    {
        Task<Todo> GetByIdAsync(int id);
        Task<List<Todo>> GetAllAsync();
        Task AddAsync(Todo todo);
        Task<bool> UpdateAsync(Todo todo);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}