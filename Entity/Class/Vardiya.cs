using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Entity.Class
{
    public class Vardiya
    {
        public int Id { get; set; }
        public string Kod { get; set; }
     
        public int MakineId { get; set; }
        public List<SelectListItem> VardiyaList { get; set; }
    }
}
