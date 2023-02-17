using atunjic_zadaca_3.Singleton_Podaci;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace atunjic_zadaca_3.MVC
{
    public class View
    {
        public int MaksimalanBrojRedova { get; set; }
        public string Omjer { get; set; }
        public string Uloga { get; set; }

        public View(int maksimalanBrojRedova, string omjer, string uloga)
        {
            MaksimalanBrojRedova = maksimalanBrojRedova;
            //Console.BufferHeight = maksimalanBrojRedova;
            Omjer = omjer;
            Uloga = uloga;
        }

        private bool GornjiRadni = true;
        private bool DonjiPogreske = true;
        
        private int _workingProportion;
        private int _errorProportion;
        
        private string[] _workingContent;
        private string[] _errorContent;
        
        private int _currentRow = 0;
        private int _currentErrorRow = 0;

        public void pripremiIspis()
        {
            PodijeliEkran();

            _workingContent = new string[_workingProportion];
            _errorContent = new string[_errorProportion];

            Console.Write("\x1b[2J"); // briše sve postojeće sadržaje ekrana
        }

        private void PodijeliEkran()
        {
            string[] poljeOmjera = Omjer.Split(':');
            float prviPostotak = float.Parse(poljeOmjera[0])/100;
            float drugiPostotak = float.Parse(poljeOmjera[1])/100;

            string[] poljeUloga = Uloga.Split(':');

            switch (poljeUloga[0])
            {
                case "R":
                    _workingProportion = (int)(MaksimalanBrojRedova * prviPostotak); // broj redaka za gornji dio ekrana (radni prozor)
                    _errorProportion = MaksimalanBrojRedova - _workingProportion; // broj redaka za donji dio ekrana (prozor za pogreške)
                    break;
                case "P":
                    GornjiRadni = false;
                    DonjiPogreske = false;
                    _workingProportion = (int)(MaksimalanBrojRedova * drugiPostotak); // broj redaka za donji dio ekrana (radni prozor)
                    _errorProportion = MaksimalanBrojRedova - _workingProportion; // broj redaka za gornji dio ekrana (prozor za pogreške)
                    break;
                default:
                    break;
            }
        }

        public void ocistiEkran()
        {
            Console.Write("\x1b[2J"); // briše sve postojeće sadržaje ekrana
        }

        public void PrintWorkingContent(string content)
        {
            if (GornjiRadni)
            {
                if (_currentRow >= _workingProportion - 1)
                    ScrollUp(content);
                else
                    _workingContent[_currentRow] = content;

                Console.Write($"\x1b[{_currentRow + 1};1H");
                //content = content.Length > Console.WindowWidth ? content.Substring(0, Console.WindowWidth - 3) + "..." : content;
                Console.Write(content);
                
                _currentRow++;
            }
            else
            {
                if (_currentRow >= _workingProportion - 1)
                    ScrollUpDonji(content);
                else
                    _workingContent[_currentRow] = content;

                Console.Write($"\x1b[{_errorProportion + _currentRow + 1};1H");
                Console.Write(content);

                _currentRow++;
            }
        }

        private void ScrollUpDonji(string content)
        {
            for (int i = 1; i < _workingProportion - 1; i++)
            {
                string noviRed = _workingContent[i];
                _workingContent[i - 1] = noviRed;
            }
            _workingContent[_workingProportion - 2] = content;
            _workingContent[_workingProportion - 1] = "";

            for (int i = 1; i < _workingProportion; i++)
            {
                Console.Write($"\x1b[{_errorProportion + i};0H");
                Console.Write("\x1b[2K");
                string zamjenskiRed = _workingContent[i - 1];
                Console.Write(zamjenskiRed);
            }
            _currentRow--;
        }

        public void ScrollUp(string content)
        {
            for (int i = 1; i < _workingProportion - 1; i++)
            {
                string noviRed = _workingContent[i];
                _workingContent[i - 1] = noviRed;
            }
            _workingContent[_workingProportion-2] = content;
            _workingContent[_workingProportion-1] = "";

            for (int i = 1; i < _workingProportion; i++)
            {
                Console.Write($"\x1b[{i};0H");
                Console.Write("\x1b[2K");
                string zamjenskiRed = _workingContent[i - 1];
                Console.Write(zamjenskiRed);
                
            }
            _currentRow--;
        }

        public void PrintErrorContent(string content)
        {
            if (DonjiPogreske)
            {
                if (_currentErrorRow >= _errorProportion - 1)
                    ScrollUpErrorProportion(content);
                else
                    _errorContent[_currentErrorRow] = content;

                Console.Write("\x1b[" + (_workingProportion + _currentErrorRow + 1) + ";1H");
                //content = content.Length > Console.WindowWidth ? content.Substring(0, Console.WindowWidth - 3) + "..." : content;
                Console.Write(content);

                _currentErrorRow++;
            }
            else
            {
                if (_currentErrorRow >= _errorProportion - 1)
                    ScrollUpErrorProportionGornji(content);
                else
                    _errorContent[_currentErrorRow] = content;

                Console.Write($"\x1b[{_currentErrorRow + 1};1H");
                Console.Write(content);

                _currentErrorRow++;
            }
           
        }

        private void ScrollUpErrorProportionGornji(string content)
        {
            for (int i = 1; i < _errorProportion - 1; i++)
            {
                string noviRed = _errorContent[i];
                _errorContent[i - 1] = noviRed;
            }
            _errorContent[_errorProportion - 2] = content;
            _errorContent[_errorProportion - 1] = "";

            for (int i = 1; i < _errorProportion; i++)
            {
                Console.Write($"\x1b[{i};0H");
                Console.Write("\x1b[2K");
                string zamjenskiRed = _errorContent[i - 1];
                Console.Write(zamjenskiRed);

            }
            _currentErrorRow--;
        }

        public void ScrollUpErrorProportion(string content)
        {
            for (int i = 1; i < _errorProportion - 1; i++)
            {
                string noviRed = _errorContent[i];
                _errorContent[i - 1] = noviRed;
            }
            _errorContent[_errorProportion - 2] = content;
            _errorContent[_errorProportion - 1] = "";

            for (int i = 1; i < _errorProportion; i++)
            {
                Console.Write($"\x1b[{_workingProportion + i};0H");
                Console.Write("\x1b[2K");
                string zamjenskiRed = _errorContent[i - 1];
                Console.Write(zamjenskiRed);
            }
            _currentErrorRow--;
        }

        public void zatraziUnos()
        {
            this.PrintWorkingContent("Izaberite aktivnost: ");
        }
    }
}
