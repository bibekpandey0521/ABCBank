using Application.Repositories;
using Common.Responses;
using Common.Wrapper;
using Domain;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Accounts.Queries
{
    public class GetAccountsByAccountHolderId : IRequest<ResponseWrapper<List<AccountResponse>>>
    {
        public int AccountHolderId { get; set; }
    }

    public class GetAccountByQueryHandler : IRequestHandler<GetAccountsByAccountHolderId, ResponseWrapper<List<AccountResponse>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        public GetAccountByQueryHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ResponseWrapper<List<AccountResponse>>> Handle(GetAccountsByAccountHolderId request, CancellationToken cancellationToken)
        {
            var accounts = _unitOfWork.ReadRepositoryFor<Account>()
                .Entities
                .Where(account => account.AccountHolderId == request.AccountHolderId)
                .ToList();
            if(accounts.Count > 0)
            {
                return new ResponseWrapper<List<AccountResponse>>().Success(data: accounts.Adapt<List<AccountResponse>>());
            }
            return new ResponseWrapper<List<AccountResponse>>().Failed(message:"No Accounts Were Found.");
        }
    }
}
