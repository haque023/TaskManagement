using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Data;
using TaskManagementSystem.Model;

namespace TaskManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenericCrudController<T> : ControllerBase where T : BaseEntity
    {
        protected readonly DataContext _context;
        public GenericCrudController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public virtual async Task<IActionResult> List()
        {
            var list = await _context.Set<T>().ToListAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Details(long id)
        {
            var data = await _context.Set<T>().FindAsync(id);
            if (data == null)
            {
                return NotFound("Data Not Found");
            }
            return Ok(data);

        }

        [HttpPost]
        public virtual async Task<IActionResult> Create(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return Ok(new { Message = "Create Successfully", Code = 200 });
        }

        [HttpPut]
        public virtual async Task<IActionResult> Update(long id, T entity)
        {
            if (id != entity.Id)
                return BadRequest();

            if (!await _context.Set<T>().AnyAsync(x => x.Id == id))
                return NotFound("Entity not found");

            entity.UpdateDate = DateTime.Now;
            _context.Entry(entity).State=EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(new { Message = "Update Successfully", Code = 200 });
        }


    }
}
