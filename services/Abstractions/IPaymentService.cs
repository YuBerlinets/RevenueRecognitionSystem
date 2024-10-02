using apbd_project.dto.requests;
using apbd_project.dto.responses;
using apbd_project.models;

namespace apbd_project.services.Abstractions;

public interface IPaymentService
{
    public Task<Payment?> GetPaymentByIdAsync(int id, CancellationToken cancellationToken);
    public Task<PaymentResponseDTO> CreatePaymentAsync(NewPaymentDTO request, CancellationToken cancellationToken);
}