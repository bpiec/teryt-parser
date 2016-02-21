using System;

namespace Dabarto.Util.Teryt.Parser.OutputModel
{
    public class Miejscowosc
    {
        public long Lp
        {
            get;
            set;
        }

        public string Symbol
        {
            get;
            set;
        }

        public string Nazwa
        {
            get;
            set;
        }

        public string Rodzaj
        {
            get;
            set;
        }

        public string RodzajId
        {
            get;
            set;
        }

        public string NazwaDzielnic
        {
            get;
            set;
        }

        public string NazwaRejonow
        {
            get;
            set;
        }

        public Gmina Gmina
        {
            get;
            set;
        }

        public DateTime StanNa
        {
            get;
            set;
        }

        public override string ToString()
        {
            return $"{Gmina} / {Symbol} {Nazwa} ({Rodzaj})";
        }
    }
}