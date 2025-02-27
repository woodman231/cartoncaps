using CartonCapsDbContext.Models;

namespace CartonCapsAccountService.Models;

public class CartonCapsAccount
{
    public int ID { get; set; }
    public string? Email { get; set; }
    public string? ReferralCode { get; set; }

    public static CartonCapsAccount fromEntityModel(AccountEntity dbModel)
    {
        return new CartonCapsAccount
        {
            ID = dbModel.ID,
            Email = dbModel.Email,
            ReferralCode = dbModel.ReferralCode
        };
    }
}
