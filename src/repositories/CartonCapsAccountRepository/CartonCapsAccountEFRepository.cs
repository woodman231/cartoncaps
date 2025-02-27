using CartonCapsDbContext.Models;
using Microsoft.EntityFrameworkCore;

namespace CartonCapsAccountRepository;

public class CartonCapsAccountEFRepository : ICartonCapsAccountRepository
{
    private readonly CartonCapsDbContext.CartonCapsEFDbContext _dbContext;

    public CartonCapsAccountEFRepository(CartonCapsDbContext.CartonCapsEFDbContext DbContext)
    {
        _dbContext = DbContext;
    }

    public async Task<Account?> GetAccountByEmailAsync(string email)
    {
        return await _dbContext.Accounts.FirstOrDefaultAsync(a => a.Email == email);
    }

    public async Task<Account?> GetAccountByReferralCodeAsync(string referralCode)
    {
        return await _dbContext.Accounts.FirstOrDefaultAsync(a => a.ReferralCode == referralCode);
    }

    public async Task<Account?> GetAccountByIDAsync(int id)
    {
        return await _dbContext.Accounts.FirstOrDefaultAsync(a => a.ID == id);
    }

    public async Task<Account?> CreateAccountAsync(string email, string referralCode)
    {
        var account = new Account
        {
            Email = email,
            ReferralCode = referralCode
        };

        await _dbContext.Accounts.AddAsync(account);
        await _dbContext.SaveChangesAsync();

        return account;
    }

    public async Task<Account?> UpdateAccountAsync(int id, string email, string referralCode)
    {
        var account = await _dbContext.Accounts.FirstOrDefaultAsync(a => a.ID == id);

        if (account == null)
        {
            return null;
        }

        account.Email = email;
        account.ReferralCode = referralCode;

        await _dbContext.SaveChangesAsync();

        return account;
    }

    public async Task DeleteAccountAsync(int id)
    {
        var account = await _dbContext.Accounts.FirstOrDefaultAsync(a => a.ID == id);

        if (account != null)
        {
            _dbContext.Accounts.Remove(account);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Account>> GetAccountsAsync(string? email, string? referralCode)
    {
        IQueryable<Account> query = _dbContext.Accounts;

        if (!string.IsNullOrEmpty(email))
        {
            query = query.Where(a => a.Email == email);
        }

        if (!string.IsNullOrEmpty(referralCode))
        {
            query = query.Where(a => a.ReferralCode == referralCode);
        }

        return await query.ToListAsync();
    }
}
