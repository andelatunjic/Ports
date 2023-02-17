using atunjic_zadaca_3.Composite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atunjic_zadaca_3.Memento
{
    public class MementoStatusi
    {
        private List<VezLeaf> Vezovi;
        private DateTime Vrijeme;

        public MementoStatusi(List<VezLeaf> vezovi, DateTime vrijeme)
        {
            Vezovi = vezovi;
            Vrijeme = vrijeme;
        }

        public List<VezLeaf> GetVezovi()
        {
            return Vezovi;
        }

        public DateTime GetVrijeme()
        {
            return Vrijeme;
        }
    }
}
