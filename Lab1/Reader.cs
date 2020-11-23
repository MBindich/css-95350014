using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Lab1
{
    public class Reader
    {
        public static void defaultReader(string path, string name)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }

            using (FileStream fstream = File.OpenRead($"{path}\\{name}.txt"))
            {
                // преобразуем строку в байты
                byte[] array = new byte[fstream.Length];
                // считываем данные
                fstream.Read(array, 0, array.Length);
                // декодируем байты в строку
                string textFromFile = System.Text.Encoding.Default.GetString(array);
                Console.WriteLine($"Текст из файла: {textFromFile}");
            }
        }

        public static void binaryReader(string path, string name, SomeClass[] classes)
        {
            using (BinaryReader reader = new BinaryReader(File.Open($"{path}\\{name}.txt", FileMode.Open)))
            {
                // пока не достигнут конец файла
                // считываем каждое значение из файла
                while (reader.PeekChar() > -1)
                {
                    string sString = reader.ReadString();
                    int sInt = reader.ReadInt32();
                    double sDouble = reader.ReadDouble();
 
                    Console.WriteLine($"String - {sString}, int - {sInt}, double - {sDouble}");
                }
            }
        }
    }
}
