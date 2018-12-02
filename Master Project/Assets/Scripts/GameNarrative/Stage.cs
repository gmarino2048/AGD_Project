using System;
using System.Xml;
using System.Xml.Serialization;

namespace GameNarrative
{
    [XmlType("stage")]
    public class Stage
    {
        [XmlAttribute("monster")]
        public Guid MonsterID;
    }
}