namespace Dabarto.Util.Teryt.Parser.TerytModel
{
    public class Catalog
    {
        public string Name
        {
            get;
            set;
        }

        public string Type
        {
            get;
            set;
        }

        public string Date
        {
            get;
            set;
        }

        public Catalog(string name, string type, string date)
        {
            Name = name;
            Type = type;
            Date = date;
        }
    }
}