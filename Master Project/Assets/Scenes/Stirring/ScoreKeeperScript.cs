using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Stirring
{
    public class ScoreKeeperScript : MonoBehaviour
    {


        public float score { get; private set; }

        private readonly Guid _NESSIE_GUID = new Guid("{060F70EA-8A92-4117-AB65-75DE3458E407}");
        
        /// <summary>
        /// takes distance and converts to 1-0 scale with 0 being the best
        /// </summary>
        void convertScore()
        {
            float distance = GameObject.Find("spoon").GetComponent<spoonScript>().travelDistance;
            score = 1 / (distance + 1);
        }

        /// <summary>
        /// Sends the score to dish score manager
        /// </summary>
        public void sendScore()
        {
            convertScore();

            var scoreManager = GameObject.FindObjectOfType<DishScoreManager>();
            scoreManager.AddIngredientToDish(_NESSIE_GUID, IngredientType.IceCream, score);
        }
    }
}

