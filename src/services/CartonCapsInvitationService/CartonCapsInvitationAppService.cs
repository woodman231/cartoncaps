using System.Text.RegularExpressions;
using CartonCapsDbContext.Models;
using GetCartonCapsInvitationsRepositoryInput = CartonCapsInvitationRepository.Models.GetCartonCapsInvitationsAsyncInput;
using GetCartonCapsServiceRepositoryInput = CartonCapsInvitationService.Models.GetCartonCapsInvitationsAsyncInput;
using CartonCapsInvitationRepository;
using CartonCapsAccountRepository;
using CartonCapsInvitationService.Models;

namespace CartonCapsInvitationService;

public class CartonCapsInvitationAppService : ICartonCapsInvitationService
{
    private readonly ICartonCapsInvitationRepository _cartonCapsInvitationRepository;
    private readonly ICartonCapsAccountRepository _cartonCapsAccountRepository;

    public CartonCapsInvitationAppService(
        ICartonCapsInvitationRepository cartonCapsInvitationRepository,
        ICartonCapsAccountRepository cartonCapsAccountRepository
    )
    {
        _cartonCapsInvitationRepository = cartonCapsInvitationRepository;
        _cartonCapsAccountRepository = cartonCapsAccountRepository;
    }

    public async Task<CartonCapsInvitation?> AcceptInvitationAsync(AcceptInvitationAsyncInput input)
    {
        // Ensure that the invitation exists
        var invitation = await _cartonCapsInvitationRepository.GetCartonCapsInvitationAsync(input.InvitationID);

        if (invitation == null)
        {
            throw new ArgumentException("InvitationID is not valid");
        }

        // Ensure that the account exists
        var account = await _cartonCapsAccountRepository.GetAccountByIDAsync(input.AcceptedAccountID);

        if (account == null)
        {
            throw new ArgumentException("AcceptedAccountID is not valid");
        }

        // Ensure that the invitation has not already been accepted
        if (invitation.AcceptedAt.HasValue)
        {
            throw new ArgumentException("Invitation has already been accepted");
        }

        // Update the invitation
        invitation.AcceptedAt = DateTime.UtcNow;
        invitation.AcceptedAccountID = input.AcceptedAccountID;

        var updatedDbModel = await _cartonCapsInvitationRepository.UpdateCartonCapsInvitationAsync(invitation);

        if (updatedDbModel == null)
        {
            return null;
        }

        return CartonCapsInvitation.fromDbModel(updatedDbModel);
    }

    public async Task<CartonCapsInvitation> CreateInvitationAsync(CreateInvitationAsyncInput input)
    {
        // Ensure that an email was supplied
        if (string.IsNullOrEmpty(input.InvitedEmail))
        {
            throw new ArgumentException("InvitedEmail is required");
        }

        // Ensure that the email is formatted as an email
        if (!Regex.IsMatch(input.InvitedEmail, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
        {
            throw new ArgumentException("InvitedEmail is not a valid email");
        }

        // Do not allow gmail infinite mailboxes i.e. someone+something@gmail.com
        if (input.InvitedEmail.Contains("+"))
        {
            throw new ArgumentException("InvitedEmail is not a valid email");
        }

        // Ensure that the invited email is not already associated with an account
        var existingAccount = await _cartonCapsAccountRepository.GetAccountByEmailAsync(input.InvitedEmail);

        if (existingAccount != null)
        {
            throw new ArgumentException("InvitedEmail is already associated with an account");
        }

        // Ensure that the invited email is not already associated with an invitation
        GetCartonCapsInvitationsRepositoryInput repositoryInput = new GetCartonCapsInvitationsRepositoryInput
        {
            EmailAddress = input.InvitedEmail
        };

        var existingInvitations = await _cartonCapsInvitationRepository.GetCartonCapsInvitationsAsync(repositoryInput);

        if (existingInvitations.Count() > 0)
        {
            throw new ArgumentException("InvitedEmail is already associated with an invitation");
        }

        // Ensure that the SenderAccountId and ReferralCode match whats in the database
        var senderAccount = await _cartonCapsAccountRepository.GetAccountByIDAsync(input.SenderAccountID);

        if (senderAccount == null)
        {
            throw new ArgumentException("SenderAccountID is not valid");
        }

        if (senderAccount.ReferralCode != input.SenderReferralCode)
        {
            throw new ArgumentException("SenderReferralCode is not valid");
        }

        // Create the invitation
        Invitation invitation = new Invitation
        {
            SenderAccountID = input.SenderAccountID,
            SenderReferralCode = input.SenderReferralCode,
            InvitedFirstName = input.InvitedFirstName,
            InvitedLastName = input.InvitedLastName,
            InvitedEmail = input.InvitedEmail,
            InvitedAt = DateTime.UtcNow
        };

        var dbModel = await _cartonCapsInvitationRepository.CreateCartonCapsInvitationAsync(invitation);
        var serviceModel = CartonCapsInvitation.fromDbModel(dbModel);
        serviceModel.ReferralURL = $"https://cartoncaps.com/register?referralCode={serviceModel.SenderReferralCode}";

        return serviceModel;
    }

    public async Task DeleteInvitationAsync(int id)
    {
        await _cartonCapsInvitationRepository.DeleteCartonCapsInvitationAsync(id);
    }

    public async Task<CartonCapsInvitation?> GetInvitationAsync(int id)
    {
        var dbModel = await _cartonCapsInvitationRepository.GetCartonCapsInvitationAsync(id);

        if (dbModel == null)
        {
            return null;
        }

        return CartonCapsInvitation.fromDbModel(dbModel);
    }

    public async Task<List<CartonCapsInvitation>> GetInvitationsAsync(GetCartonCapsServiceRepositoryInput input)
    {
        GetCartonCapsInvitationsRepositoryInput repositoryInput = new GetCartonCapsInvitationsRepositoryInput
        {
            AccountID = input.AccountID,
            EmailAddress = input.EmailAddress,
            ReferralCode = input.ReferralCode
        };

        var dbModels = await _cartonCapsInvitationRepository.GetCartonCapsInvitationsAsync(repositoryInput);

        return dbModels.Select(CartonCapsInvitation.fromDbModel).ToList();
    }    
}
