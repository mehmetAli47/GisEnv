using GisBackend.Core.Domain.Entities;

namespace GisBackend.Core.Application.Common.Interfaces
{
    public interface ILayerRepository
    {
        Task<IReadOnlyList<Layer>> GetAllAsync();
        Task<Layer?> GetByIdAsync(Guid id);
        Task AddAsync(Layer layer);
        Task DeleteAsync(Guid id);
    }
}
