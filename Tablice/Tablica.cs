using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atunjic_zadaca_3.Tablice
{
    public class Tablica
    {
        //StackOwerflow izvor

        static int sirinaTablice = 100;
        public static string ispisiPregradu()
        {
            return new string('-', sirinaTablice);
        }

        public static string ispisiRed(params string[] red)
        {
            int width = (sirinaTablice - red.Length) / red.Length;
            string redZaIspis = "|";

            foreach (string element in red)
            {
                redZaIspis += poravnaj(element, width) + "|";
            }

            return redZaIspis;
        }

        public static string poravnaj(string tekst, int sirina)
        {
            tekst = tekst.Length > sirina ? tekst.Substring(0, sirina - 3) + "..." : tekst;

            if (string.IsNullOrEmpty(tekst))
            {
                return new string(' ', sirina);
            }
            else
            {
                if (float.TryParse(tekst, out float broj))
                {
                    return tekst.PadRight(3).PadLeft(sirina);
                }
                else
                {
                    return tekst.PadRight(sirina-2).PadLeft(sirina);
                }
            }
        }
    }
}
