using apbd_project.dto;
using apbd_project.dto.responses;
using apbd_project.models;

namespace apbd_project.services.Abstractions;

public interface IClientService
{
    public Task<Client?> GetClientByIdAsync(int id, CancellationToken cancellationToken);

    public Task<IndividualResponse> CreateIndividualClientAsync(NewIndividualClientDTO client,
        CancellationToken cancellationToken);

    public Task<CompanyResponse> CreateCompanyClientAsync(NewCompanyClientDTO client, CancellationToken cancellationToken);
    public Task UpdateClientAsync(Client clientToUpdate,Client client, CancellationToken cancellationToken);
    public Task DeleteClientAsync(int id, CancellationToken cancellationToken);
    public Task<Individual?> GetClientByPESELAsync(string PESEL, CancellationToken cancellationToken);
    public Task<Company?> GetClientByKRSAsync(string KRS, CancellationToken cancellationToken);
    
}