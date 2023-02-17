using atunjic_zadaca_3.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atunjic_zadaca_3.Tablice.TabliceModeli
{
    public class ZauzetiSlobodniVezovi
    {
        public int IdVeza { get; set; }
        public string OznakaVeza { get; set; }
        public string Vrsta { get; set; }
        public StatusVeza StatusVeza { get; set; }
        public string ZauzetOd { get; set; }
        public string ZauzetDo { get; set; }

        public ZauzetiSlobodniVezovi(int idVeza, string oznakaVeza, string vrsta, StatusVeza status, string zauzetOd, string zauzetDo)
        {
            IdVeza = idVeza;
            OznakaVeza = oznakaVeza;
            Vrsta = vrsta;
            StatusVeza = status;
            ZauzetOd = zauzetOd;
            ZauzetDo = zauzetDo;
        }
    }
}
