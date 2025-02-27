using Microsoft.AspNetCore.Mvc;
using CartonCapsAPI.Models.Invitations;
using CartonCapsInvitationService;
using Swashbuckle.AspNetCore.Annotations;
using CartonCapsInvitationService.Models;

namespace CartonCapsAPI.Controllers;

[ApiController]
public class InvitationsController : ControllerBase
{
    private readonly ICartonCapsInvitationService _invitationService;

    public InvitationsController(ICartonCapsInvitationService invitationService)
    {
        _invitationService = invitationService;
    }

    [HttpPost]
    [Route("/invitations")]
    [SwaggerOperation("CreateInvitation")]
    [SwaggerResponse(201, type: typeof(Invitation))]
    [SwaggerResponse(400)]
    public virtual async Task<IActionResult> CreateInvitationAction([FromBody] CreateInvitation invitation)
    {
        try
        {
            var serviceModel = invitation.toServiceModel();
            var createdInvitation = await _invitationService.CreateInvitationAsync(serviceModel);
            if (createdInvitation == null)
            {
                throw new Exception("Failed to create invitation");
            }
            return CreatedAtAction(nameof(CreateInvitationAction), createdInvitation);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Route("/invitations/{invitationID}")]
    [SwaggerOperation("GetInvitation")]
    [SwaggerResponse(statusCode: 200, type: typeof(Invitation), description: "Invitation found")]
    [SwaggerResponse(statusCode: 404, description: "Invitation not found")]
    public virtual async Task<IActionResult> GetInvitationAction([FromRoute] int invitationID)
    {
        var invitation = await _invitationService.GetInvitationAsync(invitationID);
        if (invitation == null)
        {
            return NotFound();
        }
        return Ok(invitation);
    }

    [HttpGet]
    [Route("/invitations")]
    [SwaggerOperation("GetInvitations")]
    [SwaggerResponse(statusCode: 200, type: typeof(List<Invitation>), description: "Invitations found")]
    [SwaggerResponse(statusCode: 404, description: "Invitations not found")]
    public virtual async Task<IActionResult> GetInvitationsAction([FromQuery] GetinvitationsOptions input)
    {
        var serviceInput = input.toServiceModel();
        var invitations = await _invitationService.GetInvitationsAsync(serviceInput);
        if (invitations == null)
        {
            return NotFound();
        }
        return Ok(invitations);
    }    

    [HttpPost]
    [Route("/invitations/{invitationID}/accept")]
    [SwaggerOperation("AcceptInvitation")]
    [SwaggerResponse(statusCode: 200, type: typeof(Invitation), description: "Invitation accepted")]
    [SwaggerResponse(statusCode: 404, description: "Invitation not found")]
    public virtual async Task<IActionResult> AcceptInvitationAction([FromRoute] int invitationID, [FromQuery] int acceptedAccountID)
    {        
        AcceptInvitationAsyncInput serviceInput = new AcceptInvitationAsyncInput
        {
            InvitationID = invitationID,
            AcceptedAccountID = acceptedAccountID
        };

        var acceptedInvitation = await _invitationService.AcceptInvitationAsync(serviceInput);

        if (acceptedInvitation == null)
        {
            return NotFound();
        }
        return Ok(Invitation.fromServiceModel(acceptedInvitation));
    }

    [HttpDelete]
    [Route("/invitations/{invitationID}")]
    [SwaggerOperation("DeleteInvitation")]
    [SwaggerResponse(statusCode: 204, description: "Invitation deleted")]
    [SwaggerResponse(statusCode: 404, description: "Invitation not found")]
    public virtual async Task<IActionResult> DeleteInvitationAction([FromRoute] int invitationID)
    {
        var invitation = await _invitationService.GetInvitationAsync(invitationID);
        if (invitation == null)
        {
            return NotFound();
        }
        await _invitationService.DeleteInvitationAsync(invitationID);
        return NoContent();
    }
}