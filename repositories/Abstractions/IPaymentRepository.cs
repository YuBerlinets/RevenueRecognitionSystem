using apbd_project.models;

namespace apbd_project.repositories.Abstractions;

public interface IPaymentRepository : IBaseRepository
{
    public Task<Payment?> GetPaymentByIdAsync(int id, CancellationToken cancellationToken);
    public Task<Payment> CreatePaymentAsync(Payment payment, CancellationToken cancellationToken);

    Task<decimal> GetTotalPaymentsForContractAsync(int id, CancellationToken cancellationToken);
}