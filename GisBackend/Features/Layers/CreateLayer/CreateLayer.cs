using FluentValidation;
using GisBackend.Core.Application.Common;
using GisBackend.Core.Application.Common.Interfaces;
using GisBackend.Core.Domain.Entities;
using MediatR;

namespace GisBackend.Features.Layers.CreateLayer
{
    // 1. Komut (Message)
    public record CreateLayerCommand(string Name, string GeometryType, string? GeometryWkt) : IRequest<Result<Layer>>;

    // 2. Bekçi (Validator)
    public class CreateLayerCommandValidator : AbstractValidator<CreateLayerCommand>
    {
        public CreateLayerCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Katman adı boş olamaz.")
                .MaximumLength(100).WithMessage("Katman adı 100 karakterden fazla olamaz.");

            RuleFor(x => x.GeometryType)
                .NotEmpty().WithMessage("Geometri tipi belirtilmelidir.")
                .Must(x => x == "Point" || x == "LineString" || x == "Polygon")
                .WithMessage("Geçersiz geometri tipi. (Point, LineString veya Polygon olmalı)");

            RuleFor(x => x.GeometryWkt)
                .NotEmpty().WithMessage("WKT verisi boş olamaz.");
        }
    }

    // 3. İşleyici (Handler)
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
