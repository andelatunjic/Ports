using atunjic_zadaca_3.Modeli;
using atunjic_zadaca_3.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atunjic_zadaca_3.VisitorZauzetiVezovi
{
    public class VrstaVezaConcreteVisitor : IVrstaVezaVisitor
    {
        public void posjeti(PutnickiVez putnicki)
        {
            putnicki.izracunaj();
        }

        public void posjeti(PoslovniVez poslovni)
        {
            poslovni.izracunaj();
        }

        public void posjeti(OstaliVez ostali)
        {
            ostali.izracunaj();
        }
    }
}
