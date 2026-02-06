using GisBackend.Application.Layers.Interface;
using GisBackend.Domain.Entities;
using GisBackend.Domain.Entities.Enums;
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
               .Where(x => x.IsDeleted == Status.Active)
               .ToListAsync();

            return entities.Adapt<List<Layer>>();
        }

        public async Task<Layer?> GetByIdAsync(Guid id)
        {
            var entity = await _context.Layers
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == Status.Active);

            return entity?.Adapt<Layer>();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.Layers.FindAsync(id);
            if (entity != null)
            {
                entity.IsDeleted = Status.Passive;
                await _context.SaveChangesAsync();
            }
        }
    }
}
