using atunjic_zadaca_3.Modeli;
using atunjic_zadaca_3.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atunjic_zadaca_3.ChainOfRStatistika
{
    public abstract class AbstractHandler
    {
        public BrodskaLuka brodskaLuka = BrodskaLuka.getInstance();

        public static int PU = 1;
        public static int PO = 2;
        public static int OS = 3;

        protected int sifra;

        protected AbstractHandler NextHandler;

        public void postaviSljedeciHandler(AbstractHandler nextHandler)
        {
            this.NextHandler = nextHandler;
        }

        public void ispisiRezultat(List<int> sifre, List<Brod> brodovi)
        {
            if (sifre.Contains(this.sifra))
            {
                ispisi(brodovi);
            }
            if (NextHandler != null)
            {
                NextHandler.ispisiRezultat(sifre, brodovi);
            }
        }
        abstract protected void ispisi(List<Brod> brodovi);

        public int prebroji(List<Brod> brodovi, string oznaka)
        {
            int brojac = 0;
            foreach (Brod brod in brodovi.Where(brod => brod.Vrsta == oznaka))
            {
                brojac++;
            }
            return brojac;
        }

        public float izracunajBrzinu(List<Brod> brodovi, string oznaka)
        {
            int brojac = 0;
            float brzina = 0;
            foreach (Brod brod in brodovi.Where(brod => brod.Vrsta == oznaka))
            {
                brojac++;
                brzina += brod.MaksimalnaBrzina;

            }
            float prosjecnaBrzina = brzina / brojac;
            return prosjecnaBrzina;
        }
    }
}
