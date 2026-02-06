using GisBackend.Domain.Entities;

namespace GisBackend.Application.Layers.Interface
{
    public interface ILayerRepository
    {
        Task<IReadOnlyList<Layer>> GetAllAsync();
        Task<Layer?> GetByIdAsync(Guid id);
        Task AddAsync(Layer layer);
        Task DeleteAsync(Guid id);
    }
}
