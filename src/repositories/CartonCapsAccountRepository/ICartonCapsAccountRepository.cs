using CartonCapsDbContext.Models;

namespace CartonCapsAccountRepository;

public interface ICartonCapsAccountRepository
{
    Task<AccountEntity?> GetAccountByEmailAsync(string email);
    Task<AccountEntity?> GetAccountByReferralCodeAsync(string referralCode);
    Task<AccountEntity?> GetAccountByIDAsync(int id);
    Task<AccountEntity?> CreateAccountAsync(string email, string referralCode);
    Task<AccountEntity?> UpdateAccountAsync(int id, string email, string referralCode);
    Task<IEnumerable<AccountEntity>> GetAccountsAsync(string? email, string? referralCode);
    Task DeleteAccountAsync(int id);
}
