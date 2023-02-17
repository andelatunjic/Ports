
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using atunjic_zadaca_3.Enums;
using atunjic_zadaca_3.Modeli;

namespace atunjic_zadaca_3.Facade
{
    internal class UcitajVezove : Ucitaj, IUcitaj
    {
        List<string> pogreske = new List<string>();
        string greska = "";

        int brojacPutnickihVezova = 0;
        int brojacPoslovnihVezova = 0;
        int brojacOstalihVezova = 0;

        public bool provjeriValjanostRetka(string noviRed)
        {
            string[] polje = noviRed.Split(';');

            if (polje.Length != 7)
            {
                greske.brojGresaka += 1;
                greska = $"ERROR {greske.brojGresaka} : {noviRed} : Netočan format retka.";
                pogreske.Add(greska);
                return false;
            }
            else
            {
                if (provjeriPrirodanBroj(polje[0], noviRed) &&
                    provjeriOznakuVeza(polje[1].Trim(' '), noviRed) &&
                    provjeriVrstuVeza(polje[2].Trim(' '), noviRed) &&
                    provjeriPrirodanBroj(polje[3], noviRed) &&
                    provjeriPrirodanBroj(polje[4], noviRed) &&
                    provjeriPrirodanBroj(polje[5], noviRed) &&
                    provjeriDecimalanBroj(polje[6], noviRed))
                {
                    return true;
                }
            }
            return false;
        }

        public void spremiRed(string noviRed)
        {
            if (provjeriValjanostRetka(noviRed))
            {
                Vez noviVez = vratiObjekt(noviRed);
                if (!postojiDuplikat(noviVez, noviRed) && dimenzijeOdgovaraju(noviVez, noviRed))
                {
                    brodskaLuka.vezovi.Add(noviVez);
                }
            }
        }

        private bool dimenzijeOdgovaraju(Vez noviVez, string noviRed)
        {
            if(noviVez.MaksimalnaDubina <= brodskaLuka.luka.DubinaLuke)
            {
                return true;
            }
            else
            {
                greske.brojGresaka += 1;
                greska = $"ERROR {greske.brojGresaka} : {noviRed} : Dubina veza ne odgovara dubini luke.";
                pogreske.Add(greska);
                return false;
            }
        }

        private bool postojiDuplikat(Vez noviVez, string noviRed)
        {
            if (brodskaLuka.vezovi.Contains(noviVez))
            {
                greske.brojGresaka += 1;
                greska = $"ERROR {greske.brojGresaka} : {noviRed} : Vez već postoji.";
                pogreske.Add(greska);
                return true;
            }
            for (int i = 0; i < brodskaLuka.vezovi.Count; i++)
            {
                if (brodskaLuka.vezovi[i].Id == noviVez.Id)
                {
                    greske.brojGresaka++;
                    greska = $"ERROR {greske.brojGresaka} : {noviRed} : Zapis s ID-om {noviVez.Id} već postoji.";
                    pogreske.Add(greska);
                    return true;
                }
            }
            return false;
        }

        private Vez vratiObjekt(string red)
        {
            string[] polje = red.Split(';');

            int id = int.Parse(polje[0]);
            string oznakaVeza = polje[1].Trim(' ');
            string vrsta = polje[2].Trim(' ');
            int cijenaVezaPoSatu = int.Parse(polje[3]);
            float maksimalnaDuljina = float.Parse(polje[4]);
            float maksimalnaSirina = float.Parse(polje[5]);
            float maksimalnaDubina = float.Parse(polje[6]);

            Vez noviVez = new Vez(id, oznakaVeza, vrsta, cijenaVezaPoSatu, maksimalnaDuljina,
                maksimalnaSirina, maksimalnaDubina, null, StatusVeza.Slobodan);
            return noviVez;
        }

        public List<string> ucitaj(string nazivDatoteke)
        {
            try
            {
                bool zaglavlje = true;
                using (StreamReader sr = new StreamReader(nazivDatoteke))
                {
                    while (!sr.EndOfStream)
                    {
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
                Console.WriteLine($"ERROR 1: Problem s {nazivDatoteke}. Pokrenite ponovno sustav" +
                    $" i učitajte sve ispravne datoteke s ispravnim nazivom.");
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

        public bool provjeriOznakuVeza(string oznaka, string red)
        {
            string pOznaka = @"^[A-Z0-9ŠĐČĆŽ]{5}$";
            Match mOznaka = Regex.Match(oznaka, pOznaka, RegexOptions.IgnoreCase);

            if (mOznaka.Success) return true;
            else
            {
                greske.brojGresaka += 1;
                greska = $"ERROR {greske.brojGresaka} : {red} : Oznaka veza: (A-Z, 0-9) * 5.";
                pogreske.Add(greska);
                return false;
            }
        }

        public bool provjeriVrstuVeza(string vrsta, string red)
        {
            string pVrsta = @"^(PU|PO|OS){1}$";
            Match mVrsta = Regex.Match(vrsta, pVrsta, RegexOptions.IgnoreCase);

            if (mVrsta.Success) return true;
            else
            {
                greske.brojGresaka += 1;
                greska = $"ERROR {greske.brojGresaka} : {red} : Vrsta veza može biti PU, PO ili OS.";
                pogreske.Add(greska);
                return false;
            }
        }
    }
}
