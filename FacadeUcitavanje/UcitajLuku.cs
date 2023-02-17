using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace atunjic_zadaca_3.Facade
{
    internal class UcitajLuku : Ucitaj, IUcitaj
    {
        List<string> pogreske = new List<string>();
        string greska = "";

        public bool provjeriValjanostRetka(string noviRed)
        {
            string[] polje = noviRed.Split(';');

            if (polje.Length != 8)
            {
                greske.brojGresaka += 1;
                greska = $"ERROR {greske.brojGresaka} : {noviRed} : Netočan format retka.";
                pogreske.Add(greska);
                return false;
            }
            else
            {
                if (provjeriNaziv(polje[0].Trim(' '), noviRed) &&
                    provjeriDecimalanBroj(polje[1], noviRed) &&
                    provjeriDecimalanBroj(polje[2], noviRed) &&
                    provjeriDecimalanBroj(polje[3], noviRed) &&
                    provjeriPrirodanBroj(polje[4], noviRed) &&
                    provjeriPrirodanBroj(polje[5], noviRed) &&
                    provjeriPrirodanBroj(polje[6], noviRed) &&
                    provjeriDatum(polje[7].Trim(' '), noviRed))
                {
                    if (int.Parse(polje[4]) == 0 && int.Parse(polje[5]) == 0 && int.Parse(polje[6]) == 0)
                    {
                        greske.brojGresaka += 1;
                        greska = $"ERROR {greske.brojGresaka} : {noviRed} : Sva tri kapaciteta ne mogu biti 0.";
                        pogreske.Add(greska);
                        return false;
                    }
                    return true;
                }
            }
            return false;
        }

        public void spremiRed(string noviRed)
        {
            if (provjeriValjanostRetka(noviRed))
            {
                string[] polje = noviRed.Split(';');
                
                // Composite
                string naziv = polje[0].Trim(' ');
                double sirina = double.Parse(polje[1]);
                double visina = double.Parse(polje[2]);
                double dubina = double.Parse(polje[3]);
                int putnicki = int.Parse(polje[4]);
                int poslovni = int.Parse(polje[5]);
                int ostali = int.Parse(polje[6]);

                brodskaLuka.luka = new Composite.BrodskaLukaComposite(naziv, sirina, visina, dubina, putnicki, poslovni, ostali);

                DateTime.TryParse(polje[7], CultureInfo.CurrentCulture, DateTimeStyles.AssumeLocal,
                    out DateTime datum);
                virtualnoVrijeme.VirtualnoVrijeme = datum;
            }
        }

        public List<string> ucitaj(string nazivDatoteke)
        {
            try
            {
                bool zaglavlje = true;
                using (StreamReader sr = new StreamReader(nazivDatoteke))
                {
                    int brojac = 0;
                    while (brojac != 2)
                    {
                        brojac++;
                        string red = sr.ReadLine();
                        if (!zaglavlje)
                        {
                            spremiRed(red);
                        }
                        else
                        {
                            zaglavlje = false;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERROR 1: Datoteka s nazivom {nazivDatoteke} ne postoji. Pokrenite ponovno sustav" +
                    $" i učitajte sve datoteke s ispravnim nazivom.");
                Environment.Exit(0);
            }

            return pogreske;
        }

        //Provjere---------------------------------------------------------------------------------------------------------------
        public bool provjeriPrirodanBroj(string broj, string red)
        {
            bool provjera = int.TryParse(broj, out int prirodanBroj);

            if (provjera && prirodanBroj >= 0) return true;
            else
            {
                greske.brojGresaka += 1;
                greska = $"ERROR {greske.brojGresaka} : {red} : Prirodan broj nije dobar.";
                pogreske.Add(greska);
                return false;
            }
        }

        public bool provjeriDecimalanBroj(string broj, string red)
        {
            string pravilanFormat = broj.Replace('.', ',');

            bool provjera = float.TryParse(pravilanFormat, out float decimalniBroj);

            if (provjera && decimalniBroj >= 0) return true;
            else
            {
                greske.brojGresaka += 1;
                greska = $"ERROR {greske.brojGresaka} : {red} : Decimalni broj nije dobar.";
                pogreske.Add(greska);
                return false;
            }
        }

        public bool provjeriDatum(string vrijeme, string red)
        {
            string pFormatVrijeme = @"^[0-3]\d.[0-1]\d.[1-2]\d\d\d. [0-2]\d:[0-6]\d:[0-6]\d$";
            Match mFormatVrijeme = Regex.Match(vrijeme, pFormatVrijeme, RegexOptions.IgnoreCase);

            bool provjera = DateTime.TryParse(vrijeme, CultureInfo.CurrentCulture, DateTimeStyles.AssumeLocal,
               out DateTime vrijemeOd);

            if (provjera && mFormatVrijeme.Success) return true;
            else
                greske.brojGresaka += 1;
                greska = $"ERROR {greske.brojGresaka} : {red} : Format datuma nije dobar.";
                pogreske.Add(greska);
                return false;
        }

        public bool provjeriNaziv(string naziv, string red)
        {
            string pNaziv = @"^[A-Za-z šđčćžŠĐČĆŽ]{2,}$";
            Match mNaziv = Regex.Match(naziv, pNaziv, RegexOptions.IgnoreCase);

            if (mNaziv.Success) return true;
            else
            {
                greske.brojGresaka += 1;
                greska = $"ERROR {greske.brojGresaka} : {red} : Naziv: min 2 znaka (A-Z, a-z).";
                pogreske.Add(greska);
                return false;
            }
        }
    }
}
