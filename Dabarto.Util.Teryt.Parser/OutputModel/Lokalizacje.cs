using System.Collections.Generic;

namespace Dabarto.Util.Teryt.Parser.OutputModel
{
    public class Lokalizacje
    {
        public List<Wojewodztwo> Wojewodztwa
        {
            get;
            set;
        }

        public List<Powiat> Powiaty
        {
            get;
            set;
        }

        public List<Gmina> Gminy
        {
            get;
            set;
        }

        public List<Miejscowosc> Miejscowosci
        {
            get;
            set;
        }

        public List<Dzielnica> Dzielnice
        {
            get;
            set;
        }

        public List<Rejon> Rejony
        {
            get;
            set;
        }

        public List<Ulica> Ulice
        {
            get;
            set;
        }
    }
}