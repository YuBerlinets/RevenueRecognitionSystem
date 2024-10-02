using apbd_project.context;
using apbd_project.dto;
using apbd_project.models;
using apbd_project.repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace apbd_project.repositories;

public class ClientRepository : BaseRepository, IClientRepository
{
    public ClientRepository(RevenueRecognitionContext dbContext) : base(dbContext)
    {
    }

    public Task<Client?> GetClientByIdAsync(int id, CancellationToken cancellationToken)
    {
        return _dbContext.Clients.FirstOrDefaultAsync(c => c.ClientId == id, cancellationToken);
    }

    public Task<Individual?> GetClientByPESELAsync(string PESEL, CancellationToken cancellationToken)
    {
        return _dbContext.Individuals.FirstOrDefaultAsync(c => c.PESEL == PESEL, cancellationToken);
    }

    public Task<Company?> GetClientByKRSAsync(string KRS, CancellationToken cancellationToken)
    {
        return _dbContext.Companies.FirstOrDefaultAsync(c => c.KRS == KRS, cancellationToken);
    }

    public async Task CreateIndividualClientAsync(Individual client, CancellationToken cancellationToken)
    {
        await _dbContext.Clients.AddAsync(client, cancellationToken);
    }

    public async Task CreateCompanyClientAsync(Company client, CancellationToken cancellationToken)
    {
        await _dbContext.Clients.AddAsync(client, cancellationToken);
    }


    public async Task UpdateClientAsync(Client client, CancellationToken cancellationToken)
    {
        _dbContext.Entry(client).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteClientAsync(int id, CancellationToken cancellationToken)
    {
        var client = await _dbContext.Clients.FindAsync(id);
        if (client == null)
        {
            throw new Exception("Client not found");
        }

        client.IsDeleted = true;
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}