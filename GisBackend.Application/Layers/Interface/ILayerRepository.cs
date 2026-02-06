using GisBackend.Domain.Entities;

namespace GisBackend.Application.Layers.Interface
{
    public interface ILayerRepository
    {
        Task<IReadOnlyList<Layer>> GetAllAsync();
        Task AddAsync(Layer layer);
    }
}
