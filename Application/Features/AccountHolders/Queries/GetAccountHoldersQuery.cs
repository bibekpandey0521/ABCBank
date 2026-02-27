using Application.Repositories;
using Common.Responses;
using Common.Wrapper;
using Domain;
using Mapster;
using MediatR;

namespace Application.Features.AccountHolders.Queries
{
    public class GetAccountHoldersQuery : IRequest<ResponseWrapper<List<AccountHolderResponse>>>
    {
    }

    public class GetAccountHolderQueryHandler : IRequestHandler<GetAccountHoldersQuery, ResponseWrapper<List<AccountHolderResponse>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        public GetAccountHolderQueryHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ResponseWrapper<List<AccountHolderResponse>>> Handle(GetAccountHoldersQuery request, CancellationToken cancellationToken)
        {
            var accountHolderInDb = await _unitOfWork.ReadRepositoryFor<AccountHolder>().GetAllAsync();

            if(accountHolderInDb.Count > 0)
            {
                return new ResponseWrapper<List<AccountHolderResponse>>().Success(accountHolderInDb.Adapt<List<AccountHolderResponse>>());
            }

            return new ResponseWrapper<List<AccountHolderResponse>>().Failed("No Account Holders Were found.");
        }
    }
}
