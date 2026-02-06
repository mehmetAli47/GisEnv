using GisBackend.Core.Application.Common;
using GisBackend.Core.Application.Common.Interfaces;
using GisBackend.Core.Domain.Entities;
using MediatR;

namespace GisBackend.Features.Layers.GetLayerById
{
    // 1. Sorgu (Message)
    public record GetLayerByIdQuery(Guid Id) : IRequest<Result<Layer>>;

    // 2. İşleyici (Handler)
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
