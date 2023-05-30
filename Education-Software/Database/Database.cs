using Npgsql;


namespace Education_Software.Database
{
    public class Database
    {
        public static NpgsqlConnection Connection()
        {
            return new NpgsqlConnection("SERVER=localhost;Port=5432;Database=EducationSoftware;User Id=postgres;Password=MariaDB1;");
        }

        public static NpgsqlDataReader ExecuteQuery(string query, NpgsqlConnection con)
        {
            NpgsqlConnection npgsqlConnection = con;
            NpgsqlCommand cmd = npgsqlConnection.CreateCommand();
            npgsqlConnection.Open();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = query;
            NpgsqlDataReader reader = cmd.ExecuteReader();
            return reader;
        }
    }
}
