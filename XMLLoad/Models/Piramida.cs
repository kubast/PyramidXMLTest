using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using XMLLoad.Files;

namespace XMLLoad
{
   [Serializable,XmlRoot("piramida")]
   public class Piramida : IXmlInput
    {
        [XmlElement("uczestnik")]
        public Member Member { get; set; }
    }
}
