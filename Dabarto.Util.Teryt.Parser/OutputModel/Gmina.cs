using System;

namespace Dabarto.Util.Teryt.Parser.OutputModel
{
    public class Gmina
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

        public Powiat Powiat
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
            return $"{Powiat} / {Symbol} {Nazwa} ({Rodzaj})";
        }
    }
}