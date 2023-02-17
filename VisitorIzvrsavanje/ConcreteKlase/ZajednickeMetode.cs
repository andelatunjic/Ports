using atunjic_zadaca_3.Enums;
using atunjic_zadaca_3.Singleton_Podaci;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using atunjic_zadaca_3.Singleton;
using atunjic_zadaca_3.Tablice.Builder;
using atunjic_zadaca_3.Modeli;
using atunjic_zadaca_3.Composite.Iterator;
using atunjic_zadaca_3.Composite;

namespace atunjic_zadaca_3.VisitorIzvrsavanje.ConcreteKlase
{
    public class ZajednickeMetode
    {
        public BrodskaLuka brodskaLuka = BrodskaLuka.getInstance();
        public UpraviteljGresaka greske = UpraviteljGresaka.getInstance();
        public VirVrijeme vrijeme = VirVrijeme.getInstance();

        public bool vezJeURasporedu(Vez vez)
        {
            Raspored pronađenVez = brodskaLuka.raspored.Find(x => x.Vez.Id.Equals(vez.Id));
            if (pronađenVez != null) return true;
            return false;
        }

        public StatusVeza dohvatiStatusVeza(Vez vez, DateTime trenutnoVrijeme)
        {
            foreach (Raspored raspored in brodskaLuka.raspored)
            {
                if (raspored.Vez.Id == vez.Id)
                {
                    string trenutniDan = trenutnoVrijeme.DayOfWeek.ToString();
                    string oznakaTrenutnogDana = pronadiOznakuDana(trenutniDan);
                    if (raspored.Dani_u_tjednu.Contains(oznakaTrenutnogDana))
                    {
                        var vrijemeZaProvjeru = trenutnoVrijeme.TimeOfDay;
                        var vrijemeOd = raspored.VrijemeOd.TimeOfDay;
                        var vrijemeDo = raspored.VrijemeDo.TimeOfDay;
                        //Console.WriteLine(vrijemeOd + " " + vrijemeZaProvjeru + " " + vrijemeDo);
                        if (vrijemeZaProvjeru < vrijemeDo && vrijemeZaProvjeru > vrijemeOd) return StatusVeza.Zauzet;
                    }
                    else
                    {
                        return StatusVeza.Slobodan;
                    }
                }
            }

            return StatusVeza.Slobodan;
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

        public bool vezPrimaVrstuBroda(string vrstaBroda, string vrstaVeza)
        {
            string[] vrsteZaPutnickiVez = { "TR", "KA", "KL", "KR" };
            string[] vrsteZaPoslovniVez = { "RI", "TE" };
            string[] vrsteZaOstaleVez = { "JA", "BR", "RO" };

            switch (vrstaVeza)
            {
                case "PU":
                    if (vrsteZaPutnickiVez.Contains(vrstaBroda)) return true;
                    else return false;
                case "PO":
                    if (vrsteZaPoslovniVez.Contains(vrstaBroda)) return true;
                    else return false;
                case "OS":
                    if (vrsteZaOstaleVez.Contains(vrstaBroda)) return true;
                    else return false;
                default:
                    return false;
            }
        }

        public List<string> dohvatiStatusVeza(Vez vez, DateTime datumOd, DateTime datumDo, StatusVeza trazeniStatus)
        {
            List<string> listaDatumaZaIspis = new List<string>();

            foreach (Raspored raspored in brodskaLuka.raspored.Where(raspored => raspored.Vez.Id == vez.Id))
            {
                StatusVeza status = StatusVeza.Slobodan;

                string TrazeniDanOd = datumOd.DayOfWeek.ToString();
                string TrazeniDanDo = datumDo.DayOfWeek.ToString();
                string TrazenaOznakaDanOd = pronadiOznakuDana(TrazeniDanOd);
                string TrazenaOznakaDanDo = pronadiOznakuDana(TrazeniDanDo);

                if (datumOd.Date < datumDo.Date)
                {
                    bool preklapanjeZaDatumOd = provjeriPreklapanje(TrazenaOznakaDanOd, datumOd, raspored);
                    bool preklapanjeZaDatumDo = provjeriPreklapanje(TrazenaOznakaDanDo, datumDo, raspored);
                    if (preklapanjeZaDatumDo || preklapanjeZaDatumDo)
                    {
                        status = StatusVeza.Zauzet;
                    }
                }
                else
                {
                    var vrijemeOdURasporedu = raspored.VrijemeOd.TimeOfDay;
                    var vrijemeDoURasporedu = raspored.VrijemeDo.TimeOfDay;
                    if (raspored.Dani_u_tjednu.Contains(TrazenaOznakaDanOd))
                    {
                        var vrijemeOd = raspored.VrijemeOd.TimeOfDay;
                        var vrijemeDo = raspored.VrijemeDo.TimeOfDay;
                        //Console.WriteLine(vrijemeOd + " " + vrijemeZaProvjeru + " " + vrijemeDo);
                        if (datumOd.TimeOfDay < vrijemeDoURasporedu && datumDo.TimeOfDay > vrijemeOdURasporedu)
                            status = StatusVeza.Zauzet;
                    }
                }

                if (trazeniStatus == status)
                {
                    listaDatumaZaIspis.Add(raspored.VrijemeOd.TimeOfDay.ToString());
                    listaDatumaZaIspis.Add(raspored.VrijemeDo.TimeOfDay.ToString());
                }
            }
            return listaDatumaZaIspis;
        }

        public bool provjeriPreklapanje(string oznakaDana, DateTime vrijemeZaProvjeru, Raspored raspored)
        {
            if (raspored.Dani_u_tjednu.Contains(oznakaDana))
            {
                var vrijemeOd = raspored.VrijemeOd.TimeOfDay;
                var vrijemeDo = raspored.VrijemeDo.TimeOfDay;
                //Console.WriteLine(vrijemeOd + " " + vrijemeZaProvjeru + " " + vrijemeDo);
                if (vrijemeZaProvjeru.TimeOfDay < vrijemeDo && vrijemeZaProvjeru.TimeOfDay > vrijemeOd) return true;
            }
            return false;
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
