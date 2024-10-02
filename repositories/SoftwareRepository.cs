using apbd_project.context;
using apbd_project.models;
using apbd_project.repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace apbd_project.repositories;

public class SoftwareRepository : BaseRepository, ISoftwareRepository
{
    public SoftwareRepository(RevenueRecognitionContext dbContext) : base(dbContext)
    {
    }

    public Task<Software?> GetSoftwareByIdAsync(int id, CancellationToken cancellationToken)
    {
        return _dbContext.Softwares.FirstOrDefaultAsync(s => s.SoftwareId == id);
    }
}