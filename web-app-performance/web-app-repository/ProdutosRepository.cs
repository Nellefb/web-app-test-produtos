using Dapper;
using MySqlConnector;
using web_app_domain;

namespace web_app_repository
{
    public class ProdutosRepository : IProdutosRepository
    {
        private readonly MySqlConnection mySqlConnection;

        public ProdutosRepository()
        {
            string connectionString = "Server = localhost;Database=sys;User=root;Password=123;";
            mySqlConnection = new MySqlConnection(connectionString);
        }

        public async Task<IEnumerable<Produtos>> ListarProdutos()
        {
            await mySqlConnection.OpenAsync();
            string query = "select id, nome, preco, quant_estoque, data_criacao from produtos;";
            var produtos = await mySqlConnection.QueryAsync<Produtos>(query);
            await mySqlConnection.CloseAsync();

            return produtos;
        }

        public async Task SalvarProdutos(Produtos produtos)
        {
            //abre conexao
            await mySqlConnection.OpenAsync();
            //faz insert
            string sql = @"insert into produtos(nome, preco, quant_estoque, data_criacao) values (@nome, @preco, @quant_estoque, @data_criacao);";
            //executa insert
            await mySqlConnection.ExecuteAsync(sql, produtos);
            //fechar conexao
            await mySqlConnection.CloseAsync();
        }

        public async Task AtualizarProdutos(Produtos produtos)
        {
            await mySqlConnection.OpenAsync();
            string sql = @"update produtos set nome = @nome, preco = @preco, quant_estoque = @quant_estoque, data_criacao = @data_criacao where id = @id;";
            await mySqlConnection.ExecuteAsync(sql, produtos);
            await mySqlConnection.CloseAsync();
        }

        public async Task RemoverProdutos(int id)
        {
            await mySqlConnection.OpenAsync();
            string sql = @"delete from produtos where id = @id;";
            await mySqlConnection.ExecuteAsync(sql, new { id });
            await mySqlConnection.CloseAsync();
        }
    }
}
