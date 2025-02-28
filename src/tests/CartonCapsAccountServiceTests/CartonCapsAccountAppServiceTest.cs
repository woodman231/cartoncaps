using CartonCapsAccountRepository;
using CartonCapsAccountService.Models;
using CartonCapsDbContext.Models;
using Moq;

namespace CartonCapsAccountServiceTests;

public class CartonCapsAccountAppServiceTest
{
    private readonly Mock<ICartonCapsAccountRepository> _repositoryMock;
    private readonly CartonCapsAccountAppService _service;

    public CartonCapsAccountAppServiceTest()
    {
        _repositoryMock = new Mock<ICartonCapsAccountRepository>();
        _service = new CartonCapsAccountAppService(_repositoryMock.Object);
    }

    [Fact]
    public async Task CreateAccountAsync_ShouldThrowException_WhenEmailIsEmpty()
    {
        var input = new CreateAccountAsyncInput { Email = "" };

        await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateAccountAsync(input));
    }

    [Fact]
    public async Task CreateAccountAsync_ShouldThrowException_WhenEmailAlreadyExists()
    {
        var input = new CreateAccountAsyncInput { Email = "test@example.com" };
        _repositoryMock.Setup(x => x.GetAccountByEmailAsync(input.Email)).ReturnsAsync(new AccountEntity
        {
            Email = input.Email
        });

        await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateAccountAsync(input));
    }

    [Fact]
    public async Task CreateAccountAsync_ShouldReturnNull_WhenAccountCreationFails()
    {
        var input = new CreateAccountAsyncInput { Email = "test@example.com" };
        _repositoryMock.Setup(x => x.GetAccountByEmailAsync(input.Email)).ReturnsAsync((AccountEntity?)null);
        _repositoryMock.Setup(x => x.CreateAccountAsync(input.Email, It.IsAny<string>())).ReturnsAsync((AccountEntity?)null);

        var result = await _service.CreateAccountAsync(input);

        Assert.Null(result);
    }

    [Fact]
    public async Task CreateAccountAsync_ShouldReturnAccount_WhenAccountIsCreated()
    {
        var input = new CreateAccountAsyncInput { Email = "test@example.com" };
        _repositoryMock.Setup(x => x.GetAccountByEmailAsync(input.Email)).ReturnsAsync((AccountEntity?)null);
        _repositoryMock.Setup(x => x.CreateAccountAsync(input.Email, It.IsAny<string>())).ReturnsAsync(new AccountEntity
        {
            ID = 1,
            Email = input.Email,
            ReferralCode = "ABC123"
        });

        var result = await _service.CreateAccountAsync(input);

        _repositoryMock.Verify(x => x.CreateAccountAsync(input.Email, It.IsAny<string>()), Times.Once);
        Assert.Equal(input.Email, result?.Email);
    }
}