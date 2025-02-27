using CartonCapsAccountService.Models;

namespace CartonCapsAPI.Models.Accounts;

public class Account
{
    public int ID { get; set; }
    public string? Email { get; set; }
    public string? ReferralCode { get; set; }

    public static Account fromServiceModel(CartonCapsAccount serviceModel)
    {
        return new Account
        {
            ID = serviceModel.ID,
            Email = serviceModel.Email,
            ReferralCode = serviceModel.ReferralCode
        };
    }
}