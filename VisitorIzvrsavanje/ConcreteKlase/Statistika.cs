using atunjic_zadaca_3.Enums;
using atunjic_zadaca_3.Tablice;
using atunjic_zadaca_3.Tablice.TabliceModeli;
using atunjic_zadaca_3.Tablice.Builder;
using atunjic_zadaca_3.Modeli;
using atunjic_zadaca_3.ChainOfRStatistika;

namespace atunjic_zadaca_3.VisitorIzvrsavanje.ConcreteKlase
{
    public class Statistika : ZajednickeMetode, IAktivnost
    {
        public void prihvati(IAktivnostVisitor aktivnostVisitor)
        {
            aktivnostVisitor.posjeti(this);
        }

        public void izvrsi(string naredba)
        {
            string[] polje = naredba.Split(' ');
            List<int> sifre = new List<int>();

            for (int i = 0; i < polje.Length; i++)
            {
                switch (polje[i])
                {
                    case "PU":
                        sifre.Add(1);
                        break;
                    case "PO":
                        sifre.Add(2);
                        break;
                    case "OS":
                        sifre.Add(3);
                        break;
                    default:
                        break;
                }
            }

            AbstractHandler handler = postaviLanacHandlera();
            handler.ispisiRezultat(sifre, brodskaLuka.brodovi);

            sifre.Clear();
        }

        private static AbstractHandler postaviLanacHandlera()
        {
            AbstractHandler putnicki = new PutnickiHandler(AbstractHandler.PU);
            AbstractHandler poslovni = new PoslovniHandler(AbstractHandler.PO);
            AbstractHandler ostali = new OstaliHandler(AbstractHandler.OS);

            putnicki.postaviSljedeciHandler(poslovni);
            poslovni.postaviSljedeciHandler(ostali);

            return putnicki;
        }
    }
}
