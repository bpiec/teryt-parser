using System.Collections.Generic;

namespace Dabarto.Util.Teryt.Parser.TerytModel
{
    /// <summary>
    /// Zbiór identyfikatorów i nazw jednostek podziału terytorialnego.
    /// </summary>
    public class Terc
    {
        public Catalog Catalog
        {
            get;
            set;
        }

        public List<TercRow> Rows
        {
            get;
            set;
        }
    }
}