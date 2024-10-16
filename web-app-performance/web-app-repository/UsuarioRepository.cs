using Dapper;
using MySqlConnector;
using web_app_domain;

namespace web_app_repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly MySqlConnection mySqlConnection;
        public UsuarioRepository()
        {
            string connectionString = "Server = localhost;Database=sys;User=root;Password=123;";
            mySqlConnection = new MySqlConnection(connectionString);
        }

        public async Task<IEnumerable<Usuario>> ListarUsuarios()
        {
            await mySqlConnection.OpenAsync();
            string query = "select Id, Nome, Email from usuarios;";
            var usuarios = await mySqlConnection.QueryAsync<Usuario>(query);
            await mySqlConnection.CloseAsync();

            return usuarios;
        }

        public async Task SalvarUsuario(Usuario usuario)
        {
            //abre conexao
            await mySqlConnection.OpenAsync();
            //faz insert
            string sql = @"insert into usuarios(nome, email) values (@nome, @email);";
            //executa insert
            await mySqlConnection.ExecuteAsync(sql, usuario);
            //fechar conexao
            await mySqlConnection.CloseAsync();
        }

        public async Task AtualizarUsuario(Usuario usuario)
        {
            await mySqlConnection.OpenAsync();
            string sql = @"update usuarios set Nome = @nome, Email = @email where Id = @id;";
            await mySqlConnection.ExecuteAsync(sql, usuario);
            await mySqlConnection.CloseAsync();
        }

        public async Task RemoverUsuario(int id)
        {
            await mySqlConnection.OpenAsync();
            string sql = @"delete from usuarios where Id = @id;";
            await mySqlConnection.ExecuteAsync(sql, new { id });
            await mySqlConnection.CloseAsync();
        }

    }
}
