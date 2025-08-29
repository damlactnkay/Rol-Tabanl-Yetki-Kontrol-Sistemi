using Application.Interfaces.Genel;
using Application.Interfaces.Ozel;
using Connect.Ozel;
using Dapper;
using Entity.Class;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Connect.Ozel
{
    public class VardiyaRepository : IVardiyaRepository
    {
        private readonly ISQLXConneciton _sqlXConnection;

        public VardiyaRepository(ISQLXConneciton sQLXConneciton)
        {
            _sqlXConnection = sQLXConneciton;
        }

        public async Task<List<Vardiya>> GetAllAsync()
        {
            using var conn = _sqlXConnection.CreateConnection();
            var sql = "SELECT * FROM Vardiya";
            var result = (await conn.QueryAsync<Vardiya>(sql)).ToList();
            return result ?? new List<Vardiya>();
        }

        public async Task<Vardiya?> GetByIdAsync(int id)
        {
            using var conn = _sqlXConnection.CreateConnection();
            var sql = "SELECT * FROM Vardiya WHERE Id = @Id";
            var vardiya = await conn.QueryFirstOrDefaultAsync<Vardiya>(sql, new { Id = id });
            return vardiya;
        }

     
        public async Task<List<Vardiya>> GetByMakineIdAsync(int makineId)
        {
            using var conn = _sqlXConnection.CreateConnection();
            var sql = "SELECT * FROM Vardiya WHERE MakineId = @MakineId";
            var result = (await conn.QueryAsync<Vardiya>(sql, new { MakineId = makineId })).ToList();
            return result ?? new List<Vardiya>();
        }

        public async Task AddAsync(Vardiya vardiya)
        {
            using var conn = _sqlXConnection.CreateConnection();
            var sql = "INSERT INTO Vardiya (Kod, MakineId) VALUES (@Kod, @MakineId)";
            await conn.ExecuteAsync(sql, vardiya);
        }

        public async Task UpdateAsync(Vardiya vardiya)
        {
            using var conn = _sqlXConnection.CreateConnection();
            var sql = @"UPDATE Vardiya 
                    SET Kod = @Kod,
                        MakineId = @MakineId
                    WHERE Id = @Id";
            await conn.ExecuteAsync(sql, vardiya);
        }

        public async Task DeleteAsync(int id)
        {
            using var conn = _sqlXConnection.CreateConnection();
            var sql = "DELETE FROM Vardiya WHERE Id = @Id";
            await conn.ExecuteAsync(sql, new { Id = id });
        }
    }
}