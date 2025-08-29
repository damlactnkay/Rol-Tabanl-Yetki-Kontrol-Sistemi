using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Entity.Class
{
    public class Bolum
    {
        public int Id { get; set; }
        public string BolumAdi { get; set; }

        public List<SelectListItem> BolumList { get; set; }
    }
}
