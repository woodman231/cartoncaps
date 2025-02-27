using CartonCapsInvitationService.Models;

namespace CartonCapsAPI.Models.Invitations;

public class GetinvitationsOptions
{
    public int? AccountID { get; set; }
    public string? EmailAddress { get; set; }
    public string? ReferralCode { get; set; }

    public GetCartonCapsInvitationsAsyncInput toServiceModel()
    {
        return new GetCartonCapsInvitationsAsyncInput
        {
            AccountID = AccountID,
            EmailAddress = EmailAddress,
            ReferralCode = ReferralCode
        };
    }
}