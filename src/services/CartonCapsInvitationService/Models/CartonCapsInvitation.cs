using CartonCapsDbContext.Models;

namespace CartonCapsInvitationService.Models;

public class CartonCapsInvitation
{
    public int ID { get; set; }
    public int SenderAccountID { get; set; }
    public string? SenderReferralCode { get; set; }
    public DateTime? InvitedAt { get; set; }
    public string? InvitedFirstName { get; set; }
    public string? InvitedLastName { get; set; }
    public string? InvitedEmail { get; set; }
    public DateTime? AcceptedAt { get; set; }
    public int? AcceptedAccountID { get; set; }
    public string? ReferralURL { get; set; }

    public static CartonCapsInvitation fromEntityModel(InvitationEntity invitation)
    {
        return new CartonCapsInvitation
        {
            ID = invitation.ID,
            SenderAccountID = invitation.SenderAccountID,
            SenderReferralCode = invitation.SenderReferralCode,
            InvitedAt = invitation.InvitedAt,
            InvitedFirstName = invitation.InvitedFirstName,
            InvitedLastName = invitation.InvitedLastName,
            InvitedEmail = invitation.InvitedEmail,
            AcceptedAt = invitation.AcceptedAt,
            AcceptedAccountID = invitation.AcceptedAccountID,            
        };
    }
}