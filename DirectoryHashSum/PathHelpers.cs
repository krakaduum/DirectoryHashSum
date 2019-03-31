using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace DirectoryHashSum
{
    public static class PathHelpers
    {
        /// <summary>
        /// Находит текущую директорию, в которой находится исполняемый файл.
        /// </summary>
        /// <returns>Абсолютный путь до папки.</returns>
        public static string GetCurrentDirectory()
        {
            return Directory.GetCurrentDirectory();
        }

        /// <summary>
        /// Устанавливает новую директорию, если она существует.
        /// </summary>
        /// <returns>Абсолютный путь до папки.</returns>
        public static string SetNewDirectory()
        {
            string path = Console.ReadLine();
            if (Directory.Exists(path))
                return path;
            else
            {
                Console.WriteLine("Такого пути не существует.");
                return GetCurrentDirectory();
            }
        }

        /// <summary>
        /// Находит все файлы в каталоге.
        /// </summary>
        /// <param name="pathToDirectory">Абсолютный путь директории.</param>
        /// <returns>Лист абсолютных путей до всех файлов в директории.</returns>
        public static List<string> GetPathsOfFilesInDirectory(string pathToDirectory)
        {
            return Directory.GetFiles(pathToDirectory, "*", SearchOption.AllDirectories)
                                .OrderBy(key => key).ToList();
        }
    }
}
