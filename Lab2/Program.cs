using Lab2;
using System;
using System.IO;
using System.Security.Permissions;

public class Program
{
    public static void Main()
    {
        string path = @"C:\SourceDirectory";
        while (true)
        {
            Console.Clear();
            Console.WriteLine("1. Запустить мониторинг");
            Console.WriteLine($"2. Изменить директорию (сейчас установлена {path})");
            Console.WriteLine("0. Выход из программы");

            string inputNum = Console.ReadLine();
            switch (inputNum)
            {
                case "1":
                    Console.Clear();
                    if (Watcher.Run(path) == 1)
                    {
                        Console.WriteLine("Некорректный путь");
                    }
                    Console.ReadLine();
                    break;

                case "2":
                    Console.Clear();
                    Console.WriteLine("Введите путь (например C:\\SourceDirectory):");
                    path = Console.ReadLine();
                    Console.WriteLine("Если что-то сломается - это на вашей совести :)");
                    Console.ReadLine();
                    break;

                case "0":
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Нажмите любую клавишу");
                    Console.ReadLine();
                    Console.Clear();
                    Console.WriteLine("Выбери нормально");
                    Console.ReadLine();
                    break;
            };
        }
    }
}    