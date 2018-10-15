using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Stirring
{
    public class ScoreKeeperScript : MonoBehaviour, IDishScoreKeeper
    {
        private readonly Guid _NESSIE_GUID = new Guid("{060F70EA-8A92-4117-AB65-75DE3458E407}");

        /// <summary>
        /// The dish preparation manager
        /// </summary>
        private DishPreparationManager _DishPreparationManager;

        /// <summary>
        /// The dish score manager
        /// </summary>
        private DishScoreManager _DishScoreManager;

        // Used for initialization
        void Start()
        {
            _DishPreparationManager = GameObject.FindObjectOfType<DishPreparationManager>();
            _DishScoreManager = GameObject.FindObjectOfType<DishScoreManager>();
        }

        /// <summary>
        /// Sends the score to dish score manager
        /// </summary>
        public void SendScore()
        {
            StartCoroutine(EndMiniGame());
        }
        
        /// <summary>
        /// takes distance and converts to 1-0 scale with 0 being the best
        /// </summary>
        public float GetScore()
        {
            float distance = GameObject.Find("spoon").GetComponent<SpoonScript>().travelDistance;
            return 1 / (distance + 1);
        }

        /// <summary>
        /// Sends the score and goes to the next scene
        /// </summary>
        private IEnumerator EndMiniGame()
        {
            yield return new WaitForSeconds(10);

            if (_DishPreparationManager != null)
            {
                if (_DishScoreManager != null)
                {
                    _DishScoreManager.AddIngredientToDish(_NESSIE_GUID, _DishPreparationManager.currentIngredient, GetScore());
                }

                _DishPreparationManager.GoToNextScene();
            }
        }
    }
}

