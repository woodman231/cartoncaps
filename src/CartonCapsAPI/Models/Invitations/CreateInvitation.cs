using CartonCapsInvitationService.Models;

namespace CartonCapsAPI.Models.Invitations;

public class CreateInvitation
{
    public int SenderAccountID { get; set; }
    public string? SenderReferralCode { get; set; }    
    public string? InvitedFirstName { get; set; }
    public string? InvitedLastName { get; set; }
    public string? InvitedEmail { get; set; }

    public CreateInvitationAsyncInput toServiceModel()
    {
        return new CreateInvitationAsyncInput
        {
            SenderAccountID = SenderAccountID,
            SenderReferralCode = SenderReferralCode,            
            InvitedFirstName = InvitedFirstName,
            InvitedLastName = InvitedLastName,
            InvitedEmail = InvitedEmail
        };
    }
}