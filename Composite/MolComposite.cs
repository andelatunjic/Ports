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
    public class MolComposite : IComponent
    {
        public int Id;
        private string Naziv;
        private List<IComponent> vezovi;


        public MolComposite(int id, string naziv)
        {
            Id = id;
            Naziv = naziv;
            vezovi= new List<IComponent>();
        }

        public void add(IComponent c)
        {
            vezovi.Add(c);
        }

        public void remove(IComponent c)
        {
            vezovi.Remove(c);
        }

        public void display()
        {
            Console.WriteLine("Mol: ID = " + this.Id + ", Naziv = " + this.Naziv);

            foreach (IComponent vez in vezovi)
            {
                vez.display();
            }
        }

        public IIteratorLuke createIterator()
        {
            return new IteratorLuke(vezovi);
        }
    }
}
