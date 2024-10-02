using apbd_project.dto.requests;
using apbd_project.dto.responses;
using apbd_project.models;

namespace apbd_project.repositories.Abstractions;

public interface IContractRepository : IBaseRepository
{
    Task<IEnumerable<Contract>> GetAllContractsAsync();
    Task<IEnumerable<Contract>> GetAllSignedContractsAsync();
    Task<IEnumerable<Contract>> GetContractsByClientIdAsync(int clientId);
    Task<IEnumerable<Contract>> GetContractsBySoftwareIdAsync(int softwareId);
    Task<Contract?> GetContractByIdAsync(int id, CancellationToken cancellationToken);

    Task<Contract?> GetContactByClientIdAndSoftwareIdAsync(int clientId, int softwareId,
        CancellationToken cancellationToken);

    Task<Contract> CreateContractAsync(Contract contract, CancellationToken cancellationToken);
    Task DeleteContractAsync(int id, CancellationToken cancellationToken);
    Task SignContract(Contract contract, CancellationToken cancellationToken);
}