using CartonCapsAccountService.Models;

namespace CartonCapsAccountService;

public interface ICartonCapsAccountService
{
    Task<CartonCapsAccount?> GetAccountByEmailAsync(string email);
    Task<CartonCapsAccount?> GetAccountByReferralCodeAsync(string referralCode);
    Task<CartonCapsAccount?> GetAccountByIDAsync(int id);
    Task<CartonCapsAccount?> CreateAccountAsync(CreateAccountAsyncInput input);
    Task<IEnumerable<CartonCapsAccount>> GetAccountsAsync(GetAccountsAsyncInput input);
    Task DeleteAccountAsync(int id);
}
