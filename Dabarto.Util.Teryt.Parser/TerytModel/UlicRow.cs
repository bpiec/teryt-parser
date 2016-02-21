namespace Dabarto.Util.Teryt.Parser.TerytModel
{
    /// <summary>
    /// Identyfikator i nazwa ulicy w systemie ULIC.
    /// </summary>
    public class UlicRow
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
        /// identyfikator miejscowości
        /// </summary>
        public string Sym
        {
            get;
            set;
        }

        /// <summary>
        /// identyfikator ulicy
        /// </summary>
        public string SymUl
        {
            get;
            set;
        }

        /// <summary>
        /// określenie rodzaju ulicy
        /// <remarks>UL., AL., PL., SKWER, BULW., RONDO, PARK, RYNEK, SZOSA, DROGA, OS., OGRÓD, WYSPA, WYB., INNE</remarks>
        /// </summary>
        public string Cecha
        {
            get;
            set;
        }

        /// <summary>
        /// część nazwy począwszy od słowa, które decyduje o pozycji ulicy w układzie alfabetycznym, aż do końca nazwy
        /// <remarks>W przypadku, gdy pole Nazwa2 nie jest puste, aby otrzymać nazwę ulicy w pełnym brzmieniu, człony nazwy należy ułożyć w kolejności: Nazwa2, Nazwa1</remarks>
        /// </summary>
        public string Nazwa1
        {
            get;
            set;
        }

        /// <summary>
        /// pozostała część nazwy lub pole puste
        /// </summary>
        public string Nazwa2
        {
            get;
            set;
        }

        /// <summary>
        /// data aktualizacji danych w podsystemie ULIC w formacie RRRR-MM-DD
        /// </summary>
        public string StanNa
        {
            get;
            set;
        }
    }
}