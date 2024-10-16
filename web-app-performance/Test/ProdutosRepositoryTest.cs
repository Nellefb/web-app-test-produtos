using Moq;
using web_app_domain;
using web_app_repository;

namespace Test
{
    public class ProdutosRepositoryTest
        {
            [Fact]
            public async Task ListarProdutos()
            {
            //Arrange - preparando o teste
            var produtos = new List<Produtos>() {
                new Produtos()
                {
                    Id = 1,
                    Nome = "Tomate",
                    Preco = 2.9,
                    Quant_estoque = 3,

                },
                new Produtos()
                {
                    Id = 3,
                    Nome = "Alface",
                    Preco = 5.0,
                    Quant_estoque = 7,

                },
            };

                var produtosRepositoryMock = new Mock<IProdutosRepository>();
                produtosRepositoryMock.Setup(u => u.ListarProdutos()).ReturnsAsync(produtos);
                var produtosRepository = produtosRepositoryMock.Object;

                //Act
                var result = await produtosRepository.ListarProdutos();

                //Assert - validacao
                Assert.Equal(produtos, result);
            }

            [Fact]
            public async Task SalvarProdutos()
            {
                //Arrange
                var produtos = new Produtos()
                {
                    Id = 7,
                    Nome = "Cebola",
                    Preco = 1.3,
                    Quant_estoque = 9,
                };

                 var produtosRepositoryMock = new Mock<IProdutosRepository>();
                 produtosRepositoryMock.Setup(u => u.SalvarProdutos(It.IsAny<Produtos>())).Returns(Task.CompletedTask);
                 var produtosRepository = produtosRepositoryMock.Object;

                //Act
                await produtosRepository.SalvarProdutos(produtos);

            //Assert
                produtosRepositoryMock.Verify(u => u.SalvarProdutos(It.IsAny<Produtos>()), Times.Once);
            }
        }
    }

