
using atunjic_zadaca_3.Composite;
using atunjic_zadaca_3.Composite.Iterator;
using atunjic_zadaca_3.Modeli;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace atunjic_zadaca_3.Facade
{
    internal class UcitajRaspored : Ucitaj, IUcitaj
    {
        List<string> pogreske = new List<string>();
        string greska = "";

        public bool provjeriValjanostRetka(string noviRed)
        {
            string[] polje = noviRed.Split(';');

            if (polje.Length != 5)
            {
                greske.brojGresaka += 1;
                greska = $"ERROR {greske.brojGresaka} : {noviRed} : Netočan format retka.";
                pogreske.Add(greska);
                return false;
            }
            else
            {
                if (provjeriPrirodanBroj(polje[0], noviRed) &&
                    provjeriPrirodanBroj(polje[1], noviRed) &&
                    provjeriDaneUTjednu(polje[2].Trim(' '), noviRed) &&
                    provjeriVrijeme(polje[3].Trim(' '), noviRed) &&
                    provjeriVrijeme(polje[4].Trim(' '), noviRed))
                {
                    if (!brodskaLuka.postojiVez(int.Parse(polje[0])))
                    {
                        greske.brojGresaka += 1;
                        greska = $"ERROR {greske.brojGresaka} : {noviRed} : Vez s ID-om {polje[0]} ne postoji.";
                        pogreske.Add(greska);
                        return false;
                    }
                    if (!brodskaLuka.postojiBrod(int.Parse(polje[1])))
                    {
                        greske.brojGresaka += 1;
                        greska = $"ERROR {greske.brojGresaka} : {noviRed} : Brod s ID-om {polje[1]} ne postoji.";
                        pogreske.Add(greska);
                        return false;
                    }
                    string[] poljeDana = polje[2].Split(',');
                    if (poljeDana.Length != poljeDana.Distinct().Count())
                    {
                        greske.brojGresaka += 1;
                        greska = $"ERROR {greske.brojGresaka} : {noviRed} : Dani u tjednu imaju duplikate.";
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
                Raspored noviZapisRasporeda = vratiObjekt(noviRed);
                if (!postojiProblem(noviZapisRasporeda, noviRed))
                {
                    brodskaLuka.raspored.Add(noviZapisRasporeda);
                }
            }
        }

        private bool postojiProblem(Raspored noviZapisRasporeda, string noviRed)
        {
            if (brodskaLuka.raspored.Contains(noviZapisRasporeda))
            {
                greske.brojGresaka += 1;
                greska = $"ERROR {greske.brojGresaka} : {noviRed} : Zapis u rasporedu već postoji.";
                pogreske.Add(greska);
                return true;
            }
            for (int i = 0; i < brodskaLuka.raspored.Count; i++)
            {
                if (brodskaLuka.raspored[i].Vez.Id == noviZapisRasporeda.Vez.Id &&
                brodskaLuka.raspored[i].Brod.Id == noviZapisRasporeda.Brod.Id &&
                brodskaLuka.raspored[i].VrijemeOd == noviZapisRasporeda.VrijemeOd &&
                    brodskaLuka.raspored[i].VrijemeDo == noviZapisRasporeda.VrijemeDo)
                {
                    greske.brojGresaka += 1;
                    greska = $"ERROR {greske.brojGresaka} : {noviRed} : Zapis za vez:" +
                        $" {noviZapisRasporeda.Vez.Id} i brod: {noviZapisRasporeda.Brod.Id} u vremenskom intervalu" +
                        $" od {noviZapisRasporeda.VrijemeOd} do {noviZapisRasporeda.VrijemeDo} već postoji.";
                    pogreske.Add(greska);
                    return true;
                }
            }
            if (!pravilnaVrstaBrodaIVeza(noviZapisRasporeda))
            {
                greske.brojGresaka += 1;
                greska = $"ERROR {greske.brojGresaka} : {noviRed} : Vez {noviZapisRasporeda.Vez.Vrsta}" +
                    $" ne prima brod {noviZapisRasporeda.Brod.Vrsta}.";
                pogreske.Add(greska);
                return true;
            }
            if (!dimenzijeOdgovaraju(noviZapisRasporeda))
            {
                greske.brojGresaka += 1;
                greska = $"ERROR {greske.brojGresaka} : {noviRed} : Brod" +
                    $" {noviZapisRasporeda.Brod.OznakaBroda} ne odgovara dimenzijama veza.";
                pogreske.Add(greska);
                return true;
            }
            if (vezJePopunjen(noviZapisRasporeda))
            {
                greske.brojGresaka += 1;
                greska = $"ERROR {greske.brojGresaka} : {noviRed} : Na vezu" +
                    $" {noviZapisRasporeda.Vez.OznakaVeza} je već u to vrijeme svezan brod.";
                pogreske.Add(greska);
                return true;
            }
            if (brodJeVecSvezan(noviZapisRasporeda))
            {
                greske.brojGresaka += 1;
                greska = $"ERROR {greske.brojGresaka} : {noviRed} : Brod " +
                    $"{noviZapisRasporeda.Brod.Naziv} ne može biti svezan u traženo vrijeme jer je " +
                    $"svezan na drugom vezu.";
                pogreske.Add(greska);
                return true;
            }
            if (noviZapisRasporeda.VrijemeOd > noviZapisRasporeda.VrijemeDo)
            {
                greske.brojGresaka += 1;
                greska = $"ERROR {greske.brojGresaka} : {noviRed} : Vrijeme Od je vece nego Vrijeme Do.";
                pogreske.Add(greska);
                return true;
            }
            return false;
        }

        private bool pravilnaVrstaBrodaIVeza(Raspored noviZapisRasporeda)
        {
            switch (noviZapisRasporeda.Vez.Vrsta)
            {
                case "PU":
                    if (noviZapisRasporeda.Brod.Vrsta == "TR" ||
                        noviZapisRasporeda.Brod.Vrsta == "KA" ||
                        noviZapisRasporeda.Brod.Vrsta == "KL" ||
                        noviZapisRasporeda.Brod.Vrsta == "KR") return true;
                    else return false;
                case "PO":
                    if (noviZapisRasporeda.Brod.Vrsta == "RI" ||
                        noviZapisRasporeda.Brod.Vrsta == "TE") return true;
                    else return false;
                case "OS":
                    if (noviZapisRasporeda.Brod.Vrsta == "JA" ||
                        noviZapisRasporeda.Brod.Vrsta == "BR" ||
                        noviZapisRasporeda.Brod.Vrsta == "RO") return true;
                    else return false;
                default: return false;
            }
        }

        private bool dimenzijeOdgovaraju(Raspored noviZapisRasporeda)
        {
            if (noviZapisRasporeda.Vez.MaksimalnaSirina >= noviZapisRasporeda.Brod.Sirina &&
                noviZapisRasporeda.Vez.MaksimalnaDuljina >= noviZapisRasporeda.Brod.Duljina &&
                noviZapisRasporeda.Vez.MaksimalnaDubina >= noviZapisRasporeda.Brod.Gaz)
            {
                return true;
            }
            return false;
        }


        private Raspored vratiObjekt(string red)
        {
            string[] polje = red.Split(';');

            VezLeaf vez = vratiVez(int.Parse(polje[0]));
            Brod brod = brodskaLuka.brodovi.Find(x => x.Id == int.Parse(polje[1]));
            string dani_u_tjednu = polje[2];
            DateTime.TryParse(polje[3], CultureInfo.CurrentCulture, DateTimeStyles.AssumeLocal,
                out DateTime vrijemeOd);
            DateTime.TryParse(polje[4], CultureInfo.CurrentCulture, DateTimeStyles.AssumeLocal,
                out DateTime vrijemeDo);
            Raspored noviZapisRasporeda = new Raspored(vez, brod, dani_u_tjednu, vrijemeOd, vrijemeDo);
            return noviZapisRasporeda;
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

        public bool provjeriVrijeme(string vrijeme, string red)
        {
            string pFormatVrijeme = @"^[0-2]\d:[0-6]\d$";
            Match mFormatVrijeme = Regex.Match(vrijeme, pFormatVrijeme, RegexOptions.IgnoreCase);

            bool provjera = DateTime.TryParse(vrijeme, CultureInfo.CurrentCulture, DateTimeStyles.AssumeLocal,
               out DateTime vrijemeOd);

            if (provjera && mFormatVrijeme.Success) return true;
            else
            {
                greske.brojGresaka += 1;
                greska = $"ERROR {greske.brojGresaka} : {red} : Format vremena nije dobar.";
                pogreske.Add(greska);
                return false;
            }
        }

        public bool provjeriDaneUTjednu(string dani, string red)
        {
            string pDaniUTjednu = @"^[0-6]{1}(,[0-6]){0,6}$";
            Match mDaniUTjednu = Regex.Match(dani, pDaniUTjednu, RegexOptions.IgnoreCase);

            if (mDaniUTjednu.Success) return true;
            else
            {
                greske.brojGresaka += 1;
                greska = $"ERROR {greske.brojGresaka} : {red} : Dani u tjednu mogu biti od 0-6 odvojeni zarezom.";
                pogreske.Add(greska);
                return false;
            }
        }
    }
}
