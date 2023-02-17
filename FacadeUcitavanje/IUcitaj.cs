using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atunjic_zadaca_3.Facade
{
    internal interface IUcitaj
    {
        public List<string> ucitaj(string nazivDatoteke);
        public void spremiRed(string NoviRed);
        public bool provjeriValjanostRetka(string noviRed);
    }
}
