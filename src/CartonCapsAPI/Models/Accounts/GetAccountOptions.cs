using CartonCapsAccountService.Models;

namespace CartonCapsAPI.Models.Accounts;

public class GetAccountsOptions
{
    public string? EmailAddress { get; set; } = string.Empty;
    public string? ReferralCode { get; set; } = string.Empty;

    public GetAccountsAsyncInput ToServiceModel()
    {
        return new GetAccountsAsyncInput
        {
            EmailAddress = EmailAddress,
            ReferralCode = ReferralCode
        };
    }
}