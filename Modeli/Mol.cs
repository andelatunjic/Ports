using atunjic_zadaca_3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atunjic_zadaca_3.Modeli
{
    public class Mol
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public List<Vez> VezoviMola { get; set; }


    public Mol(int id, string naziv)
        {
            Id = id;
            Naziv = naziv;
        }
    }
}
