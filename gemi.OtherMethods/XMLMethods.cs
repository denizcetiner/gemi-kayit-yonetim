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
    }
}
