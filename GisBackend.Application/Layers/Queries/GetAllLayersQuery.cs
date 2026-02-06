using GisBackend.Application.Layers.Interface;
using GisBackend.Domain.Entities;
using MediatR;

namespace GisBackend.Application.Layers.Queries
{
    public record GetAllLayersQuery() : IRequest<IReadOnlyList<Layer>>;

    public class GetAllLayersQueryHandler : IRequestHandler<GetAllLayersQuery, IReadOnlyList<Layer>>
    {
        private readonly ILayerRepository _repository;

        public GetAllLayersQueryHandler(ILayerRepository repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyList<Layer>> Handle(GetAllLayersQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync();
        }
    }
}
