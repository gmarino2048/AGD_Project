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
        public string monsterDataFilePath;

        private void Awake()
        {
            monsterDataFilePath = Application.dataPath + "/StreamingAssets/Data/Monsters.xml";
        }

        /// <summary>
        /// Retrieves existing cached data about a monster or loads a monster from xml
        /// </summary>
        /// <param name="monsterId">The ID of the monster to load information on</param>
        /// <returns>Data on the given monster</returns>
        public MonsterData LoadMonster(Guid monsterId)
        {
            if (!_Monsters.ContainsKey(monsterId))
            {
                var xmlFileContents = File.ReadAllText(monsterDataFilePath);
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xmlFileContents);

                var monsterNode = xmlDoc.GetElementById(monsterId.ToString("B").ToUpper());

                var name = monsterNode.Attributes["name"].Value;
                var fightThreshold = float.Parse(monsterNode.Attributes["fightThreshold"].Value);

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

                _Monsters.Add(monsterId, new MonsterData(name, fightThreshold, desiredIngredients));
            }

            return _Monsters[monsterId];
        }
    }
}