using atunjic_zadaca_3.Modeli;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atunjic_zadaca_3.Modeli
{
    public class ZahtjevRezervacije
    {
        public Brod Brod { get; set; }
        public DateTime DatumVrijemeOd { get; set; }
        public int TrajanjePriveza { get; set; }

        public ZahtjevRezervacije(Brod brod, DateTime datumVrijemeOd, int trajanjePriveza)
        {
            Brod = brod;
            DatumVrijemeOd = datumVrijemeOd;
            TrajanjePriveza = trajanjePriveza;
        }
    }
}
