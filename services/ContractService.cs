using apbd_project.dto.requests;
using apbd_project.dto.responses;
using apbd_project.models;
using apbd_project.repositories.Abstractions;
using apbd_project.services.Abstractions;

namespace apbd_project.services;

public class ContractService : IContractService
{
    private readonly IContractRepository _contractRepository;
    private readonly ISoftwareRepository _softwareRepository;
    private readonly IClientRepository _clientRepository;

    public ContractService(IContractRepository contractRepository, ISoftwareRepository softwareRepository,
        IClientRepository clientRepository)
    {
        _contractRepository = contractRepository;
        _softwareRepository = softwareRepository;
        _clientRepository = clientRepository;
    }

    public async Task<ContractResponseDTO?> GetContractByIdAsync(int id, CancellationToken cancellationToken)
    {
        var contract = await _contractRepository.GetContractByIdAsync(id, cancellationToken);
        if (contract == null)
        {
            return null;
        }

        var client = await _clientRepository.GetClientByIdAsync(contract.ClientId, cancellationToken);

        var response = new ContractResponseDTO
        {
            ClientId = contract.ClientId,
            SoftwareId = contract.SoftwareId,
            StartDate = contract.StartDate,
            EndDate = contract.EndDate,
            Price = contract.Price,
            IsSigned = contract.IsSigned,
            Client = client
        };
        return response;
    }


    public Task DeleteContractAsync(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<ContractResponseDTO> CreateContractAsync(NewContractDTO request,
        CancellationToken cancellationToken)
    {
        var software = await _softwareRepository.GetSoftwareByIdAsync(request.SoftwareId, cancellationToken);
        if (software == null)
        {
            throw new ArgumentException("Software with given id does not exist");
        }

        var client = await _clientRepository.GetClientByIdAsync(request.ClientId, cancellationToken);

        if (client == null)
        {
            throw new ArgumentException("Client with given id does not exist");
        }

        var contractExists = await _contractRepository.GetContactByClientIdAndSoftwareIdAsync(request.ClientId,
            request.SoftwareId, cancellationToken);

        if (contractExists != null)
        {
            throw new ArgumentException("Contract already exists");
        }

        var contract = new Contract
        {
            ClientId = request.ClientId,
            SoftwareId = request.SoftwareId,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Price = request.Price,
            IsSigned = request.IsSigned
        };

        var createContract = await _contractRepository.CreateContractAsync(contract, cancellationToken);
        var response = new ContractResponseDTO
        {
            ClientId = createContract.ClientId,
            SoftwareId = createContract.SoftwareId,
            StartDate = createContract.StartDate,
            EndDate = createContract.EndDate,
            Price = createContract.Price,
            IsSigned = createContract.IsSigned,
            Client = client,
        };

        return response;
    }
}