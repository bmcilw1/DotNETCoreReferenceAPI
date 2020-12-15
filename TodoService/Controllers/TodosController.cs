using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoService.Models;
using TodoService.Repositories;

namespace TodoService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly ITodoRepository _todoRepository;

        public TodosController(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoDTO>>> GetTodoItems()
        {
            return await _todoRepository.GetAllAsync();
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoDTO>> GetTodoItem(long id)
        {
            return await _todoRepository.GetByIdAsync(id);
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TodoDTO todoDTO)
        {
            if (id != todoDTO.Id)
                return BadRequest();

            var todo = await _todoRepository.GetByIdAsync(id);

            if (todo == null)
                return NotFound();

            try
            {
                await _todoRepository.UpdateAsync(todoDTO);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _todoRepository.ExistsAsync(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TodoDTO>> PostTodoItem(TodoDTO todoDTO)
        {
            await _todoRepository.AddAsync(todoDTO);

            return CreatedAtAction(nameof(GetTodoItem), new { id = todoDTO.Id }, todoDTO);
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var found = await _todoRepository.DeleteAsync(id);

            if (!found)
                return NotFound();

            return NoContent();
        }

        private static TodoDTO ItemToDTO(Todo todoItem) => new TodoDTO
        {
            Id = todoItem.Id,
            Name = todoItem.Name,
            IsComplete = todoItem.IsComplete
        };
    }
}
