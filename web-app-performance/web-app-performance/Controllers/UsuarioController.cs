using Dapper;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using Newtonsoft.Json;
using StackExchange.Redis;
using web_app_domain;
using web_app_repository;

namespace web_app_performance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private static ConnectionMultiplexer redis;
        private readonly IUsuarioRepository _repository;

        //onde ira receber a injeção
        public UsuarioController(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsuario() 
        {

            //tem q comentar essa parte para os testes
            //string key = "getusuario";
            //redis = ConnectionMultiplexer.Connect("localhost:6379");
            //IDatabase db = redis.GetDatabase();
            //await db.KeyExpireAsync(key, TimeSpan.FromSeconds(10));
            //string user = await db.StringGetAsync(key);

            //if (!string.IsNullOrEmpty(user))
            //{
            //    return Ok(user);
            //}

            var usuarios = await _repository.ListarUsuarios();
            if (usuarios == null)
                return NotFound();

            string usuariosJson = JsonConvert.SerializeObject(usuarios);
            //await db.StringSetAsync(key, usuariosJson);
            return Ok(usuarios);

        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Usuario usuario)
        {
            //tem q comentar essa parte para os testes
            await _repository.SalvarUsuario(usuario);
            ////apagar o cache
            ////conexao c redis
            //string key = "getusuario";
            //redis = ConnectionMultiplexer.Connect("localhost:6379");
            //IDatabase db = redis.GetDatabase();
            //await db.KeyDeleteAsync(key);

            return Ok(new { mensagem = "Criado com sucesso!" });


        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Usuario usuario)
        {
           await _repository.AtualizarUsuario(usuario);

            //apagar o cache
            //conexao c redis
            string key = "getusuario";
            redis = ConnectionMultiplexer.Connect("localhost:6379");
            IDatabase db = redis.GetDatabase();
            await db.KeyDeleteAsync(key);
            return Ok();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.RemoverUsuario(id);

            string key = "getusuario";
            redis = ConnectionMultiplexer.Connect("localhost:6379");
            IDatabase db = redis.GetDatabase();
            await db.KeyDeleteAsync(key);

            return Ok();

        }
    }
}

//docker run --name database-mysql -e MYSQL_ROOT_PASSWORD=123 -p 3306:3306 -d mysql


//docker ps
