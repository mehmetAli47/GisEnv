using GisBackend.Core.Application.Common.Interfaces;
using MediatR;

namespace GisBackend.Core.Application.Common.Behaviors
{
    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionBehavior(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request.GetType().Name.EndsWith("Command"))
            {
                try
                {
                    await _unitOfWork.BeginTransactionAsync();
                    var response = await next();
                    await _unitOfWork.CommitTransactionAsync();
                    return response;
                }
                catch
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    throw;
                }
            }
            return await next();
        }
    }
}
