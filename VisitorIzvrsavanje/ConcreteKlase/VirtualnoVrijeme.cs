
using atunjic_zadaca_3.Singleton;
using atunjic_zadaca_3.VisitorIzvrsavanje;
using atunjic_zadaca_3.VisitorIzvrsavanje.ConcreteKlase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atunjic_zadaca_3.VisitorIzvrsavanje.ConcreteKlase
{
    //ConcreteProduct
    public class VirtualnoVrijeme : ZajednickeMetode, IAktivnost
    {
        public void prihvati(IAktivnostVisitor aktivnostVisitor)
        {
            aktivnostVisitor.posjeti(this);
        }

        public void izvrsi(string naredba)
        {
            if(brodskaLuka.radniPodaci.Count > 0) brodskaLuka.radniPodaci.Clear();
            if(brodskaLuka.pogreskePodaci.Count > 0) brodskaLuka.pogreskePodaci.Clear();

            string[] polje = naredba.Split(' ');

            var pravilanDatum = DateTime.TryParse(naredba.Substring(3), CultureInfo.CurrentCulture,
                    DateTimeStyles.AssumeLocal, out DateTime _pravilanDatum);
            if (pravilanDatum)
            {
                vrijeme.RazlikaVremenaTicks = DateTime.Now.Subtract(_pravilanDatum).Ticks;
                brodskaLuka.azurirajStatuseVezova(_pravilanDatum, _pravilanDatum, false);
                brodskaLuka.radniPodaci.Add($"Uspješno je postavljeno novo virtualno vrijeme na datum: {_pravilanDatum}");
            }
            else
            {
                greske.brojGresaka += 1;
                brodskaLuka.pogreskePodaci.Add($"ERROR {greske.brojGresaka} : Datum ne postoji.");
            }
        }
    }
}
