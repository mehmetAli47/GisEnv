using GisBackend.Core.Application.Common;
using GisBackend.Core.Application.Common.Interfaces;
using MediatR;

namespace GisBackend.Features.Layers.DeleteLayer
{
    // 1. Komut (Message)
    public record DeleteLayerCommand(Guid Id) : IRequest<Result>;

    // 2. İşleyici (Handler)
    public class DeleteLayerCommandHandler : IRequestHandler<DeleteLayerCommand, Result>
    {
        private readonly ILayerRepository _repository;

        public DeleteLayerCommandHandler(ILayerRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(DeleteLayerCommand request, CancellationToken cancellationToken)
        {
            var layer = await _repository.GetByIdAsync(request.Id);
            
            if (layer == null)
                return Result.Failure("Silinecek katman bulunamadı.");

            await _repository.DeleteAsync(request.Id);
            return Result.Success();
        }
    }
}
