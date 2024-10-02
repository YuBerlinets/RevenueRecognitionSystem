using apbd_project.dto.requests;
using apbd_project.services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace apbd_project.controllers;

[ApiController]
[Route("api/contracts")]
public class ContractController : ControllerBase
{
    private readonly IContractService _contractService;
    private readonly IPaymentService _paymentService;

    public ContractController(IContractService contractService, IPaymentService paymentService)
    {
        _contractService = contractService;
        _paymentService = paymentService;
    }
    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetContractById(int id, CancellationToken cancellationToken)
    {
        var contract = await _contractService.GetContractByIdAsync(id, cancellationToken);
        if (contract == null)
        {
            return NotFound();
        }

        return Ok(contract);
    }
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateContract([FromBody] NewContractDTO request,
        CancellationToken cancellationToken)
    {
        if (request.EndDate - request.StartDate > TimeSpan.FromDays(30) ||
            request.EndDate - request.StartDate < TimeSpan.FromDays(3))
        {
            return BadRequest("Date range of contract must be between 3 and 30 days");
        }

        try
        {
            var contract = await _contractService.CreateContractAsync(request, cancellationToken);
            return Ok(contract);
        }
        catch (ArgumentException exc)
        {
            return BadRequest(exc.Message);
        }
    }
    [Authorize]
    [HttpPost("pay")]
    public async Task<IActionResult> PayForContract( [FromBody] NewPaymentDTO request,
        CancellationToken cancellationToken)
    {
        try
        {
            var payment = await _paymentService.CreatePaymentAsync(request, cancellationToken);
            return Ok(payment);
        }
        catch (Exception exc)
        {
            return BadRequest(exc.Message);
        }
    }
}