using System.Collections.Generic;

namespace Dabarto.Util.Teryt.Parser.TerytModel
{
    /// <summary>
    /// Zbiór identyfikatorów i nazw ulic w systemie ULIC.
    /// </summary>
    public class Ulic
    {
        public Catalog Catalog
        {
            get;
            set;
        }

        public List<UlicRow> Rows
        {
            get;
            set;
        }
    }
}