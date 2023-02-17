using atunjic_zadaca_3.Composite.Iterator;
using atunjic_zadaca_3.Modeli;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atunjic_zadaca_3.Composite
{
    public class BrodskaLukaComposite : IComponent
    {
        private string Naziv { get; set; }
        private double GPS_sirina { get; set; }
        private double GPS_visina { get; set; }
        public double DubinaLuke { get; set; }
        public int UkupniBrojPutnickihVezova { get; set; }
        public int UkupniBrojPoslovnihVezova { get; set; }
        public int UkupniBrojOstalihVezova { get; set; }

        private List<IComponent> _molovi;

        public BrodskaLukaComposite(string naziv, double gpsSirina, double gpsVisina, double dubinaLuke, int brojPutnickih, int brojPoslovnih, int brojOstalih)
        {
            Naziv = naziv;
            GPS_sirina= gpsSirina;
            GPS_visina= gpsVisina;
            DubinaLuke= dubinaLuke;
            UkupniBrojPutnickihVezova= brojPutnickih;
            UkupniBrojPoslovnihVezova= brojPoslovnih;
            UkupniBrojOstalihVezova = brojOstalih;
            _molovi = new List<IComponent>();
        }

        public void add(IComponent component)
        {
            _molovi.Add(component);
        }

        public void display()
        {
            Console.WriteLine("Brodska luka: " + this.Naziv);
            foreach (IComponent mol in this._molovi)
            {
                mol.display();
            }
        }

        public void remove(IComponent component)
        {
            _molovi.Remove(component);
        }

        public IIteratorLuke createIterator()
        {
            return new IteratorLuke(_molovi);
        }
    }
}

