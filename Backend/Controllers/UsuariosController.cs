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
