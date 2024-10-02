using apbd_project.dto;
using apbd_project.models;

namespace apbd_project.repositories.Abstractions;

public interface IClientRepository : IBaseRepository
{
    public Task<Client?> GetClientByIdAsync(int id, CancellationToken cancellationToken);
    public Task<Individual?> GetClientByPESELAsync(string PESEL, CancellationToken cancellationToken);
    public Task<Company?> GetClientByKRSAsync(string KRS, CancellationToken cancellationToken);

    public Task CreateIndividualClientAsync(Individual client,
        CancellationToken cancellationToken);

    public Task CreateCompanyClientAsync(Company client, CancellationToken cancellationToken);
    public Task UpdateClientAsync(Client client, CancellationToken cancellationToken);
    public Task DeleteClientAsync(int id, CancellationToken cancellationToken);
}