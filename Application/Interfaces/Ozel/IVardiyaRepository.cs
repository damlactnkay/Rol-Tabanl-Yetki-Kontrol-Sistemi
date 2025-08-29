using Entity.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Ozel
{
    public interface IVardiyaRepository
    {
        Task<List<Vardiya>> GetAllAsync();
        Task<Vardiya?> GetByIdAsync(int id);
        Task AddAsync(Vardiya makine);
        Task UpdateAsync(Vardiya makine);
        Task DeleteAsync(int id);

        Task<List<Vardiya>> GetByMakineIdAsync(int makineId);
    }
}
