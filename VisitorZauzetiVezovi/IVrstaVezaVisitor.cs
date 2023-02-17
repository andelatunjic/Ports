using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atunjic_zadaca_3.VisitorZauzetiVezovi
{
    public interface IVrstaVezaVisitor
    {
        public void posjeti(PutnickiVez putnicki);
        public void posjeti(PoslovniVez poslovni);
        public void posjeti(OstaliVez ostali);
    }
}
