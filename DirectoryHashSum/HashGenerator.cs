using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Threading;

namespace DirectoryHashSum
{
    public class HashGenerator
    {
        /// <summary>
        /// Создает MD5-хеш для всех файлов в каталоге (в потоке).
        /// </summary>
        /// <param name="pathToDirectory">Абсолютный путь до каталога.</param>
        /// <returns>Список значений MD5-хеша в виде строки для каждого файла в каталоге.</returns>
        public List<string> CreateMd5HashForFilesInDirectory(string pathToDirectory)
        {
            List<string> pathsOfFiles = new List<string>();

            Thread pathsThread = new Thread(() => pathsOfFiles = PathHelpers.GetPathsOfFilesInDirectory(pathToDirectory));
            pathsThread.Start();
            pathsThread.Join();

            List<string> hashes = new List<string>();
            
            for (int i = 0; i < 2; i++)
            {
                Thread hashesThread = new Thread(() => hashes = CreateMd5HashForEachFile(pathsOfFiles));
                hashesThread.Start();
                Thread.Sleep(100);
            }
            return hashes;
        }

        /// <summary>
        /// Создает MD5-хеш для всех файлов в каталоге (включая подкаталоги).
        /// </summary>
        /// <param name="pathsOfFiles">Абсолютные пути до файлов.</param>
        /// <returns>MD5-хеш для всех файлов.</returns>
        public List<string> CreateMd5HashForEachFile(List<string> pathsOfFiles)
        {
            List<string> hashes = new List<string>();

            for (int i = 0; i < pathsOfFiles.Count; i++)
            {
                string pathToFile = pathsOfFiles[i];
                string hashOfFile = CreateMd5HashForFile(pathToFile);
                hashes.Add(hashOfFile);
            }

            return hashes;
        }

        /// <summary>
        /// Создает MD5-хеш для файла.
        /// </summary>
        /// <param name="pathToFile">Абсолютный путь до файла.</param>
        /// <returns>Значение MD5-хеша для файла в виде строки, приведенное к нижнему регистру.</returns>
        private string CreateMd5HashForFile(string pathToFile)
        {
            using (MD5 md5 = MD5.Create()) {
                using (FileStream stream = File.OpenRead(pathToFile)) {
                    byte[] hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }
    }
}
