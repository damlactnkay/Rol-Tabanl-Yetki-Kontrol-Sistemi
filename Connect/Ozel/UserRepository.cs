using Application.Interfaces.Genel;
using Application.Interfaces.Ozel;
using Dapper;
using Entity.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect.Ozel
{
    public class UserRepository : IUserRepository
    {
        private readonly ISQLXConneciton _sqlXConnection;
        private readonly IXConnectionProvider _den;

        public UserRepository(ISQLXConneciton sqlXConnection, IXConnectionProvider denConnection)
        {
            _sqlXConnection = sqlXConnection;
            _den = denConnection;
        }

        public async Task<User> GetByUsernameAsync(string KullaniciAdi, string Password)
        {
            using var conn = _sqlXConnection.CreateConnection();

            var sql = "SELECT * FROM Users WHERE KullaniciAdi = @KullaniciAdi AND Password = @Password";

            return await conn.QueryFirstOrDefaultAsync<User>(
                sql,
                new { KullaniciAdi, Password }
            );
        }

    }


}
   

    


