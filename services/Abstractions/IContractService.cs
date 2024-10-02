using apbd_project.dto.requests;
using apbd_project.dto.responses;
using apbd_project.models;

namespace apbd_project.services.Abstractions;

public interface IContractService
{
    public Task<ContractResponseDTO?> GetContractByIdAsync(int id, CancellationToken cancellationToken);
    public Task<ContractResponseDTO> CreateContractAsync(NewContractDTO request, CancellationToken cancellationToken);
    public Task DeleteContractAsync(int id, CancellationToken cancellationToken);
}