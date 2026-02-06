using MediatR;
using GisBackend.Application.Common;
using System.Transactions;

namespace GisBackend.Application.Common.Behaviors
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
            // Sadece Command (yazma) işlemlerinde transaction başlatalım
            // İstersen Query'lerde de açabilirsin ama genellikle okuma işlemlerinde gerekmez.
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

            // Sorgu (Query) ise direkt devam et
            return await next();
        }
    }
}
