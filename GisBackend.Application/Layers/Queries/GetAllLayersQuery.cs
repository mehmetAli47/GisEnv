using GisBackend.Application.Common;
using GisBackend.Application.Layers.Interface;
using GisBackend.Domain.Entities;
using MediatR;

namespace GisBackend.Application.Layers.Queries
{
    public record GetAllLayersQuery() : IRequest<Result<IReadOnlyList<Layer>>>;

    public class GetAllLayersQueryHandler : IRequestHandler<GetAllLayersQuery, Result<IReadOnlyList<Layer>>>
    {
        private readonly ILayerRepository _repository;

        public GetAllLayersQueryHandler(ILayerRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<IReadOnlyList<Layer>>> Handle(GetAllLayersQuery request, CancellationToken cancellationToken)
        {
            var layers = await _repository.GetAllAsync();
            return Result<IReadOnlyList<Layer>>.Success(layers);
        }
    }
}
