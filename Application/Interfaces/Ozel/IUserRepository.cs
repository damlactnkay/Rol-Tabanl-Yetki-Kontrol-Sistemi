using Entity.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Ozel
{
    public interface IUserRepository
    {
        Task<User> GetByUsernameAsync(string KullaniciAdi, string Password);
        //Task<List<User>> GetAllAsync();       // Listeleme
        //Task<User> GetByIdAsync(int id);      // Tek kullanıcı
        //Task AddAsync(User user);             // Ekleme
        //Task UpdateAsync(User user);          // Güncelleme
        //Task DeleteAsync(int id);

    }
}
