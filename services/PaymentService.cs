using apbd_project.dto.requests;
using apbd_project.dto.responses;
using apbd_project.models;
using apbd_project.repositories.Abstractions;
using apbd_project.services.Abstractions;

namespace apbd_project.services;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IContractRepository _contractRepository;

    public PaymentService(IPaymentRepository paymentRepository, IContractRepository contractRepository)
    {
        _paymentRepository = paymentRepository;
        _contractRepository = contractRepository;
    }

    public Task<Payment?> GetPaymentByIdAsync(int id, CancellationToken cancellationToken)
    {
        return _paymentRepository.GetPaymentByIdAsync(id, cancellationToken);
    }

    public async Task<PaymentResponseDTO> CreatePaymentAsync(NewPaymentDTO request, CancellationToken cancellationToken)
    {
        var contract = await _contractRepository.GetContractByIdAsync(request.ContractId, cancellationToken);
        if (contract == null)
            throw new Exception("Contract not found");

        if (contract.IsSigned)
            throw new Exception("Contract is already signed");

        if (request.PaymentDate > contract.EndDate)
            throw new Exception("Payment is past due date and cannot be accepted");

        var totalPayments =
            await _paymentRepository.GetTotalPaymentsForContractAsync(contract.ContractId, cancellationToken);
        if (totalPayments + request.Amount > contract.Price)
            throw new Exception("Total payment amount exceeds contract price");

        var payment = new Payment
        {
            PaymentDate = request.PaymentDate,
            Amount = request.Amount,
            ContractId = request.ContractId
        };

        var createdPayment = await _paymentRepository.CreatePaymentAsync(payment, cancellationToken);

        totalPayments += request.Amount;
        if (totalPayments >= contract.Price)
        {
            contract.IsSigned = true;
            await _contractRepository.SignContract(contract, cancellationToken);
        }

        var response = new PaymentResponseDTO()
        {
            ContractId = createdPayment.Id,
            PaymentDate = createdPayment.PaymentDate,
            Amount = createdPayment.Amount,
            LeftToPay = contract.Price - totalPayments
        };

        return response;
    }
}