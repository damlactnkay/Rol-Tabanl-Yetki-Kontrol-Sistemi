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
    public class BolumRepository : IBolumRepository
    {
        private readonly ISQLXConneciton _sqlXConnection;

        public BolumRepository(ISQLXConneciton sqlXConnection)
        {
            _sqlXConnection = sqlXConnection;
        }


        public async Task<List<Bolum>> GetAllAsync()
        {
            using var conn = _sqlXConnection.CreateConnection();
            var sql = "SELECT * FROM Bolum";
            var result = await conn.QueryAsync<Bolum>(sql);
            return result.ToList();
         }

 
        public async Task<Bolum?> GetByIdAsync(int id)
        {
            using var conn = _sqlXConnection.CreateConnection();
            var sql = "SELECT * FROM Bolum WHERE Id = @Id";
            return await conn.QueryFirstOrDefaultAsync<Bolum>(sql, new { Id = id });
        }

        public async Task AddAsync(Bolum bolum)
        {
            using var conn = _sqlXConnection.CreateConnection();
            var sql = "INSERT INTO Bolum (Ad) VALUES (@Ad)";
            await conn.ExecuteAsync(sql, bolum);
        }

        public async Task UpdateAsync(Bolum bolum)
        {
            using var conn = _sqlXConnection.CreateConnection();
            var sql = "UPDATE Bolum SET Ad = @Ad WHERE Id = @Id";
            await conn.ExecuteAsync(sql, bolum);
        }

   
        public async Task DeleteAsync(int id)
        {
            using var conn = _sqlXConnection.CreateConnection();
            var sql = "DELETE FROM Bolum WHERE Id = @Id";
            await conn.ExecuteAsync(sql, new { Id = id });
        }

      
        public async Task<List<Bolum>> TumBolumGetirAsync()
        {
            return await GetAllAsync(); 
        }
    }

}
