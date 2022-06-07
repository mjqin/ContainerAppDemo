using ContainerAppDemo.Models;
using System.Data.Common;
using System.Data.SqlClient;

namespace ContainerAppDemo.Helpers
{
    public static class SqlHelper
    {
        public const string TablesQuery = @"select t.name as table_name, t.create_date, t.modify_date
           from sys.tables t order by table_name;";

        public static List<SqlTable> GetTables(string connectionString)
        {
            var result = new List<SqlTable>();
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = TablesQuery;
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        result.Add(new SqlTable()
                        {
                            TableName = reader.GetString(0),
                            CreatedTime = reader.GetDateTime(1),
                            UpdatedTime = reader.GetDateTime(2),
                        });

                    }
                }
            }

            return result;
        }
    }
}
