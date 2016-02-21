using System.Collections.Generic;

namespace Dabarto.Util.Teryt.Parser.TerytModel
{
    /// <summary>
    /// Słownik symboli i nazw rodzajów miejscowości.
    /// </summary>
    public class WmRodz
    {
        public Catalog Catalog
        {
            get;
            set;
        }

        public List<WmRodzRow> Rows
        {
            get;
            set;
        }
    }
}