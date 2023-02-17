
using atunjic_zadaca_3.Modeli;
using System.Globalization;
using System.Text.RegularExpressions;

namespace atunjic_zadaca_3.Facade
{
    internal class UcitajBrodove : Ucitaj, IUcitaj
    {
        List<string> pogreske = new List<string>();
        string greska = "";

        public bool provjeriValjanostRetka(string noviRed)
        {
            string[] polje = noviRed.Split(';');

            if (polje.Length != 11)
            {
                greske.brojGresaka += 1;
                greska = $"ERROR {greske.brojGresaka} : {noviRed} : Netočan format retka.";
                pogreske.Add(greska);
                return false;
            }
            else
            {
                if (provjeriPrirodanBroj(polje[0], noviRed) &&
                    provjeriOznakuBroda(polje[1].Trim(' '), noviRed) &&
                    provjeriNaziv(polje[2].Trim(' '), noviRed) &&
                    provjeriVrstuBroda(polje[3].Trim(' '), noviRed) &&
                    provjeriDecimalanBroj(polje[4], noviRed) &&
                    provjeriDecimalanBroj(polje[5], noviRed) &&
                    provjeriDecimalanBroj(polje[6], noviRed) &&
                    provjeriDecimalanBroj(polje[7], noviRed) &&
                    provjeriPrirodanBroj(polje[8], noviRed) &&
                    provjeriPrirodanBroj(polje[9], noviRed) &&
                    provjeriPrirodanBroj(polje[10], noviRed))

                {
                    if (int.Parse(polje[8]) == 0 && int.Parse(polje[9]) == 0 && int.Parse(polje[10]) == 0)
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
                Brod noviBrod = vratiObjekt(noviRed);
                if (!postojiDuplikat(noviBrod, noviRed))
                {
                    brodskaLuka.brodovi.Add(noviBrod);
                }
            }
        }

        private bool postojiDuplikat(Brod noviBrod, string noviRed)
        {
            if (brodskaLuka.brodovi.Contains(noviBrod))
            {
                greske.brojGresaka += 1;
                greska = $"ERROR {greske.brojGresaka} : {noviRed} : Brod već postoji.";
                pogreske.Add(greska);
                return true;
            }
            for (int i = 0; i < brodskaLuka.brodovi.Count; i++)
            {
                if (brodskaLuka.brodovi[i].Id == noviBrod.Id)
                {
                    greske.brojGresaka += 1;
                    greska = $"ERROR {greske.brojGresaka} : {noviRed} : Zapis s ID-om {noviBrod.Id} već postoji.";
                    pogreske.Add(greska);
                    return true;
                }
            }
            return false;
        }

        private Brod vratiObjekt(string red)
        {
            string[] polje = red.Split(';');

            int id = int.Parse(polje[0]);
            string oznakaBroda = polje[1].Trim(' ');
            string naziv = polje[2].Trim(' ');
            string vrsta = polje[3].Trim(' ');
            float duljina = float.Parse(polje[4]);
            float sirina = float.Parse(polje[5]);
            float gaz = float.Parse(polje[6]);
            float maksimalnaBrzina = float.Parse(polje[7].Replace(',', '.'));
            int kapacitetPutnika = int.Parse(polje[8]);
            int kapacitetOsobnihVozila = int.Parse(polje[9]);
            int kapacitetTereta = int.Parse(polje[10]);

            Brod noviBrod = new Brod(id, oznakaBroda, naziv, vrsta, duljina, sirina, gaz, maksimalnaBrzina,
                kapacitetPutnika, kapacitetOsobnihVozila, kapacitetTereta);
            return noviBrod;
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

        public bool provjeriOznakuBroda(string oznaka, string red)
        {
            string pOznakaBroda = @"^[A-ZŠĐČĆŽ]{5}$";
            Match mOznakaBroda = Regex.Match(oznaka, pOznakaBroda, RegexOptions.IgnoreCase);

            if (mOznakaBroda.Success) return true;
            else
            {
                greske.brojGresaka += 1;
                greska = $"ERROR {greske.brojGresaka} : {red} : Oznaka broda: (A-Z) * 5.";
                pogreske.Add(greska);
                return false;
            }
        }

        public bool provjeriVrstuBroda(string vrsta, string red)
        {
            string pVrsta = @"^(TR|KA|KL|KR|RI|TE|JA|BR|RO){1}$";
            Match mVrsta = Regex.Match(vrsta, pVrsta, RegexOptions.IgnoreCase);

            if (mVrsta.Success) return true;
            else
            {
                greske.brojGresaka += 1;
                greska = $"ERROR {greske.brojGresaka} : {red} : Vrsta broda može biti TR, KA, KL, KR, RI, TE, JA, BR ili RO.";
                pogreske.Add(greska);
                return false;
            }
        }
    }
}
