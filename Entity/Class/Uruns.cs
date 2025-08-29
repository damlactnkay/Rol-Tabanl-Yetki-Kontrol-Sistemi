using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Entity.Class
{
    public class Uruns
    {
        public int Id { get; set; }
        public string  UrunKodu { get; set; }
        public string UrunAdi { get; set; }
        public int UrunAdedi { get; set; }
        public decimal UrunFiyat { get; set; }
        public int VardiyaId { get; set; }
 
    }
}
