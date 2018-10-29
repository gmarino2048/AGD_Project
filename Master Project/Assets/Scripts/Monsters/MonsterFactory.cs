using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

namespace Monsters
{
    public class MonsterFactory : MonoBehaviour
    {
        private Dictionary<Guid, MonsterData> _Monsters = new Dictionary<Guid, MonsterData>();

        /// <summary>
        /// The path to the data file for the monsters
        /// </summary>
        private readonly string _MONSTER_STREAMING_ASSETS_FILE_PATH = "Data/Monsters.xml";

        /// <summary>
        /// Retrieves existing cached data about a monster or loads a monster from xml
        /// </summary>
        /// <param name="monsterId">The ID of the monster to load information on</param>
        /// <returns>Data on the given monster</returns>
        public MonsterData LoadMonster(Guid monsterId)
        {
            if (!_Monsters.ContainsKey(monsterId))
            {
                try
                {
                    var xmlFilePath = Path.Combine(Application.streamingAssetsPath, _MONSTER_STREAMING_ASSETS_FILE_PATH);
                    var xmlFileContents = File.ReadAllText(xmlFilePath);
                    var xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(xmlFileContents);

                    var monsterNode = xmlDoc.GetElementById(monsterId.ToString("B").ToUpper());

                    var name = monsterNode.Attributes["name"].Value;
                    var fightThreshold = float.Parse(monsterNode.Attributes["fightThreshold"].Value);

                    // Get desired ingredients
                    var desiredIngredients = new List<IngredientType>();
                    foreach (XmlNode xmlNode in monsterNode.GetElementsByTagName("ingredient"))
                    {
                        desiredIngredients.Add((IngredientType)Enum.Parse(typeof(IngredientType), xmlNode.Attributes["type"].Value));
                    }

                    // Get the combat choice stats
                    var combatChoices = new Dictionary<CombatChoice, CombatChoiceStatus>();
                    foreach (XmlNode xmlNode in monsterNode.GetElementsByTagName("combatchoice"))
                    {
                        var combatChoice = (CombatChoice)Enum.Parse(typeof(CombatChoice), xmlNode.Attributes["name"].Value);
                        var combatChoiceStatus = new CombatChoiceStatus(
                            int.Parse(xmlNode.Attributes["start"].Value),
                            float.Parse(xmlNode.Attributes["decayrate"].Value),
                            int.Parse(xmlNode.Attributes["min"].Value)
                        );
                        combatChoices.Add(combatChoice, combatChoiceStatus);
                    }

                    _Monsters.Add(monsterId, new MonsterData(name, fightThreshold, desiredIngredients, combatChoices));
                }
                catch (Exception e) {
                    Debug.LogError(e);
                }
            }

            return _Monsters[monsterId];
        }
    }
}