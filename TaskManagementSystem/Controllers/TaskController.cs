using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TaskManagementSystem.Data;
using TaskManagementSystem.Model;

namespace TaskManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class TaskController : GenericCrudController<TaskList>
    {
        public TaskController(DataContext dataContext) : base(dataContext) { }

        [HttpPost]
        [Route("UpdateStatus")]

        public virtual async Task<IActionResult> UpdateStatus(int id, string status)
        {
            var data = await _context.Set<TaskList>().FindAsync(id);
            data.Status = status;
            _context.Entry(data).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(new { Message = "Task Update", Code = 200 });
        }


    }
}
