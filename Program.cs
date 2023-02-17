using atunjic_zadaca_3;
using atunjic_zadaca_3.Composite;
using atunjic_zadaca_3.Composite.Iterator;
using atunjic_zadaca_3.Modeli;
using atunjic_zadaca_3.MVC;
using atunjic_zadaca_3.Singleton;
using atunjic_zadaca_3.Singleton_Podaci;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

internal class Program
{
    private static void Main(string[] args)
    {
        List<string> argumenti = new List<string>(args);
        if (argumenti.Count != 18 && argumenti.Count != 20)
        {
            Console.WriteLine("ERROR 1: Potreban broj argumenata je 18 ili 20. Datoteka rasporeda je opcionalna.");
            Environment.Exit(0);
        }

        View v = PostaviEkrane(argumenti);
        Model m = new Model();
        Controller c = new Controller(v, m);
        c.pripremiIzvrsavanje(argumenti);
    }

    private static View PostaviEkrane(List<string> argumenti)
    {
        int index = 0;
        int maxBroj = 0;
        string uloga = "";
        string omjer = "";

        if (argumenti.Contains("-br"))
        {
            index = argumenti.IndexOf("-br");
            string br = argumenti[index + 1];

            string pBroj = @"^((2[4-9])|(50)|([3-4]\d))$";
            Match mBroj = Regex.Match(br, pBroj, RegexOptions.IgnoreCase);

            if (mBroj.Success) maxBroj = int.Parse(br);
            else
            {
                Console.WriteLine("ERROR 1: Max. broj linija je broj između 24 i 50.");
                Environment.Exit(0);
            }
        }
        else
        {
            Console.WriteLine("ERROR 1: Max broj redova je obavezan. Koristite zastavicu -br.");
            Environment.Exit(0);
        }

        if (argumenti.Contains("-vt"))
        {
            index = argumenti.IndexOf("-vt");
            string om = argumenti[index + 1];

            string pOmjer = @"^(50:50|25:75|75:25)$";
            Match mOmjer = Regex.Match(om, pOmjer, RegexOptions.IgnoreCase);

            if (mOmjer.Success) omjer = om;
            else
            {
                Console.WriteLine("ERROR 1: Omjer je 50:50|25:75|75:25.");
                Environment.Exit(0);
            }
        }
        else
        {
            Console.WriteLine("ERROR 1: Omjer je obavezan. Koristite zastavicu -vt.");
            Environment.Exit(0);
        }

        if (argumenti.Contains("-pd"))
        {
            index = argumenti.IndexOf("-pd");
            string ul = argumenti[index + 1];

            string pUloga = @"^(R:P|P:R)$";
            Match mUloga = Regex.Match(ul, pUloga, RegexOptions.IgnoreCase);

            if (mUloga.Success) uloga = ul;
            else
            {
                Console.WriteLine("ERROR 1: Uloga je R:P|P:R");
                Environment.Exit(0);
            }
        }
        else
        {
            Console.WriteLine("ERROR 1: Uloga je obavezna. Koristite zastavicu -pd.");
            Environment.Exit(0);
        }

        return new View(maxBroj, omjer, uloga);
    }
}