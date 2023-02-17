using atunjic_zadaca_3.Enums;
using atunjic_zadaca_3.Modeli;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atunjic_zadaca_3.Modeli
{
    public class Vez
    {
        public int Id { get; set; }
        public string OznakaVeza { get; set; }
        public string Vrsta { get; set; }
        public int CijenaVezaPoSatu { get; set; }
        public float MaksimalnaDuljina { get; set; }
        public float MaksimalnaSirina { get; set; }
        public float MaksimalnaDubina { get; set; }
        public Mol Mol { get; set; }
        public StatusVeza StatusVeza { get; set; }

        public Vez(int id, string oznakaVeza, string vrsta, int cijenaVezaPoSatu, float maksimalnaDuljina, 
            float maksimalnaSirina, float maksimalnaDubina, Mol mol, StatusVeza statusVeza)
        {
            Id = id;
            OznakaVeza = oznakaVeza;
            Vrsta = vrsta;
            CijenaVezaPoSatu = cijenaVezaPoSatu;
            MaksimalnaDuljina = maksimalnaDuljina;
            MaksimalnaSirina = maksimalnaSirina;
            MaksimalnaDubina = maksimalnaDubina;
            Mol = mol;
            StatusVeza = statusVeza;
        }
    }
}
