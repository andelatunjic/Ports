
using atunjic_zadaca_3.Modeli;
using atunjic_zadaca_3.ObserverKapetanija;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace atunjic_zadaca_3.Facade
{
    internal class UcitajKanale : Ucitaj, IUcitaj
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
                if (
                    provjeriPrirodanBroj(polje[0], noviRed) &&
                    provjeriPrirodanBroj(polje[1], noviRed) &&
                    provjeriPrirodanBroj(polje[2], noviRed))
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
                LuckaKapetanija noviKanal = vratiObjekt(noviRed);

                if (!postojiDuplikat(noviKanal, noviRed) && !postojiFrekvencija(noviKanal, noviRed))
                {
                    brodskaLuka.kanali.Add(noviKanal);
                }
            }
        }

        private bool postojiFrekvencija(LuckaKapetanija noviKanal, string noviRed)
        {
            string[] polje = noviRed.Split(';');
            LuckaKapetanija kanal = brodskaLuka.kanali.Find(x => x.Frekvencija == int.Parse(polje[1]));
            if (kanal == null)
            {
                return false;
            }
            else
            {
                greske.brojGresaka += 1;
                greska = $"ERROR {greske.brojGresaka} : {noviRed} : Kanal s frekvencijom {polje[1]} već postoji.";
                pogreske.Add(greska);
                return true;
            }
        }

        private bool postojiDuplikat(LuckaKapetanija noviKanal, string noviRed)
        {
            if (brodskaLuka.kanali.Contains(noviKanal))
            {
                greske.brojGresaka += 1;
                greska = $"ERROR {greske.brojGresaka} : {noviRed} : Kanal već postoji.";
                pogreske.Add(greska);
                return true;
            }
            for (int i = 0; i < brodskaLuka.kanali.Count; i++)
            {
                if (brodskaLuka.kanali[i].Id == noviKanal.Id)
                {
                    greske.brojGresaka += 1;
                    greska = $"ERROR {greske.brojGresaka} : {noviRed} : Zapis s ID-om {noviKanal.Id} već postoji.";
                    pogreske.Add(greska);
                    return true;
                }
            }
            return false;
        }

        private LuckaKapetanija vratiObjekt(string red)
        {
            string[] polje = red.Split(';');

            int id = int.Parse(polje[0]);
            int frekvencija = int.Parse(polje[1]);
            int maksimalanBroj = int.Parse(polje[2]);

            LuckaKapetanija noviKanal = new LuckaKapetanija(id, frekvencija, maksimalanBroj);
            return noviKanal;
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
    }
}
