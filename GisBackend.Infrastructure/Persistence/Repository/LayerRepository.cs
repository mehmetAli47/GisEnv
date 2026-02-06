using GisBackend.Application.Layers.Interface;
using GisBackend.Domain.Entities;
using GisBackend.Infrastructure.Persistence.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace GisBackend.Infrastructure.Persistence.Repository
{
    public class LayerRepository : ILayerRepository
    {
        private readonly AppDbContext _context;

        public LayerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Layer layer)
        {
            var entity = layer.Adapt<LayerEntity>();

            await _context.Layers.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<Layer>> GetAllAsync()
        {
            var entities = await _context.Layers
               .AsNoTracking()
               .Where(x => x.IsDeleted == Domain.Entities.Enums.Status.Active)
               .ToListAsync();

            return entities.Adapt<List<Layer>>();
        }
    }
}
