using atunjic_zadaca_3.Facade;
using atunjic_zadaca_3.Singleton_Podaci;
using atunjic_zadaca_3.VisitorIzvrsavanje.ConcreteKlase;
using atunjic_zadaca_3.VisitorIzvrsavanje;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using atunjic_zadaca_3.Singleton;

namespace atunjic_zadaca_3.MVC
{
    public class Controller
    {
        private View view;
        private Model model;

        VirVrijeme virtualnoVrijeme = VirVrijeme.getInstance();
        UpraviteljGresaka greske = UpraviteljGresaka.getInstance();

        public Controller(View view, Model model)
        {
            this.view = view;
            this.model = model;
        }

        public void pripremiIzvrsavanje(List<string> argumenti)
        {
            VirVrijeme vrijeme = VirVrijeme.getInstance();
            
            view.pripremiIspis();

            izvrsiUcitavanje(argumenti);

            vrijeme.inicijalizirajVirtualnoVrijeme();
            string naredba;
            do
            {
                view.zatraziUnos();
                naredba = Console.ReadLine();

                izvrsiNaredbu(naredba);

            } while (naredba.ToUpper() != "Q");
        }

        private void izvrsiUcitavanje(List<string> argumenti)
        {
            int index = 0;
            string nazivDatoteke = "";
            FacadeUcitaj ucitavanje = new FacadeUcitaj();
            List<String> greske;

            if (argumenti.Contains("-l"))
            {
                index = argumenti.IndexOf("-l");
                nazivDatoteke = argumenti[index + 1];

                greske = ucitavanje.ucitajLuku(nazivDatoteke);
                ispisiGreske(greske);
            }
            else
            {
                Console.WriteLine("ERROR 1: Datoteka luke je obavezna. Koristite zastavicu -l.");
                Environment.Exit(0);
            }

            if (argumenti.Contains("-v"))
            {
                index = argumenti.IndexOf("-v");
                nazivDatoteke = argumenti[index + 1];

                greske = ucitavanje.ucitajVezove(nazivDatoteke);
                ispisiGreske(greske);
            }
            else
            {
                Console.WriteLine("ERROR 1: Datoteka vezova je obavezna. Koristite zastavicu -v.");
                Environment.Exit(0);
            }

            if (argumenti.Contains("-b"))
            {
                index = argumenti.IndexOf("-b");
                nazivDatoteke = argumenti[index + 1];

                greske = ucitavanje.ucitajBrodove(nazivDatoteke);
                ispisiGreske(greske);
            }
            else
            {
                Console.WriteLine("ERROR 1: Datoteka brodova je obavezna. Koristite zastavicu -b.");
                Environment.Exit(0);
            }

            if (argumenti.Contains("-m"))
            {
                index = argumenti.IndexOf("-m");
                nazivDatoteke = argumenti[index + 1];

                greske = ucitavanje.ucitajMolove(nazivDatoteke);
                ispisiGreske(greske);
            }
            else
            {
                Console.WriteLine("ERROR 1: Datoteka molova je obavezna. Koristite zastavicu -m.");
                Environment.Exit(0);
            }

            if (argumenti.Contains("-k"))
            {
                index = argumenti.IndexOf("-k");
                nazivDatoteke = argumenti[index + 1];

                greske = ucitavanje.ucitajKanale(nazivDatoteke);
                ispisiGreske(greske);
            }
            else
            {
                Console.WriteLine("ERROR 1: Datoteka kanala je obavezna. Koristite zastavicu -m.");
                Environment.Exit(0);
            }

            if (argumenti.Contains("-mv"))
            {
                index = argumenti.IndexOf("-mv");
                nazivDatoteke = argumenti[index + 1];

                greske = ucitavanje.ucitajMolVezove(nazivDatoteke);
                ispisiGreske(greske);
            }
            else
            {
                Console.WriteLine("ERROR 1: Datoteka mol-vez je obavezna. Koristite zastavicu -mv.");
                Environment.Exit(0);
            }

            if (argumenti.Contains("-r"))
            {
                index = argumenti.IndexOf("-r");
                nazivDatoteke = argumenti[index + 1];

                greske = ucitavanje.ucitajRaspored(nazivDatoteke);
                ispisiGreske(greske);
            }
        }

        private void ispisiGreske(List<string> greske)
        {
            for (int i = 0; i < greske.Count; i++)
            {
                view.PrintErrorContent(greske[i]);
            }
        }

