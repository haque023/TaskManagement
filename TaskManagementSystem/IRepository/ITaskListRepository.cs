namespace TaskManagementSystem.IRepository
{
    public interface ITaskListRepository
    {
        public Task<object> UpdateStatus(int id, string status);
    }
}
