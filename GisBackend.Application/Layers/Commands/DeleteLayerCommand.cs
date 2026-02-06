using GisBackend.Application.Common;
using GisBackend.Application.Layers.Interface;
using MediatR;

namespace GisBackend.Application.Layers.Commands
{
    public record DeleteLayerCommand(Guid Id) : IRequest<Result>;

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
