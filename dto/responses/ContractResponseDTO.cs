using apbd_project.models;

namespace apbd_project.dto.responses;

public class ContractResponseDTO
{
    public int ClientId { get; set; }
    public int SoftwareId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public decimal Price { get; set; }
    public bool IsSigned { get; set; }
    public object? Client { get; set; }
}