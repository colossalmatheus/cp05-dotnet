using cp05.Model;
using cp05.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Banco
{
    public class BancoControllerTests
    {
        private readonly Mock<IBancoRepository> _mockRepo;
        private readonly BancoController _controller;

        public BancoControllerTests()
        {
            _mockRepo = new Mock<IBancoRepository>();
            _controller = new BancoController(_mockRepo.Object);
        }

        [Fact]
        public async Task AddBanco_ReturnsOk_WhenBancoIsValid()
        {
            // Arrange
            var banco = new BancoModel { Id = "1", Nome = "Banco Teste", Agencia = "123", Conta = "456789" };

            // Act
            var result = await _controller.AddBanco(banco);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<ResponseModel>(okResult.Value);
            Assert.Equal("Banco Teste adicionado com sucesso!", response.Message);
            _mockRepo.Verify(repo => repo.SaveAsync(banco), Times.Once);
        }

        [Fact]
        public async Task AddBanco_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("Nome", "O campo Nome é obrigatório.");
            var banco = new BancoModel();

            // Act
            var result = await _controller.AddBanco(banco);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var validationErrors = Assert.IsType<SerializableError>(badRequestResult.Value);
            Assert.True(validationErrors.ContainsKey("Nome"));
            Assert.Contains("O campo Nome é obrigatório.", validationErrors["Nome"] as IEnumerable<string>);
        }

        [Fact]
        public async Task UpdateBanco_ReturnsOk_WhenBancoExists()
        {
            // Arrange
            var banco = new BancoModel { Id = "1", Nome = "Banco Teste Atualizado", Agencia = "123", Conta = "456789" };
            _mockRepo.Setup(repo => repo.GetByIdAsync("1")).ReturnsAsync(banco);

            // Act
            var result = await _controller.UpdateBanco("1", banco);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<ResponseModel>(okResult.Value);
            Assert.Equal("Banco Teste atualizado com sucesso!", response.Message);
            _mockRepo.Verify(repo => repo.UpdateAsync("1", banco), Times.Once);
        }

        [Fact]
        public async Task UpdateBanco_ReturnsNotFound_WhenBancoDoesNotExist()
        {
            // Arrange
            var banco = new BancoModel { Id = "1", Nome = "Banco Teste Atualizado" };
            _mockRepo.Setup(repo => repo.GetByIdAsync("1")).ReturnsAsync((BancoModel)null);

            // Act
            var result = await _controller.UpdateBanco("1", banco);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            var response = Assert.IsType<ResponseModel>(notFoundResult.Value);
            Assert.Equal("Banco Teste não encontrado.", response.Message);
        }

        [Fact]
        public async Task DeleteBanco_ReturnsOk_WhenBancoExists()
        {
            // Arrange
            var banco = new BancoModel { Id = "1", Nome = "Banco Teste" };
            _mockRepo.Setup(repo => repo.GetByIdAsync("1")).ReturnsAsync(banco);

            // Act
            var result = await _controller.DeleteBanco("1");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<ResponseModel>(okResult.Value);
            Assert.Equal("Banco Teste deletado com sucesso!", response.Message);
            _mockRepo.Verify(repo => repo.DeleteAsync("1"), Times.Once);
        }

        [Fact]
        public async Task DeleteBanco_ReturnsNotFound_WhenBancoDoesNotExist()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetByIdAsync("1")).ReturnsAsync((BancoModel)null);

            // Act
            var result = await _controller.DeleteBanco("1");

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            var response = Assert.IsType<ResponseModel>(notFoundResult.Value);
            Assert.Equal("Banco Teste não encontrado.", response.Message);
        }

        [Fact]
        public async Task GetBancoById_ReturnsOk_WhenBancoExists()
        {
            // Arrange
            var banco = new BancoModel { Id = "1", Nome = "Banco Teste" };
            _mockRepo.Setup(repo => repo.GetByIdAsync("1")).ReturnsAsync(banco);

            // Act
            var result = await _controller.GetBancoById("1");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(banco, okResult.Value);
        }

        [Fact]
        public async Task GetBancoById_ReturnsNotFound_WhenBancoDoesNotExist()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetByIdAsync("1")).ReturnsAsync((BancoModel)null);

            // Act
            var result = await _controller.GetBancoById("1");

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            var response = Assert.IsType<ResponseModel>(notFoundResult.Value);
            Assert.Equal("Banco Teste não encontrado.", response.Message);
        }

        [Fact]
        public async Task GetAllBanco_ReturnsOk_WhenBancosExist()
        {
            // Arrange
            var bancos = new List<BancoModel>
            {
                new BancoModel { Id = "1", Nome = "Banco Teste 1", Agencia = "123", Conta = "456789" },
                new BancoModel { Id = "2", Nome = "Banco Teste 2", Agencia = "456", Conta = "987654" }
            };
            _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(bancos);

            // Act
            var result = await _controller.GetAllBanco();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedBancos = Assert.IsAssignableFrom<List<BancoModel>>(okResult.Value);
            Assert.Equal(bancos.Count, returnedBancos.Count);
        }
    }
}
