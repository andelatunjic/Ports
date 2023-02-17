using atunjic_zadaca_3.Tablice.TabliceModeli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atunjic_zadaca_3.Tablice.Builder
{
    public interface IBuilderTablica
    {
        List<string> DodajZaglavljeTabliceStatusi(bool redniBrojevi);
        List<string> DodajTijeloTabliceStatusi(List<Statusi> statusi, bool redniBrojevi);
        List<string> DodajPodnozjeTabliceStatusi(int brojPodataka);

        List<string> DodajZaglavljeTabliceIspisZS(bool redniBrojevi);
        List<string> DodajTijeloTabliceIspisZS(List<ZauzetiSlobodniVezovi> zsVezovi, bool redniBrojevi);
        List<string> DodajPodnozjeTabliceIspisZS(int brojPodataka);

        List<string> DodajZaglavljeTabliceUkupniBrojZ(bool redniBrojevi);
        List<string> DodajTijeloTabliceUkupniBrojZ(List<ZauzetiVezoviPremaVrstama> zauzeti, bool redniBrojevi);
        List<string> DodajPodnozjeTabliceUkupniBrojZ(int brojPodataka);

        List<string> DodajZaglavljeTabliceStatistika(bool redniBrojevi, string kategorija);
        List<string> DodajTijeloTabliceStatistika(List<Brodovi> zauzeti, bool redniBrojevi);
        List<string> DodajPodnozjeTabliceStatistika(int brojPodataka);
    }
}
