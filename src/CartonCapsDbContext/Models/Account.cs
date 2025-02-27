using System.ComponentModel.DataAnnotations.Schema;

namespace CartonCapsDbContext.Models
{
    [Table("Accounts")]
    public class AccountEntity
    {
        public int ID { get; set; }        
        public string? Email { get; set; }
        public string? ReferralCode { get; set; }

        public List<InvitationEntity>? InvitationsSent { get; set; }
        public List<InvitationEntity>? InvitationsAccepted { get; set; }
    }
}