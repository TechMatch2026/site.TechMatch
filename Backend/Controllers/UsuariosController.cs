using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace TechMatch.backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        [HttpPost]
        public IActionResult CriarUsuario([FromBody] Usuario usuario)
        {
            try
            {
                Conexao conexao = new Conexao();

                using (MySqlConnection banco = conexao.Conectar())
                {
                    string sql = "INSERT INTO usuarios (nome, email, senha) VALUES (@nome, @email, @senha)";

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
    }

    public class Usuario
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}