
using atunjic_zadaca_3.Composite;
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
    internal class UcitajRezervacije : Ucitaj, IUcitaj
    {
        List<string> pogreske = new List<string>();
        string greska = "";

        public bool provjeriValjanostRetka(string noviRed)
        {
            string[] polje = noviRed.Split(';');

            if (polje.Length != 3)
            {
                greske.brojGresaka += 1;
                greska = $"ERROR {greske.brojGresaka} : {noviRed} : Netočan format retka.";
                pogreske.Add(greska);
                return false;
            }
            else
            {
                if (provjeriPrirodanBroj(polje[0], noviRed) &&
                    provjeriDatum(polje[1].Trim(' '), noviRed) &&
                    provjeriPrirodanBroj(polje[2], noviRed))
                {
                    if (!brodskaLuka.postojiBrod(int.Parse(polje[0])))
                    {
                        greske.brojGresaka += 1;
                        greska = $"ERROR {greske.brojGresaka} : {noviRed} : Brod s ID-om {polje[0]} ne postoji.";
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
                ZahtjevRezervacije novaRezervacija = vratiObjekt(noviRed);
                VezLeaf odabraniVez = null;
                List<Vez> potencijalniVezovi = new List<Vez>();

                string trenutniDan = novaRezervacija.DatumVrijemeOd.DayOfWeek.ToString();
                string oznakaTrenutnogDana = pronadiOznakuDana(trenutniDan);

                foreach (Vez vez in brodskaLuka.vezovi.Where(vez =>
                vez.MaksimalnaDubina >= novaRezervacija.Brod.Gaz &&
                vez.MaksimalnaSirina >= novaRezervacija.Brod.Sirina &&
                vez.MaksimalnaDuljina >= novaRezervacija.Brod.Duljina))
                {
                    if (vezPrimaVrstuBroda(novaRezervacija.Brod.Vrsta, vez.Vrsta))
                    {
                        potencijalniVezovi.Add(vez);
                    }
                }

                foreach (Vez v in potencijalniVezovi)
                {
                    MolComposite mol = pronadiMol(v.Mol.Id);
                    VezLeaf vez = new VezLeaf(v.Id, v.OznakaVeza, v.Vrsta, v.CijenaVezaPoSatu, v.MaksimalnaDuljina, v.MaksimalnaSirina, v.MaksimalnaDubina, mol, v.StatusVeza);
                    Raspored noviZapisRasporeda = new Raspored(vez, novaRezervacija.Brod, oznakaTrenutnogDana,
                        novaRezervacija.DatumVrijemeOd,
                        novaRezervacija.DatumVrijemeOd.AddHours(novaRezervacija.TrajanjePriveza));
                    if (!brodJeVecSvezan(noviZapisRasporeda) && !vezJePopunjen(noviZapisRasporeda))
                    {
                        odabraniVez = vez;
                        break;
                    }
                }

                if (odabraniVez != null)
                {
                    Raspored noviZapisRasporeda = new Raspored(odabraniVez, novaRezervacija.Brod, oznakaTrenutnogDana,
                        novaRezervacija.DatumVrijemeOd,
                        novaRezervacija.DatumVrijemeOd.AddHours(novaRezervacija.TrajanjePriveza));
                    if (!brodskaLuka.raspored.Contains(noviZapisRasporeda))
                        brodskaLuka.raspored.Add(noviZapisRasporeda);
                    else
                    {
                        greske.brojGresaka += 1;
                        greska = $"ERROR {greske.brojGresaka} : {noviRed} : Zahtjev je već odobren.";
                        pogreske.Add(greska);
                    }
                }
            }
        }

        private bool vezPrimaVrstuBroda(string vrstaBroda, string vrstaVeza)
        {
            string[] vrsteZaPutnickiVez = { "TR", "KA", "KL", "KR" };
            string[] vrsteZaPoslovniVez = { "RI", "TE" };
            string[] vrsteZaOstaleVez = { "JA", "BR", "RO" };

            switch (vrstaVeza)
            {
                case "PU":
                    if (vrsteZaPutnickiVez.Contains(vrstaBroda)) return true;
                    else return false;
                case "PO":
                    if (vrsteZaPoslovniVez.Contains(vrstaBroda)) return true;
                    else return false;
                case "OS":
                    if (vrsteZaOstaleVez.Contains(vrstaBroda)) return true;
                    else return false;
                default:
                    return false;
            }
        }

        private ZahtjevRezervacije vratiObjekt(string red)
        {
            string[] polje = red.Split(';');

            Brod brod = brodskaLuka.brodovi.Find(x => x.Id == int.Parse(polje[0]));
            DateTime.TryParse(polje[1], CultureInfo.CurrentCulture, DateTimeStyles.AssumeLocal,
                out DateTime vrijemeOd);
            int trajanjePriveza = int.Parse(polje[2]);

            ZahtjevRezervacije noviZahtjev = new ZahtjevRezervacije(brod, vrijemeOd, trajanjePriveza);
            return noviZahtjev;
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
                Console.WriteLine($"ERROR 1: Problem s {nazivDatoteke}. Pokusajte ponovno.");
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

        public bool provjeriDatum(string vrijeme, string red)
        {
            string pFormatVrijeme = @"^[0-3]\d.[0-1]\d.[1-2]\d\d\d. [0-2]\d:[0-6]\d:[0-6]\d$";
            Match mFormatVrijeme = Regex.Match(vrijeme, pFormatVrijeme, RegexOptions.IgnoreCase);

            bool provjera = DateTime.TryParse(vrijeme, CultureInfo.CurrentCulture, DateTimeStyles.AssumeLocal,
               out DateTime vrijemeOd);

            if (provjera && mFormatVrijeme.Success) return true;
            else
            {
                greske.brojGresaka += 1;
                greska = $"ERROR {greske.brojGresaka} : {red} : Format datuma nije dobar.";
                pogreske.Add(greska);
                return false;
            }
        }
    }
}
