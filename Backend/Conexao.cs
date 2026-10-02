using MySql.Data.MySqlClient;

namespace TechMatch.backend
{
    public class Conexao
    {
        public MySqlConnection Conectar()
        {
            string conexao = "Server=localhost;Database=techmatchbd;Uid=root;Pwd=;";

            MySqlConnection banco = new MySqlConnection(conexao);

            banco.Open();

            return banco;
        }
    }
}