using apbd_project.dto;
using apbd_project.dto.responses;
using apbd_project.models;
using apbd_project.repositories.Abstractions;
using apbd_project.services.Abstractions;

namespace apbd_project.services;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;

    public ClientService(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public Task<Client?> GetClientByIdAsync(int id, CancellationToken cancellationToken)
    {
        return _clientRepository.GetClientByIdAsync(id, cancellationToken);
    }

    public async Task<IndividualResponse> CreateIndividualClientAsync(NewIndividualClientDTO client,
        CancellationToken cancellationToken)
    {
        var newClient = new Individual
        {
            FirstName = client.FirstName,
            Email = client.Email,
            LastName = client.LastName,
            PESEL = client.PESEL,
            Address = client.Address,
            PhoneNumber = client.PhoneNumber
        };

        await _clientRepository.CreateIndividualClientAsync(newClient, cancellationToken);
        await _clientRepository.SaveChangesAsync(cancellationToken);

        var clientResponse = new IndividualResponse
        {
            Address = newClient.Address,
            Email = newClient.Email,
            FirstName = newClient.FirstName,
            LastName = newClient.LastName,
            PESEL = newClient.PESEL,
            PhoneNumber = newClient.PhoneNumber
        };

        return clientResponse;
    }

    public async Task<CompanyResponse> CreateCompanyClientAsync(NewCompanyClientDTO client,
        CancellationToken cancellationToken)
    {
        var newClient = new Company
        {
            CompanyName = client.CompanyName,
            KRS = client.KRS,
            Address = client.Address,
            Email = client.Email,
            PhoneNumber = client.PhoneNumber,
        };

        await _clientRepository.CreateCompanyClientAsync(newClient, cancellationToken);
        await _clientRepository.SaveChangesAsync(cancellationToken);

        var clientResponse = new CompanyResponse
        {
            Address = newClient.Address,
            CompanyName = newClient.CompanyName,
            Email = newClient.Email,
            KRS = newClient.KRS,
            PhoneNumber = newClient.PhoneNumber
        };

        return clientResponse;
    }

    public async Task UpdateClientAsync(Client existingClient, Client updatedClient, CancellationToken cancellationToken)
    {
        existingClient.Address = updatedClient.Address;
        existingClient.Email = updatedClient.Email;
        existingClient.PhoneNumber = updatedClient.PhoneNumber;

        if (existingClient is Individual existingIndividual && updatedClient is Individual updatedIndividual)
        {
            existingIndividual.FirstName = updatedIndividual.FirstName;
            existingIndividual.LastName = updatedIndividual.LastName;
        }
        else if (existingClient is Company existingCompany && updatedClient is Company updatedCompany)
        {
            existingCompany.CompanyName = updatedCompany.CompanyName;
        }

        await _clientRepository.UpdateClientAsync(existingClient, cancellationToken);
    }


    public Task DeleteClientAsync(int id, CancellationToken cancellationToken)
    {
        return _clientRepository.DeleteClientAsync(id, cancellationToken);
    }

    public Task<Individual?> GetClientByPESELAsync(string PESEL, CancellationToken cancellationToken)
    {
        return _clientRepository.GetClientByPESELAsync(PESEL, cancellationToken);
    }

    public Task<Company?> GetClientByKRSAsync(string KRS, CancellationToken cancellationToken)
    {
        return _clientRepository.GetClientByKRSAsync(KRS, cancellationToken);
    }
}