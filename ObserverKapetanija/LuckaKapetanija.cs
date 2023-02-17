using atunjic_zadaca_3.Singleton;
using atunjic_zadaca_3.Singleton_Podaci;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atunjic_zadaca_3.ObserverKapetanija
{
    public class LuckaKapetanija : ILuckaKapetanija
    {
        UpraviteljGresaka greske = UpraviteljGresaka.getInstance();
        public BrodskaLuka brodskaLuka = BrodskaLuka.getInstance();

        private int brojacPretplatnika = 0;

        public int Id { get; set; }
        public int Frekvencija { get; set; }
        public int MaksimalanBroj { get; set; }

        public LuckaKapetanija(int id, int frekvencija, int maksimalanBroj)
        {
            Id = id;
            Frekvencija = frekvencija;
            MaksimalanBroj = maksimalanBroj;
        }

        private List<IBrodPretplatnik> pretplaceniBrodovi = new List<IBrodPretplatnik> { }; 

        public void Notify(string poruka)
        {
            brodskaLuka.radniPodaci.Add("Šalje se obavijest pretplatnicima...");
            foreach (var observer in pretplaceniBrodovi)
            {
                observer.Update(poruka);
            }
        }

        public void Odjavi(IBrodPretplatnik pretplatnik)
        {
            if (pretplacen(pretplatnik))
            {
                this.pretplaceniBrodovi.Remove(pretplatnik);
                brojacPretplatnika--;
            }
            else
            {
                greske.brojGresaka += 1;
                brodskaLuka.pogreskePodaci.Add($"ERROR {greske.brojGresaka} : Brod nije pretplacen na ovaj kanal.");
            }
        }

        public void Pretplati(IBrodPretplatnik pretplatnik)
        {
            if (validnaPretplata(pretplatnik))
            {
                this.pretplaceniBrodovi.Add(pretplatnik);
                brojacPretplatnika++;
            }
        }

        private bool validnaPretplata(IBrodPretplatnik pretplatnik)
        {
            if (brojacPretplatnika == MaksimalanBroj)
            {
                greske.brojGresaka += 1;
                brodskaLuka.pogreskePodaci.Add($"ERROR {greske.brojGresaka} : Kanal je popunjen.");
                return false;
            }
            else if (pretplacen(pretplatnik))
            {
                greske.brojGresaka += 1;
                brodskaLuka.pogreskePodaci.Add($"ERROR {greske.brojGresaka} : Brod je vec pretplacen na drugi kanal.");
                return false;
            }
            return true;
        }

        public bool pretplacen(IBrodPretplatnik pretplatnik)
        {
            if (pretplaceniBrodovi.Contains(pretplatnik))
            {
                return true;
            }
            return false;
        }
    }
}
