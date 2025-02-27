using CartonCapsDbContext.Models;

namespace CartonCapsAccountRepository;

public interface ICartonCapsAccountRepository
{
    Task<Account?> GetAccountByEmailAsync(string email);
    Task<Account?> GetAccountByReferralCodeAsync(string referralCode);
    Task<Account?> GetAccountByIDAsync(int id);
    Task<Account?> CreateAccountAsync(string email, string referralCode);
    Task<Account?> UpdateAccountAsync(int id, string email, string referralCode);
    Task<IEnumerable<Account>> GetAccountsAsync(string? email, string? referralCode);
    Task DeleteAccountAsync(int id);
}
