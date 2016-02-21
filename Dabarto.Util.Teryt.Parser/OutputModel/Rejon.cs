using System;

namespace Dabarto.Util.Teryt.Parser.OutputModel
{
    public class Rejon
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

        public Dzielnica Dzielnica
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
            return $"{Dzielnica} / {Symbol} {Nazwa}";
        }
    }
}