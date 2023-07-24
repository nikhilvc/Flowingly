using Flowingly.Application.Interfaces;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace Flowingly.Application.Services
{
    public class ExtractXmlService : IExtractXmlService
    {
        public XmlDocument? GetXmlFromText(string text)
        {
            string pattern = @"<(?!\/)(?<tag>[\w]+)[^<>]*>(?<content>[\s\S]*?)<\/\k<tag>>";
            MatchCollection matches = Regex.Matches(text, pattern, RegexOptions.Singleline);
            if (matches == null || !matches.Any()) { return null; }
            XmlDocument xmlDoc = new();
            StringBuilder xmlBuilder = new("<Reservation>");

            xmlBuilder.Append(string.Join("", matches.Select(match => match.Value)));

            xmlBuilder.Append("</Reservation>");
            xmlDoc.LoadXml(xmlBuilder.ToString());   
            return xmlDoc;

        }
   
    }
}
