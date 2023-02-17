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
    public class PoslovniHandler : AbstractHandler
    {
        public PoslovniHandler(int sifra)
        {
            this.sifra = sifra;
        }

        protected override void ispisi(List<Brod> brodovi)
        {
            List<Brodovi> trazeniBrodovi = new List<Brodovi>();

            Brodovi ribarica = new Brodovi("Ribarice", "RI", prebroji(brodovi, "RI"), izracunajBrzinu(brodovi, "RI"));
            Brodovi teretni = new Brodovi("Teretni brodovi", "TE", prebroji(brodovi, "TE"), izracunajBrzinu(brodovi, "TE"));
            
            trazeniBrodovi.Add(ribarica);
            trazeniBrodovi.Add(teretni);

            var director = new DirectorTablica();
            var builder = new ConcreteBuilderTablica();
            director.Builder = builder;
            director.ispisiTablicuStatistika(trazeniBrodovi, brodskaLuka.zaglavlje, brodskaLuka.podnozje, brodskaLuka.redniBroj, "poslovne");
        }

        
    }
}
