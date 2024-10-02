namespace apbd_project.dto.requests;

public class NewPaymentDTO
{
    public int ContractId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
}