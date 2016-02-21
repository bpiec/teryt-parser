using System;

namespace Dabarto.Util.Teryt.Parser.OutputModel
{
    public class Dzielnica
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

        public Miejscowosc Miejscowosc
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
            return $"{Miejscowosc} / {Symbol} {Nazwa}";
        }
    }
}