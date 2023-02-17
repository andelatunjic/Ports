using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atunjic_zadaca_3.MVC
{
    public class Model
    {
        private List<String> podaci;

        public Model() { }

        public List<String> getData()
        {
            return this.podaci;
        }

        public void setData(List<String> podaci)
        {
            this.podaci = podaci;
        }
    }
}
