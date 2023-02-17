using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atunjic_zadaca_3.Memento
{
    public class Caretaker
    {
        private List<MementoStatusi> mementoList = new List<MementoStatusi>();
        
        public void Add(MementoStatusi state)
        {
            mementoList.Add(state);
        }
        public MementoStatusi get(int index)
        {
            return mementoList[index];
        }
    }
}
