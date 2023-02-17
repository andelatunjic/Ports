
using atunjic_zadaca_3.Composite;
using atunjic_zadaca_3.Composite.Iterator;
using atunjic_zadaca_3.Enums;
using atunjic_zadaca_3.Modeli;
using atunjic_zadaca_3.ObserverKapetanija;
using atunjic_zadaca_3.Tablice.TabliceModeli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace atunjic_zadaca_3.Singleton
{
    public class BrodskaLuka
    {
        private static BrodskaLuka? brodskaLukaInstance;
        private static object _lock = new object();

        public List<Brod> brodovi = new List<Brod>();
        public List<Vez> vezovi = new List<Vez>();
        public List<Raspored> raspored = new List<Raspored>();
        public List<ZahtjevRezervacije> zahtjeviRezervacija = new List<ZahtjevRezervacije>();
        public List<LuckaKapetanija> kanali = new List<LuckaKapetanija>();
        public List<Mol> molovi = new List<Mol>();

        public BrodskaLukaComposite luka;
        public List<string> radniPodaci = new List<string>();
        public List<string> pogreskePodaci = new List<string>();

        public int brojZauzetihPutnickihVezova = 0;
        public int brojZauzetihPoslovnihVezova = 0;
        public int brojZauzetihOstalihVezova = 0;

        public bool podnozje = false;
        public bool redniBroj = false;
        public bool zaglavlje = false;

        private BrodskaLuka(){}

        public static BrodskaLuka getInstance()
        {
            if(brodskaLukaInstance == null)
            {
                lock (_lock)
                {
                    if(brodskaLukaInstance == null)
                    {
                        brodskaLukaInstance = new BrodskaLuka();
                    }
                }
            }
            return brodskaLukaInstance;
        }

        public bool postojiBrod(int idBroda)
        {
            Brod brod = brodovi.Find(x => x.Id.Equals(idBroda));
            if (brod != null) return true;
            return false;
        }

        public bool postojiVez(int idVeza)
        {
            //Composite
            IteratorLuke iterator = (IteratorLuke)luka.createIterator();
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
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public bool postojiMol(int idMol)
        {
            //Composite
            IteratorLuke iterator = (IteratorLuke)luka.createIterator();
            for (iterator.First(); !iterator.IsDone(); iterator.Next())
            {
                MolComposite mol = (MolComposite)iterator.CurrentItem();
                if (mol.Id == idMol)
                {
                    return true;
                }
            }
            return false;
        }

        public void azurirajStatuseVezova(DateTime zadaniDatum, DateTime datumDo, bool interval)
        {
            //Composite
            IteratorLuke iterator = (IteratorLuke)luka.createIterator();
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
                        if (vezJeURasporedu(v))
                        {
                            v.StatusVeza = dohvatiStatusVeza(v, zadaniDatum, datumDo, interval);
                        }
                    }
                }
            }
        }

        private bool vezJeURasporedu(VezLeaf vez)
        {
            Raspored pronađenVez = raspored.Find(x => x.Vez.Id.Equals(vez.Id));
            if (pronađenVez != null) return true;
            return false;
        }

        private StatusVeza dohvatiStatusVeza(VezLeaf vez, DateTime zadaniDatum, DateTime datumDo, bool interval)
        {
            foreach (Raspored raspored in raspored.Where(raspored => raspored.Vez.Id == vez.Id))
            {
                if (interval)
                {
                    string TrazeniDanOd = zadaniDatum.DayOfWeek.ToString();
                    string TrazeniDanDo = datumDo.DayOfWeek.ToString();
                    string TrazenaOznakaDanOd = pronadiOznakuDana(TrazeniDanOd);
                    string TrazenaOznakaDanDo = pronadiOznakuDana(TrazeniDanDo);

                    if (zadaniDatum.Date < datumDo.Date)
                    {
                        //Fali logika za dane između...Ovo je ako se radi o jednom danu
                        bool preklapanjeZaDatumOd = provjeriPreklapanje(TrazenaOznakaDanOd, zadaniDatum, raspored);
                        bool preklapanjeZaDatumDo = provjeriPreklapanje(TrazenaOznakaDanDo, datumDo, raspored);
                        if (preklapanjeZaDatumDo || preklapanjeZaDatumDo)
                        {
                            return StatusVeza.Zauzet;
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
                            if (zadaniDatum.TimeOfDay < vrijemeDoURasporedu && datumDo.TimeOfDay > vrijemeOdURasporedu)
                                return StatusVeza.Zauzet;
                        }
                    }
                }
                else
                {
                    string trenutniDan = zadaniDatum.DayOfWeek.ToString();
                    string oznakaTrenutnogDana = pronadiOznakuDana(trenutniDan);
                    if (raspored.Dani_u_tjednu.Contains(oznakaTrenutnogDana))
                    {
                        var vrijemeZaProvjeru = zadaniDatum.TimeOfDay;
                        var vrijemeOd = raspored.VrijemeOd.TimeOfDay;
                        var vrijemeDo = raspored.VrijemeDo.TimeOfDay;
                        if (vrijemeZaProvjeru < vrijemeDo && vrijemeZaProvjeru > vrijemeOd) return StatusVeza.Zauzet;
                    }
                }
            }
            return StatusVeza.Slobodan;
        }

        public bool provjeriPreklapanje(string oznakaDana, DateTime vrijemeZaProvjeru, Raspored raspored)
        {
            if (raspored.Dani_u_tjednu.Contains(oznakaDana))
            {
                var vrijemeOd = raspored.VrijemeOd.TimeOfDay;
                var vrijemeDo = raspored.VrijemeDo.TimeOfDay;
                if (vrijemeZaProvjeru.TimeOfDay < vrijemeDo && vrijemeZaProvjeru.TimeOfDay > vrijemeOd) return true;
            }
            return false;
        }

        private string pronadiOznakuDana(string trenutniDan)
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

    }
}
