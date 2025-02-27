using System.ComponentModel.DataAnnotations.Schema;

namespace CartonCapsDbContext.Models
{
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

        [ForeignKey("SenderAccountID")]
        public Account? SourceAccount { get; set; }

        [ForeignKey("AcceptedAccountID")]
        public Account? AcceptedAccount { get; set; }
    }
}