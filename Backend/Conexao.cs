using MySql.Data.MySqlClient;

namespace TechMatch.backend
{
    public class Conexao
    {
        public MySqlConnection Conectar()
        {
            string servidor = Environment.GetEnvironmentVariable("DB_SERVER") ?? "localhost";
            string bancoDados = Environment.GetEnvironmentVariable("DB_NAME") ?? "techmatchbd";
            string usuario = Environment.GetEnvironmentVariable("DB_USER") ?? "root";
            string senha = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "";

            string conexao =
                $"Server={servidor};Database={bancoDados};Uid={usuario};Pwd={senha};";

            MySqlConnection banco = new MySqlConnection(conexao);

            banco.Open();

            return banco;
        }
    }
}
