using Dabarto.Util.Teryt.Parser.OutputModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dabarto.Util.Teryt.Parser
{
    public class OutputModelCreator
    {
        private Dictionary<string, Miejscowosc> _miejscowosciDictionary;

        public Lokalizacje Create(TerytModel.Teryt teryt, bool useNaturalDistricts)
        {
            var loc = new Lokalizacje
            {
                Wojewodztwa = GetWojewodztwa(teryt)
            };
            loc.Powiaty = GetPowiaty(teryt, loc.Wojewodztwa);
            loc.Gminy = GetGminy(teryt, loc.Powiaty);
            loc.Miejscowosci = GetMiejscowosci(teryt, loc.Gminy);
            loc.Dzielnice = GetDzielnice(teryt);
            loc.Rejony = new List<Rejon>();
            loc.Ulice = GetUlice(teryt);

            if (useNaturalDistricts)
            {
                var cracow = loc.Miejscowosci.Single(q => q.Nazwa == "Kraków" && q.RodzajId == "96");
                cracow.NazwaRejonow = "część dzielnicy";
                var cracowDistricts = loc.Miejscowosci.Where(q => q.Gmina.Powiat.Wojewodztwo.Symbol == cracow.Gmina.Powiat.Wojewodztwo.Symbol && q.Gmina.Powiat.Symbol == cracow.Gmina.Powiat.Symbol && q.Symbol != cracow.Symbol).ToList();
                PrepareNonStandardDistrict(cracowDistricts, loc, cracow);
                AddCracowDistricts(loc, cracow);

                var poznan = loc.Miejscowosci.Single(q => q.Nazwa == "Poznań" && q.RodzajId == "96");
                poznan.NazwaDzielnic = "osiedle";
                var poznanDistricts = loc.Miejscowosci.Where(q => q.Gmina.Powiat.Wojewodztwo.Symbol == poznan.Gmina.Powiat.Wojewodztwo.Symbol && q.Gmina.Powiat.Symbol == poznan.Gmina.Powiat.Symbol && q.Symbol != poznan.Symbol).ToList();
                PrepareNonStandardDistrict(poznanDistricts, loc, poznan);
                AddPoznanDistricts(loc, poznan);

                var lodz = loc.Miejscowosci.Single(q => q.Nazwa == "Łódź" && q.RodzajId == "96");
                lodz.NazwaRejonow = "osiedle";
                var lodzDistricts = loc.Miejscowosci.Where(q => q.Gmina.Powiat.Wojewodztwo.Symbol == lodz.Gmina.Powiat.Wojewodztwo.Symbol && q.Gmina.Powiat.Symbol == lodz.Gmina.Powiat.Symbol && q.Symbol != lodz.Symbol).ToList();
                SetStandardDistricts(lodzDistricts, loc, lodz);

                var wroclaw = loc.Miejscowosci.Single(q => q.Nazwa == "Wrocław" && q.RodzajId == "96");
                wroclaw.NazwaRejonow = "osiedle";
                var wroclawDistricts = loc.Miejscowosci.Where(q => q.Gmina.Powiat.Wojewodztwo.Symbol == wroclaw.Gmina.Powiat.Wojewodztwo.Symbol && q.Gmina.Powiat.Symbol == wroclaw.Gmina.Powiat.Symbol && q.Symbol != wroclaw.Symbol).ToList();
                SetStandardDistricts(wroclawDistricts, loc, wroclaw);

                var warsaw = loc.Miejscowosci.Single(q => q.Nazwa == "Warszawa" && q.RodzajId == "96");
                warsaw.NazwaRejonow = "rejon";
                var warsawDistricts = loc.Miejscowosci.Where(q => q.Gmina.Powiat.Wojewodztwo.Symbol == warsaw.Gmina.Powiat.Wojewodztwo.Symbol && q.Gmina.Powiat.Symbol == warsaw.Gmina.Powiat.Symbol && q.Symbol != warsaw.Symbol).ToList();
                SetStandardDistricts(warsawDistricts, loc, warsaw);
            }

            // adjust ids
            var i = 1;
            loc.Wojewodztwa.ForEach(q => q.Lp = i++);
            i = 1;
            loc.Powiaty.ForEach(q => q.Lp = i++);
            i = 1;
            loc.Gminy.ForEach(q => q.Lp = i++);
            i = 1;
            loc.Miejscowosci.ForEach(q => q.Lp = i++);
            i = 1;
            loc.Dzielnice.ForEach(q => q.Lp = i++);
            i = 1;
            loc.Rejony.ForEach(q => q.Lp = i++);
            i = 1;
            loc.Ulice.ForEach(q => q.Lp = i++);

            return loc;
        }

        private List<Wojewodztwo> GetWojewodztwa(TerytModel.Teryt teryt)
        {
            var list = new List<Wojewodztwo>();

            list.AddRange(teryt.Terc.Rows.Where(q => string.IsNullOrWhiteSpace(q.Pow)).Select(q => new Wojewodztwo
            {
                Nazwa = q.Nazwa.ToLower(),
                StanNa = DateTime.Parse(q.StanNa),
                Symbol = q.Woj
            }));

            return list;
        }

        private List<Powiat> GetPowiaty(TerytModel.Teryt teryt, List<Wojewodztwo> wojewodztwa)
        {
            var list = new List<Powiat>();

            list.AddRange(teryt.Terc.Rows.Where(q => !string.IsNullOrWhiteSpace(q.Pow) && string.IsNullOrWhiteSpace(q.Gmi)).Select(q => new Powiat
            {
                Nazwa = q.Nazwa,
                Rodzaj = q.NazDod,
                StanNa = DateTime.Parse(q.StanNa),
                Symbol = q.Pow,
                Wojewodztwo = wojewodztwa.Single(r => r.Symbol == q.Woj)
            }));

            return list;
        }

        private List<Gmina> GetGminy(TerytModel.Teryt teryt, List<Powiat> powiaty)
        {
            var list = new List<Gmina>();

            list.AddRange(teryt.Terc.Rows.Where(q => !string.IsNullOrWhiteSpace(q.Gmi)).Select(q => new Gmina
            {
                Nazwa = q.Nazwa,
                Rodzaj = q.NazDod,
                RodzajId = q.Rodz,
                StanNa = DateTime.Parse(q.StanNa),
                Symbol = q.Gmi,
                Powiat = powiaty.Single(r => r.Symbol == q.Pow && r.Wojewodztwo.Symbol == q.Woj)
            }));

            // clean up
            var cityCommunes = list.Where(q => q.RodzajId == "3").ToList();
            foreach (var gm in cityCommunes)
            {
                list.RemoveAll(q => q.Symbol == gm.Symbol && q.Powiat.Symbol == gm.Powiat.Symbol && q.Powiat.Wojewodztwo.Symbol == gm.Powiat.Wojewodztwo.Symbol && q.RodzajId != "3");
            }

            return list;
        }

        private List<Miejscowosc> GetMiejscowosci(TerytModel.Teryt teryt, List<Gmina> gminy)
        {
            _miejscowosciDictionary = new Dictionary<string, Miejscowosc>();
            var list = new List<Miejscowosc>();

            list.AddRange(teryt.Simc.Rows.Where(q => q.Sym == q.SymPod).Select(q =>
                                                                               {
                                                                                   var m = new Miejscowosc
                                                                                   {
                                                                                       Nazwa = q.Nazwa,
                                                                                       RodzajId = q.Rm,
                                                                                       Rodzaj = teryt.WmRodz.Rows.Single(r => r.Rm == q.Rm).NazwaRm,
                                                                                       StanNa = DateTime.Parse(q.StanNa),
                                                                                       Symbol = q.Sym,
                                                                                       Gmina = gminy.Single(r => r.Symbol == q.Gmi && r.Powiat.Symbol == q.Pow && r.Powiat.Wojewodztwo.Symbol == q.Woj),
                                                                                       NazwaDzielnic = "dzielnica"
                                                                                   };
                                                                                   _miejscowosciDictionary.Add(q.Sym, m);
                                                                                   return m;
                                                                               }));

            return list;
        }

        private List<Dzielnica> GetDzielnice(TerytModel.Teryt teryt)
        {
            var list = new List<Dzielnica>();

            list.AddRange(teryt.Simc.Rows.Where(q => q.Rm == "99").Select(q => new Dzielnica
            {
                Nazwa = q.Nazwa,
                StanNa = DateTime.Parse(q.StanNa),
                Symbol = q.Sym,
                Miejscowosc = _miejscowosciDictionary[q.SymPod]
            }));

            return list;
        }

        private List<Ulica> GetUlice(TerytModel.Teryt teryt)
        {
            var list = new List<Ulica>();

            foreach (var row in teryt.Ulic.Rows)
            {
                if (!_miejscowosciDictionary.ContainsKey(row.Sym))
                {
                    continue;
                }

                var miejscowosc = _miejscowosciDictionary[row.Sym];
                list.Add(new Ulica
                {
                    Cecha = row.Cecha,
                    Nazwa1 = row.Nazwa1,
                    Nazwa2 = row.Nazwa2,
                    StanNa = DateTime.Parse(row.StanNa),
                    Symbol = row.SymUl,
                    Miejscowosc = miejscowosc
                });
            }

            return list;
        }

        private void AddCracowDistricts(Lokalizacje lokalizacje, Miejscowosc cracow)
        {
            #region Names

            var names = new Dictionary<string, List<string>>
                        {
                            {
                                "Stare Miasto (I)", new List<string>
                                                    {
                                                        "Kazimierz",
                                                        "Kleparz",
                                                        "Nowe Miasto",
                                                        "Nowy Świat",
                                                        "Piasek",
                                                        "Stare Miasto",
                                                        "Stradom",
                                                        "Warszawskie",
                                                        "Wawel"
                                                    }
                            },
                            {
                                "Grzegórzki (II)", new List<string>
                                                   {
                                                       "Dąbie",
                                                       "Grzegórzki",
                                                       "Kazimierz",
                                                       "Olsza",
                                                       "Osiedle Oficerskie",
                                                       "Wesoła"
                                                   }
                            },
                            {
                                "Prądnik Czerwony (III)", new List<string>
                                                          {
                                                              "Olsza",
                                                              "Olsza II",
                                                              "Prądnik Czerwony",
                                                              "Rakowice",
                                                              "Śliczna",
                                                              "Ugorek",
                                                              "Warszawskie",
                                                              "Wieczysta",
                                                              "Osiedle Gotyk"
                                                          }
                            },
                            {
                                "Prądnik Biały (IV)", new List<string>
                                                      {
                                                          "Azory",
                                                          "Bronowice Wielkie",
                                                          "Górka Narodowa",
                                                          "Górka Narodowa Wschód",
                                                          "Górka Narodowa Zachód",
                                                          "Osiedle Krowodrza Górka",
                                                          "Osiedle Witkowice Nowe",
                                                          "Prądnik Biały",
                                                          "Tonie",
                                                          "Witkowice",
                                                          "Żabiniec"
                                                      }
                            },
                            {
                                "Krowodrza (V)", new List<string>
                                                 {
                                                     "Cichy Kącik",
                                                     "Czarna Wieś",
                                                     "Krowodrza",
                                                     "Łobzów",
                                                     "Miasteczko Studenckie AGH",
                                                     "Nowa Wieś"
                                                 }
                            },
                            {
                                "Bronowice (VI)", new List<string>
                                                  {
                                                      "Bronowice",
                                                      "Bronowice Małe",
                                                      "Mydlniki",
                                                      "Osiedle Bronowice Nowe",
                                                      "Osiedle Widok Zarzecze"
                                                  }
                            },
                            {
                                "Zwierzyniec (VII)", new List<string>
                                                     {
                                                         "Bielany",
                                                         "Chełm",
                                                         "Olszanica",
                                                         "Półwsie Zwierzynieckie",
                                                         "Przegorzały",
                                                         "Wola Justowska",
                                                         "Zwierzyniec",
                                                         "Salwator",
                                                         "Zakamycze"
                                                     }
                            },
                            {
                                "Dębniki (VIII)", new List<string>
                                                  {
                                                      "Bodzów",
                                                      "Dębniki",
                                                      "Kobierzyn",
                                                      "Koło Tynieckie",
                                                      "Kostrze",
                                                      "Ludwinów",
                                                      "Podgórki Tynieckie",
                                                      "Pychowice",
                                                      "Sidzina",
                                                      "Skotniki",
                                                      "Tyniec",
                                                      "Zakrzówek",
                                                      "Kapelanka",
                                                      "Kliny Zacisze",
                                                      "Mochnaniec",
                                                      "Osiedle Europejskie",
                                                      "Osiedle Interbud",
                                                      "Osiedle Kolejowe",
                                                      "Osiedle Panorama",
                                                      "Osiedle Podwawelskie",
                                                      "Osiedle Proins",
                                                      "Osiedle Ruczaj",
                                                      "Osiedle Ruczaj-Zaborze"
                                                  }
                            },
                            {
                                "Łagiewniki-Borek Fałęcki (IX)", new List<string>
                                                                 {
                                                                     "Borek Fałęcki",
                                                                     "Łagiewniki",
                                                                     "Osiedle Cegielniana",
                                                                     "Osiedle Zaułek Jugowicki"
                                                                 }
                            },
                            {
                                "Swoszowice (X)", new List<string>
                                                  {
                                                      "Bania",
                                                      "Barycz",
                                                      "Jugowice",
                                                      "Kliny Borkowskie",
                                                      "Kosocice",
                                                      "Lusina",
                                                      "Łysa Góra",
                                                      "Opatkowice",
                                                      "Rajsko",
                                                      "Siarczana Góra",
                                                      "Soboniowice",
                                                      "Wróblowice",
                                                      "Zbydniowice"
                                                  }
                            },
                            {
                                "Podgórze Duchackie (XI)", new List<string>
                                                           {
                                                               "Kurdwanów",
                                                               "Kurdwanów Nowy",
                                                               "Osiedle Piaski Nowe",
                                                               "Osiedle Podlesie",
                                                               "Piaski Wielkie",
                                                               "Wola Duchacka",
                                                               "Wola Duchacka Wschód",
                                                               "Wola Duchacka Zachód"
                                                           }
                            },
                            {
                                "Bieżanów-Prokocim (XII)", new List<string>
                                                           {
                                                               "Bieżanów",
                                                               "Bieżanów Kolonia",
                                                               "Kaim",
                                                               "Łazy",
                                                               "Osiedle Bieżanów Nowy",
                                                               "Osiedle Kolejowe",
                                                               "Osiedle Medyków",
                                                               "Osiedle Na Kozłówce",
                                                               "Osiedle Nad Potokiem",
                                                               "Osiedle Parkowe",
                                                               "Osiedle Nowy Prokocim",
                                                               "Osiedle Złocień",
                                                               "Prokocim",
                                                               "Rżąka"
                                                           }
                            },
                            {
                                "Podgórze (XIII)", new List<string>
                                                   {
                                                       "Łutnia",
                                                       "Mateczny",
                                                       "Płaszów",
                                                       "Podgórze",
                                                       "Przewóz",
                                                       "Rybitwy",
                                                       "Zabłocie"
                                                   }
                            },
                            {
                                "Czyżyny (XIV)", new List<string>
                                                 {
                                                     "Czyżyny",
                                                     "Łęg",
                                                     "Osiedle 2 Pułku Lotniczego",
                                                     "Osiedle Akademickie",
                                                     "Osiedle Dywizjonu 303"
                                                 }
                            },
                            {
                                "Mistrzejowice (XV)", new List<string>
                                                      {
                                                          "Batowice",
                                                          "Dziekanowice",
                                                          "Mistrzejowice",
                                                          "Osiedle Bohaterów Września",
                                                          "Osiedle Kombatantów",
                                                          "Osiedle Mistrzejowice Nowe",
                                                          "Osiedle Oświecenia",
                                                          "Osiedle Piastów",
                                                          "Osiedle Srebrnych Orłów",
                                                          "Osiedle Tysiąclecia",
                                                          "Osiedle Złotego Wieku"
                                                      }
                            },
                            {
                                "Bieńczyce (XVI)", new List<string>
                                                   {
                                                       "Bieńczyce",
                                                       "Osiedle Albertyńskie",
                                                       "Osiedle Jagiellońskie",
                                                       "Osiedle Kalinowe",
                                                       "Osiedle Kazimierzowskie",
                                                       "Osiedle Kościuszkowskie",
                                                       "Osiedle Na Lotnisku",
                                                       "Osiedle Niepodległości",
                                                       "Osiedle Przy Arce",
                                                       "Osiedle Strusia",
                                                       "Osiedle Wysokie",
                                                       "Osiedle Złotej Jesieni"
                                                   }
                            },
                            {
                                "Wzgórza Krzesławickie (XVII)", new List<string>
                                                                {
                                                                    "Grębałów",
                                                                    "Kantorowice",
                                                                    "Krzesławice",
                                                                    "Lubocza",
                                                                    "Łuczanowice",
                                                                    "Dłubnia",
                                                                    "Osiedle Na Stoku",
                                                                    "Osiedle Na Wzgórzach",
                                                                    "Wadów",
                                                                    "Węgrzynowice",
                                                                    "Zesławice"
                                                                }
                            },
                            {
                                "Nowa Huta (XVIII)", new List<string>
                                                     {
                                                         "Błonie",
                                                         "Branice",
                                                         "Osiedle Centrum A",
                                                         "Osiedle Centrum B",
                                                         "Osiedle Centrum C",
                                                         "Osiedle Centrum D",
                                                         "Osiedle Centrum E",
                                                         "Chałupki",
                                                         "Chałupki Górne",
                                                         "Cło",
                                                         "Górka Kościelnicka",
                                                         "Holendry",
                                                         "Kopaniny",
                                                         "Kościelniki",
                                                         "Kujawy",
                                                         "Mogiła",
                                                         "Nowa Huta",
                                                         "Nowa Wieś",
                                                         "Osiedle Górali",
                                                         "Osiedle Handlowe",
                                                         "Osiedle Hutnicze",
                                                         "Osiedle Kolorowe",
                                                         "Osiedle Krakowiaków",
                                                         "Osiedle Lesisko",
                                                         "Osiedle Młodości",
                                                         "Osiedle Na Skarpie",
                                                         "Osiedle Ogrodowe",
                                                         "Osiedle Słoneczne",
                                                         "Osiedle Sportowe",
                                                         "Osiedle Spółdzielcze",
                                                         "Osiedle Stalowe",
                                                         "Osiedle Szklane Domy",
                                                         "Osiedle Szkolne",
                                                         "Osiedle Teatralne",
                                                         "Osiedle Urocze",
                                                         "Osiedle Wandy",
                                                         "Osiedle Willowe",
                                                         "Osiedle Zgody",
                                                         "Osiedle Zielone",
                                                         "Piekiełko",
                                                         "Pleszów",
                                                         "Przylasek Rusiecki",
                                                         "Przylasek Wyciąski",
                                                         "Ruszcza",
                                                         "Stryjów",
                                                         "Wola Rusiecka",
                                                         "Wolica",
                                                         "Wróżenice",
                                                         "Wyciąże"
                                                     }
                            }
                        };

            #endregion Names

            var i = 1;
            var j = 1;
            var date = DateTime.Now.Date;
            foreach (var name in names)
            {
                var district = new Dzielnica
                {
                    Miejscowosc = cracow,
                    Symbol = "K" + i.ToString().PadLeft(3, '0'),
                    StanNa = date,
                    Nazwa = name.Key
                };
                foreach (var regionName in name.Value)
                {
                    var region = new Rejon
                    {
                        Nazwa = regionName,
                        Dzielnica = district,
                        Symbol = district.Symbol + "R" + j.ToString().PadLeft(3, '0'),
                        StanNa = date
                    };

                    lokalizacje.Rejony.Add(region);
                    j++;
                }

                lokalizacje.Dzielnice.Add(district);
                i++;
            }
        }

        private void AddPoznanDistricts(Lokalizacje lokalizacje, Miejscowosc poznan)
        {
            var names = new List<string>
                        {
                            "Antoninek-Zieliniec-Kobylepole",
                            "Chartowo",
                            "Fabianowo-Kotowo",
                            "Główna",
                            "Głuszyna",
                            "Górczyn",
                            "Grunwald Południe",
                            "Grunwald Północ",
                            "Jana III Sobieskiego i Marysieńki",
                            "Jeżyce",
                            "Junikowo",
                            "Kiekrz",
                            "Krzesiny-Pokrzywno-Garaszewo",
                            "Krzyżowniki-Smochowice",
                            "Kwiatowe",
                            "Ławica",
                            "Morasko-Radojewo",
                            "Naramowice",
                            "Nowe Winogrady Południe",
                            "Nowe Winogrady Północ",
                            "Nowe Winogrady Wschód",
                            "Ogrody",
                            "Ostrów Tumski-Śródka-Zawady-Komandoria",
                            "Piątkowo",
                            "Podolany",
                            "Rataje",
                            "Sołacz",
                            "Stare Miasto",
                            "Stare Winogrady",
                            "Starołęka-Minikowo-Marlewo",
                            "Stary Grunwald",
                            "Strzeszyn",
                            "Szczepankowo-Spławie-Krzesinki",
                            "Świerczewo",
                            "Św. Łazarz",
                            "Umultowo",
                            "Warszawskie-Pomet-Maltańskie",
                            "Wilda",
                            "Winiary",
                            "Wola",
                            "Zielony Dębiec",
                            "Żegrze"
                        };

            var i = 1;
            var date = DateTime.Now.Date;
            foreach (var name in names)
            {
                var district = new Dzielnica
                {
                    Miejscowosc = poznan,
                    Symbol = "P" + i.ToString().PadLeft(3, '0'),
                    StanNa = date,
                    Nazwa = name
                };

                lokalizacje.Dzielnice.Add(district);
                i++;
            }
        }

        private void PrepareNonStandardDistrict(List<Miejscowosc> gusDistricts, Lokalizacje loc, Miejscowosc city)
        {
            foreach (var district in gusDistricts)
            {
                loc.Ulice.Where(q => q.Miejscowosc.Symbol == district.Symbol && q.Miejscowosc.Gmina.Symbol == district.Gmina.Symbol && q.Miejscowosc.Gmina.Powiat.Symbol == district.Gmina.Powiat.Symbol && q.Miejscowosc.Gmina.Powiat.Wojewodztwo.Symbol == district.Gmina.Powiat.Wojewodztwo.Symbol).ToList().ForEach(q => q.Miejscowosc = city);
            }
            loc.Miejscowosci.RemoveAll(gusDistricts.Contains);
            loc.Dzielnice.RemoveAll(q => q.Miejscowosc.Gmina.Powiat.Nazwa == city.Nazwa);
        }

        private void SetStandardDistricts(List<Miejscowosc> gusDistricts, Lokalizacje loc, Miejscowosc city)
        {
            foreach (var district in gusDistricts)
            {
                loc.Ulice.Where(q => q.Miejscowosc.Symbol == district.Symbol && q.Miejscowosc.Gmina.Symbol == district.Gmina.Symbol && q.Miejscowosc.Gmina.Powiat.Symbol == district.Gmina.Powiat.Symbol && q.Miejscowosc.Gmina.Powiat.Wojewodztwo.Symbol == district.Gmina.Powiat.Wojewodztwo.Symbol).ToList().ForEach(q => q.Miejscowosc = city);

                var cityName = city.Nazwa + "-";
                var name = district.Nazwa.StartsWith(cityName) ? district.Nazwa.Substring(cityName.Length) : district.Nazwa;
                var d = new Dzielnica
                {
                    Miejscowosc = city,
                    Symbol = district.Symbol,
                    StanNa = district.StanNa,
                    Nazwa = name
                };
                loc.Dzielnice.Add(d);

                var regions = loc.Dzielnice.Where(q => q.Miejscowosc.Symbol == district.Symbol).ToList();
                loc.Rejony.AddRange(regions.Select(q => new Rejon
                {
                    Dzielnica = d,
                    Nazwa = q.Nazwa,
                    Symbol = q.Symbol,
                    StanNa = q.StanNa
                }));
                loc.Dzielnice.RemoveAll(q => regions.Contains(q));
            }
            loc.Miejscowosci.RemoveAll(gusDistricts.Contains);
        }
    }
}