using Microsoft.AspNetCore.Mvc;
using CartonCapsAPI.Models.Accounts;
using CartonCapsAccountService;
using Swashbuckle.AspNetCore.Annotations;

namespace CartonCapsAPI.Controllers;

[ApiController]
public class AccountsController : ControllerBase
{
    private readonly ICartonCapsAccountService _accountService;

    public AccountsController(ICartonCapsAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost]
    [Route("/accounts")]
    [SwaggerOperation("CreateAccount")]
    [SwaggerResponse(201, type: typeof(Account))]
    [SwaggerResponse(400)]    
    public virtual async Task<IActionResult> CreateAccountAction([FromBody] CreateAccount account)
    {
        try
        {
            var serviceModel = account.toServiceModel();
            var createdAccount = await _accountService.CreateAccountAsync(serviceModel);
            if (createdAccount == null)
            {
                throw new Exception("Failed to create account");
            }
            return CreatedAtAction(nameof(CreateAccountAction), createdAccount);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Route("/accounts/{accountID}")]
    [SwaggerOperation("GetAccount")]
    [SwaggerResponse(statusCode: 200, type: typeof(Account), description: "Account found")]
    [SwaggerResponse(statusCode: 404, description: "Account not found")]
    public virtual async Task<IActionResult> GetAccountAction([FromRoute] int accountID)
    {
        var account = await _accountService.GetAccountByIDAsync(accountID);
        if (account == null)
        {
            return NotFound();
        }
        return Ok(account);
    }

    [HttpGet]
    [Route("/accounts")]
    [SwaggerOperation("GetAccounts")]
    [SwaggerResponse(statusCode: 200, type: typeof(List<Account>), description: "Account found")]    
    public virtual async Task<IActionResult> GetAccountsAction([FromQuery] GetAccountsOptions options)
    {
        var serviceModel = options.ToServiceModel();
        var accounts = await _accountService.GetAccountsAsync(serviceModel);
        return Ok(accounts);
    }

    [HttpDelete]
    [Route("/accounts/{accountID}")]
    [SwaggerOperation("DeleteAccount")]
    [SwaggerResponse(statusCode: 204, description: "Account deleted")]
    [SwaggerResponse(statusCode: 404, description: "Account not found")]
    public virtual async Task<IActionResult> DeleteAccountAction([FromRoute] int accountID)
    {
        var account = await _accountService.GetAccountByIDAsync(accountID);
        if (account == null)
        {
            return NotFound();
        }
        await _accountService.DeleteAccountAsync(accountID);
        return NoContent();
    }    
}
