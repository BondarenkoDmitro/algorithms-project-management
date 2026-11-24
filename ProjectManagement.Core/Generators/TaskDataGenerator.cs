using ProjectManagement.Core.Models;

namespace ProjectManagement.Core.Generators;

// Типи тестових наборів: кожен по-своєму "важкий" для алгоритмів сортування.
public enum DataSetType
{
    Random,       // випадковий порядок — середній випадок
    Sorted,       // вже відсортовано — найкращий випадок
    Reverse,      // зворотний порядок — найгірший випадок для купи
    Duplicates,   // багато задач з однаковими ключами
    NearlySorted  // майже відсортовано, кілька обмінів
}

// Генератор тестових наборів задач.
// Відповідає за: створення N задач та підготовку їх у 5 різних порядках.
public class TaskDataGenerator
{
    // Звідки беруться випадкові пріоритети.
    private static readonly TaskPriority[] Priorities =
    {
        TaskPriority.Low,
        TaskPriority.Medium,
        TaskPriority.High,
        TaskPriority.Critical
    };

    // Шаблони назв, щоб тестові дані виглядали реалістично.
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

    // Фіксований seed: однаковий номер завжди дає однаковий набір,
    // інакше результати бенчмарку неможливо перевірити повторно.
    private readonly Random _random;

    // seed за замовчуванням, щоб бенчмарк був однаковий при кожному запуску.
    public TaskDataGenerator() : this(42)
    {
    }

    public TaskDataGenerator(int seed)
    {
        _random = new Random(seed);
    }

    // Єдина точка входу: обирає потрібний набір за типом.
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

    // Випадкові задачі — основа для решти наборів.
    public TaskItem[] GenerateRandom(int count)
    {
        CheckCount(count);

        var tasks = new TaskItem[count];
        for (var i = 0; i < count; i++)
            tasks[i] = CreateTask(i);

        return tasks;
    }

    // Той самий набір, але впорядкований за TaskItem.CompareTo (пріоритет -> дедлайн -> Id).
    public TaskItem[] GenerateSorted(int count)
    {
        var tasks = GenerateRandom(count);
        Array.Sort(tasks);
        return tasks;
    }

    // Зворотний порядок — максимальна кількість інверсій.
    public TaskItem[] GenerateReverse(int count)
    {
        var tasks = GenerateSorted(count);
        Array.Reverse(tasks);
        return tasks;
    }

    // Лише 8 різних наборів ключів, що повторюються по колу:
    // перевіряємо, як алгоритм поводиться з рівними елементами.
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

    // Відсортовано, але ~5% сусідніх пар обміняно місцями.
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

    // Одна випадкова задача: назва, дедлайн +1..90 днів, пріоритет, оцінка 1..24 год.
    private TaskItem CreateTask(int id)
    {
        return new TaskItem(
            id,
            Titles[_random.Next(Titles.Length)] + " #" + (id + 1),
            DateTime.Today.AddDays(_random.Next(1, 91)),
            Priorities[_random.Next(Priorities.Length)],
            _random.Next(1, 25));
    }

    // Розмір набору має бути додатним.
    private static void CheckCount(int count)
    {
        if (count <= 0)
            throw new ArgumentOutOfRangeException(nameof(count), count, "Кількість задач має бути більшою за нуль.");
    }
}
