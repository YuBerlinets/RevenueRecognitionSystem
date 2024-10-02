using apbd_project.context;
using apbd_project.models;
using apbd_project.repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace apbd_project.repositories;

public class PaymentRepository : BaseRepository, IPaymentRepository
{
    public PaymentRepository(RevenueRecognitionContext dbContext) : base(dbContext)
    {
    }

    public async Task<Payment?> GetPaymentByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _dbContext.Payments.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<Payment> CreatePaymentAsync(Payment payment, CancellationToken cancellationToken)
    {
        var response = await _dbContext.Payments.AddAsync(payment, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return response.Entity;
    }

    public Task<decimal> GetTotalPaymentsForContractAsync(int id, CancellationToken cancellationToken)
    {
        return _dbContext.Payments
            .Where(p => p.ContractId == id)
            .SumAsync(p => p.Amount, cancellationToken);
    }
}