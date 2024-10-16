using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using web_app_domain;
using web_app_performance.Controllers;
using web_app_repository;

namespace Test
{
    public class UsuarioControllerTest
    {
        //p testar repository
        private readonly Mock<IUsuarioRepository> _userRepositoryMock;
        private readonly UsuarioController _controller;

        public UsuarioControllerTest()
        {
            _userRepositoryMock = new Mock<IUsuarioRepository>();
            _controller = new UsuarioController(_userRepositoryMock.Object);
        }

        [Fact]
        public async Task Get_ListarUsuariosOK()
        {
            //arrange
            //cria a lista e coloca  obj dentro dela, simula uma lista do banco
            var usuarios = new List<Usuario>() {
                new Usuario()
                {
                    Email = "xxx@gmail.com",
                    Id = 1,
                    Nome = "Ellen"
                }
            };
            _userRepositoryMock.Setup(r => r.ListarUsuarios()).ReturnsAsync(usuarios);

            //Act
            var result = await _controller.GetUsuario();

            //Asserts
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;

            //comparar o resultado com a lista mock
            Assert.Equal(JsonConvert.SerializeObject(usuarios), JsonConvert.SerializeObject(okResult.Value));

        }

        [Fact]
        public async Task Get_ListarRetornaNotFound()
        {
            _userRepositoryMock.Setup(u => u.ListarUsuarios()).ReturnsAsync((IEnumerable<Usuario>)null);

            var result = await _controller.GetUsuario();

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Post_SalvarUsuario()
        {
            //Arrange
            var usuario = new Usuario()
            {
                Id = 1,
                Email = "ellen@gmail.com",
                Nome = "Ellen"
            };

            _userRepositoryMock.Setup(u => u.SalvarUsuario(It.IsAny<Usuario>())).Returns(Task.CompletedTask);

            //Act 
            var result = await _controller.Post(usuario);

            //Asserts
            _userRepositoryMock.Verify(u => u.SalvarUsuario(It.IsAny<Usuario>()), Times.Once);

            //inserir so devolve o ok sem o obj = okResult
            //se o metod devolver obj, deve trocar para okObject
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
