using System;
using System.Collections.Generic;
using System.Text;

namespace XMLLoad
{
   public interface IXmlService
    {
        K ParseXml<K>(string path) where K : IXmlInput;
    }
}
