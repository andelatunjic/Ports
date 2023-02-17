using atunjic_zadaca_3.Modeli;
using atunjic_zadaca_3.Tablice.Builder;
using atunjic_zadaca_3.Tablice.TabliceModeli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atunjic_zadaca_3.ChainOfRStatistika
{
    public class OstaliHandler : AbstractHandler
    {
        public OstaliHandler(int sifra)
        {
            this.sifra = sifra;
        }

        protected override void ispisi(List<Brod> brodovi)
        {
            List<Brodovi> trazeniBrodovi = new List<Brodovi>();

            Brodovi jahta = new Brodovi("Jahte", "JA", prebroji(brodovi, "JA"), izracunajBrzinu(brodovi, "JA"));
            Brodovi brodica = new Brodovi("Brodice", "BR", prebroji(brodovi, "BR"), izracunajBrzinu(brodovi, "BR"));
            Brodovi ronilacki = new Brodovi("Ronilicki brodovi", "RO", prebroji(brodovi, "RO"), izracunajBrzinu(brodovi, "BR"));

            trazeniBrodovi.Add(jahta);
            trazeniBrodovi.Add(brodica);
            trazeniBrodovi.Add(ronilacki);

            var director = new DirectorTablica();
            var builder = new ConcreteBuilderTablica();
            director.Builder = builder;
            director.ispisiTablicuStatistika(trazeniBrodovi, brodskaLuka.zaglavlje, brodskaLuka.podnozje, brodskaLuka.redniBroj, "ostale");
        }
    }
}
