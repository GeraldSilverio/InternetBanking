﻿using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Interfaces.Repositories
{
    public interface ICreditsCardRepository : IGenericRepository<CreditsCard>
    {
        Task<List<CreditsCard>> GetCreditCardsByUserIdAsync(string idUser);
    }
}
