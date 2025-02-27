using CartonCapsInvitationService.Models;

namespace CartonCapsAPI.Models.Invitations;

public class Invitation
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

    public static Invitation fromServiceModel(CartonCapsInvitation serviceModel)
    {
        return new Invitation
        {
            ID = serviceModel.ID,
            InvitedEmail = serviceModel.InvitedEmail,
            InvitedFirstName = serviceModel.InvitedFirstName,
            InvitedLastName = serviceModel.InvitedLastName,
            SenderAccountID = serviceModel.SenderAccountID,
            SenderReferralCode = serviceModel.SenderReferralCode,
            InvitedAt = serviceModel.InvitedAt,
            AcceptedAt = serviceModel.AcceptedAt,
            AcceptedAccountID = serviceModel.AcceptedAccountID,
            ReferralURL = serviceModel.ReferralURL
        };
    }

    public CartonCapsInvitation toServiceModel()
    {
        return new CartonCapsInvitation
        {
            ID = ID,
            InvitedEmail = InvitedEmail,
            InvitedFirstName = InvitedFirstName,
            InvitedLastName = InvitedLastName,
            SenderAccountID = SenderAccountID,
            SenderReferralCode = SenderReferralCode,
            InvitedAt = InvitedAt,
            AcceptedAt = AcceptedAt,
            AcceptedAccountID = AcceptedAccountID,
            ReferralURL = ReferralURL
        };
    }
}