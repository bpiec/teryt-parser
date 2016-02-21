namespace Dabarto.Util.Teryt.Parser.TerytModel
{
    /// <summary>
    /// Identyfikator i nazwa jednostki podziału terytorialnego.
    /// <remarks>Uwaga, Identyfikator jednostki podziału terytorialnego składa się z symbolu województwa, powiatu, gminy i rodzaju gminy (razem 7 znaków ‒ znaki w przedziale 0-9 oraz spacje, które dopełniają symbol województwa lub powiatu lub gminy do 7 znaków).</remarks>
    /// </summary>
    public class TercRow
    {
        /// <summary>
        /// symbol województwa
        /// </summary>
        public string Woj
        {
            get;
            set;
        }

        /// <summary>
        /// symbol powiatu
        /// </summary>
        public string Pow
        {
            get;
            set;
        }

        /// <summary>
        /// symbol gminy
        /// </summary>
        public string Gmi
        {
            get;
            set;
        }

        /// <summary>
        /// symbol gminy
        /// <remarks>
        /// 1 - gmina miejska,
        /// 2 - gmina wiejska,
        /// 3 - gmina miejsko-wiejska,
        /// 4 - miasto w gminie miejsko-wiejskiej,
        /// 5 - obszar wiejski w gminie miejsko-wiejskiej,
        /// 8 - dzielnica w m.st. Warszawa,
        /// 9 - delegatury w gminach miejskich
        /// </remarks>
        /// </summary>
        public string Rodz
        {
            get;
            set;
        }

        /// <summary>
        /// nazwa województwa/ powiatu/ gminy
        /// </summary>
        public string Nazwa
        {
            get;
            set;
        }

        /// <summary>
        /// określenie jednostki
        /// <remarks>województwo; powiat; miasto na prawach powiatu; miasto stołeczne, na prawach powiatu; gmina miejska, miasto stołeczne; gmina miejska; gmina wiejska; gmina miejsko-wiejska; miasto; obszar wiejski; dzielnica; delegatura</remarks>
        /// </summary>
        public string NazDod
        {
            get;
            set;
        }

        /// <summary>
        /// data aktualizacji danych w systemie TERC w formacie RRRR-MM-DD
        /// </summary>
        public string StanNa
        {
            get;
            set;
        }
    }
}