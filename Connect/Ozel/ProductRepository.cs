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
  
        public class ProductRepository : IProductRepository
        {
            private readonly ISQLXConneciton _sqlXConnection;

            public ProductRepository(ISQLXConneciton sqlXConnection)
            {
                _sqlXConnection = sqlXConnection;
            }

            public async Task<List<Uruns>> GetByVardiyaIdAsync(int vardiyaId)
            {
                using var conn = _sqlXConnection.CreateConnection();
                var sql = "SELECT * FROM Uruns WHERE VardiyaId = @VardiyaId";
                var result = await conn.QueryAsync<Uruns>(sql, new { VardiyaId = vardiyaId });
                return result.ToList();
            }

            public async Task<List<Uruns>> GetAllAsync()
            {
                using var conn = _sqlXConnection.CreateConnection();
                var sql = "SELECT * FROM Uruns";
                var result = await conn.QueryAsync<Uruns>(sql);
                return result.ToList();
            }

            public async Task<Uruns?> GetByIdAsync(int id)
            {
                using var conn = _sqlXConnection.CreateConnection();
                var sql = "SELECT * FROM Uruns WHERE Id = @Id";
                return await conn.QueryFirstOrDefaultAsync<Uruns>(sql, new { Id = id });
            }

            public async Task AddAsync(Uruns product)
            {
                using var conn = _sqlXConnection.CreateConnection();
                var sql = "INSERT INTO Uruns (UrunAdi, UrunKodu, UrunAdedi, UrunFiyat, VardiyaId) " +
                          "VALUES (@UrunAdi, @UrunKodu, @UrunAdedi, @UrunFiyat, @VardiyaId)";
                await conn.ExecuteAsync(sql, product);
            }

            public async Task UpdateAsync(Uruns product)
            {
                using var conn = _sqlXConnection.CreateConnection();
            var sql = @"
UPDATE Uruns
SET UrunAdi = @UrunAdi,
    UrunKodu = @UrunKodu,
    UrunAdedi = @UrunAdedi,
    UrunFiyat = @UrunFiyat,
    VardiyaId = @VardiyaId
WHERE Id = @Id";

            await conn.ExecuteAsync(sql, new
            {
                UrunAdi = product.UrunAdi,
                UrunKodu = product.UrunKodu,
                UrunAdedi = product.UrunAdedi,
                UrunFiyat = product.UrunFiyat,
                VardiyaId = product.VardiyaId,
                Id = product.Id
            });
        }
        public async Task DeleteHierarchyByBolumIdAsync(int bolumId)
        {
            using var conn = _sqlXConnection.CreateConnection();
            var sql = "DELETE FROM Uruns WHERE Id = @Id";
            await conn.ExecuteAsync(sql, new { Id = bolumId });
        }

    }
}

