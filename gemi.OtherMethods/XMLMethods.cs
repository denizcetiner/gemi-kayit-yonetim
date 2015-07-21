using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;


namespace gemi.OtherMethods
{
    public class XMLMethods
    {
        /// <summary>
        /// Her gün güncellenen bir xml dosyasından para değerlerini çeker.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetCurrencies()
        {
            Dictionary<string, string> currencies = new Dictionary<string, string>();
            
            string url = "http://www.tcmb.gov.tr/kurlar/today.xml";
            XDocument xdoc = XDocument.Load(url);
            var elements = from nm in xdoc.Descendants("Currency").Where(a => (string)a.Attribute("CurrencyCode") == "USD") select nm.Element("ForexBuying").Value;
            currencies.Add("USD $:", elements.ElementAt(0));
            elements = from nm in xdoc.Descendants("Currency").Where(a => (string)a.Attribute("CurrencyCode") == "EUR") select nm.Element("ForexBuying").Value;
            currencies.Add("EUR €:", elements.ElementAt(0));
            elements = from nm in xdoc.Descendants("Currency").Where(a => (string)a.Attribute("CurrencyCode") == "GBP") select nm.Element("ForexBuying").Value;
            currencies.Add("GBP £:", elements.ElementAt(0));

            return currencies;
        }

        /// <summary>
        /// Her gün güncellenen bir xml dosyasından Tekirdağ'ın hava durumunu çeker.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetForecast()
        {
            Dictionary<string, string> forecast = new Dictionary<string, string>();

            string url = "http://www.owebtools.com/xmlhavadurumu.php?sehir=TEKIRDAG";
            XDocument xdoc = XDocument.Load(url);

            forecast.Add("Sehir", xdoc.Descendants("Sehir").ElementAt(0).Value);
            forecast.Add("Hava", xdoc.Descendants("Hava").ElementAt(0).Value);
            forecast.Add("Sıcaklık", xdoc.Descendants("Sicaklik").ElementAt(0).Value);
            forecast.Add("Zaman", xdoc.Descendants("Zaman").ElementAt(0).Value);

            return forecast;
        }

        public Dictionary<string, string> GetForecastYahoo()
        {
            Dictionary<string, string> forecast = new Dictionary<string, string>();

            string url = "https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20weather.forecast%20where%20woeid%20in%20(select%20woeid%20from%20geo.places(1)%20where%20text%3D%22tekirdag%2C%20tr%22)&format=xml&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys";

            XNamespace xns = "http://xml.weather.yahoo.com/ns/rss/1.0";

            XDocument xdoc = XDocument.Load(url);
            
            string sehir = (from feed in xdoc.Descendants("channel") select feed.Element(xns+"location").Attribute("city").Value).ElementAt(0);
            string hava = (from feed in xdoc.Descendants("item") select feed.Element(xns + "condition").Attribute("code").Value).ElementAt(0);
            string temp = (from feed in xdoc.Descendants("item") select feed.Element(xns + "condition").Attribute("temp").Value).ElementAt(0);
            string time = xdoc.Descendants("lastBuildDate").ElementAt(0).Value;

            forecast.Add("Sehir",sehir);
            forecast.Add("Hava", TranslateCondition(Convert.ToInt32(hava)));
            forecast.Add("Sıcaklık", FahrenheitToCelsius(temp));
            forecast.Add("Zaman", time);

            return forecast;
        }

        public string FahrenheitToCelsius(string temp)
        {
            return Math.Round(((Convert.ToInt32(temp)-32)/1.8),1).ToString();
        }

        public string TranslateCondition(int code)
        {
            switch (code)
            {
                case 0:
                    return "Hortum";
                case 1:
                    return "Tropik fırtına";
                case 2:
                    return "Kasırga";
                case 3:
                    return "Şiddetli sağanak";
                case 4:
                    return "Sağanak yağışlı";
                case 5:
                    return "Karla karışık yağmur";
                case 6:
                    return "Karla karışık yağmur";
                case 7:
                    return "Karla karışık yağmur";
                case 8:
                    return "Donan çisenti";
                case 9:
                    return "Çiseleyen yağmur";
                case 10:
                    return "Dolu-yağmur";
                case 11:
                    return "Yağmur";
                case 12:
                    return "Yağmur";
                case 13:
                    return "Kar";
                case 14:
                    return "Kar";
                case 15:
                    return "Esen kar";
                case 16:
                    return "Kar";
                case 17:
                    return "Dolu";
                case 18:
                    return "Islak kar";
                case 19:
                    return "Toz";
                case 20:
                    return "Sisli";
                case 21:
                    return "Sisli";
                case 22:
                    return "Dumanlı";
                case 23:
                    return "Şiddetli rüzgarlı";
                case 24:
                    return "Rüzgarlı";
                case 25:
                    return "Soğuk";
                case 26:
                    return "Bulutlu";
                case 27:
                    return "Çoğunlukla bulutlu";
                case 28:
                    return "Çoğunlukla bulutlu";
                case 29:
                    return "Parçalı bulutlu";
                case 30:
                    return "Parçalı bulutlu";
                case 31:
                    return "Açık";
                case 32:
                    return "Güneşli";
                case 33:
                    return "Açık";
                case 34:
                    return "Açık";
                case 35:
                    return "Doluyla karışık yağmur";
                case 36:
                    return "Sıcak";
                case 37:
                    return "İzole sağanak";
                case 38:
                    return "Aralıklı sağanak";
                case 39:
                    return "Aralıklı sağanak";
                case 40:
                    return "Aralıklı sağanak";
                case 41:
                    return "Şiddetli kar";
                case 42:
                    return "Aralıklı şiddetli kar";
                case 43:
                    return "Şiddetli kar";
                case 44:
                    return "Parçalı bulutlu";
                case 45:
                    return "Sağanak yağmur";
                case 46:
                    return "Sağanak kar";
                case 47:
                    return "İzole sağanak";
                default:
                    return "Bulunamadı";
            }
        }
    }
}
