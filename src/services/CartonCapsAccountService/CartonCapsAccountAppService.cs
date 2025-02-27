using CartonCapsAccountService;
using CartonCapsAccountService.Models;

namespace CartonCapsAccountRepository;

public class CartonCapsAccountAppService : ICartonCapsAccountService
{
    private readonly ICartonCapsAccountRepository _repository;

    public CartonCapsAccountAppService(ICartonCapsAccountRepository repository)
    {
        _repository = repository;
    }

    private string GenerateReferralCode() {
        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        int maxChars = 6;
        Random random = new Random();
        return new string(Enumerable.Repeat(chars, maxChars)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
    
    public async Task<CartonCapsAccount?> CreateAccountAsync(CreateAccountAsyncInput input)
    {
        // Ensure that an email address was provided
        if (string.IsNullOrWhiteSpace(input.Email))
        {
            throw new ArgumentException("Email address is required");
        }

        // Check if the email address already exists
        var existingAccount = await _repository.GetAccountByEmailAsync(input.Email);
        if (existingAccount != null)
        {
            throw new ArgumentException("An account with this email address already exists");
        }

        // Generate a referral code
        string referralCode = GenerateReferralCode();

        // Create the account
        var dbAccount = await _repository.CreateAccountAsync(input.Email, referralCode);

        // If the account was succesfully created, then return it
        if (dbAccount != null)
        {
            return CartonCapsAccount.fromEntityModel(dbAccount);
        }

        return null;
    }

    public async Task DeleteAccountAsync(int id)
    {
        await _repository.DeleteAccountAsync(id);
    }

    public async Task<CartonCapsAccount?> GetAccountByEmailAsync(string email)
    {
        var dbAccount = await _repository.GetAccountByEmailAsync(email);
        if (dbAccount != null)
        {
            return CartonCapsAccount.fromEntityModel(dbAccount);
        }

        return null;
    }

    public async Task<CartonCapsAccount?> GetAccountByIDAsync(int id)
    {
        var dbAccount = await _repository.GetAccountByIDAsync(id);
        if (dbAccount != null)
        {
            return CartonCapsAccount.fromEntityModel(dbAccount);
        }

        return null;        
    }

    public async Task<CartonCapsAccount?> GetAccountByReferralCodeAsync(string referralCode)
    {
        var dbAccount = await _repository.GetAccountByReferralCodeAsync(referralCode);
        if (dbAccount != null)
        {
            return CartonCapsAccount.fromEntityModel(dbAccount);
        }

        return null;
    }

    public async Task<IEnumerable<CartonCapsAccount>> GetAccountsAsync(GetAccountsAsyncInput input)
    {
        var accountsInDb = await _repository.GetAccountsAsync(input.EmailAddress, input.ReferralCode);
        
        return accountsInDb.Select(CartonCapsAccount.fromEntityModel);
    }
}
