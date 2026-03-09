using Application.Repositories;
using Common.Responses;
using Common.Wrapper;
using Domain;
using Mapster;
using MediatR;


namespace Application.Features.Accounts.Queries
{
    public class GetAccountTransactionQuery : IRequest<ResponseWrapper<List<TransactionResponse>>>
    {
        public int AccountId { get; set; }
    }

    public class GetAccountTransactionsQueryHandler : IRequestHandler<GetAccountTransactionQuery, ResponseWrapper<List<TransactionResponse>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        public GetAccountTransactionsQueryHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ResponseWrapper<List<TransactionResponse>>> Handle(GetAccountTransactionQuery request, CancellationToken cancellationToken)
        {
            var transactionsInDb =  _unitOfWork.ReadRepositoryFor<Transaction>()
                .Entities
                .Where(transaction => transaction.AccountId == request.AccountId)
                .ToList();

            if (transactionsInDb.Count > 0)
            {
                return new ResponseWrapper<List<TransactionResponse>>().Success(data: transactionsInDb.Adapt<List<TransactionResponse>>());
            }

            return new ResponseWrapper<List<TransactionResponse>>().Failed(message:"No Transaction on specified acount were found");
        }
    }
}
