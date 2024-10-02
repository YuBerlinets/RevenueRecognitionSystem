using apbd_project.services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace apbd_project.controllers;

[ApiController]
[Route("api/revenues")]
public class RevenueController : ControllerBase
{
    private readonly IRevenueService _revenueService;

    public RevenueController(IRevenueService revenueService)
    {
        _revenueService = revenueService;
    }
    [Authorize]
    [HttpGet("total")]
    public async Task<IActionResult> GetTotalRevenue()
    {
        var revenue = await _revenueService.CalculateRevenue();
        return Ok(revenue);
    }
    [Authorize]
    [HttpGet("predicted")]
    public async Task<IActionResult> GetPredictedRevenue()
    {
        var revenue = await _revenueService.CalculatePredictedRevenue();
        return Ok(revenue);
    }
    [Authorize]
    [HttpGet("client/{clientId:int}")]
    public async Task<IActionResult> GetRevenueForClient(int clientId)
    {
        var revenue = await _revenueService.CalculateRevenueForClient(clientId);
        return Ok(revenue);
    }
    [Authorize]
    [HttpGet("software/{softwareId:int}")]
    public async Task<IActionResult> GetRevenueForSoftware(int softwareId)
    {
        var revenue = await _revenueService.CalculateRevenueForSoftware(softwareId);
        return Ok(revenue);
    }
    [Authorize]
    [HttpGet("currency/{currency}")]
    public async Task<IActionResult> GetRevenueInSpecificCurrency(string currency, CancellationToken cancellationToken)
    {
        var revenue = await _revenueService.CalculateRevenueInSpecificCurrency(currency, cancellationToken);
        return Ok(revenue);
    }
}