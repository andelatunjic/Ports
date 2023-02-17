using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atunjic_zadaca_3.VisitorZauzetiVezovi
{
    public interface IVrstaVeza
    {
        public void prihvati(IVrstaVezaVisitor vrstaVezaVisitor);
    }
}
