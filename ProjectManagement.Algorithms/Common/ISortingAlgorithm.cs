namespace ProjectManagement.Algorithms.Common;

/// <summary>
/// Базовий уніфікований узагальнений інтерфейс для алгоритмів сортування.
/// Дозволяє сортувати масиви типу T (де T реалізує IComparable&lt;T&gt;)
/// та збирати метрики ефективності у SortStatistics.
/// </summary>
/// <typeparam name="T">Тип елементів масиву, що порівнюються між собою.</typeparam>
public interface ISortingAlgorithm<T> where T : IComparable<T>
{
    /// <summary>
    /// Назва алгоритму сортування (наприклад, "HeapSort", "QuickSort").
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Виконує сортування масиву на місці (in-place) або повертає статистику.
    /// </summary>
    /// <param name="array">Масив елементів для сортування.</param>
    /// <returns>Об'єкт SortStatistics з метриками (час, порівняння, перестановки).</returns>
    SortStatistics Sort(T[] array);
}
