namespace CartonCapsInvitationService.Models;

public class CreateInvitationAsyncInput {
    public int SenderAccountID { get; set; }
    public string? SenderReferralCode { get; set; }
    public string? InvitedFirstName { get; set; }
    public string? InvitedLastName { get; set; }
    public string? InvitedEmail { get; set; }
}
