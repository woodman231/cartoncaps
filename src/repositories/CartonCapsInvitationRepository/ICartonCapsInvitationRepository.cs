using CartonCapsDbContext.Models;
using CartonCapsInvitationRepository.Models;

namespace CartonCapsInvitationRepository;

public interface ICartonCapsInvitationRepository
{
    Task<Invitation?> GetCartonCapsInvitationAsync(int id);
    Task<IEnumerable<Invitation>> GetCartonCapsInvitationsAsync(GetCartonCapsInvitationsAsyncInput input);
    Task<Invitation> CreateCartonCapsInvitationAsync(Invitation cartonCapsInvitation);
    Task<Invitation?> UpdateCartonCapsInvitationAsync(Invitation cartonCapsInvitation);
    Task DeleteCartonCapsInvitationAsync(int id);    
}
