using atunjic_zadaca_3.Composite.Iterator;
using atunjic_zadaca_3.Enums;
using atunjic_zadaca_3.Modeli;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atunjic_zadaca_3.Composite
{
    public class VezLeaf : IComponent
    {
        public int Id;
        public string OznakaVeza;
        public string Vrsta;
        public int CijenaVezaPoSatu;
        public float MaksimalnaDuljina;
        public float MaksimalnaSirina;
        public float MaksimalnaDubina;
        public MolComposite Mol;
        public StatusVeza StatusVeza;

        public VezLeaf(int id, string oznakaVeza, string vrsta, int cijenaVezaPoSatu, float maksimalnaDuljina,
            float maksimalnaSirina, float maksimalnaDubina, MolComposite mol, StatusVeza statusVeza)
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

        public void add(IComponent c)
        {
            throw new NotImplementedException("Vez ne može sadržavati druge elemente.");
        }

        public void remove(IComponent c)
        {
            throw new NotImplementedException("Vez ne može sadržavati druge elemente.");
        }

        public void display()
        {
            Console.WriteLine(" Vez: ID = " + this.Id + ", Vrsta = " + this.OznakaVeza);
        }

        public IIteratorLuke createIterator()
        {
            throw new NotImplementedException();
        }
    }
}
