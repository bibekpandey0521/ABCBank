using Application.Repositories;
using Common.Requests;
using Common.Wrapper;
using Domain;
using MediatR;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;


namespace Application.Features.Accounts.Commands
{
    public class CreateTransactionCommand : IRequest<ResponseWrapper<int>>
    {
        public TransactionRequest Transaction { get; set; }
    }

    public class CreateTransactionCommmandHandler : IRequestHandler<CreateTransactionCommand, ResponseWrapper<int>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        public CreateTransactionCommmandHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ResponseWrapper<int>> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            // Find Account
            var accountInDb = await _unitOfWork.ReadRepositoryFor<Account>().GetByIdAsync(request.Transaction.AccountId);
            if (accountInDb is not null)
            {
                // know the transaction type
                // validate if ness
                if (request.Transaction.Type == Common.Enums.TransactionType.Withdrawal )
                {
                    if(request.Transaction.Amount > accountInDb.Balance)
                    {
                        return new ResponseWrapper<int>().Failed(message:"Withdrawal amount is higher than account balance");
                    }
                    // create  a transaction
                    var transaction = new Transaction()
                    {
                        AccountId = accountInDb.Id,
                        Amount = request.Transaction.Amount,
                        Type = Common.Enums.TransactionType.Withdrawal,
                        Date = DateTime.Now
                    };

                    // Update acccount balance
                    accountInDb.Balance -= request.Transaction.Amount;

                    await _unitOfWork.WriteRepositoryFor<Transaction>().AddAsync(transaction);
                    await _unitOfWork.WriteRepositoryFor<Account>().UpdateAsync(accountInDb);
                    await _unitOfWork.CommitAsync(cancellationToken);
                    return new ResponseWrapper<int>().Success(data: transaction.Id, message: "Withdrawal was successfully");
                }
                // Validate if ness
                // Create a transaction
                // Update account balance
                else if(request.Transaction.Type == Common.Enums.TransactionType.Deposit)
                {
                    var transaction = new Transaction()
                    {
                        AccountId = accountInDb.Id,
                        Amount = request.Transaction.Amount,
                        Type = Common.Enums.TransactionType.Deposit,
                        Date = DateTime.Now
                    };

                    // Update account balance
                    accountInDb.Balance += request.Transaction.Amount;

                    await _unitOfWork.WriteRepositoryFor<Transaction>().AddAsync(transaction);
                    await _unitOfWork.WriteRepositoryFor<Account>().UpdateAsync(accountInDb);
                    await _unitOfWork.CommitAsync(cancellationToken);
                    return new ResponseWrapper<int>().Success(data: transaction.Id, message: "Deposit was successfully.");
                }

            }

            return new ResponseWrapper<int>().Failed(message: "Accounts Does Not  Exists.");
        }
    }
}
