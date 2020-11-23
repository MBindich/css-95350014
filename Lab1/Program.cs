using System;
using System.IO;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            SomeClass[] classes = new SomeClass[2];
            classes[0] = new SomeClass("tmp1", 1, 1.1);
            classes[1] = new SomeClass("tmp2", 2, 2.2);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Создать новый файл и открыть для записи");
                Console.WriteLine("2. Открыть и прочитать существующий файл");
                Console.WriteLine("3. Записать информацию экземпляров класса SomeClass в бинарный файл");
                Console.WriteLine("4. Считать информацию экземпляров класса SomeClass из бинарного файла");
                Console.WriteLine("5. Выход из программы");

                string path = @"C:\SomeDir2\note.txt";
                string name = "note";

                string inputNum = Console.ReadLine();
                switch (inputNum)
                {
                    case "1":
                        Console.WriteLine("\nВведите путь, где создать файл, например C:\\SomeDir2");
                        path = Console.ReadLine();
                        Console.WriteLine("Введите название файла без расширения, например note");
                        name = Console.ReadLine();

                        Writer.defaultWriter(path, name);
                        Console.WriteLine("Нажмите любую клавишу для продолжения");
                        Console.ReadLine();
                        break;

                    case "2":
                        Console.WriteLine("\nВведите путь файла, например C:\\SomeDir2");
                        path = Console.ReadLine();
                        Console.WriteLine("Введите название файла без расширения, например note");
                        name = Console.ReadLine();

                        Reader.defaultReader(path, name);
                        Console.WriteLine("Нажмите любую клавишу для продолжения");
                        Console.ReadLine();
                        break;

                    case "3":
                        Console.WriteLine("\nВведите путь, где создать файл, например C:\\SomeDir2");
                        path = Console.ReadLine();
                        Console.WriteLine("Введите название файла без расширения, например note");
                        name = Console.ReadLine();

                        Writer.binaryWriter(path, name, classes);
                        Console.WriteLine("Нажмите любую клавишу для продолжения");
                        Console.ReadLine();
                        break;

                    case "4":
                        Console.WriteLine("\nВведите путь файла, например C:\\SomeDir2");
                        path = Console.ReadLine();
                        Console.WriteLine("Введите название файла без расширения, например note");
                        name = Console.ReadLine();

                        Reader.binaryReader(path, name, classes);
                        Console.WriteLine("Нажмите любую клавишу для продолжения");
                        Console.ReadLine();
                        break;

                    case "5":
                        Environment.Exit(0);
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Выбери нормально");
                        Console.ReadLine();
                        break;
                };

            }                
        }
    }
}