        public void izvrsiNaredbu(string naredba)
        {
            AktivnostConcreteVisitor aktivnostVisitor = new AktivnostConcreteVisitor(naredba);
            BrodskaLuka brodskaLuka = BrodskaLuka.getInstance();

            string[] polje = naredba.Split(' ');

            string pStatus = @"^I$";
            string pVirtualnoVrijeme = @"^VR [0-3]\d.[0-1]\d.[1-2]\d\d\d. [0-2]\d:[0-6]\d:[0-6]\d$";
            string pIspisVezova = @"^V ((PU)|(PO)|(OS)) (S|Z) [0-3]\d.[0-1]\d.[1-2]\d\d\d. [0-2]\d:[0-6]\d:[0-6]\d [0-3]\d.[0-1]\d.[1-2]\d\d\d. [0-2]\d:[0-6]\d:[0-6]\d$";
            string pDatoteka = @"^UR [^<>:;,?""|\/\\]+\.csv$";
            string pRezerviraniVez = @"^ZD [1-9]\d*$";
            string pSlobodanVez = @"^ZP [1-9]\d* [1-9]\d*$";
            string pSpajanjeBroda = @"^F [1-9]\d* [1-9]\d*( Q){0,1}$";
            string pUredenje = @"^T( Z| P| RB){0,3}$";
            string pBrojZauzetihVezova = @"^ZA [0-3]\d.[0-1]\d.[1-2]\d\d\d. [0-2]\d:[0-6]\d$";
            string pStatistikaBrodova = @"^SB( PU| PO| OS){1,3}$";

            Match mStatus = Regex.Match(naredba, pStatus, RegexOptions.IgnoreCase);
            Match mVirtualnoVrijeme = Regex.Match(naredba, pVirtualnoVrijeme, RegexOptions.IgnoreCase);
            Match mIspisVezova = Regex.Match(naredba, pIspisVezova, RegexOptions.IgnoreCase);
            Match mDatoteka = Regex.Match(naredba, pDatoteka, RegexOptions.IgnoreCase);
            Match mRezerviraniVez = Regex.Match(naredba, pRezerviraniVez, RegexOptions.IgnoreCase);
            Match mSlobodanVez = Regex.Match(naredba, pSlobodanVez, RegexOptions.IgnoreCase);
            Match mSpajanjeBroda = Regex.Match(naredba, pSpajanjeBroda, RegexOptions.IgnoreCase);
            Match mUredenje = Regex.Match(naredba, pUredenje, RegexOptions.IgnoreCase);
            Match mBrojZauzetihVezova = Regex.Match(naredba, pBrojZauzetihVezova, RegexOptions.IgnoreCase);
            Match mStatistikaBrodova = Regex.Match(naredba, pStatistikaBrodova, RegexOptions.IgnoreCase);

            if (mStatus.Success)
            {
                view.PrintWorkingContent(virtualnoVrijeme.ispisiVrijemeVirtualnogSata());
                Status status = new Status();
                status.prihvati(aktivnostVisitor);

                model.setData(brodskaLuka.radniPodaci);
                ispisiRadnePodatke();
            }

            else if (mVirtualnoVrijeme.Success)
            {
                view.PrintWorkingContent(virtualnoVrijeme.ispisiVrijemeVirtualnogSata());
                VirtualnoVrijeme virVrijeme = new VirtualnoVrijeme();
                virVrijeme.prihvati(aktivnostVisitor);

                if (brodskaLuka.radniPodaci.Count > 0)
                {
                    model.setData(brodskaLuka.radniPodaci);
                    ispisiRadnePodatke();
                }
                if (brodskaLuka.pogreskePodaci.Count > 0)
                {
                    model.setData(brodskaLuka.pogreskePodaci);
                    ispisiPogreskePodatke();
                }
            }
            else if (mIspisVezova.Success)
            {
                view.PrintWorkingContent(virtualnoVrijeme.ispisiVrijemeVirtualnogSata());
                IspisVezova ispisVezova = new IspisVezova();
                ispisVezova.prihvati(aktivnostVisitor);

                model.setData(brodskaLuka.radniPodaci);
                ispisiRadnePodatke();
            }
            else if (mDatoteka.Success)
            {
                view.PrintWorkingContent(virtualnoVrijeme.ispisiVrijemeVirtualnogSata());
                FacadeUcitaj ucitavanje = new FacadeUcitaj();
                List<string> greske = ucitavanje.ucitajRezervacije(polje[1]);
                ispisiGreske(greske);
            }
            else if (mRezerviraniVez.Success)
            {
                view.PrintWorkingContent(virtualnoVrijeme.ispisiVrijemeVirtualnogSata());
                ZahtjevZaRezerviraniVez zahRezervirani = new ZahtjevZaRezerviraniVez();
                zahRezervirani.prihvati(aktivnostVisitor);

                if (brodskaLuka.radniPodaci.Count > 0)
                {
                    model.setData(brodskaLuka.radniPodaci);
                    ispisiRadnePodatke();
                }
                if (brodskaLuka.pogreskePodaci.Count > 0)
                {
                    model.setData(brodskaLuka.pogreskePodaci);
                    ispisiPogreskePodatke();
                }
            }
            else if (mSlobodanVez.Success)
            {
                view.PrintWorkingContent(virtualnoVrijeme.ispisiVrijemeVirtualnogSata());
                ZahtjevZaSlobodanVez zahSlobodan = new ZahtjevZaSlobodanVez();
                zahSlobodan.prihvati(aktivnostVisitor);

                if (brodskaLuka.radniPodaci.Count > 0)
                {
                    model.setData(brodskaLuka.radniPodaci);
                    ispisiRadnePodatke();
                }
                if (brodskaLuka.pogreskePodaci.Count > 0)
                {
                    model.setData(brodskaLuka.pogreskePodaci);
                    ispisiPogreskePodatke();
                }
            }
            else if (mSpajanjeBroda.Success)
            {
                view.PrintWorkingContent(virtualnoVrijeme.ispisiVrijemeVirtualnogSata());
                Spajanje spajanje = new Spajanje();
                spajanje.prihvati(aktivnostVisitor);

                if (brodskaLuka.radniPodaci.Count > 0)
                {
                    model.setData(brodskaLuka.radniPodaci);
                    ispisiRadnePodatke();
                }
                if (brodskaLuka.pogreskePodaci.Count > 0)
                {
                    model.setData(brodskaLuka.pogreskePodaci);
                    ispisiPogreskePodatke();
                }
            }
            else if (mUredenje.Success && razliciteOpcije(naredba))
            {
                view.PrintWorkingContent(virtualnoVrijeme.ispisiVrijemeVirtualnogSata());
                UredenjeIspisa uredenje = new UredenjeIspisa();
                uredenje.prihvati(aktivnostVisitor);

                model.setData(brodskaLuka.radniPodaci);
                ispisiRadnePodatke();
            }
            else if (mBrojZauzetihVezova.Success)
            {
                view.PrintWorkingContent(virtualnoVrijeme.ispisiVrijemeVirtualnogSata());
                BrojZauzetihVezova brojZauzetih = new BrojZauzetihVezova();
                brojZauzetih.prihvati(aktivnostVisitor);

                model.setData(brodskaLuka.radniPodaci);
                ispisiRadnePodatke();
            }
            else if (mStatistikaBrodova.Success && razliciteOpcije(naredba))
            {
                view.PrintWorkingContent(virtualnoVrijeme.ispisiVrijemeVirtualnogSata());
                Statistika statistika = new Statistika();
                statistika.prihvati(aktivnostVisitor);

                model.setData(brodskaLuka.radniPodaci);
                ispisiRadnePodatke();
            }
            else if (naredba.ToUpper() == "Q")
            {
                view.ocistiEkran();
                Environment.Exit(0);
            }
            else
            {
                greske.brojGresaka += 1;
                view.PrintErrorContent($"ERROR {greske.brojGresaka} : Neispravna naredba.");
            }
        }

        private void ispisiPogreskePodatke()
        {
            List<string> reci = model.getData();
            for (int i = 0; i < reci.Count; i++)
            {
                view.PrintErrorContent(reci[i]);
            }
        }

        private void ispisiRadnePodatke()
        {
            List<string> reci = model.getData();
            for (int i = 0; i < reci.Count; i++)
            {
                view.PrintWorkingContent(reci[i]);
            }
        }

        private bool razliciteOpcije(string naredba)
        {
            string[] opcije = naredba.Split(' ');
            if (opcije.Length != opcije.Distinct().Count()) return false;
            return true;
        }
    }
}
