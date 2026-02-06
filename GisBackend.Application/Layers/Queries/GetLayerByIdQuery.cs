using GisBackend.Application.Common;
using GisBackend.Application.Layers.Interface;
using GisBackend.Domain.Entities;
using MediatR;

namespace GisBackend.Application.Layers.Queries
{
    public record GetLayerByIdQuery(Guid Id) : IRequest<Result<Layer>>;

    public class GetLayerByIdQueryHandler : IRequestHandler<GetLayerByIdQuery, Result<Layer>>
    {
        private readonly ILayerRepository _repository;

        public GetLayerByIdQueryHandler(ILayerRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<Layer>> Handle(GetLayerByIdQuery request, CancellationToken cancellationToken)
        {
            var layer = await _repository.GetByIdAsync(request.Id);
            
            if (layer == null)
                return Result<Layer>.Failure("Katman bulunamadı.");

            return Result<Layer>.Success(layer);
        }
    }
}
