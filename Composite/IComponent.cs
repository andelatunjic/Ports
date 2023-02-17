using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using atunjic_zadaca_3.Composite.Iterator;

namespace atunjic_zadaca_3.Composite
{
    public interface IComponent 
    {
        public void add(IComponent component);
        public void remove(IComponent component);
        public void display();
        public IIteratorLuke createIterator();
    }
}
