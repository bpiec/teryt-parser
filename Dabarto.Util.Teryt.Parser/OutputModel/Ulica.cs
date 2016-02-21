using System;

namespace Dabarto.Util.Teryt.Parser.OutputModel
{
    public class Ulica
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

        public string Cecha
        {
            get;
            set;
        }

        public string Nazwa1
        {
            get;
            set;
        }

        public string Nazwa2
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
            var name = string.IsNullOrWhiteSpace(Nazwa2) ? Nazwa1 : string.Concat(Nazwa2, " ", Nazwa1);
            return $"{Miejscowosc} / {Symbol} {Cecha} {name}";
        }
    }
}