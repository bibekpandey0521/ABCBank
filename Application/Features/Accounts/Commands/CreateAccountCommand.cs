using Application.Repositories;
using Common.Wrapper;
using Domain;
using Mapster;
using MediatR;
using Common.Requests;

namespace Application.Features.Accounts.Commands
{
    public class CreateAccountCommand : IRequest<ResponseWrapper<int>>
    {
        public CreateAccountRequest CreateAccount { get; set; }
    }

    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, ResponseWrapper<int>>
    {
        private IUnitOfWork<int> _unitOfWork;
        public CreateAccountCommandHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ResponseWrapper<int>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            // Map incoming to Domain Account entity
            var account = request.CreateAccount.Adapt<Account>();
            // Generate account Number -> yyMMddHmmss
            account.AccountNumber = AccountNumberGenerator.GenerateAccountNumber();
            // Activate account
            account.IsActive = true;
            // Create Account
            await _unitOfWork.WriteRepositoryFor<Account>().AddAsync(account);
            await _unitOfWork.CommitAsync(cancellationToken);

            return new ResponseWrapper<int>().Success(data : account.Id,"Account created successfully");
        }
    }
}
