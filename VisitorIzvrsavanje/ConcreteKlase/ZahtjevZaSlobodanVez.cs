
using atunjic_zadaca_3.Composite;
using atunjic_zadaca_3.Modeli;
using atunjic_zadaca_3.ObserverKapetanija;
using atunjic_zadaca_3.VisitorIzvrsavanje;
using atunjic_zadaca_3.VisitorIzvrsavanje.ConcreteKlase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atunjic_zadaca_3.VisitorIzvrsavanje.ConcreteKlase
{
    //ConcreteProduct
    public class ZahtjevZaSlobodanVez : ZajednickeMetode, IAktivnost
    {
        LuckaKapetanija trenutniKanal;

        public void prihvati(IAktivnostVisitor aktivnostVisitor)
        {
            aktivnostVisitor.posjeti(this);
        }

        public void izvrsi(string naredba)
        {
            if (brodskaLuka.radniPodaci.Count > 0) brodskaLuka.radniPodaci.Clear();
            if (brodskaLuka.pogreskePodaci.Count > 0) brodskaLuka.pogreskePodaci.Clear();

            string[] polje = naredba.Split(' ');
            int IdBroda = int.Parse(polje[1]);
            int Trajanje = int.Parse(polje[2]);
            
            DateTime trenutnoVrijeme = DateTime.Now.Subtract(new TimeSpan(vrijeme.RazlikaVremenaTicks));

            Brod brod = brodskaLuka.brodovi.Find(x => x.Id == IdBroda);

            if (brod == null)
            {
                greske.brojGresaka += 1;
                brodskaLuka.pogreskePodaci.Add($"ERROR {greske.brojGresaka}: Brod s ID-om {IdBroda} ne postoji.");
            }
            else
            {
                if (brodJePretplacen(brod))
                {
                    string poruka = "";

                    ZahtjevRezervacije novaRezervacija = new ZahtjevRezervacije(brod, trenutnoVrijeme, Trajanje);
                    string trenutniDan = novaRezervacija.DatumVrijemeOd.DayOfWeek.ToString();
                    string oznakaTrenutnogDana = pronadiOznakuDana(trenutniDan);

                    VezLeaf odabraniVez = null;
                    List<Vez> potencijalniVezovi = vratiPotencijalneVezove(novaRezervacija);

                    foreach (Vez v in potencijalniVezovi)
                    {
                        VezLeaf vez = vratiVez(v.Id);
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
                        {
                            poruka = "Zahtjev je odobren.";
                            brodskaLuka.raspored.Add(noviZapisRasporeda);
                        }
                        else
                        {
                            greske.brojGresaka += 1;
                            brodskaLuka.pogreskePodaci.Add($"ERROR {greske.brojGresaka} : Ovaj zahtjev je već odobren.");
                        }
                    }
                    else
                    {
                        poruka = "Zahtjev je odbijen.";
                    }
                    brodskaLuka.radniPodaci.Add(poruka);
                    trenutniKanal.Notify(poruka);
                    //Dodaj u dnevnik
                }
                else
                {
                    greske.brojGresaka += 1;
                    brodskaLuka.pogreskePodaci.Add($"ERROR {greske.brojGresaka} : Brod nije u komunikaciji s lučkom kapetanijom.");
                }
            }
        }

        private List<Vez> vratiPotencijalneVezove(ZahtjevRezervacije novaRezervacija)
        {
            List<Vez> lista = new List<Vez>();
            foreach (Vez vez in brodskaLuka.vezovi.Where(vez =>
                    vez.MaksimalnaDubina >= novaRezervacija.Brod.Gaz &&
                    vez.MaksimalnaSirina >= novaRezervacija.Brod.Sirina &&
                    vez.MaksimalnaDuljina >= novaRezervacija.Brod.Duljina))
            {
                if (vezPrimaVrstuBroda(novaRezervacija.Brod.Vrsta, vez.Vrsta))
                {
                    lista.Add(vez);
                }
            }
            return lista;
        }

        private bool brodJePretplacen(Brod brod)
        {
            if(brod != null)
            {
                foreach (var item in brodskaLuka.kanali)
                {
                    if (item.pretplacen(brod)) 
                    {
                        trenutniKanal = item;
                        return true; 
                    }
                }
            }
            return false;
        }
    }
}
