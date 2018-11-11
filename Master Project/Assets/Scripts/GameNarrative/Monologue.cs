using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace GameNarrative
{
    [XmlType("monologue")]
    public class Monologue
    {
        [XmlElement("entry")]
        public List<string> Entries;
    }
}