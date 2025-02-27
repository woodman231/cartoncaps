namespace CartonCapsInvitationService.Models;

public class GetCartonCapsInvitationsAsyncInput
{
        public int? AccountID { get; set; }
        public string? EmailAddress { get; set; }
        public string? ReferralCode { get; set; }    
}