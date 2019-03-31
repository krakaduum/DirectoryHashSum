using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;

namespace DirectoryHashSum
{
    public class DatabaseWriter
    {
        SqlConnection connection = new SqlConnection("Server=ADMIN\\SQLEXPRESS;Database=DirectoryHS;Integrated Security=true");

        /// <summary>
        /// Запись всех хеш-сумм в БД.
        /// </summary>
        /// <param name="hashes">Список хеш-сумм файлов.</param>
        public void WriteAllHashesInDb(List<string> hashes)
        {
            using (connection)
            {
                connection.Open();
                for (int i = 0; i < hashes.Count; i++)
                {
                    WriteHashInDb(hashes[i]);
                }
            }
        }

        /// <summary>
        /// Запись одной хеш-суммы в БД.
        /// </summary>
        /// <param name="hash">Значение MD5-хеша.</param>
        private void WriteHashInDb(string hash)
        {
            try {
                const string sql = "Insert into HashSums (HashSum) "
                                                 + " values (@FileCheckSum) ";

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = sql;

                SqlParameter CheckSum = cmd.Parameters.Add("@FileCheckSum", SqlDbType.NVarChar);
                CheckSum.Value = hash;

                int rowCount = cmd.ExecuteNonQuery();
            }
            catch (Exception e) {
                Console.WriteLine("Error: " + e);
            }
        }
    }
}
