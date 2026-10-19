using System;
using System.Text;

// Підключення всіх модулів проєкту
using ProjectManagement.Core.Models;
using ProjectManagement.Core.Generators;
using ProjectManagement.Algorithms.Common;
using ProjectManagement.Algorithms.Heap;
using ProjectManagement.Algorithms.Comparators;


// Перевірка доступності класів з обох проєктів (створення пустих об'єктів)
var task = new TaskItem();
var project = new Project();
var heapSort = new HeapSort();
var quickSort = new QuickSort();
var mergeSort = new MergeSort();

Console.WriteLine("Натисніть Enter для завершення...");
Console.ReadLine();