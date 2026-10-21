using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace TechMatch.backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        // CADASTRAR USUÁRIO
        [HttpPost]
        public IActionResult CriarUsuario([FromBody] Usuario usuario)
        {
            try
            {
                Conexao conexao = new Conexao();

                using (MySqlConnection banco = conexao.Conectar())
                {
                    // Verifica se o nome já existe
                    string verificarSql =
                        "SELECT COUNT(*) FROM usuarios WHERE nome = @nome";

                    using (MySqlCommand verificar = new MySqlCommand(verificarSql, banco))
                    {
                        verificar.Parameters.AddWithValue("@nome", usuario.Nome);

                        int quantidade = Convert.ToInt32(verificar.ExecuteScalar());

                        if (quantidade > 0)
                        {
                            return Conflict("Esse usuário já existe.");
                        }
                    }

                    // Cadastra o novo usuário
                    string sql =
                        "INSERT INTO usuarios (nome, email, senha) " +
                        "VALUES (@nome, @email, @senha)";

                    using (MySqlCommand comando = new MySqlCommand(sql, banco))
                    {
                        comando.Parameters.AddWithValue("@nome", usuario.Nome);
                        comando.Parameters.AddWithValue("@email", usuario.Email);
                        comando.Parameters.AddWithValue("@senha", usuario.Senha);

                        comando.ExecuteNonQuery();
                    }
                }

                return Ok("Usuário cadastrado com sucesso!");
            }
            catch (Exception erro)
            {
                return BadRequest("Erro: " + erro.Message);
            }
        }


        // VERIFICAR LOGIN
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest login)
        {
            try
            {
                Conexao conexao = new Conexao();

                using (MySqlConnection banco = conexao.Conectar())
                {
                    string sql =
                        "SELECT email, senha FROM usuarios WHERE nome = @nome";

                    using (MySqlCommand comando = new MySqlCommand(sql, banco))
                    {
                        comando.Parameters.AddWithValue("@nome", login.Nome);

                        using (MySqlDataReader leitor = comando.ExecuteReader())
                        {
                            if (!leitor.Read())
                            {
                                return NotFound("Usuário não encontrado.");
                            }

                            string emailBanco = leitor["email"].ToString();
                            string senhaBanco = leitor["senha"].ToString();

                            if (emailBanco != login.Email || senhaBanco != login.Senha)
                            {
                                return Unauthorized("E-mail ou senha incorretos.");
                            }

                            return Ok("Login realizado com sucesso!");
                        }
                    }
                }
            }
            catch (Exception erro)
            {
                return BadRequest("Erro: " + erro.Message);
            }
        }
    }


    public class Usuario
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }


    public class LoginRequest
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
