using atunjic_zadaca_3.Composite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atunjic_zadaca_3.Memento
{
    public class Originator
    {
        private List<VezLeaf> Vezovi;
        private DateTime Vrijeme;

        public void setState(List<VezLeaf> vezovi, DateTime vrijeme)
        {
            Vezovi = vezovi;
            Vrijeme = vrijeme;
        }
        
        public List<VezLeaf> getStatusi()
        {
            return Vezovi;
        }

        public DateTime getVrijeme()
        {
            return Vrijeme;
        }

        public MementoStatusi saveStateToMemento()
        {
            return new MementoStatusi(Vezovi, Vrijeme);
        }
        
        public void getStateFromMemento(MementoStatusi memento)
        {
            Vezovi = memento.GetVezovi();
            Vrijeme = memento.GetVrijeme();
        }
    }
}
