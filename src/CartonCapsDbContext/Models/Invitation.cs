using System.ComponentModel.DataAnnotations.Schema;

namespace CartonCapsDbContext.Models
{
    [Table("Invitations")]
    public class InvitationEntity
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
        public AccountEntity? SourceAccount { get; set; }

        [ForeignKey("AcceptedAccountID")]
        public AccountEntity? AcceptedAccount { get; set; }
    }
}