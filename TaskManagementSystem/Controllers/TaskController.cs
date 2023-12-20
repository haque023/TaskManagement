using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TaskManagementSystem.Data;
using TaskManagementSystem.IRepository;
using TaskManagementSystem.Model;

namespace TaskManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class TaskController : GenericCrudController<TaskList>
    {
        private readonly ITaskListRepository _Repository;
        public TaskController(DataContext dataContext, ITaskListRepository Repository) : base(dataContext)
        {
            _Repository = Repository;
        }

        [HttpPost]
        [Route("UpdateStatus")]
        public async Task<IActionResult> UpdateStatus(int id, string status)
        {
            try
            {
                var data = await _Repository.UpdateStatus(id, status);
                return Ok(data);

            }
            catch (Exception ex) {
                throw ex;
            }
        }


    }
}
