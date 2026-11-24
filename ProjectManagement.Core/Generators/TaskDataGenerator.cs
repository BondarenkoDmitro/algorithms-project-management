using ProjectManagement.Core.Models;

namespace ProjectManagement.Core.Generators;

public enum DataSetType
{
    Random,
    Sorted,
    Reverse,
    Duplicates,
    NearlySorted
}

public class TaskDataGenerator
{
    private static readonly TaskPriority[] Priorities =
    {
        TaskPriority.Low,
        TaskPriority.Medium,
        TaskPriority.High,
        TaskPriority.Critical
    };

    private static readonly string[] Titles =
    {
        "Аналіз вимог",
        "Проєктування бази даних",
        "Розробка модуля",
        "Написання тестів",
        "Код-рев'ю",
        "Оновлення документації",
        "Зустріч із замовником",
        "Виправлення дефектів"
    };

    private readonly Random _random;

    public TaskDataGenerator() : this(42)
    {
    }

    public TaskDataGenerator(int seed)
    {
        _random = new Random(seed);
    }

    public TaskItem[] Generate(int count, DataSetType type)
    {
        return type switch
        {
            DataSetType.Random => GenerateRandom(count),
            DataSetType.Sorted => GenerateSorted(count),
            DataSetType.Reverse => GenerateReverse(count),
            DataSetType.Duplicates => GenerateDuplicates(count),
            DataSetType.NearlySorted => GenerateNearlySorted(count),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, "Невідомий тип набору даних.")
        };
    }

    public TaskItem[] GenerateRandom(int count)
    {
        CheckCount(count);

        var tasks = new TaskItem[count];
        for (var i = 0; i < count; i++)
            tasks[i] = CreateTask(i);

        return tasks;
    }

    public TaskItem[] GenerateSorted(int count)
    {
        var tasks = GenerateRandom(count);
        Array.Sort(tasks);
        return tasks;
    }

    public TaskItem[] GenerateReverse(int count)
    {
        var tasks = GenerateSorted(count);
        Array.Reverse(tasks);
        return tasks;
    }

    public TaskItem[] GenerateDuplicates(int count)
    {
        CheckCount(count);

        var patterns = new TaskItem[8];
        for (var i = 0; i < patterns.Length; i++)
            patterns[i] = CreateTask(i);

        var tasks = new TaskItem[count];
        for (var i = 0; i < count; i++)
        {
            var pattern = patterns[i % patterns.Length];
            tasks[i] = new TaskItem(i, pattern.Title, pattern.Deadline, pattern.Priority, pattern.EstimatedHours);
        }

        return tasks;
    }

    public TaskItem[] GenerateNearlySorted(int count)
    {
        var tasks = GenerateSorted(count);
        if (count < 2)
            return tasks;

        var swaps = Math.Max(1, count / 20);
        for (var i = 0; i < swaps; i++)
        {
            var left = _random.Next(0, count - 1);
            (tasks[left], tasks[left + 1]) = (tasks[left + 1], tasks[left]);
        }

        return tasks;
    }

    private TaskItem CreateTask(int id)
    {
        return new TaskItem(
            id,
            Titles[_random.Next(Titles.Length)] + " #" + (id + 1),
            DateTime.Today.AddDays(_random.Next(1, 91)),
            Priorities[_random.Next(Priorities.Length)],
            _random.Next(1, 25));
    }

    private static void CheckCount(int count)
    {
        if (count <= 0)
            throw new ArgumentOutOfRangeException(nameof(count), count, "Кількість задач має бути більшою за нуль.");
    }
}
