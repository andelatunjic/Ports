using atunjic_zadaca_3.Enums;
using atunjic_zadaca_3;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using atunjic_zadaca_3.VisitorIzvrsavanje;
using atunjic_zadaca_3.VisitorIzvrsavanje.ConcreteKlase;
using atunjic_zadaca_3.Tablice;
using atunjic_zadaca_3.Tablice.TabliceModeli;
using atunjic_zadaca_3.Tablice.Builder;
using atunjic_zadaca_3.Modeli;
using atunjic_zadaca_3.Composite.Iterator;
using atunjic_zadaca_3.Composite;

namespace atunjic_zadaca_3.VisitorIzvrsavanje.ConcreteKlase
{
    //ConcreteProduct
    public class IspisVezova : ZajednickeMetode, IAktivnost
    {
        public void prihvati(IAktivnostVisitor aktivnostVisitor)
        {
            aktivnostVisitor.posjeti(this);
        }

        public void izvrsi(string naredba)
        {
            List<ZauzetiSlobodniVezovi> zauzetiSlobodniVezovi = new List<ZauzetiSlobodniVezovi>();

            string[] polje = naredba.Split(' ');

            string VrstaVeza = polje[1];
            string Status = polje[2];
            string datumOd = polje[3] + " " + polje[4];
            string datumDo = polje[5] + " " + polje[6];

            var pravilanDatumOd = DateTime.TryParse(datumOd, CultureInfo.CurrentCulture,
                DateTimeStyles.AssumeLocal, out DateTime DatumOd);
            var pravilanDatumDo = DateTime.TryParse(datumDo, CultureInfo.CurrentCulture,
                DateTimeStyles.AssumeLocal, out DateTime DatumDo);

            if (pravilanDatumOd && pravilanDatumDo && DatumOd < DatumDo)
            {
                StatusVeza trazeniStatus;
                if (Status == "Z")
                {
                    trazeniStatus = StatusVeza.Zauzet;
                }
                else trazeniStatus = StatusVeza.Slobodan;

                brodskaLuka.azurirajStatuseVezova(DatumOd, DatumDo, true);

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
                            if (vez.StatusVeza == trazeniStatus && vez.Vrsta == VrstaVeza)
                            {
                                ZauzetiSlobodniVezovi noviZapis = new ZauzetiSlobodniVezovi(vez.Id, vez.OznakaVeza, vez.Vrsta, vez.StatusVeza, "-", "-");
                                zauzetiSlobodniVezovi.Add(noviZapis);
                            }
                        }
                    }
                }
            }
            else
            {
                greske.brojGresaka += 1;
                brodskaLuka.pogreskePodaci.Add($"ERROR {greske.brojGresaka} : Datum ne postoji ili je datum od veći od datuma do.");
            }

            var director = new DirectorTablica();
            var builder = new ConcreteBuilderTablica();
            director.Builder = builder;
            director.ispisiTablicuZauzetiSlobodni(zauzetiSlobodniVezovi, brodskaLuka.zaglavlje, brodskaLuka.podnozje, brodskaLuka.redniBroj);
        }
    }
}
