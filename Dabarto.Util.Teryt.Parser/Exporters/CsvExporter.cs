using Dabarto.Util.Teryt.Parser.OutputModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Dabarto.Util.Teryt.Parser.Exporters
{
    public class CsvExporter
    {
        public void Export(Lokalizacje lokalizacje, string outputDirectoryPath)
        {
            ExportWojewodztwa(lokalizacje, Path.Combine(outputDirectoryPath, "wojewodztwa.csv"));
            ExportPowiaty(lokalizacje, Path.Combine(outputDirectoryPath, "powiaty.csv"));
            ExportGminy(lokalizacje, Path.Combine(outputDirectoryPath, "gminy.csv"));
            ExportMiejscowosci(lokalizacje, Path.Combine(outputDirectoryPath, "miejscowosci.csv"));
            ExportDzielnice(lokalizacje, Path.Combine(outputDirectoryPath, "dzielnice.csv"));
            ExportRejony(lokalizacje, Path.Combine(outputDirectoryPath, "rejony.csv"));
            ExportUlice(lokalizacje, Path.Combine(outputDirectoryPath, "ulice.csv"));
        }

        private void ExportWojewodztwa(Lokalizacje lokalizacje, string outputFileName)
        {
            const string header = "Lp.;Symbol;Nazwa;Stan na";
            WriteFile(outputFileName, lokalizacje.Wojewodztwa, header, q => $"{q.Lp};{q.Symbol};{q.Nazwa};{q.StanNa:d}");
        }

        private void ExportPowiaty(Lokalizacje lokalizacje, string outputFileName)
        {
            const string header = "Lp.;Symbol województwa;Symbol powiatu;Nazwa;Rodzaj;Stan na";
            WriteFile(outputFileName, lokalizacje.Powiaty, header, q => $"{q.Lp};{q.Wojewodztwo.Symbol};{q.Symbol};{q.Nazwa};{q.Rodzaj};{q.StanNa:d}");
        }

        private void ExportGminy(Lokalizacje lokalizacje, string outputFileName)
        {
            const string header = "Lp.;Symbol województwa;Symbol powiatu;Symbol gminy;Nazwa;Id rodzaju;Rodzaj;Stan na";
            WriteFile(outputFileName, lokalizacje.Gminy, header, q => $"{q.Lp};{q.Powiat.Wojewodztwo.Symbol};{q.Powiat.Symbol};{q.Symbol};{q.Nazwa};{q.RodzajId};{q.Rodzaj};{q.StanNa:d}");
        }

        private void ExportMiejscowosci(Lokalizacje lokalizacje, string outputFileName)
        {
            const string header = "Lp.;Symbol województwa;Symbol powiatu;Symbol gminy;Symbol miejscowości;Nazwa;Id rodzaju;Rodzaj;Nazwa dzielnic;Nazwa rejonów;Stan na";
            WriteFile(outputFileName, lokalizacje.Miejscowosci, header, q => $"{q.Lp};{q.Gmina.Powiat.Wojewodztwo.Symbol};{q.Gmina.Powiat.Symbol};{q.Gmina.Symbol};{q.Symbol};{q.Nazwa};{q.RodzajId};{q.Rodzaj};{q.NazwaDzielnic};{q.NazwaRejonow};{q.StanNa:d}");
        }

        private void ExportDzielnice(Lokalizacje lokalizacje, string outputFileName)
        {
            const string header = "Lp.;Symbol miejscowości;Symbol dzielnicy;Nazwa;Stan na";
            WriteFile(outputFileName, lokalizacje.Dzielnice, header, q => $"{q.Lp};{q.Miejscowosc.Symbol};{q.Symbol};{q.Nazwa};{q.StanNa:d}");
        }

        private void ExportRejony(Lokalizacje lokalizacje, string outputFileName)
        {
            if (lokalizacje.Rejony.Count == 0)
            {
                if (File.Exists(outputFileName))
                {
                    File.Delete(outputFileName);
                }
                return;
            }

            const string header = "Lp.;Symbol miejscowości;Symbol dzielnicy;Symbol rejonu;Nazwa;Stan na";
            WriteFile(outputFileName, lokalizacje.Rejony, header, q => $"{q.Lp};{q.Dzielnica.Miejscowosc.Symbol};{q.Dzielnica.Symbol};{q.Symbol};{q.Nazwa};{q.StanNa:d}");
        }

        private void ExportUlice(Lokalizacje lokalizacje, string outputFileName)
        {
            const string header = "Lp.;Symbol miejscowości;Symbol ulicy;Cecha;Nazwa 1;Nazwa 2;Stan na";
            WriteFile(outputFileName, lokalizacje.Ulice, header, q => $"{q.Lp};{q.Miejscowosc.Symbol};{q.Symbol};{q.Cecha};{q.Nazwa1};{q.Nazwa2};{q.StanNa:d}");
        }

        private void WriteFile<T>(string outputFileName, IReadOnlyList<T> list, string header, Func<T, string> lineProvider)
        {
            using (var fs = new FileStream(outputFileName, FileMode.Create, FileAccess.Write))
            {
                using (var sw = new StreamWriter(fs, Encoding.UTF8))
                {
                    sw.WriteLine(header);
                    var count = list.Count;
                    for (var i = 0; i < count; i++)
                    {
                        var loc = list[i];
                        var line = lineProvider(loc);
                        if (i == count - 1)
                        {
                            sw.Write(line);
                        }
                        else
                        {
                            sw.WriteLine(line);
                        }
                    }
                }
            }
        }
    }
}