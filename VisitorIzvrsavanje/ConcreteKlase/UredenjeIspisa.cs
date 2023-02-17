using atunjic_zadaca_3.Enums;
using atunjic_zadaca_3;

namespace atunjic_zadaca_3.VisitorIzvrsavanje.ConcreteKlase
{
    public class UredenjeIspisa : ZajednickeMetode, IAktivnost
    {
        public void prihvati(IAktivnostVisitor aktivnostVisitor)
        {
            aktivnostVisitor.posjeti(this);
        }

        public void izvrsi(string naredba)
        {
            if (brodskaLuka.radniPodaci.Count > 0) brodskaLuka.radniPodaci.Clear();

            resetirajOpcije();
            
            string[] polje = naredba.Split(' ');

            if(polje.Length == 1 )
            {
                brodskaLuka.podnozje = false;
                brodskaLuka.redniBroj = false;
                brodskaLuka.zaglavlje = false;
            }
            else
            {
                foreach (string s in polje)
                {
                    promijeniOpcijeUredivanja(s);
                }
                brodskaLuka.radniPodaci.Add("Uspješno spremljene nove postavke izgleda tablice.");
            }
        }

        private void promijeniOpcijeUredivanja(string s)
        {
            switch (s)
            {
                case "Z":
                    brodskaLuka.zaglavlje = true;
                    break;
                case "RB":
                    brodskaLuka.redniBroj = true;
                    break;
                case "P":
                    brodskaLuka.podnozje = true;
                    break;
                default:
                    break;
            }
        }

        private void resetirajOpcije()
        {
            brodskaLuka.redniBroj = false;
            brodskaLuka.podnozje = false;
            brodskaLuka.zaglavlje = false;
        }
    }
}
