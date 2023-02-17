using atunjic_zadaca_3.Composite;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atunjic_zadaca_3.Modeli
{
    public class Raspored
    {
        public VezLeaf Vez { get; set; }
        public Brod Brod { get; set; }
        public string Dani_u_tjednu { get; set; }
        public DateTime VrijemeOd { get; set; }
        public DateTime VrijemeDo { get; set; }

        public Raspored(VezLeaf vez, Brod brod, string dani_u_tjednu, DateTime vrijemeOd, DateTime vrijemeDo)
        {
            Vez = vez;
            Brod = brod;
            Dani_u_tjednu = dani_u_tjednu;
            VrijemeOd = vrijemeOd;
            VrijemeDo = vrijemeDo;
        }
    }
}
