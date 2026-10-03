using MySql.Data.MySqlClient;

namespace TechMatch.backend
{
    public class Conexao
    {
        public MySqlConnection Conectar()
        {
            string conexao =
                $"Server={Environment.GetEnvironmentVariable("DB_HOST")};" +
                $"Port={Environment.GetEnvironmentVariable("DB_PORT")};" +
                $"Database={Environment.GetEnvironmentVariable("DB_NAME")};" +
                $"Uid={Environment.GetEnvironmentVariable("DB_USER")};" +
                $"Pwd={Environment.GetEnvironmentVariable("DB_PASSWORD")};" +
                "SslMode=Required;";

            MySqlConnection banco = new MySqlConnection(conexao);

            banco.Open();

            return banco;
        }
    }
}
