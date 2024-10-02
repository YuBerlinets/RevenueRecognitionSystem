namespace apbd_project.dto.responses;

public class PaymentResponseDTO
{
    public int ContractId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public decimal LeftToPay { get; set; }
}