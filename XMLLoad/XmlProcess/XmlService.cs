using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;
using XMLLoad;
using System.Linq;
using XMLLoad.Files;

namespace XMLLoad
{
    public class XmlService: IXmlService
    {
        public T ParseXml<T>(string path) where T: IXmlInput
        {
            T result = default(T);
            if (!string.IsNullOrWhiteSpace(path))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                using (FileStream reader = new FileStream(path, FileMode.Open))
                {
                    result = (T)serializer.Deserialize(reader);
                }
            }
            return (result);
        }
    }
}
