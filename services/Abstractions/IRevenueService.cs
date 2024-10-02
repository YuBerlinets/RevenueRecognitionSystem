namespace apbd_project.services.Abstractions;

public interface IRevenueService
{
    public Task<double> CalculateRevenue();
    public Task<double> CalculatePredictedRevenue();
    public Task<double> CalculateRevenueForClient(int clientId);
    public Task<double> CalculateRevenueForSoftware(int softwareId);
    public Task<double> CalculateRevenueInSpecificCurrency(string currency, CancellationToken cancellationToken);
 
}