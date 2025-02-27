namespace CartonCapsDbContext.Models
{
    public class Account
    {
        public int ID { get; set; }        
        public string? Email { get; set; }
        public string? ReferralCode { get; set; }

        public List<Invitation>? InvitationsSent { get; set; }
        public List<Invitation>? InvitationsAccepted { get; set; }
    }
}