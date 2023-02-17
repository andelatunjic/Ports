using atunjic_zadaca_3.ObserverKapetanija;
using atunjic_zadaca_3.Singleton;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atunjic_zadaca_3.Modeli
{
    public class Brod : IBrodPretplatnik
    {
        public int Id { get; set; }
        public string OznakaBroda { get; set; }
        public string Naziv { get; set; }
        public string Vrsta { get; set; }
        public float Duljina { get; set; }
        public float Sirina { get; set; }
        public float Gaz { get; set; }
        public float MaksimalnaBrzina { get; set; }
        public int KapacitetPutnika { get; set; }
        public int KapacitetOsobnihVozila { get; set; }
        public int KapacitetTereta { get; set; }

        public Brod(int id, string oznakaBroda, string naziv, string vrsta, float duljina, float sirina, float gaz, 
            float maksimalnaBrzina, int kapacitetPutnika, int kapacitetOsobnihVozila, int kapacitetTereta)
        {
            Id = id;
            OznakaBroda = oznakaBroda;
            Naziv = naziv;
            Vrsta = vrsta;
            Duljina = duljina;
            Sirina = sirina;
            Gaz = gaz;
            MaksimalnaBrzina = maksimalnaBrzina;
            KapacitetPutnika = kapacitetPutnika;
            KapacitetOsobnihVozila = kapacitetOsobnihVozila;
            KapacitetTereta = kapacitetTereta;
        }
        public BrodskaLuka brodskaLuka = BrodskaLuka.getInstance();
        public void Update(string poruka)
        {
            brodskaLuka.radniPodaci.Add($"BROD {Id}: {poruka}");
        }
    }
}
