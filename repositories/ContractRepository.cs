using apbd_project.context;
using apbd_project.dto.requests;
using apbd_project.dto.responses;
using apbd_project.models;
using apbd_project.repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace apbd_project.repositories;

public class ContractRepository : BaseRepository, IContractRepository
{
    public ContractRepository(RevenueRecognitionContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<Contract>> GetAllContractsAsync()
    {
        return _dbContext.Contracts.AsEnumerable();
    }

    public async Task<IEnumerable<Contract>> GetAllSignedContractsAsync()
    {
        return _dbContext.Contracts.Where(c => c.IsSigned).AsEnumerable();
    }

    public async Task<IEnumerable<Contract>> GetContractsByClientIdAsync(int clientId)
    {
        return _dbContext.Contracts.Where(c => c.ClientId == clientId).AsEnumerable();
    }

    public async Task<IEnumerable<Contract>> GetContractsBySoftwareIdAsync(int softwareId)
    {
        return _dbContext.Contracts.Where(c => c.SoftwareId == softwareId).AsEnumerable();
    }

    public async Task<Contract?> GetContractByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _dbContext.Contracts.FirstOrDefaultAsync(c => c.ContractId == id, cancellationToken);
    }

    public async Task<Contract?> GetContactByClientIdAndSoftwareIdAsync(int clientId, int softwareId,
        CancellationToken cancellationToken)
    {
        return await _dbContext.Contracts.FirstOrDefaultAsync(c => c.ClientId == clientId && c.SoftwareId == softwareId,
            cancellationToken);
    }

    public async Task<Contract> CreateContractAsync(Contract contract, CancellationToken cancellationToken)
    {
        var response = await _dbContext.Contracts.AddAsync(contract, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return response.Entity;
    }

    public Task DeleteContractAsync(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task SignContract(Contract contract, CancellationToken cancellationToken)
    {
        _dbContext.Contracts.Update(contract);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}