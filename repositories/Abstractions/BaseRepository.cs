using apbd_project.context;

namespace apbd_project.repositories.Abstractions;

public abstract class BaseRepository : IBaseRepository
{
    protected readonly RevenueRecognitionContext _dbContext;

    protected BaseRepository(RevenueRecognitionContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}