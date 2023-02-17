using atunjic_zadaca_3.Composite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atunjic_zadaca_3.Facade
{
    public class FacadeUcitaj
    {
        private IUcitaj luka;
        private IUcitaj vez;
        private IUcitaj brod;
        private IUcitaj raspored;
        private IUcitaj rezervacija;
        private IUcitaj mol;
        private IUcitaj kanal;
        private IUcitaj molVezovi;

        public FacadeUcitaj()
        {
            luka = new UcitajLuku();
            vez = new UcitajVezove();
            brod = new UcitajBrodove();
            raspored = new UcitajRaspored();
            rezervacija = new UcitajRezervacije();
            mol = new UcitajMolove();
            kanal = new UcitajKanale();
            molVezovi = new UcitajMolVezove();
        }

        public List<string> ucitajLuku(string nazivDatoteke)
        {
            return luka.ucitaj(nazivDatoteke);
        }

        public List<string> ucitajVezove(string nazivDatoteke)
        {
            return vez.ucitaj(nazivDatoteke);
        }

        public List<string> ucitajBrodove(string nazivDatoteke)
        {
            return brod.ucitaj(nazivDatoteke);
        }

        public List<string> ucitajRaspored(string nazivDatoteke)
        {
            return raspored.ucitaj(nazivDatoteke);
        }

        public List<string> ucitajRezervacije(string nazivDatoteke)
        {
            return rezervacija.ucitaj(nazivDatoteke);
        }

        public List<string> ucitajMolove(string nazivDatoteke)
        {
            return mol.ucitaj(nazivDatoteke);
        }

        public List<string> ucitajKanale(string nazivDatoteke)
        {
            return kanal.ucitaj(nazivDatoteke);
        }

        public List<string> ucitajMolVezove(string nazivDatoteke)
        {
            return molVezovi.ucitaj(nazivDatoteke);
        }
    }
}
