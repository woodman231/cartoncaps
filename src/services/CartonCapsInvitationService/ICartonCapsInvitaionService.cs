using CartonCapsInvitationService.Models;

namespace CartonCapsInvitationService;

public interface ICartonCapsInvitationService
{
    Task<CartonCapsInvitation> CreateInvitationAsync(CreateInvitationAsyncInput input);
    Task<CartonCapsInvitation?> GetInvitationAsync(int id);
    Task<List<CartonCapsInvitation>> GetInvitationsAsync(GetCartonCapsInvitationsAsyncInput input);    
    Task<CartonCapsInvitation?> AcceptInvitationAsync(AcceptInvitationAsyncInput input);
    Task DeleteInvitationAsync(int id);
}
