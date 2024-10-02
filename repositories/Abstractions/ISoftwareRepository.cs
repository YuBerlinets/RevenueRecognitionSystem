using apbd_project.models;

namespace apbd_project.repositories.Abstractions;

public interface ISoftwareRepository : IBaseRepository
{
    Task<Software?> GetSoftwareByIdAsync(int id, CancellationToken cancellationToken);
}