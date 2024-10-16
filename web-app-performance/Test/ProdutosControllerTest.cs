using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using web_app_domain;
using web_app_performance.Controllers;
using web_app_repository;

namespace Test
{
    public class ProdutosControllerTest
    {
        //p testar repository
        private readonly Mock<IProdutosRepository> _produtosRepositoryMock;
        private readonly ProdutosController _controller;

        public ProdutosControllerTest()
        {
            _produtosRepositoryMock = new Mock<IProdutosRepository>();
            _controller = new ProdutosController(_produtosRepositoryMock.Object);
        }

        [Fact]
        public async Task Get_ListarProdutosOK()
        {
            //arrange
            var produtos = new List<Produtos>() {
                new Produtos()
                {
                    Id = 12,
                    Nome = "Abacate",
                    Preco = 7,
                    Quant_estoque = 5,
                }
            };
            _produtosRepositoryMock.Setup(r => r.ListarProdutos()).ReturnsAsync(produtos);

            //Act
            var result = await _controller.GetProdutos();

            //Asserts
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;

            //comparar o resultado com a lista mock
            Assert.Equal(JsonConvert.SerializeObject(produtos), JsonConvert.SerializeObject(okResult.Value));

        }

        [Fact]
        public async Task Get_ListarProdutosRetornaNotFound()
        {
            _produtosRepositoryMock.Setup(u => u.ListarProdutos()).ReturnsAsync((IEnumerable<Produtos>)null);

            var result = await _controller.GetProdutos();

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Post_SalvarProdutos()
        {
            //Arrange
            var produtos = new Produtos()
            {
                Id = 17,
                Nome = "Melancia",
                Preco = 15.7,
                Quant_estoque = 9,
            };

            _produtosRepositoryMock.Setup(u => u.SalvarProdutos(It.IsAny<Produtos>())).Returns(Task.CompletedTask);

            //Act 
            var result = await _controller.Post(produtos);

            //Asserts
            _produtosRepositoryMock.Verify(u => u.SalvarProdutos(It.IsAny<Produtos>()), Times.Once);

            //inserir so devolve o ok sem o obj = okResult
            //se o metod devolver obj, deve trocar para okObject
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
