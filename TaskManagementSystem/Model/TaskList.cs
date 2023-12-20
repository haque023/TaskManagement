namespace TaskManagementSystem.Model
{
    public class TaskList : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; } = "Pending";
    }
}
