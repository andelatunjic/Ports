using atunjic_zadaca_3.Enums;
using System.Globalization;
using atunjic_zadaca_3.Tablice.TabliceModeli;
using atunjic_zadaca_3.VisitorZauzetiVezovi;
using atunjic_zadaca_3.Tablice.Builder;

namespace atunjic_zadaca_3.VisitorIzvrsavanje.ConcreteKlase
{
    public class BrojZauzetihVezova : ZajednickeMetode, IAktivnost
    {
        public void prihvati(IAktivnostVisitor aktivnostVisitor)
        {
            aktivnostVisitor.posjeti(this);
        }

        public void izvrsi(string naredba)
        {
            string[] polje = naredba.Split(' ');
            string datum = polje[1] + " " + polje[2];

            List<ZauzetiVezoviPremaVrstama> listaZauzetihPremaVrsti = new List<ZauzetiVezoviPremaVrstama>();

            var pravilanDatum = DateTime.TryParse(datum, CultureInfo.CurrentCulture,
                DateTimeStyles.AssumeLocal, out DateTime Datum);
            
            if(pravilanDatum)
            {
                brodskaLuka.azurirajStatuseVezova(Datum, Datum, false);

                listaZauzetihPremaVrsti = vratiZapise(datum);

                var director = new DirectorTablica();
                var builder = new ConcreteBuilderTablica();
                director.Builder = builder;
                director.ispisiTablicuUkupniBrojZauzetih(listaZauzetihPremaVrsti, brodskaLuka.zaglavlje, brodskaLuka.podnozje, brodskaLuka.redniBroj);
            }
            else
            {
                greske.brojGresaka++;
                Console.WriteLine($"ERROR {greske.brojGresaka} : Datum ne postoji");
            }
        }

        private List<ZauzetiVezoviPremaVrstama> vratiZapise(string datum)
        {
            List<ZauzetiVezoviPremaVrstama> lista = new List<ZauzetiVezoviPremaVrstama>();

            zbrojiZauzeteVezove();
            
            int PU = brodskaLuka.brojZauzetihPutnickihVezova;
            int PO = brodskaLuka.brojZauzetihPoslovnihVezova;
            int OS = brodskaLuka.brojZauzetihOstalihVezova;

            ZauzetiVezoviPremaVrstama pu = new ZauzetiVezoviPremaVrstama("PU - Putnicki", datum, PU);
            ZauzetiVezoviPremaVrstama po = new ZauzetiVezoviPremaVrstama("PO - Poslovni", datum, PO);
            ZauzetiVezoviPremaVrstama os = new ZauzetiVezoviPremaVrstama("OS - Ostali", datum, OS);

            lista.Add(pu);
            lista.Add(po);
            lista.Add(os);

            return lista;
        }

        private void zbrojiZauzeteVezove()
        {
            List<ZauzetiVezoviPremaVrstama> lista = new List<ZauzetiVezoviPremaVrstama>();
            VrstaVezaConcreteVisitor visitor = new VrstaVezaConcreteVisitor();

            PoslovniVez poslovni = new PoslovniVez();
            poslovni.prihvati(visitor);

            PutnickiVez putnicki = new PutnickiVez();
            putnicki.prihvati(visitor);

            OstaliVez ostali = new OstaliVez();
            ostali.prihvati(visitor);
        }
    }
}
