using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace XMLLoad
{
    [Serializable, XmlRoot("przelewy")]
    public class Payments : IXmlInput
    {
        [XmlElement("przelew")]
        public List<Payment> Payment { get; set; }
    }

   
    public class Payment
    {
        [XmlAttribute("od")]
        public int From { get; set; }
        [XmlAttribute("kwota")]
        public decimal Amount { get; set; }
    }
}