using Entity.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Ozel
{
    public interface IBolumRepository
    {
        Task<List<Bolum>> GetAllAsync();
        Task<Bolum?> GetByIdAsync(int id);
        Task AddAsync(Bolum bolum);
        Task UpdateAsync(Bolum bolum);
        Task DeleteAsync(int id);
       Task<List<Bolum>> TumBolumGetirAsync();
        
    }
}
