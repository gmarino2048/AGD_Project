using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace GameNarrative
{
    [XmlRoot("narrative")]
    public class Narrative
    {
        [XmlElement("stage")]
        public List<Stage> Stages;

        [XmlElement("monologue")]
        public List<Monologue> Monologues;
    }
}