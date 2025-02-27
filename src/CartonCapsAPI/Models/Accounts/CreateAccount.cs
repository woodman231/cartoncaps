using CartonCapsAccountService.Models;

namespace CartonCapsAPI.Models.Accounts;

public class CreateAccount
{
    public string? EmailAddress { get; set; } = string.Empty;

    public CreateAccountAsyncInput toServiceModel()
    {
        return new CreateAccountAsyncInput
        {
            Email = EmailAddress
        };
    }
}
