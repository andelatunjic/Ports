
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
    class UcitajMolove : Ucitaj, IUcitaj
    {
        List<string> pogreske = new List<string>();
        string greska = "";

        public bool provjeriValjanostRetka(string noviRed)
        {
            string[] polje = noviRed.Split(';');

            if (polje.Length != 2)
            {
                greske.brojGresaka += 1;
                greska = $"ERROR {greske.brojGresaka} : {noviRed} : Netočan format retka.";
                pogreske.Add(greska);
                return false;
            }
            else
            {
                if (
                    provjeriPrirodanBroj(polje[0], noviRed) &&
                    provjeriNaziv(polje[1].Trim(' '), noviRed))
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
                MolComposite noviMol = vratiObjekt(noviRed);
                if (!postojiDuplikat(noviMol, noviRed))
                {
                    // Composite
                    brodskaLuka.luka.add(noviMol);

                    //Stara lista za lakše spajanje mol-vez kasnije
                    brodskaLuka.molovi.Add(vratiMol(noviRed));
                }
            }
        }

        private bool postojiDuplikat(MolComposite noviMol, string noviRed)
        {
            if (brodskaLuka.postojiMol(noviMol.Id))
            {
                greske.brojGresaka += 1;
                greska = $"ERROR {greske.brojGresaka} : {noviRed} : Mol već postoji.";
                pogreske.Add(greska);
                return true;
            }
            return false;
        }

        private MolComposite vratiObjekt(string red)
        {
            string[] polje = red.Split(';');

            int id = int.Parse(polje[0]);
            string naziv = polje[1].Trim(' ');

            MolComposite noviMol = new MolComposite(id, naziv);
            return noviMol;
        }

        private Mol vratiMol(string red)
        {
            string[] polje = red.Split(';');

            int id = int.Parse(polje[0]);
            string naziv = polje[1].Trim(' ');

            Mol noviMol = new Mol(id, naziv);
            return noviMol;
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
