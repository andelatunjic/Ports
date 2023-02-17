
using atunjic_zadaca_3.Composite;
using atunjic_zadaca_3.Composite.Iterator;
using atunjic_zadaca_3.Modeli;
using atunjic_zadaca_3.Tablice.TabliceModeli;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace atunjic_zadaca_3.Facade
{
    internal class UcitajMolVezove : Ucitaj, IUcitaj
    {
        List<string> pogreske = new List<string>();
        string greska = "";

        int brojacPutnickihVezova = 0;
        int brojacPoslovnihVezova = 0;
        int brojacOstalihVezova = 0;

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
                if (provjeriPrirodanBroj(polje[0], noviRed) &&
                    provjeriVezoveUMolu(polje[1].Trim(' '), noviRed))
                {
                    if (!brodskaLuka.postojiMol(int.Parse(polje[0])))
                    {
                        greske.brojGresaka += 1;
                        greska = $"ERROR {greske.brojGresaka} : {noviRed} : Mol s ID-om {polje[0]} ne postoji.";
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
                string[] poljeVezova = polje[1].Split(',');

                for (int i = 0; i < poljeVezova.Length; i++)
                {
                    Vez vez = brodskaLuka.vezovi.Find(x => x.Id.Equals(int.Parse(poljeVezova[i])));
                    if (vez != null)
                    {
                        dodijeliVezuMol(int.Parse(polje[0]), int.Parse(poljeVezova[i]), noviRed);
                    }
                    else
                    {
                        greske.brojGresaka += 1;
                        greska = $"ERROR {greske.brojGresaka} : {noviRed} : Vez s ID-om {poljeVezova[i]} ne postoji.";
                        pogreske.Add(greska);
                    }
                }
            }
        }

        private void dodijeliVezuMol(int molId, int vezId, string noviRed)
        {
            Vez trenutniVez = brodskaLuka.vezovi.Find(x => x.Id.Equals(vezId));
            if (trenutniVez.Mol == null)
            {
                if(provjeriKapacitetLuke(trenutniVez, noviRed))
                {
                    trenutniVez.Mol = brodskaLuka.molovi.Find(x => x.Id.Equals(molId));
                    
                    //Composite
                    MolComposite mol = pronadiMol(molId);
                    VezLeaf noviLeaf = new VezLeaf(trenutniVez.Id, trenutniVez.OznakaVeza, trenutniVez.Vrsta, trenutniVez.CijenaVezaPoSatu, trenutniVez.MaksimalnaDuljina, trenutniVez.MaksimalnaSirina, trenutniVez.MaksimalnaDubina, mol, Enums.StatusVeza.Slobodan);
                    mol.add(noviLeaf);
                }
            }
            else
            {
                greske.brojGresaka += 1;
                greska = $"ERROR {greske.brojGresaka} : {noviRed} : Vez s ID-om {vezId} već pripada molu {trenutniVez.Mol.Id}.";
                pogreske.Add(greska);
            }
        }

        private bool provjeriKapacitetLuke(Vez trenutniVez, string noviRed)
        {
            string vrstaVeza = trenutniVez.Vrsta;

            switch (vrstaVeza)
            {
                case "PU":
                    {
                        brojacPutnickihVezova++;
                        if (brojacPutnickihVezova <= brodskaLuka.luka.UkupniBrojPutnickihVezova)
                        {
                            return true;
                        }
                        else
                        {
                            greske.brojGresaka += 1;
                            greska = $"ERROR {greske.brojGresaka} : {noviRed} : Kapacitet putnickih vezova je popunjen.";
                            pogreske.Add(greska);
                            return false;
                        }
                    }
                case "PO":
                    {
                        brojacPoslovnihVezova++;
                        if (brojacPoslovnihVezova <= brodskaLuka.luka.UkupniBrojPoslovnihVezova)
                        {
                            return true;
                        }
                        else
                        {
                            greske.brojGresaka += 1;
                            greska = $"ERROR {greske.brojGresaka} : {noviRed} : Kapacitet poslovnih vezova je popunjen.";
                            pogreske.Add(greska);
                            return false;
                        }
                    }
                case "OS":
                    {
                        brojacOstalihVezova++;
                        if (brojacOstalihVezova <= brodskaLuka.luka.UkupniBrojOstalihVezova)
                        {
                            return true;
                        }
                        else
                        {
                            greske.brojGresaka += 1;
                            greska = $"ERROR {greske.brojGresaka} : {noviRed} : Kapacitet ostalih vezova je popunjen.";
                            pogreske.Add(greska);
                            return false;
                        }
                    }
                default: { return false; }
            }
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

            izbrisiVezoveBezMola();
            return pogreske;
        }

        private void izbrisiVezoveBezMola()
        {
            greske.brojGresaka++;
            string uklonjeniVezovi = $"ERROR {greske.brojGresaka} : Vezovi:";
           
            List<Vez> vezoviBezMola = brodskaLuka.vezovi.FindAll(vez => vez.Mol == null);
            foreach (var item in vezoviBezMola)
            {
                uklonjeniVezovi += $" {item.Id},";
                brodskaLuka.vezovi.Remove(item);
            }
            uklonjeniVezovi += " su uklonjeni jer nemaju dodijeljen mol.";
            pogreske.Add(uklonjeniVezovi);
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

        public bool provjeriVezoveUMolu(string vezovi, string red)
        {
            string pVezoviUMolu = @"^\d+(,\d+){0,}$";
            Match mVezoviUMolu = Regex.Match(vezovi, pVezoviUMolu, RegexOptions.IgnoreCase);

            string[] poljeVezova = vezovi.Split(',');
            if (poljeVezova.Length != poljeVezova.Distinct().Count() || !mVezoviUMolu.Success)
            {
                greske.brojGresaka += 1;
                greska = $"ERROR {greske.brojGresaka} : {red} : Duplikati u nabrojanim vezovima ili nepravilan format.";
                pogreske.Add(greska);
                return false;
            }
            return true;
        }
    }
}
