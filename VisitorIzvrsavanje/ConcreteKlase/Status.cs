using atunjic_zadaca_3.Enums;
using atunjic_zadaca_3.Tablice;
using atunjic_zadaca_3.Tablice.TabliceModeli;
using atunjic_zadaca_3.Tablice.Builder;
using atunjic_zadaca_3.Modeli;
using atunjic_zadaca_3.Composite.Iterator;
using atunjic_zadaca_3.Composite;
using atunjic_zadaca_3.VisitorZauzetiVezovi;

namespace atunjic_zadaca_3.VisitorIzvrsavanje.ConcreteKlase
{
    public class Status : ZajednickeMetode, IAktivnost
    {
        public void prihvati(IAktivnostVisitor aktivnostVisitor)
        {
            aktivnostVisitor.posjeti(this);
        }

        public void izvrsi()
        {
            List<Statusi> listaStatusa = new List<Statusi>();

            DateTime trenutnoVrijeme = DateTime.Now.Subtract(new TimeSpan(vrijeme.RazlikaVremenaTicks));

            brodskaLuka.azurirajStatuseVezova(trenutnoVrijeme, trenutnoVrijeme, false);

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
                        VezLeaf vez = (VezLeaf)it.CurrentItem();
                        Statusi noviZapis = new Statusi(vez.Id, vez.OznakaVeza, vez.Vrsta, vez.StatusVeza.ToString());
                        listaStatusa.Add(noviZapis);
                    }
                }
            }

            var director = new DirectorTablica();
            var builder = new ConcreteBuilderTablica();
            director.Builder = builder;
            director.ispisiTablicuStatusi(listaStatusa, brodskaLuka.zaglavlje, brodskaLuka.podnozje, brodskaLuka.redniBroj);
        }
    }
}
