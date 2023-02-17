using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atunjic_zadaca_3.Tablice.TabliceModeli
{
    public class Statusi
    {
        public int Id { get; set; }
        public string OznakaVeza { get; set; }
        public string Vrsta { get; set; }
        public string Status { get; set; }

        public Statusi(int id, string oznakaVeza, string vrsta, string status)
        {
            Id = id;
            OznakaVeza = oznakaVeza;
            Vrsta = vrsta;
            Status = status;
        }
    }
}
