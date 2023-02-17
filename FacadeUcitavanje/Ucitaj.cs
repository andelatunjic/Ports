using atunjic_zadaca_3.Singleton_Podaci;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using atunjic_zadaca_3.Singleton;
using atunjic_zadaca_3.Modeli;
using atunjic_zadaca_3.Composite;
using System.Globalization;
using System.Text.RegularExpressions;
using atunjic_zadaca_3.Composite.Iterator;

namespace atunjic_zadaca_3.Facade
{
    internal abstract class Ucitaj
    {
        public BrodskaLuka brodskaLuka = BrodskaLuka.getInstance();
        public VirVrijeme virtualnoVrijeme = VirVrijeme.getInstance();
        public UpraviteljGresaka greske = UpraviteljGresaka.getInstance();


        public bool brodJeVecSvezan(Raspored noviZapisRasporeda)
        {
            bool preklapanje = false;
            string[] poljeDanaNovogZapisa = noviZapisRasporeda.Dani_u_tjednu.Split(',');
            foreach (Raspored zapis in brodskaLuka.raspored.Where(zapis => noviZapisRasporeda.Brod == zapis.Brod))
            {
                foreach (string dan in poljeDanaNovogZapisa)
                {
                    if (zapis.Dani_u_tjednu.Contains(dan))
                    {
                        var vrijemeOdZapis = zapis.VrijemeOd.TimeOfDay;
                        var vrijemeDoZapis = zapis.VrijemeDo.TimeOfDay;
                        var vrijemeOdNoviZapis = noviZapisRasporeda.VrijemeOd.TimeOfDay;
                        var vrijemeDoNoviZapis = noviZapisRasporeda.VrijemeDo.TimeOfDay;
                        preklapanje = (vrijemeOdZapis < vrijemeDoNoviZapis && vrijemeOdNoviZapis < vrijemeDoZapis);
                    }
                }
            }
            return preklapanje;
        }

        public bool vezJePopunjen(Raspored noviZapisRasporeda)
        {
            bool preklapanje = false;
            string[] poljeDanaNovogZapisa = noviZapisRasporeda.Dani_u_tjednu.Split(',');
            foreach (Raspored zapis in brodskaLuka.raspored.Where(zapis => noviZapisRasporeda.Vez == zapis.Vez))
            {
                foreach (string dan in poljeDanaNovogZapisa)
                {
                    if (zapis.Dani_u_tjednu.Contains(dan))
                    {
                        var vrijemeOdZapis = zapis.VrijemeOd.TimeOfDay;
                        var vrijemeDoZapis = zapis.VrijemeDo.TimeOfDay;
                        var vrijemeOdNoviZapis = noviZapisRasporeda.VrijemeOd.TimeOfDay;
                        var vrijemeDoNoviZapis = noviZapisRasporeda.VrijemeDo.TimeOfDay;
                        preklapanje = (vrijemeOdZapis < vrijemeDoNoviZapis && vrijemeOdNoviZapis < vrijemeDoZapis);
                    }
                }
            }
            return preklapanje;
        }

        public string pronadiOznakuDana(string trenutniDan)
        {
            switch (trenutniDan)
            {
                case "Sunday":
                    return "0";
                case "Monday":
                    return "1";
                case "Tuesday":
                    return "2";
                case "Wednesday":
                    return "3";
                case "Thursday":
                    return "4";
                case "Friday":
                    return "5";
                case "Saturday":
                    return "6";
                default:
                    return null;
            }
        }

        public VezLeaf vratiVez(int idVeza)
        {
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
                        if (v.Id == idVeza)
                        {
                            return v;
                        }
                    }
                }
            }
            return null;
        }

        public MolComposite pronadiMol(int molId)
        {
            IteratorLuke iterator = (IteratorLuke)brodskaLuka.luka.createIterator();
            for (iterator.First(); !iterator.IsDone(); iterator.Next())
            {
                MolComposite mol = (MolComposite)iterator.CurrentItem();
                if (mol.Id == molId)
                {
                    return (MolComposite)mol;
                }
            }
            return null;
        }
    }
}
