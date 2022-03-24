using System;
using System.Globalization;
using System.Xml;

namespace CurrencyConverter
{
    internal class CurrencyConverter
    {
        public static XmlDocument LoadXmlFile(string url)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(url);
            return xmlDoc;
        }

        public static string[] GetCurrencyTagArray() //rework so it takes the currency codes from file etc.
        {
            return new string[] { "EUR", "USD", "JPY", "RUB", "DKK", "GBP" };
        }

        public static float GetExchangeRate(string from, string to, float amount = 1)
        {
            if (to == null || from == null)
                return 0;

            if (to.ToLower() == "eur" && from.ToLower() == "eur")
                return amount;

            try
            {
                float toRate = GetCurrencyRateInEuro(to);
                float fromRate = GetCurrencyRateInEuro(from);

                if (from.ToLower() == "eur")
                {
                    return (amount * toRate);
                }
                else if (from.ToLower() == "eur")
                {
                    return (amount / fromRate);
                }
                else
                {
                    return (amount * toRate) / fromRate;
                }
            }
            catch
            {
                return 0;
            }
        }

        public static float GetCurrencyRateInEuro(string currency)
        {
            if (currency.ToLower() == "")
                throw new ArgumentException("Invalid Argument! Currency code can't be empty!");
            if (currency.ToLower() == "eur")
                return 1;

            try
            {
                string concatUrl = string.Concat("https://www.ecb.europa.eu/rss/fxref-" + currency.ToLower() + ".html");

                var xmlDoc = LoadXmlFile(concatUrl);
                var nsmgr = CreateNameSpaceManager(xmlDoc);

                XmlNodeList nodeList = xmlDoc.SelectNodes("//rdf:item",  nsmgr);

                foreach (XmlNode node in nodeList)
                {
                    CultureInfo ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();               
                    ci.NumberFormat.CurrencyDecimalSeparator = ".";

                    try
                    {
                        float exchangeRate = float.Parse(
                            node.SelectSingleNode("//cb:statistics//cb:exchangeRate//cb:value", nsmgr).InnerText,
                            NumberStyles.Any, ci);

                        return exchangeRate;
                    }
                    catch { }
                }
                return 0;
            }
            catch
            {
                return 0;
            }
        }

        public static XmlNamespaceManager CreateNameSpaceManager(XmlDocument doc)
        {
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("rdf", "http://purl.org/rss/1.0/");
            nsmgr.AddNamespace("cb", "http://www.cbwiki.net/wiki/index.php/Specification_1.1");
            return nsmgr;
        }
    }
}