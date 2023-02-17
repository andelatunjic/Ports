using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atunjic_zadaca_3.Tablice.TabliceModeli
{
    public class Brodovi
    {
        public string Vrsta { get; set; }
        public string Oznaka { get; set; }
        public int UkupanBroj { get; set; }
        public float MaxBrzina { get; set; }

        public Brodovi(string vrsta, string oznaka, int ukupanBroj, float maxBrzina)
        {
            Vrsta = vrsta;
            Oznaka = oznaka;
            UkupanBroj = ukupanBroj;
            MaxBrzina = maxBrzina;
        }
    }
}
