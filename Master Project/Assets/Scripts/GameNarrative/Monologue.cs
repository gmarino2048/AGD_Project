using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace GameNarrative
{
    [XmlType("monologue")]
    public class Monologue
    {
        [XmlAttribute("end")]
        public bool IsForEnd;

        [XmlAttribute("win")]
        public bool IsForWin;

        [XmlElement("entry")]
        public List<string> Entries;
    }
}