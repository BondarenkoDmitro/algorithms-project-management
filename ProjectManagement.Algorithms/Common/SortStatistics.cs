using System.Diagnostics;

namespace ProjectManagement.Algorithms.Common;

/// <summary>
/// Клас для збереження та представлення результатів бенчмарку/сортування:
/// час виконання, кількість операцій порівняння та кількість перестановок (обмінів).
/// </summary>
public class SortStatistics
{
    /// <summary>
    /// Назва алгоритму сортування.
    /// </summary>
    public string AlgorithmName { get; set; } = string.Empty;

    /// <summary>
    /// Кількість елементів у масиві.
    /// </summary>
    public int ElementsCount { get; set; }

    /// <summary>
    /// Кількість виконаних порівнянь елементів.
    /// </summary>
    public long Comparisons { get; set; }

    /// <summary>
    /// Кількість виконаних перестановок (обмінів / копіювань) елементів.
    /// </summary>
    public long Swaps { get; set; }

    /// <summary>
    /// Загальний витрачений час (у мілісекундах або мікросекундах через TimeSpan).
    /// </summary>
    public TimeSpan ElapsedTime { get; set; }

    /// <summary>
    /// Час виконання у мілісекундах (зручно для експорту в таблиці/CSV).
    /// </summary>
    public double ElapsedMilliseconds => ElapsedTime.TotalMilliseconds;

    public SortStatistics()
    {
    }

    public SortStatistics(string algorithmName, int elementsCount, long comparisons, long swaps, TimeSpan elapsedTime)
    {
        AlgorithmName = algorithmName;
        ElementsCount = elementsCount;
        Comparisons = comparisons;
        Swaps = swaps;
        ElapsedTime = elapsedTime;
    }

    public override string ToString()
    {
        return $"{AlgorithmName} | Елементів: {ElementsCount} | Час: {ElapsedMilliseconds:F3} мс | Порівнянь: {Comparisons:N0} | Перестановок: {Swaps:N0}";
    }
}
