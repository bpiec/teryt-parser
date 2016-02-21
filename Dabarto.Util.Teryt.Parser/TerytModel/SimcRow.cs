namespace Dabarto.Util.Teryt.Parser.TerytModel
{
    /// <summary>
    /// Identyfikator (urzędowy) i nazwa miejscowości w systemie SIMC.
    /// </summary>
    public class SimcRow
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
        /// symbol miasta, gminy, dzielnicy lub delegatury
        /// </summary>
        public string Gmi
        {
            get;
            set;
        }

        /// <summary>
        /// symbol rodzaju jednostki
        /// <remarks>
        /// 1 - gmina miejska,
        /// 2 - gmina wiejska,
        /// 3 - gmina miejsko-wiejska,
        /// 4 - miasto w gminie miejsko-wiejskiej,
        /// 5 - obszar wiejski gminy miejsko-wiejskiej,
        /// 8 - dzielnica w m.st. Warszawa,
        /// 9 - delegatury w gminach miejskich
        /// </remarks>
        /// </summary>
        public string RodzGmi
        {
            get;
            set;
        }

        /// <summary>
        /// rodzaj miejscowości
        /// </summary>
        public string Rm
        {
            get;
            set;
        }

        /// <summary>
        /// występowanie nazwy zwyczajowej (0-tak,1-nie)
        /// </summary>
        public string Mz
        {
            get;
            set;
        }

        /// <summary>
        /// nazwa miejscowości
        /// </summary>
        public string Nazwa
        {
            get;
            set;
        }

        /// <summary>
        /// identyfikator miejscowości
        /// </summary>
        public string Sym
        {
            get;
            set;
        }

        /// <summary>
        /// identyfikator miejscowości podstawowej
        /// <remarks>
        /// ‒ dla części miejscowości wiejskich ‒ identyfikator miejscowości, do której dana część należy,
        /// ‒ dla części miast ‒ identyfikator danego miasta (w miastach posiadających dzielnice/delegatury ‒ identyfikator tej jednostki)
        /// </remarks>
        /// </summary>
        public string SymPod
        {
            get;
            set;
        }

        /// <summary>
        /// data aktualizacji danych w podsystemie SIMC w formacie RRRR-MM-DD
        /// </summary>
        public string StanNa
        {
            get;
            set;
        }
    }
}