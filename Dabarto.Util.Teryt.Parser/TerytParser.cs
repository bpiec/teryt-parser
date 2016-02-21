using Dabarto.Util.Teryt.Parser.TerytModel;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Dabarto.Util.Teryt.Parser
{
    public class TerytParser
    {
        public TerytModel.Teryt Parse(string path)
        {
            var teryt = new TerytModel.Teryt();

            var simcPath = Path.Combine(path, "simc.xml");
            var tercPath = Path.Combine(path, "terc.xml");
            var ulicPath = Path.Combine(path, "ulic.xml");
            var wmRodzPath = Path.Combine(path, "wmrodz.xml");

            teryt.Simc = ParseSimc(simcPath);
            teryt.Terc = ParseTerc(tercPath);
            teryt.Ulic = ParseUlic(ulicPath);
            teryt.WmRodz = ParseWmRodz(wmRodzPath);

            return teryt;
        }

        public Simc ParseSimc(string simcPath)
        {
            var simcXml = new XmlDocument();
            simcXml.Load(simcPath);

            var simc = new Simc();
            var simcCatalog = simcXml.SelectSingleNode("/teryt/catalog");
            simc.Catalog = new Catalog(simcCatalog.Attributes["name"].Value, simcCatalog.Attributes["type"].Value, simcCatalog.Attributes["date"].Value);
            simc.Rows = new List<SimcRow>();

            foreach (XmlNode row in simcXml.SelectNodes("/teryt/catalog/row"))
            {
                var simcRow = new SimcRow();
                foreach (XmlNode col in row.SelectNodes("col"))
                {
                    var name = col.Attributes["name"].Value;
                    switch (name)
                    {
                        case "WOJ":
                            simcRow.Woj = col.InnerText;
                            break;

                        case "POW":
                            simcRow.Pow = col.InnerText;
                            break;

                        case "GMI":
                            simcRow.Gmi = col.InnerText;
                            break;

                        case "RODZ_GMI":
                            simcRow.RodzGmi = col.InnerText;
                            break;

                        case "RM":
                            simcRow.Rm = col.InnerText;
                            break;

                        case "MZ":
                            simcRow.Mz = col.InnerText;
                            break;

                        case "NAZWA":
                            simcRow.Nazwa = col.InnerText;
                            break;

                        case "SYM":
                            simcRow.Sym = col.InnerText;
                            break;

                        case "SYMPOD":
                            simcRow.SymPod = col.InnerText;
                            break;

                        case "STAN_NA":
                            simcRow.StanNa = col.InnerText;
                            break;
                    }
                }

                simc.Rows.Add(simcRow);
            }

            return simc;
        }

        public Terc ParseTerc(string tercPath)
        {
            var tercXml = new XmlDocument();
            tercXml.Load(tercPath);

            var terc = new Terc();
            var tercCatalog = tercXml.SelectSingleNode("/teryt/catalog");
            terc.Catalog = new Catalog(tercCatalog.Attributes["name"].Value, tercCatalog.Attributes["type"].Value, tercCatalog.Attributes["date"].Value);
            terc.Rows = new List<TercRow>();

            foreach (XmlNode row in tercXml.SelectNodes("/teryt/catalog/row"))
            {
                var tercRow = new TercRow();
                foreach (XmlNode col in row.SelectNodes("col"))
                {
                    var name = col.Attributes["name"].Value;
                    switch (name)
                    {
                        case "WOJ":
                            tercRow.Woj = col.InnerText;
                            break;

                        case "POW":
                            tercRow.Pow = col.InnerText;
                            break;

                        case "GMI":
                            tercRow.Gmi = col.InnerText;
                            break;

                        case "RODZ":
                            tercRow.Rodz = col.InnerText;
                            break;

                        case "NAZWA":
                            tercRow.Nazwa = col.InnerText;
                            break;

                        case "NAZDOD":
                            tercRow.NazDod = col.InnerText;
                            break;

                        case "STAN_NA":
                            tercRow.StanNa = col.InnerText;
                            break;
                    }
                }

                terc.Rows.Add(tercRow);
            }

            return terc;
        }

        public Ulic ParseUlic(string ulicPath)
        {
            var ulicXml = new XmlDocument();
            ulicXml.Load(ulicPath);

            var ulic = new Ulic();
            var ulicCatalog = ulicXml.SelectSingleNode("/teryt/catalog");
            ulic.Catalog = new Catalog(ulicCatalog.Attributes["name"].Value, ulicCatalog.Attributes["type"].Value, ulicCatalog.Attributes["date"].Value);
            ulic.Rows = new List<UlicRow>();

            foreach (XmlNode row in ulicXml.SelectNodes("/teryt/catalog/row"))
            {
                var ulicRow = new UlicRow();
                foreach (XmlNode col in row.SelectNodes("col"))
                {
                    var name = col.Attributes["name"].Value;
                    switch (name)
                    {
                        case "WOJ":
                            ulicRow.Woj = col.InnerText;
                            break;

                        case "POW":
                            ulicRow.Pow = col.InnerText;
                            break;

                        case "GMI":
                            ulicRow.Gmi = col.InnerText;
                            break;

                        case "RODZ_GMI":
                            ulicRow.RodzGmi = col.InnerText;
                            break;

                        case "SYM":
                            ulicRow.Sym = col.InnerText;
                            break;

                        case "SYM_UL":
                            ulicRow.SymUl = col.InnerText;
                            break;

                        case "CECHA":
                            ulicRow.Cecha = col.InnerText;
                            break;

                        case "NAZWA_1":
                            ulicRow.Nazwa1 = col.InnerText;
                            break;

                        case "NAZWA_2":
                            ulicRow.Nazwa2 = col.InnerText;
                            break;

                        case "STAN_NA":
                            ulicRow.StanNa = col.InnerText;
                            break;
                    }
                }

                ulic.Rows.Add(ulicRow);
            }

            return ulic;
        }

        public WmRodz ParseWmRodz(string wmRodzPath)
        {
            var wmRodzXml = new XmlDocument();
            wmRodzXml.Load(wmRodzPath);

            var wmRodz = new WmRodz();
            var tercCatalog = wmRodzXml.SelectSingleNode("/teryt/catalog");
            wmRodz.Catalog = new Catalog(tercCatalog.Attributes["name"].Value, tercCatalog.Attributes["type"].Value, tercCatalog.Attributes["date"].Value);
            wmRodz.Rows = new List<WmRodzRow>();

            foreach (XmlNode row in wmRodzXml.SelectNodes("/teryt/catalog/row"))
            {
                var wmRodzRow = new WmRodzRow();
                foreach (XmlNode col in row.SelectNodes("col"))
                {
                    var name = col.Attributes["name"].Value;
                    switch (name)
                    {
                        case "RM":
                            wmRodzRow.Rm = col.InnerText;
                            break;

                        case "NAZWA_RM":
                            wmRodzRow.NazwaRm = col.InnerText.TrimEnd();
                            break;

                        case "STAN_NA":
                            wmRodzRow.StanNa = col.InnerText;
                            break;
                    }
                }

                wmRodz.Rows.Add(wmRodzRow);
            }

            return wmRodz;
        }
    }
}