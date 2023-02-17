using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atunjic_zadaca_3.Composite.Iterator
{
    public interface IIteratorLuke
    {
        void First();
        void Next();
        bool IsDone();
        IComponent CurrentItem();
    }
}
