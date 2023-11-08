using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InternetBanking.Core.Application.Interfaces.Services
{
    public interface IGenericService<Model, SaveViewModel, ViewModel>
       where Model : class
       where SaveViewModel : class
       where ViewModel : class       
    {
        Task<SaveViewModel> Add(SaveViewModel model);
        Task Update(SaveViewModel model, int id);        
        Task Delete(int id);
        Task<List<ViewModel>> GetAll();
        Task<SaveViewModel> GetById(int id);       
    }
}
