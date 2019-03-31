using System;
using System.Collections.Generic;
using System.Threading;

namespace DirectoryHashSum
{
    public static class Program
    {
        /// <summary>
        /// Каталог по умолчанию. Из этого каталога был запущен исполняемый файл.
        /// </summary>
        private static readonly string _defaultWorkingDirectory = PathHelpers.GetCurrentDirectory();

        static int x = 0;
        static object locker = new object();

        private static void Main()
        {
            string workingDirectory = _defaultWorkingDirectory;

            HashGenerator hashGenerator = new HashGenerator();
            DatabaseWriter databaseWriter = new DatabaseWriter();

            while (true) {
                Console.Clear();
                Console.WriteLine($"Текущий каталог: {workingDirectory}");
                Console.WriteLine("1. Выбрать каталог.");
                Console.WriteLine("2. Сгенерировать MD5-хеш для текущего каталога.");
                Console.WriteLine("Для завершения работы нажмите на любую клавишу...");

                ConsoleKeyInfo key = Console.ReadKey();
                switch (key.KeyChar) {
                    case '1':
                        Console.WriteLine();
                        workingDirectory = PathHelpers.SetNewDirectory();
                        Console.ReadKey();
                        break;
                    case '2':
                        List<string> hashes = hashGenerator.CreateMd5HashForFilesInDirectory(workingDirectory);
                        databaseWriter.WriteAllHashesInDb(hashes);
                        Console.ReadKey();
                        break;
                    default:
                        return;
                }
            }
        }
    }
}
