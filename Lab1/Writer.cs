using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Lab1
{
    public class Writer
    {
        public static void defaultWriter(string path, string name)
        {
            Console.WriteLine("Введите строку для записи в файл:");
            string text = Console.ReadLine();

            // запись в файл
            using (FileStream fstream = new FileStream($"{path}\\{name}.txt", FileMode.Create))
            {
                // преобразуем строку в байты
                byte[] array = System.Text.Encoding.Default.GetBytes(text);
                // запись массива байтов в файл
                fstream.Write(array, 0, array.Length);
                Console.WriteLine("Текст записан в файл");
            }
        }

        public static void binaryWriter(string path, string name, SomeClass[] classes)
        {
            try
            {
                // создаем объект BinaryWriter
                using (BinaryWriter writer = new BinaryWriter(File.Open($"{path}\\{name}.txt", FileMode.OpenOrCreate)))
                {
                    // записываем в файл значение каждого поля структуры
                    foreach (SomeClass c in classes)
                    {
                        writer.Write(c.someString);
                        writer.Write(c.someInt);
                        writer.Write(c.someDouble);
                    }
                }

            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
