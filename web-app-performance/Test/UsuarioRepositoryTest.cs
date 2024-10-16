using Moq;
using web_app_domain;
using web_app_repository;

namespace Test
{
    public class UsuarioRepositoryTest
    {
        [Fact]
        public async Task ListarUsuarios()
        {
            //Arrange - preparando o teste
            var usuarios = new List<Usuario>() {
                new Usuario()
                {
                    Email = "xxx@gmail.com",
                    Id = 1,
                    Nome = "Ellen"
                },
                new Usuario()
                {
                    Email = "user2@gmail.com",
                    Id = 2,
                    Nome = "User2"
                }
            };

            var userRepositoryMock = new Mock<IUsuarioRepository>();
            userRepositoryMock.Setup(u => u.ListarUsuarios()).ReturnsAsync(usuarios);
            var userRepository = userRepositoryMock.Object;

            //Act
            var result = await userRepository.ListarUsuarios();

            //Assert - validacao
            //ver se a lista q trouxe é igual aos users mockados
            Assert.Equal(usuarios, result);
        }

        [Fact]
        public async Task SalvarUsuario()
        {
            //Arrange
            var usuario = new Usuario()
            {
                Id = 1,
                Email = "ellen@gmail.com",
                Nome = "Ellen"
            };

            var userRepositoryMock = new Mock<IUsuarioRepository>();
            userRepositoryMock.Setup(u => u.SalvarUsuario(It.IsAny<Usuario>())).Returns(Task.CompletedTask);
            var userRepository = userRepositoryMock.Object;

            //Act
            await userRepository.SalvarUsuario(usuario);

            //Assert
            userRepositoryMock.Verify(u => u.SalvarUsuario(It.IsAny<Usuario>()), Times.Once);
        }
    }
}
