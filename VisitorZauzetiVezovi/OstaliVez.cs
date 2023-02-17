using atunjic_zadaca_3.Composite.Iterator;
using atunjic_zadaca_3.Composite;
using atunjic_zadaca_3.Modeli;
using atunjic_zadaca_3.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atunjic_zadaca_3.VisitorZauzetiVezovi
{
    public class OstaliVez : IVrstaVeza
    {
        BrodskaLuka brodskaLuka = BrodskaLuka.getInstance();

        public void prihvati(IVrstaVezaVisitor vrstaVezaVisitor)
        {
            vrstaVezaVisitor.posjeti(this);
        }

        public void izracunaj()
        {
            int brZauzetihOstalih = 0;

            //Composite
            IteratorLuke iterator = (IteratorLuke)brodskaLuka.luka.createIterator();
            for (iterator.First(); !iterator.IsDone(); iterator.Next())
            {
                IComponent c = iterator.CurrentItem();
                if (c is MolComposite)
                {
                    MolComposite mol = (MolComposite)c;
                    IteratorLuke it = (IteratorLuke)mol.createIterator();
                    for (it.First(); !it.IsDone(); it.Next())
                    {
                        VezLeaf v = (VezLeaf)it.CurrentItem();
                        if (v.Vrsta == "OS" && v.StatusVeza == Enums.StatusVeza.Zauzet)
                        {
                            brZauzetihOstalih++;
                        }
                    }
                }
            }

            brodskaLuka.brojZauzetihOstalihVezova = brZauzetihOstalih;
        }
    }
}
