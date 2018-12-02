using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace GameNarrative
{
    [XmlType("stage")]
    public class Stage
    {
        [XmlAttribute("monster")]
        public Guid MonsterID;

        [XmlIgnore]
        public List<string> DialogueHistory = new List<string>();
    }
}