using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atunjic_zadaca_3.VisitorIzvrsavanje
{
    public interface IAktivnost
    {
        public void prihvati(IAktivnostVisitor aktivnostVisitor);
    }
}
