using atunjic_zadaca_3.Singleton;
using atunjic_zadaca_3.Tablice.TabliceModeli;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atunjic_zadaca_3.Tablice.Builder
{
    public class DirectorTablica
    {
        private IBuilderTablica builder;
        private BrodskaLuka brodskaLuka = BrodskaLuka.getInstance();

        public IBuilderTablica Builder 
        { 
            set { builder = value; }
        }

        public void ispisiTablicuStatusi(List<Statusi> statusi, bool zaglavlje, bool podnozje, bool redniBroj)
        {
            List<string> reci = new List<string>();
            if(zaglavlje)
            {
                reci.AddRange(builder.DodajZaglavljeTabliceStatusi(redniBroj));
            }
            reci.AddRange(builder.DodajTijeloTabliceStatusi(statusi, redniBroj));
            if(podnozje)
            {
                reci.AddRange(builder.DodajPodnozjeTabliceStatusi(statusi.Count()));
            }
            brodskaLuka.radniPodaci = reci;
        }

        public void ispisiTablicuZauzetiSlobodni(List<ZauzetiSlobodniVezovi> zsVezovi, bool zaglavlje, bool podnozje, bool redniBroj)
        {
            List<string> reci = new List<string>();
            if (zaglavlje)
            {
                reci.AddRange(builder.DodajZaglavljeTabliceIspisZS(redniBroj));
            }
            reci.AddRange(builder.DodajTijeloTabliceIspisZS(zsVezovi, redniBroj));
            if (podnozje)
            {
                reci.AddRange(builder.DodajPodnozjeTabliceIspisZS(zsVezovi.Count()));
            }
            brodskaLuka.radniPodaci = reci;
        }

        public void ispisiTablicuUkupniBrojZauzetih(List<ZauzetiVezoviPremaVrstama> zauzeti, bool zaglavlje, bool podnozje, bool redniBroj)
        {
            List<string> reci = new List<string>();
            if (zaglavlje)
            {
                reci.AddRange(builder.DodajZaglavljeTabliceUkupniBrojZ(redniBroj));
            }
            reci.AddRange(builder.DodajTijeloTabliceUkupniBrojZ(zauzeti, redniBroj));
            if (podnozje)
            {
                reci.AddRange(builder.DodajPodnozjeTabliceUkupniBrojZ(zauzeti.Count()));
            }
            brodskaLuka.radniPodaci = reci;
        }

        public void ispisiTablicuStatistika(List<Brodovi> brodovi, bool zaglavlje, bool podnozje, bool redniBroj, string kategorija)
        {
            List<string> reci = new List<string>();
            if (zaglavlje)
            {
                reci.AddRange(builder.DodajZaglavljeTabliceStatistika(redniBroj, kategorija));
            }
            reci.AddRange(builder.DodajTijeloTabliceStatistika(brodovi, redniBroj));
            if (podnozje)
            {
                reci.AddRange(builder.DodajPodnozjeTabliceStatistika(brodovi.Count()));
            }
            brodskaLuka.radniPodaci = reci;
        }
    }
}
