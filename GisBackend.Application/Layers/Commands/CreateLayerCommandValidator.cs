using FluentValidation;

namespace GisBackend.Application.Layers.Commands
{
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
}
