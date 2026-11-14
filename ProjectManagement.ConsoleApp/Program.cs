using System;
using System.Text;

using ProjectManagement.Core.Models;
using ProjectManagement.Core.Generators;
using ProjectManagement.Algorithms.Common;
using ProjectManagement.Algorithms.Heap;
using ProjectManagement.Algorithms.Comparators;


var task = new TaskItem(1, "Приклад задачі", DateTime.Today.AddDays(7), TaskPriority.High, 4);
var project = new Project();
project.Add(task);

Console.WriteLine(project);
Console.WriteLine(task);

var heapSort = new HeapSort();
var quickSort = new QuickSort();
var mergeSort = new MergeSort();

Console.WriteLine("Натисніть Enter для завершення...");
Console.ReadLine();