namespace ProjectManagement.Core.Models;

public enum TaskPriority
{
    Low = 1,
    Medium = 2,
    High = 3,
    Critical = 4
}

public class TaskItem : IComparable<TaskItem>
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public DateTime Deadline { get; set; }
    public TaskPriority Priority { get; set; }
    public double EstimatedHours { get; set; }

    public TaskItem(int id, string title, DateTime deadline, TaskPriority priority, double estimatedHours)
    {
        Id = id;
        Title = title;
        Deadline = deadline;
        Priority = priority;
        EstimatedHours = estimatedHours;
    }
    
    public int CompareTo(TaskItem? other)
    {
        if (other is null) return 1;
        if (Priority != other.Priority)
            return other.Priority.CompareTo(Priority);
        if (Deadline != other.Deadline)
            return Deadline.CompareTo(other.Deadline);
        return Id.CompareTo(other.Id);
    }

    public override string ToString() =>
        $"{Id,4}  {Title,-32}  {Priority,-9}  {Deadline:dd.MM.yyyy}  {EstimatedHours,5:0.#}";
}