using atunjic_zadaca_3.Modeli;
using atunjic_zadaca_3.Singleton;
using atunjic_zadaca_3.Tablice.Builder;
using atunjic_zadaca_3.Tablice.TabliceModeli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atunjic_zadaca_3.ChainOfRStatistika
{
    internal class PutnickiHandler : AbstractHandler
    {
        public PutnickiHandler(int sifra)
        {
            this.sifra = sifra;
        }

        protected override void ispisi(List<Brod> brodovi)
        {
            List<Brodovi> trazeniBrodovi = new List<Brodovi>();

            Brodovi trajekt = new Brodovi("Trajekti", "TR", prebroji(brodovi, "TR"), izracunajBrzinu(brodovi, "TR"));
            Brodovi katamaran = new Brodovi("Katamarani", "KA", prebroji(brodovi, "KA"), izracunajBrzinu(brodovi, "KA"));
            Brodovi klasican = new Brodovi("Putnički klasični", "KL", prebroji(brodovi, "KL"), izracunajBrzinu(brodovi, "KL"));
            Brodovi kruzni = new Brodovi("Putnički za kružno", "KR", prebroji(brodovi, "KR"), izracunajBrzinu(brodovi, "KR"));

            trazeniBrodovi.Add(trajekt);
            trazeniBrodovi.Add(katamaran);
            trazeniBrodovi.Add(klasican);
            trazeniBrodovi.Add(kruzni);

            var director = new DirectorTablica();
            var builder = new ConcreteBuilderTablica();
            director.Builder = builder;
            director.ispisiTablicuStatistika(trazeniBrodovi, brodskaLuka.zaglavlje, brodskaLuka.podnozje, brodskaLuka.redniBroj, "putničke");
        }
    }
}
