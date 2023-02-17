using atunjic_zadaca_3.VisitorIzvrsavanje.ConcreteKlase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atunjic_zadaca_3.VisitorIzvrsavanje
{
    public interface IAktivnostVisitor
    {
        public void posjeti(Status status);
        public void posjeti(IspisVezova ispisVezova);
        public void posjeti(VirtualnoVrijeme virVrijeme);
        public void posjeti(ZahtjevZaRezerviraniVez zahtjevRezervirani);
        public void posjeti(ZahtjevZaSlobodanVez zahtjevSlobodan);
        public void posjeti(Spajanje spajanje);
        public void posjeti(BrojZauzetihVezova brojZauzetih);
        public void posjeti(UredenjeIspisa uredi);
        public void posjeti(Statistika statistika);
    }
}
