﻿using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Interfaces.Repositories
{
    public interface ITransactionRepository:IGenericRepository<Transaction>
    {
        int GetTransactionPerDay();
    }
}
