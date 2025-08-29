using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Class
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string KullaniciAdi { get; set; }
         public string Role { get; set; }
        public string Password { get; set; }
        public List<string> Roles { get; set; } = new();

    }
}
