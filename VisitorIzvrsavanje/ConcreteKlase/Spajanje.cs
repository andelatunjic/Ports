using atunjic_zadaca_3.Enums;
using atunjic_zadaca_3.Modeli;
using atunjic_zadaca_3.ObserverKapetanija;

namespace atunjic_zadaca_3.VisitorIzvrsavanje.ConcreteKlase
{
    public class Spajanje : ZajednickeMetode, IAktivnost
    {
        public void prihvati(IAktivnostVisitor aktivnostVisitor)
        {
            aktivnostVisitor.posjeti(this);
        }

        public void izvrsi(string naredba)
        {
            if (brodskaLuka.radniPodaci.Count > 0) brodskaLuka.radniPodaci.Clear();
            if (brodskaLuka.pogreskePodaci.Count > 0) brodskaLuka.pogreskePodaci.Clear();

            string[] polje = naredba.Split(' ');
            int idBrod = int.Parse(polje[1]);
            int frekvencija = int.Parse(polje[2]);

            LuckaKapetanija kanal = brodskaLuka.kanali.Find(kanal => kanal.Frekvencija == frekvencija);
            Brod brod = brodskaLuka.brodovi.Find(brod => brod.Id == idBrod);

            if (brodKanalPostoje(kanal, brod))
            {
                if (polje.Contains("Q"))
                {
                    kanal.Odjavi(brod);
                    kanal.Notify($"Primljena poruka: Brod {brod.Id} se odjavljuje.");
                  
                }
                else
                {
                    kanal.Pretplati(brod);
                    kanal.Notify($"Primljena poruka: Brod {brod.Id} se prijavljuje.");
                }
            }
        }

        private bool brodKanalPostoje(LuckaKapetanija? kanal, Brod? brod)
        {
            if (kanal == null)
            {
                greske.brojGresaka += 1;
                brodskaLuka.pogreskePodaci.Add($"ERROR {greske.brojGresaka} : Kanal sa zeljenom frekvencijom ne postoji.");
                return false;
            }
            else if (brod == null)
            {
                greske.brojGresaka += 1;
                brodskaLuka.pogreskePodaci.Add($"ERROR {greske.brojGresaka} : Brod ne postoji.");
                return false;
            }
            return true;
        }
    }
}
