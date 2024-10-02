using apbd_project.context;
using apbd_project.dto;
using apbd_project.models;
using apbd_project.services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apbd_project.controllers;

[ApiController]
[Route("api/clients")]
public class ClientController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [Authorize]
    [HttpPost("individual")]
    public async Task<IActionResult> CreateIndividualClient([FromBody] NewIndividualClientDTO request,
        CancellationToken cancellationToken)
    {
        var clientPESEL = await _clientService.GetClientByPESELAsync(request.PESEL, cancellationToken);
        if (clientPESEL != null)
        {
            return BadRequest("Client with this PESEL already exists");
        }

        var client = await _clientService.CreateIndividualClientAsync(request, cancellationToken);
        return Ok(client);
    }

    [Authorize]
    [HttpPost("company")]
    public async Task<IActionResult> CreateCompanyClient([FromBody] NewCompanyClientDTO request,
        CancellationToken cancellationToken)
    {
        var clientKRS = await _clientService.GetClientByKRSAsync(request.KRS, cancellationToken);
        if (clientKRS != null)
        {
            return BadRequest("Client with this KRS already exists");
        }

        var client = await _clientService.CreateCompanyClientAsync(request, cancellationToken);

        return Ok(client);
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetClientById(int id, CancellationToken cancellationToken)
    {
        var client = await _clientService.GetClientByIdAsync(id, cancellationToken);
        if (client == null)
        {
            return NotFound();
        }

        return Ok(client);
    }

    [Authorize]
    [HttpPut]
    public async Task<IActionResult> UpdateClient([FromBody] Client client, CancellationToken cancellationToken)
    {
        var clientToUpdate = await _clientService.GetClientByIdAsync(client.ClientId, cancellationToken);
        if (clientToUpdate == null)
        {
            return NotFound();
        }

        await _clientService.UpdateClientAsync(clientToUpdate, client, cancellationToken);
        return Ok(clientToUpdate.GetType());
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteClient(int id, CancellationToken cancellationToken)
    {
        try
        {
            await _clientService.DeleteClientAsync(id, cancellationToken);
        }
        catch (Exception e)
        {
            return NotFound("Client not found");
        }

        return NoContent();
    }
}