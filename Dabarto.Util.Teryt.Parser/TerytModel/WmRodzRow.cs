namespace Dabarto.Util.Teryt.Parser.TerytModel
{
    /// <summary>
    /// Symbol i nazwa rodzaju miejscowości.
    /// <remarks>
    /// Symbole i nazwy rodzajów miejscowości wg stanu na dzień 2013-02-28:
    /// 00 część miejscowości
    /// 01 wieś
    /// 02 kolonia
    /// 03 przysiółek
    /// 04 osada
    /// 05 osada leśna
    /// 06 osiedle
    /// 07 schronisko turystyczne
    /// 95 dzielnica m. st. Warszawy
    /// 96 miasto
    /// 98 delegatura
    /// 99 część miasta
    /// </remarks>
    /// </summary>
    public class WmRodzRow
    {
        /// <summary>
        /// symbol rodzaju miejscowości
        /// </summary>
        public string Rm
        {
            get;
            set;
        }

        /// <summary>
        /// nazwa rodzaju miejscowości
        /// </summary>
        public string NazwaRm
        {
            get;
            set;
        }

        /// <summary>
        /// data danych w formacie RRRR-MM-DD
        /// </summary>
        public string StanNa
        {
            get;
            set;
        }
    }
}