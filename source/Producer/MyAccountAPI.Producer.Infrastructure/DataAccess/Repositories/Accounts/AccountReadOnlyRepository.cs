﻿namespace MyAccountAPI.Producer.Infrastructure.DataAccess.Repositories.Accounts
{
    using MongoDB.Driver;
    using MyAccountAPI.Domain.Model.Accounts;
    using System;
    using System.Threading.Tasks;
    using System.Linq;

    public class AccountReadOnlyRepository : IAccountReadOnlyRepository
    {
        private readonly MongoContext _mongoContext;

        public AccountReadOnlyRepository(MongoContext mongoContext)
        {
            _mongoContext = mongoContext;
        }

        public async Task<bool> CheckTransactionReceived(Guid accountId, Guid transactionId)
        {
            Account account = await _mongoContext
                .Accounts
                .Find(e => e.Id == accountId)
                .SingleOrDefaultAsync();

            if (account == null)
                return false;

            var transactions = account.GetTransactions();

            if (transactions == null)
                return false;

            bool contains = transactions
                .Where(e => e.Id == transactionId)
                .Count() > 0;

            return contains;
        }

        public async Task<Account> GetAccount(Guid accountId)
        {
            return await _mongoContext
                .Accounts
                .Find(e => e.Id == accountId)
                .SingleOrDefaultAsync();
        }
    }
}
