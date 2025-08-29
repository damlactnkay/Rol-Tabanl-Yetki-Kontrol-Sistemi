using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Class
{
    public class UrunEkle
    {
        // Ürünün kendi alanları
        public string UrunAdi { get; set; }
        public string? UrunFiyat { get; set; }

        // Dropdown seçimleri
        public int BolumId { get; set; }
        public int MakineId { get; set; }
        public int VardiyaId { get; set; }
    }
}
