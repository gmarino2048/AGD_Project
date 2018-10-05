using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace Monsters
{
    public class MonsterData
    {
        private const string _MONSTER_DATA_FILE_PATH = "Data/Monsters.xml";

        /// <summary>
        /// The name of the monster
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The level to which a monster's {what_should_we_call_it} drops before a fight starts
        /// </summary>
        public float FightThreshold { get; private set; }

        /// <summary>
        /// The ingredients this monster desires
        /// </summary>
        public IEnumerable<IngredientType> DesiredIngredients { get; set; }

        private MonsterData()
        {
        }

        /// <summary>
        /// Loads a monster from xml
        /// </summary>
        /// <param name="monsterId">The ID of the monster to load information on</param>
        /// <returns>Data on the given monster</returns>
        public static MonsterData LoadFromXML(Guid monsterId)
        {
            var xmlAsset = new TextAsset();
            xmlAsset = (TextAsset)Resources.Load(_MONSTER_DATA_FILE_PATH, typeof(TextAsset));

            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlAsset.text);

            var monsterNode = xmlDoc.GetElementById(monsterId.ToString());

            var monsterData = new MonsterData
            {
                Name = monsterNode.Attributes["name"].Value,
                FightThreshold = float.Parse(monsterNode.Attributes["fightThreshold"].Value)
            };

            // Get desired ingredients
            var desiredIngredients = new List<IngredientType>();
            foreach (XmlNode xmlNode in monsterNode.GetElementsByTagName("ingredient"))
            {
                try
                {
                    desiredIngredients.Add((IngredientType)Enum.Parse(typeof(IngredientType), xmlNode.Attributes["type"].Value));
                }
                catch (Exception e) {
                    Debug.LogError(e);
                }
            }

            monsterData.DesiredIngredients = desiredIngredients;

            return monsterData;
        }
    }
}
