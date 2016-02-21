using Dabarto.Util.Teryt.Parser.OutputModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Dabarto.Util.Teryt.Parser.Exporters
{
    public class SqlQueryExporter
    {
        public void Export(Lokalizacje lokalizacje, string outputDirectoryPath)
        {
            ExportWojewodztwa(lokalizacje, Path.Combine(outputDirectoryPath, "wojewodztwa.sql"));
            ExportPowiaty(lokalizacje, Path.Combine(outputDirectoryPath, "powiaty.sql"));
            ExportGminy(lokalizacje, Path.Combine(outputDirectoryPath, "gminy.sql"));
            ExportMiejscowosci(lokalizacje, Path.Combine(outputDirectoryPath, "miejscowosci.sql"));
            ExportDzielnice(lokalizacje, Path.Combine(outputDirectoryPath, "dzielnice.sql"));
            ExportRejony(lokalizacje, Path.Combine(outputDirectoryPath, "rejony.sql"));
            ExportUlice(lokalizacje, Path.Combine(outputDirectoryPath, "ulice.sql"));
        }

        private void ExportWojewodztwa(Lokalizacje lokalizacje, string outputFileName)
        {
            WriteFile(outputFileName, lokalizacje.Wojewodztwa, q => $"INSERT INTO `location_province` (`Id`, `Name`, `TerytId`) VALUES ({q.Lp}, '{q.Nazwa}', '{q.Symbol}');");
        }

        private void ExportPowiaty(Lokalizacje lokalizacje, string outputFileName)
        {
            WriteFile(outputFileName, lokalizacje.Powiaty, q => $"INSERT INTO `location_county` (`Id`, `ProvinceId`, `Name`, `Type`, `TerytId`) VALUES ({q.Lp}, (SELECT `Id` FROM `location_province` WHERE `TerytId` = {q.Wojewodztwo.Symbol}), '{q.Nazwa}', '{q.Rodzaj}', '{q.Symbol}');");
        }

        private void ExportGminy(Lokalizacje lokalizacje, string outputFileName)
        {
            WriteFile(outputFileName, lokalizacje.Gminy, q => $"INSERT INTO `location_commune` (`Id`, `CountyId`, `Name`, `Type`, `TerytId`) VALUES ({q.Lp}, (SELECT `location_county`.`Id` FROM `location_county` JOIN `location_province` ON `location_county`.`ProvinceId` = `location_province`.`Id` WHERE `location_province`.`TerytId` = '{q.Powiat.Wojewodztwo.Symbol}' AND `location_county`.`TerytId` = '{q.Powiat.Symbol}'), '{q.Nazwa}', '{q.Rodzaj}', '{q.Symbol}');");
        }

        private void ExportMiejscowosci(Lokalizacje lokalizacje, string outputFileName)
        {
            WriteFile(outputFileName, lokalizacje.Miejscowosci, q => $"INSERT INTO `location_city` (`Id`, `CommuneId`, `Name`, `Type`, `DistrictsName`, `RegionsName`, `TerytId`) VALUES ({q.Lp}, (SELECT `location_commune`.`Id` FROM `location_commune` JOIN `location_county` ON `location_commune`.`CountyId` = `location_county`.`Id` JOIN `location_province` ON `location_county`.`ProvinceId` = `location_province`.`Id` WHERE `location_province`.`TerytId` = '{q.Gmina.Powiat.Wojewodztwo.Symbol}' AND `location_county`.`TerytId` = '{q.Gmina.Powiat.Symbol}' AND `location_commune`.`TerytId` = '{q.Gmina.Symbol}'), '{q.Nazwa}', '{q.Rodzaj}', '{q.NazwaDzielnic}', '{q.NazwaRejonow}', '{q.Symbol}');");
        }

        private void ExportDzielnice(Lokalizacje lokalizacje, string outputFileName)
        {
            WriteFile(outputFileName, lokalizacje.Dzielnice, q => $"INSERT INTO `location_citydistrict` (`Id`, `CityId`, `Name`, `TerytId`) VALUES ({q.Lp}, (SELECT `Id` FROM `location_city` WHERE `TerytId` = '{q.Miejscowosc.Symbol}'), '{q.Nazwa}', '{q.Symbol}');");
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

            WriteFile(outputFileName, lokalizacje.Rejony, q => $"INSERT INTO `location_cityregion` (`Id`, `CityDistrictId`, `Name`, `TerytId`) VALUES ({q.Lp}, (SELECT `location_citydistrict`.`Id` FROM `location_citydistrict` JOIN `location_city` ON `location_citydistrict`.`CityId` = `location_city`.`Id` WHERE `location_city`.`TerytId` = '{q.Dzielnica.Miejscowosc.Symbol}' AND `location_citydistrict`.`TerytId` = '{q.Dzielnica.Symbol}'), '{q.Nazwa}', '{q.Symbol}');");
        }

        private void ExportUlice(Lokalizacje lokalizacje, string outputFileName)
        {
            WriteFile(outputFileName, lokalizacje.Ulice, q => $"INSERT INTO `location_street` (`Id`, `CityId`, `Attribute`, `Name1`, `Name2`, `TerytId`) VALUES ({q.Lp}, (SELECT `Id` FROM `location_city` WHERE `TerytId` = '{q.Miejscowosc.Symbol}'), '{q.Cecha}', '{q.Nazwa1.Replace("'", "\\'")}', '{q.Nazwa2.Replace("'", "\\'")}', '{q.Symbol}');");
        }

        private void WriteFile<T>(string outputFileName, IReadOnlyList<T> list, Func<T, string> lineProvider)
        {
            using (var fs = new FileStream(outputFileName, FileMode.Create, FileAccess.Write))
            {
                using (var sw = new StreamWriter(fs, Encoding.UTF8))
                {
                    var count = list.Count;
                    for (var i = 0; i < count; i++)
                    {
                        var loc = list[i];
                        var line = lineProvider(loc);
                        sw.WriteLine(line);
                    }
                }
            }
        }
    }
}