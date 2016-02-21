# teryt-parser
This is a C# parser for [TERYT](http://www.stat.gov.pl/broker/access/index.jspa) files ‒ used by [Central Statistical Office](http://stat.gov.pl/) in Poland to describe locations: provinces, communes, counties, cities, districts and streets.

It also cleans the data a bit and adds districts for additional cities not covered by TERYT: Cracow and Poznań.

## Usage
Clone or download and build in Visual Studio. Current version of code uses .NET Framework 4.6.1.

It can also can export data in two different ways: to CSV file or generate SQL INSERT queries. Please modify to suit your needs.

Sample console application is also included.

## License
[The MIT License (MIT)](LICENSE)
