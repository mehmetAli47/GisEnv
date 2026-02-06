using GisBackend.Application.Common;
using GisBackend.Application.Layers.Interface;
using GisBackend.Domain.Entities;
using MediatR;

namespace GisBackend.Application.Layers.Commands
{
    public record CreateLayerCommand(string Name, string GeometryType, string? GeometryWkt) : IRequest<Result<Layer>>;

    public class CreateLayerCommandHandler : IRequestHandler<CreateLayerCommand, Result<Layer>>
    {
        private readonly ILayerRepository _repository;

        public CreateLayerCommandHandler(ILayerRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<Layer>> Handle(CreateLayerCommand request, CancellationToken cancellationToken)
        {
            var layer = new Layer(
                Guid.NewGuid(),
                request.Name,
                request.GeometryType,
                request.GeometryWkt
            );

            await _repository.AddAsync(layer);

            return Result<Layer>.Success(layer);
        }
    }
}
