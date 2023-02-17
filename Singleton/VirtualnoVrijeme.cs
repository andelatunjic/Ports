
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atunjic_zadaca_3.Singleton_Podaci
{
    public class VirVrijeme
    {
        private static VirVrijeme? Instance;
        private static object _lock = new object();

        public DateTime VirtualnoVrijeme;
        public long RazlikaVremenaTicks = 0;

        private VirVrijeme() { }

        public static VirVrijeme getInstance()
        {
            if (Instance == null)
            {
                lock (_lock)
                {
                    if (Instance == null)
                    {
                        Instance = new VirVrijeme();
                    }
                }
            }
            return Instance;
        }

        public void inicijalizirajVirtualnoVrijeme()
        {
            RazlikaVremenaTicks = DateTime.Now.Subtract(VirtualnoVrijeme).Ticks;
        }

        public string ispisiVrijemeVirtualnogSata()
        {
            string vrijeme = "Trenutno vrijeme virtualnog sata: " +
                DateTime.Now.Subtract(new TimeSpan(RazlikaVremenaTicks));
            return vrijeme;
        }
    }
}
