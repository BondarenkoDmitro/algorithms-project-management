using ProjectManagement.Core.Models;

namespace ProjectManagement.Core.Generators;

public class TaskDataGenerator
{
    private static readonly TaskPriority[] Priorities = Enum.GetValues<TaskPriority>();
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
    private static readonly Random _random = new(42);

    // 1. Одна випадкова задача. З неї будується все інше.
    private TaskItem CreateTask(int id)
    {
        return new TaskItem(
            id,
            Titles[_random.Next(Titles.Length)] + " #" + (id + 1),
            DateTime.Today.AddDays(_random.Next(1, 91)),
            Priorities[_random.Next(Priorities.Length)],
            _random.Next(1, 25));
    }

    // 2. Розмір набору має бути додатним.
    private static void CheckCount(int count)
    {
        if (count <= 0)
            throw new ArgumentOutOfRangeException(nameof(count), count, "Кількість задач має бути більшою за нуль.");
    }

    // 3. N випадкових карточок: CreateTask у циклі.
    public TaskItem[] GenerateRandom(int count)
    {
        CheckCount(count);
        var tasks = new TaskItem[count];
        for (var i = 0; i < count; i++)
            tasks[i] = CreateTask(i);
        return tasks;
    }

    // 4. Ті самі карточки, впорядковані за TaskItem.CompareTo.
    public TaskItem[] GenerateSorted(int count)
    {
        var tasks = GenerateRandom(count);
        Array.Sort(tasks);
        return tasks;
    }

    // 5. Ті самі карточки у зворотному порядку — найгірший випадок.
    public TaskItem[] GenerateReverse(int count)
    {
        var tasks = GenerateSorted(count);
        Array.Reverse(tasks);
        return tasks;
    }

    // 6. Відсортовано, але ~5% сусідніх пар обміняно місцями.
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

    // 7. Багато карточок з однаковими ключами: 8 шаблонів повторюються по колу.
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
}
