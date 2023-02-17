using System.Globalization;
using System.Text.RegularExpressions;

namespace atunjic_zadaca_3.Singleton_Podaci
{
    public class UpraviteljGresaka
    {
        private static UpraviteljGresaka? Instance;
        private static object _lock = new object();

        public int brojGresaka = 0;

        private UpraviteljGresaka() { }

        public static UpraviteljGresaka getInstance()
        {
            if (Instance == null)
            {
                lock (_lock)
                {
                    if (Instance == null)
                    {
                        Instance = new UpraviteljGresaka();
                    }
                }
            }
            return Instance;
        }
    }
}
    

