using CartonCapsDbContext.Models;
using CartonCapsInvitationRepository.Models;

namespace CartonCapsInvitationRepository;

public interface ICartonCapsInvitationRepository
{
    Task<InvitationEntity?> GetCartonCapsInvitationAsync(int id);
    Task<IEnumerable<InvitationEntity>> GetCartonCapsInvitationsAsync(GetCartonCapsInvitationsAsyncInput input);
    Task<InvitationEntity> CreateCartonCapsInvitationAsync(InvitationEntity cartonCapsInvitation);
    Task<InvitationEntity?> UpdateCartonCapsInvitationAsync(InvitationEntity cartonCapsInvitation);
    Task DeleteCartonCapsInvitationAsync(int id);    
}
