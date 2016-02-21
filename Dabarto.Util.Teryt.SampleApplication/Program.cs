using Dabarto.Util.Teryt.Parser;
using Dabarto.Util.Teryt.Parser.Exporters;
using System;
using System.IO;

namespace Dabarto.Util.Teryt.SampleApplication
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                PrintUsage();
                return;
            }

            var path = args[0];
            if (!ValidateFiles(path))
            {
                return;
            }

            var natural = args.Length > 1 && args[1] == "1";

            Console.WriteLine("{0:T} Parsowanie plików TERYT...", DateTime.Now);
            var teryt = new TerytParser().Parse(path);

            Console.WriteLine("{0:T} Konwersja na format wewnętrzny...", DateTime.Now);
            var locations = new OutputModelCreator().Create(teryt, natural);

            Console.WriteLine("{0:T} Eksport do plików...", DateTime.Now);
            new SqlQueryExporter().Export(locations, AppDomain.CurrentDomain.BaseDirectory);

            Console.WriteLine("{0:T} Gotowe", DateTime.Now);
        }

        private static void PrintUsage()
        {
            Console.WriteLine("Usage: {0} teryt_xml_directory_path", AppDomain.CurrentDomain.FriendlyName);
        }

        private static bool ValidateFiles(string path)
        {
            if (!File.Exists(Path.Combine(path, "simc.xml")))
            {
                Console.WriteLine("Error: simc.xml file not found");
                return false;
            }

            if (!File.Exists(Path.Combine(path, "terc.xml")))
            {
                Console.WriteLine("Error: terc.xml file not found");
                return false;
            }

            if (!File.Exists(Path.Combine(path, "ulic.xml")))
            {
                Console.WriteLine("Error: ulic.xml file not found");
                return false;
            }

            if (!File.Exists(Path.Combine(path, "wmrodz.xml")))
            {
                Console.WriteLine("Error: wmrodz.xml file not found");
                return false;
            }

            return true;
        }
    }
}