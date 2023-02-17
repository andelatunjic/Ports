using atunjic_zadaca_3.Modeli;
using atunjic_zadaca_3.ObserverKapetanija;

namespace atunjic_zadaca_3.VisitorIzvrsavanje.ConcreteKlase
{
    //ConcreteProduct
    public class ZahtjevZaRezerviraniVez : ZajednickeMetode, IAktivnost
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
                    string poruka = "Zahtjev odbijen.";
                    foreach (Raspored raspored in brodskaLuka.raspored.Where(raspored => raspored.Brod.Id == IdBroda))
                    {
                        if (vrijemeJeUIntervalu(trenutnoVrijeme, raspored))
                        {
                            poruka = "Zahtjev je odobren.";
                            break;
                        }
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

        private bool brodJePretplacen(Brod brod)
        {
            if (brod != null)
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

        private bool vrijemeJeUIntervalu(DateTime trenutnoVrijeme, Raspored raspored)
        {
            string trenutniDan = trenutnoVrijeme.DayOfWeek.ToString();
            string oznakaTrenutnogDana = pronadiOznakuDana(trenutniDan);
            if (raspored.Dani_u_tjednu.Contains(oznakaTrenutnogDana))
            {
                var vrijemeZaProvjeru = trenutnoVrijeme.TimeOfDay;
                var vrijemeOd = raspored.VrijemeOd.TimeOfDay;
                var vrijemeDo = raspored.VrijemeDo.TimeOfDay;
                if (vrijemeZaProvjeru < vrijemeDo && vrijemeZaProvjeru > vrijemeOd) return true;
            }
            return false;
        }
    }
}
