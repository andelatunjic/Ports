
using atunjic_zadaca_3.VisitorIzvrsavanje.ConcreteKlase;

namespace atunjic_zadaca_3.VisitorIzvrsavanje
{
    public class AktivnostConcreteVisitor : IAktivnostVisitor
    {
        public string Naredba { get; set; }

        public AktivnostConcreteVisitor(string naredba)
        {
            Naredba = naredba;
        }

        public void posjeti(Status status)
        {
            status.izvrsi();
        }

        public void posjeti(IspisVezova ispisVezova)
        {
            ispisVezova.izvrsi(Naredba);
        }

        public void posjeti(VirtualnoVrijeme virVrijeme)
        {
            virVrijeme.izvrsi(Naredba);
        }

        public void posjeti(ZahtjevZaRezerviraniVez zahtjevRezervirani)
        {
            zahtjevRezervirani.izvrsi(Naredba);
        }

        public void posjeti(ZahtjevZaSlobodanVez zahtjevSlobodan)
        {
            zahtjevSlobodan.izvrsi(Naredba);
        }

        public void posjeti(Spajanje spajanje)
        {
            spajanje.izvrsi(Naredba);
        }

        public void posjeti(BrojZauzetihVezova brojZauzetih)
        {
            brojZauzetih.izvrsi(Naredba);
        }

        public void posjeti(UredenjeIspisa uredi)
        {
            uredi.izvrsi(Naredba);
        }

        public void posjeti(Statistika statistika)
        {
            statistika.izvrsi(Naredba);
        }
    }
}
