using System.Net;
using apbd_project.dto.responses;
using apbd_project.repositories.Abstractions;
using apbd_project.services.Abstractions;
using Newtonsoft.Json;

namespace apbd_project.services;

public class RevenueService : IRevenueService
{
    private readonly IContractRepository _contractRepository;

    private readonly HttpClient _httpClient;


    public RevenueService(IContractRepository contractRepository, HttpClient httpClient)
    {
        _contractRepository = contractRepository;
        _httpClient = httpClient;
    }

    public async Task<double> CalculateRevenue()
    {
        var contracts = await _contractRepository.GetAllSignedContractsAsync();
        return (double)contracts.Sum(c => c.Price);
    }

    public async Task<double> CalculatePredictedRevenue()
    {
        var contracts = await _contractRepository.GetAllContractsAsync();
        return (double)contracts.Sum(c => c.Price);
    }

    public async Task<double> CalculateRevenueForClient(int clientId)
    {
        var contracts = await _contractRepository.GetContractsByClientIdAsync(clientId);
        return (double)contracts.Sum(c => c.Price);
    }

    public async Task<double> CalculateRevenueForSoftware(int softwareId)
    {
        var contracts = await _contractRepository.GetContractsBySoftwareIdAsync(softwareId);
        return (double)contracts.Sum(c => c.Price);
    }

    public async Task<double> CalculateRevenueInSpecificCurrency(string currency, CancellationToken cancellationToken)
    {
        var contracts = await _contractRepository.GetAllSignedContractsAsync();
        var totalRevenue = contracts.Sum(c => c.Price);

        var exchangeRate = await GetExchangeRateAsync(currency);
        return (double)totalRevenue / exchangeRate;
    }

    private async Task<double> GetExchangeRateAsync(string currency)
    {
        var response =
            await _httpClient.GetAsync($"https://api.nbp.pl/api/exchangerates/rates/A/{currency}/?format=json");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var exchangeRateResponse = JsonConvert.DeserializeObject<ExchangeRateResponse>(content);

        return exchangeRateResponse.Rates.First().Mid;
    }
}