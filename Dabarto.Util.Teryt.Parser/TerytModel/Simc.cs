using System.Collections.Generic;

namespace Dabarto.Util.Teryt.Parser.TerytModel
{
    /// <summary>
    /// Zbiór identyfikatorów (urzędowych) i nazw miejscowości w systemie SIMC.
    /// </summary>
    public class Simc
    {
        public Catalog Catalog
        {
            get;
            set;
        }

        public List<SimcRow> Rows
        {
            get;
            set;
        }
    }
}