using System;

namespace Dabarto.Util.Teryt.Parser.OutputModel
{
    public class Wojewodztwo
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

        public DateTime StanNa
        {
            get;
            set;
        }

        public override string ToString()
        {
            return string.Concat(Symbol, " ", Nazwa);
        }
    }
}