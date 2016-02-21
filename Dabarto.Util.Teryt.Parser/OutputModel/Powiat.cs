using System;

namespace Dabarto.Util.Teryt.Parser.OutputModel
{
    public class Powiat
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

        public Wojewodztwo Wojewodztwo
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
            var type = Rodzaj == "powiat" ? string.Empty : string.Concat(" (", Rodzaj, ")");
            return $"{Wojewodztwo} / {Symbol} {Nazwa}{type}";
        }
    }
}