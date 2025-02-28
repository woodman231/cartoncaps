using CartonCapsAccountRepository;
using CartonCapsInvitationRepository;
using CartonCapsInvitationService;
using CartonCapsInvitationService.Models;
using CartonCapsDbContext.Models;
using Moq;

namespace CartonCapsInvitationServiceTests;

public class CartonCapsInvitationAppServiceTests
{
    private readonly Mock<ICartonCapsAccountRepository> _accountRepositoryMock;
    private readonly Mock<ICartonCapsInvitationRepository> _invitationRepositoryMock;
    private readonly CartonCapsInvitationAppService _service;

    public CartonCapsInvitationAppServiceTests()
    {
        _accountRepositoryMock = new Mock<ICartonCapsAccountRepository>();
        _invitationRepositoryMock = new Mock<ICartonCapsInvitationRepository>();
        _service = new CartonCapsInvitationAppService(_invitationRepositoryMock.Object, _accountRepositoryMock.Object);
    }

    [Fact]
    public async Task CreateInvitationAsync_ShouldThrowException_WhenInvitedEmailIsNotProvided()
    {
        // Arrange
        var input = new CreateInvitationAsyncInput
        {
            InvitedEmail = null,            
        };

        // Act
        await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateInvitationAsync(input));
    }

    [Fact]
    public async Task CreateInvitationAsync_ShouldThrowException_WhenInvitedEmailIsInvalid()
    {
        // Arrange
        var input = new CreateInvitationAsyncInput
        {
            InvitedEmail = "invalidemail",            
        };

        // Act
        await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateInvitationAsync(input));
    }

    [Fact]
    public async Task CreateInvitationAsync_ShouldThrowException_WhenInvitedEmailContainsPlusSign()
    {
        // Arrange
        var input = new CreateInvitationAsyncInput
        {
            InvitedEmail = "someone+something@somedomain.com",
        };

        // Act
        await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateInvitationAsync(input));
    }

    [Fact]
    public async Task CreateInvitationAsync_ShouldThrowException_WhenInvitedEmailIsAlreadyAnAccount()
    {
        // Arrange
        var input = new CreateInvitationAsyncInput
        {
            InvitedEmail = "someone@somedomain.com",
        };

        _accountRepositoryMock.Setup(x => x.GetAccountByEmailAsync(input.InvitedEmail)).ReturnsAsync(new AccountEntity());

        // Act
        await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateInvitationAsync(input));
    }

    [Fact]
    public async Task CreateInvitationAsync_ShouldThrowException_WhenInvitedEmailIsAlreadyInvited()
    {
        // Arrange
        var input = new CreateInvitationAsyncInput
        {
            InvitedEmail = "someone@somedomain.com",
        };

        _accountRepositoryMock.Setup(x => x.GetAccountByEmailAsync(input.InvitedEmail)).ReturnsAsync((AccountEntity?)null);

        CartonCapsInvitationRepository.Models.GetCartonCapsInvitationsAsyncInput repositoryInput = new CartonCapsInvitationRepository.Models.GetCartonCapsInvitationsAsyncInput
        {
            EmailAddress = input.InvitedEmail
        };

        _invitationRepositoryMock.Setup(x => x.GetCartonCapsInvitationsAsync(repositoryInput)).ReturnsAsync(new List<InvitationEntity>() { new InvitationEntity() });

        // Act
        await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateInvitationAsync(input));
    }

    [Fact]
    public async Task CreateInvitationAsync_ThrowsException_WhenSenderAccountIDIsInvalid()
    {
        // Arrange
        var input = new CreateInvitationAsyncInput
        {
            InvitedEmail = "someone@somedomain.com",
            SenderAccountID = 0,
        };

        _accountRepositoryMock.Setup(x => x.GetAccountByIDAsync(input.SenderAccountID)).ReturnsAsync((AccountEntity?)null);

        // Act
        await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateInvitationAsync(input));
    }

    [Fact]
    public async Task CreateInvitationAsync_ThrowsException_WhenSenderReferralCodeIsInvalid()
    {
        // Arrange
        var input = new CreateInvitationAsyncInput
        {
            InvitedEmail = "someone@somedomain.com",
            SenderAccountID = 1,
            SenderReferralCode = "ABC123",
        };

        _accountRepositoryMock.Setup(x => x.GetAccountByIDAsync(input.SenderAccountID)).ReturnsAsync(new AccountEntity { ReferralCode = "XYZ789" });

        // Act
        await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateInvitationAsync(input));
    }

    [Fact]
    public async Task CreateInvitationAsync_CreatesInvitation_WhenInputIsValid()
    {
        // Arrange
        var input = new CreateInvitationAsyncInput
        {
            InvitedFirstName = "Some",
            InvitedLastName = "One",
            InvitedEmail = "someone@somedomain.com",
            SenderAccountID = 1,
            SenderReferralCode = "ABC123",
        };

        _accountRepositoryMock.Setup(x => x.GetAccountByEmailAsync(input.InvitedEmail)).ReturnsAsync((AccountEntity?)null);
        _accountRepositoryMock.Setup(x => x.GetAccountByIDAsync(input.SenderAccountID)).ReturnsAsync(new AccountEntity { ReferralCode = input.SenderReferralCode });
        _invitationRepositoryMock.Setup(x => x.CreateCartonCapsInvitationAsync(It.IsAny<InvitationEntity>())).ReturnsAsync(new InvitationEntity() {
            ID = 1,
            SenderAccountID = input.SenderAccountID,
            SenderReferralCode = input.SenderReferralCode,
            InvitedFirstName = input.InvitedFirstName,
            InvitedLastName = input.InvitedLastName,
            InvitedEmail = input.InvitedEmail,
            InvitedAt = DateTime.UtcNow
        });

        // Act
        var result = await _service.CreateInvitationAsync(input);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(input.InvitedEmail, result.InvitedEmail);
        Assert.Equal(input.InvitedFirstName, result.InvitedFirstName);
        Assert.Equal(input.InvitedLastName, result.InvitedLastName);
        Assert.Equal(input.SenderAccountID, result.SenderAccountID);
        Assert.Equal(input.SenderReferralCode, result.SenderReferralCode);
    }
}