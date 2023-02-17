using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atunjic_zadaca_3.Tablice.TabliceModeli
{
    public class ZauzetiVezoviPremaVrstama
    {
        public string Vrsta { get; set; }
        public string Vrijeme { get; set; }
        public int UkupanBrojZauzetih { get; set; }

        public ZauzetiVezoviPremaVrstama(string vrsta, string vrijeme, int ukupanBrojZauzetih)
        {
            Vrsta = vrsta;
            Vrijeme = vrijeme;
            UkupanBrojZauzetih = ukupanBrojZauzetih;
        }
    }
}
