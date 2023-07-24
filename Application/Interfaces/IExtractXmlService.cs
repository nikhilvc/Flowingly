using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Flowingly.Application.Interfaces
{
    public interface IExtractXmlService
    {
        public XmlDocument? GetXmlFromText(string text);
    }
}
