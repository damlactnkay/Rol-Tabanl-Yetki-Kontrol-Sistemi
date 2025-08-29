using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Entity.Class
{
    public class Makineler
    {
        public int Id { get; set; }
        public string MakineKodu { get; set; }
        public string MakineAdi { get; set; }
        public decimal MakineFiyat { get; set; }
        public int MakineAdedi { get; set; }
        public int BolumId { get; set; }

   public List<SelectListItem> MakineList { get; set; }
    }
}
