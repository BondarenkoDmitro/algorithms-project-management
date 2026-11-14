namespace ProjectManagement.Core.Models;

public class Project
{
    private readonly List<TaskItem> _tasks = new();
    public string Name { get; set; } = "Новий проєкт";

    public IReadOnlyList<TaskItem> Tasks => _tasks;

    public int Count => _tasks.Count;

    public void Add(TaskItem task)
    {
        _tasks.Add(task);
    }

    public TaskItem[] GetPlanningOrder()
    {
        var copy = _tasks.ToArray();
        Array.Sort(copy);
        return copy;
    }

    public override string ToString() => $"{Name}: {Count} задач";
}
