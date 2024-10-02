namespace apbd_project.repositories.Abstractions;

public interface IBaseRepository
{
    Task SaveChangesAsync(CancellationToken cancellationToken);
}