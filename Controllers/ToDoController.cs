using to_do_api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace to_do_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ToDoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetTaskList()
        {
            return Ok(await _context.Tasks.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> CreateTaskByDescription([FromBody] string description)
        {
            var task = new ToDoTask { Description = description };
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return Ok(task);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id}/description")]
        public async Task<IActionResult> UpdateTaskDescription(int id, [FromBody] string updatedDescription)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            task.Description = updatedDescription;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id}/flag")]
        public async Task<IActionResult> SwitchTaskFlag(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            task.flag = !task.flag;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> SetNewList(List<ToDoTask> tasks)
        {
            _context.Tasks.RemoveRange(_context.Tasks);
            _context.Tasks.AddRange(tasks);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
