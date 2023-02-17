using atunjic_zadaca_3.Tablice.TabliceModeli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atunjic_zadaca_3.Tablice.Builder
{
    public class ConcreteBuilderTablica : IBuilderTablica
    {
        //Podnozja tablica

        public List<string> DodajPodnozjeTabliceIspisZS(int brojPodataka)
        {
            List<string> podnozje = new List<string>();
            podnozje.Add(Tablica.ispisiRed($"Ukupan broj podataka: {brojPodataka}"));
            podnozje.Add(Tablica.ispisiPregradu());
            return podnozje;
        }

        public List<string> DodajPodnozjeTabliceStatusi(int brojPodataka)
        {
            List<string> podnozje = new List<string>();
            podnozje.Add(Tablica.ispisiRed($"Ukupan broj podataka: {brojPodataka}"));
            podnozje.Add(Tablica.ispisiPregradu());
            return podnozje;
        }

        public List<string> DodajPodnozjeTabliceUkupniBrojZ(int brojPodataka)
        {
            List<string> podnozje = new List<string>();
            podnozje.Add(Tablica.ispisiRed($"Ukupan broj podataka: {brojPodataka}"));
            podnozje.Add(Tablica.ispisiPregradu());
            return podnozje;
        }

        public List<string> DodajPodnozjeTabliceStatistika(int brojPodataka)
        {
            List<string> podnozje = new List<string>();
            podnozje.Add(Tablica.ispisiRed($"Ukupan broj podataka: {brojPodataka}"));
            podnozje.Add(Tablica.ispisiPregradu());
            return podnozje;
        }

        //Tijela tablica

        public List<string> DodajTijeloTabliceIspisZS(List<ZauzetiSlobodniVezovi> zsVezovi, bool redniBrojevi)
        {
            List<string> tijelo = new List<string>();
            tijelo.Add(Tablica.ispisiPregradu());
            int redniBroj = 0;
            if(redniBrojevi)
            {
                foreach (var item in zsVezovi)
                {
                    redniBroj++;
                    tijelo.Add(Tablica.ispisiRed(redniBroj.ToString(), item.IdVeza.ToString(), item.OznakaVeza, item.Vrsta, item.StatusVeza.ToString(), item.ZauzetOd, item.ZauzetDo));
                }
            }
            else
            {
                foreach (var item in zsVezovi)
                {
                    tijelo.Add(Tablica.ispisiRed(item.IdVeza.ToString(), item.OznakaVeza, item.Vrsta, item.StatusVeza.ToString(), item.ZauzetOd, item.ZauzetDo));
                }
            }
            tijelo.Add(Tablica.ispisiPregradu());
            return tijelo;
        }

        public List<string> DodajTijeloTabliceStatusi(List<Statusi> statusi, bool redniBrojevi)
        {
            List<string> tijelo = new List<string>();
            tijelo.Add(Tablica.ispisiPregradu());
            int redniBroj = 0;
            if (redniBrojevi)
            {
                foreach (var item in statusi)
                {
                    redniBroj++;
                    tijelo.Add(Tablica.ispisiRed(redniBroj.ToString(), item.Id.ToString(), item.OznakaVeza, item.Vrsta, item.Status));
                }
            }
            else
            {
                foreach (var item in statusi)
                {
                    tijelo.Add(Tablica.ispisiRed(item.Id.ToString(), item.OznakaVeza, item.Vrsta, item.Status));
                }
            }
            tijelo.Add(Tablica.ispisiPregradu());
            return tijelo;
        }

        public List<string> DodajTijeloTabliceUkupniBrojZ(List<ZauzetiVezoviPremaVrstama> zauzeti, bool redniBrojevi)
        {
            List<string> tijelo = new List<string>();
            tijelo.Add(Tablica.ispisiPregradu());
            int redniBroj = 0;
            if (redniBrojevi)
            {
                foreach (var item in zauzeti)
                {
                    redniBroj++;
                    tijelo.Add(Tablica.ispisiRed(redniBroj.ToString(), item.Vrsta, item.Vrijeme, item.UkupanBrojZauzetih.ToString()));
                }
            }
            else
            {
                foreach (var item in zauzeti)
                {
                    tijelo.Add(Tablica.ispisiRed(item.Vrsta, item.Vrijeme, item.UkupanBrojZauzetih.ToString()));
                }
            }
            tijelo.Add(Tablica.ispisiPregradu());
            return tijelo;
        }

        public List<string> DodajTijeloTabliceStatistika(List<Brodovi> brodovi, bool redniBrojevi)
        {
            List<string> tijelo = new List<string>();
            tijelo.Add(Tablica.ispisiPregradu());
            int redniBroj = 0;
            if (redniBrojevi)
            {
                foreach (var item in brodovi)
                {
                    redniBroj++;
                    tijelo.Add(Tablica.ispisiRed(redniBroj.ToString(), item.Vrsta, item.Oznaka, item.UkupanBroj.ToString(), item.MaxBrzina.ToString()));
                }
            }
            else
            {
                foreach (var item in brodovi)
                {
                    tijelo.Add(Tablica.ispisiRed(item.Vrsta, item.Oznaka, item.UkupanBroj.ToString(), item.MaxBrzina.ToString()));
                }
            }
            tijelo.Add(Tablica.ispisiPregradu());
            return tijelo;
        }

        //Zaglavlja tablica

        public List<string> DodajZaglavljeTabliceIspisZS(bool redniBrojevi)
        {
            List<string> zaglavlje = new List<string>();
            zaglavlje.Add(Tablica.ispisiPregradu());
            zaglavlje.Add(Tablica.ispisiRed("Tablica: Ispis Zauzetih/Slobodnih vezova za dano razdoblje"));
            zaglavlje.Add(Tablica.ispisiPregradu());
            if (redniBrojevi)
            {
                string[] zaglavljeTablice = { "Rb.", "ID", "Oznaka veza", "Vrsta", "Status", "Zauzet Od", "Zauzet Do" };
                zaglavlje.Add(Tablica.ispisiRed(zaglavljeTablice));
            }
            else
            {
                string[] zaglavljeTablice = { "ID", "Oznaka veza", "Vrsta", "Status", "Zauzet Od", "Zauzet Do" };
                zaglavlje.Add(Tablica.ispisiRed(zaglavljeTablice));
            }
            return zaglavlje;
        }

        public List<string> DodajZaglavljeTabliceStatusi(bool redniBrojevi)
        {
            List<string> zaglavlje = new List<string>();
            zaglavlje.Add(Tablica.ispisiPregradu());
            zaglavlje.Add(Tablica.ispisiRed("Tablica: Pregled statusa vezova"));
            zaglavlje.Add(Tablica.ispisiPregradu());
            if (redniBrojevi)
            {
                string[] zaglavljeTablice = { "Rb.", "ID", "Oznaka veza", "Vrsta", "Status" };
                zaglavlje.Add(Tablica.ispisiRed(zaglavljeTablice));
            }
            else
            {
                string[] zaglavljeTablice = { "ID", "Oznaka veza", "Vrsta", "Status" };
                zaglavlje.Add(Tablica.ispisiRed(zaglavljeTablice));
            }
            return zaglavlje;
        }

        public List<string> DodajZaglavljeTabliceUkupniBrojZ(bool redniBrojevi)
        {
            List<string> zaglavlje = new List<string>();
            zaglavlje.Add(Tablica.ispisiPregradu());
            zaglavlje.Add(Tablica.ispisiRed("Tablica: Ukupan broj zauzetih vezova prema vrstama"));
            zaglavlje.Add(Tablica.ispisiPregradu());
            if (redniBrojevi)
            {
                string[] zaglavljeTablice = { "Rb.", "Vrsta", "Vrijeme", "Ukupan Broj zauzetih" };
                zaglavlje.Add(Tablica.ispisiRed(zaglavljeTablice));
            }
            else
            {
                string[] zaglavljeTablice = { "Vrsta", "Vrijeme", "Ukupan broj zauzetih" };
                zaglavlje.Add(Tablica.ispisiRed(zaglavljeTablice));
            }
            return zaglavlje;
        }

        public List<string> DodajZaglavljeTabliceStatistika(bool redniBrojevi, string kategorija)
        {
            List<string> zaglavlje = new List<string>();
            zaglavlje.Add(Tablica.ispisiPregradu());
            zaglavlje.Add(Tablica.ispisiRed($"Tablica: Statistika brodova za {kategorija} brodove"));
            zaglavlje.Add(Tablica.ispisiPregradu());
            if (redniBrojevi)
            {
                string[] zaglavljeTablice = { "Rb.", "Vrsta", "Oznaka", "Ukupan Broj", "Prosječna brzina" };
                zaglavlje.Add(Tablica.ispisiRed(zaglavljeTablice));
            }
            else
            {
                string[] zaglavljeTablice = { "Vrsta", "Oznaka", "Ukupan Broj", "Prosječna brzina" };
                zaglavlje.Add(Tablica.ispisiRed(zaglavljeTablice));
            }
            return zaglavlje;
        }
    }
}
