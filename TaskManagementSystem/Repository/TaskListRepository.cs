using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Data;
using TaskManagementSystem.IRepository;
using TaskManagementSystem.Model;

namespace TaskManagementSystem.Repository
{
    public class TaskListRepository : ITaskListRepository
    {
        private readonly DataContext _context;
        public TaskListRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<object> UpdateStatus(int id, string status)
        {
            var data = await _context.Set<TaskList>().FindAsync(id);
            data.Status = status;
            _context.Entry(data).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return new { Message = "Task Update", Code = 200 };
        }
    }
}
