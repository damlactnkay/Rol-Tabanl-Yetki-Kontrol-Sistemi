using Entity.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Ozel
{
    public interface IMakineRepository
    {
        Task<List<Makineler>> GetAllAsync();
        Task<Makineler?> GetByIdAsync(int id);
        Task AddAsync(Makineler makine);
        Task UpdateAsync(Makineler makine);
        Task DeleteAsync(int id);
        Task<List<Makineler>> GetByBolumIdAsync(int bolumId);
    
    }
}
