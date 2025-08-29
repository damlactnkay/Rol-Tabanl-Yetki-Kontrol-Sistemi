using Application.Interfaces.Genel;
using Application.Interfaces.Ozel;
using Dapper;
using Entity.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Connect.Ozel
{
    public class MakineRepository : IMakineRepository
    {
        private readonly ISQLXConneciton _sqlXConnection;

        public MakineRepository(ISQLXConneciton sqlXConnection)
        {
            _sqlXConnection = sqlXConnection;
        }

       
        public async Task<List<Makineler>> GetAllAsync()
        {
            using var conn = _sqlXConnection.CreateConnection();
            var sql = "SELECT * FROM Makineler";
            var result = await conn.QueryAsync<Makineler>(sql);
            return result.ToList();
        }

        
        public async Task<Makineler?> GetByIdAsync(int id)
        {
            using var conn = _sqlXConnection.CreateConnection();
            var sql = "SELECT * FROM Makineler WHERE Id = @Id";
            return await conn.QueryFirstOrDefaultAsync<Makineler>(sql, new { Id = id });
        }

        public async Task AddAsync(Makineler makine)
        {
            using var conn = _sqlXConnection.CreateConnection();
            var sql = @"INSERT INTO Makineler (MakineKodu, MakineAdi, MakineFiyat, MakineAdedi, BolumId) 
                        VALUES (@MakineKodu, @MakineAdi, @MakineFiyat, @MakineAdedi, @BolumId)";
            await conn.ExecuteAsync(sql, makine);
        }

       
        public async Task UpdateAsync(Makineler makine)
        {
            using var conn = _sqlXConnection.CreateConnection();
            var sql = @"UPDATE Makineler 
                        SET MakineKodu = @MakineKodu, 
                            MakineAdi = @MakineAdi, 
                            MakineFiyat = @MakineFiyat, 
                            MakineAdedi = @MakineAdedi, 
                            BolumId = @BolumId
                        WHERE Id = @Id";
            await conn.ExecuteAsync(sql, makine);
        }

      
        public async Task DeleteAsync(int id)
        {
            using var conn = _sqlXConnection.CreateConnection();
            var sql = "DELETE FROM Makineler WHERE Id = @Id";
            await conn.ExecuteAsync(sql, new { Id = id });
        }

       
        public async Task<List<Makineler>> GetByBolumIdAsync(int bolumId)
        {
            using var conn = _sqlXConnection.CreateConnection();
            var sql = "SELECT * FROM Makineler WHERE BolumId = @BolumId";
            var result = await conn.QueryAsync<Makineler>(sql, new { BolumId = bolumId });
            return result.ToList();
        }
    }
}

