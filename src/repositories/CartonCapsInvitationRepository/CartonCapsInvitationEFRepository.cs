using CartonCapsDbContext.Models;
using CartonCapsInvitationRepository.Models;
using Microsoft.EntityFrameworkCore;

namespace CartonCapsInvitationRepository;

public class CartonCapsInvitationEFRepository : ICartonCapsInvitationRepository
{
    private readonly CartonCapsDbContext.CartonCapsEFDbContext _dbContext;

    public CartonCapsInvitationEFRepository(CartonCapsDbContext.CartonCapsEFDbContext dbContext)
    {
        _dbContext = dbContext;        
    }

    public async Task<InvitationEntity> CreateCartonCapsInvitationAsync(InvitationEntity cartonCapsInvitation)
    {
        var result = await _dbContext.Invitations.AddAsync(cartonCapsInvitation);
        await _dbContext.SaveChangesAsync();
        return result.Entity;
    }

    public async Task DeleteCartonCapsInvitationAsync(int id)
    {
        var invitation = await _dbContext.Invitations.FindAsync(id);
        if (invitation != null)
        {
            _dbContext.Invitations.Remove(invitation);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<InvitationEntity?> GetCartonCapsInvitationAsync(int id)
    {
        return await _dbContext.Invitations.FindAsync(id);
    }

    public async Task<IEnumerable<InvitationEntity>> GetCartonCapsInvitationsAsync(GetCartonCapsInvitationsAsyncInput input)
    {
        IQueryable<InvitationEntity> query = _dbContext.Invitations;
        
        if (input.AccountID.HasValue)
        {
            query = query.Where(i => i.SenderAccountID == input.AccountID.Value);
        }

        if (!string.IsNullOrWhiteSpace(input.EmailAddress))
        {
            query = query.Where(i => i.InvitedEmail == input.EmailAddress);
        }

        if (!string.IsNullOrWhiteSpace(input.ReferralCode))
        {
            query = query.Where(i => i.SenderReferralCode == input.ReferralCode);
        }

        return await query.ToListAsync();
    }

    public async Task<InvitationEntity?> UpdateCartonCapsInvitationAsync(InvitationEntity cartonCapsInvitation)
    {
        var invitation = await _dbContext.Invitations.FindAsync(cartonCapsInvitation.ID);
        if (invitation != null)
        {
            _dbContext.Entry(invitation).CurrentValues.SetValues(cartonCapsInvitation);
            await _dbContext.SaveChangesAsync();
        }

        return invitation;
    }
}
