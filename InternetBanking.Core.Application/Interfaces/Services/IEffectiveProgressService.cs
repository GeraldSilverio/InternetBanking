using InternetBanking.Core.Application.ViewModels.Payment.EffectiveProgress;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Interfaces.Services
{
    public interface IEffectiveProgressService : IGenericService<EffectiveProgress, SaveEffectiveProgressViewModel, EffectiveProgress>
    {
    }
}
