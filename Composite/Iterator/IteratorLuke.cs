using atunjic_zadaca_3.Singleton;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atunjic_zadaca_3.Composite.Iterator
{
    public class IteratorLuke : IIteratorLuke
    {
        private List<IComponent> _elements;
        private int _current = 0;
        private int _end;

        public IteratorLuke(List<IComponent> elements)
        {
            _elements = elements;
            _end = _elements.Count - 1;
        }

        public void First()
        {
            _current = 0;
        }

        public void Next()
        {
            _current++;
        }

        public bool IsDone()
        {
            return _current > _end;
        }

        public IComponent CurrentItem()
        {
            return _elements[_current];
        }
    }
}
