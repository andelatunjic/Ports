using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atunjic_zadaca_3.ObserverKapetanija
{
    public interface ILuckaKapetanija
    {
        void Pretplati(IBrodPretplatnik observer);

        void Odjavi(IBrodPretplatnik observer);

        void Notify(string poruka);
    }
}
