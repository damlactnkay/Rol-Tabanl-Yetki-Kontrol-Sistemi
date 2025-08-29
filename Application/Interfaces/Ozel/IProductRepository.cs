using Entity.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Ozel
{
    public interface IProductRepository
    {
        Task<List<Uruns>> GetAllAsync();
        Task<Uruns?> GetByIdAsync(int id);
        Task AddAsync(Uruns product);
        Task UpdateAsync(Uruns product);
        //   Task DeleteAsync(int id);
        Task DeleteHierarchyByBolumIdAsync(int bolumId);
        Task<List<Uruns>> GetByVardiyaIdAsync(int vardiyaId);
       
    }
}
