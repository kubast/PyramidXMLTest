using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace XMLLoad.Files
{
    public class Member
    {
        [XmlElement("uczestnik")]
        public List<Member> Members { get; set; }

        [XmlAttribute("id")]
        public int Id { get; set; }

        [XmlIgnore]
        public decimal Cash { get; set; }

        [XmlIgnore]
        public int Level { get; set; }

        [XmlIgnore]
        public int ChildlessCnt { get; set; }
    }
}
