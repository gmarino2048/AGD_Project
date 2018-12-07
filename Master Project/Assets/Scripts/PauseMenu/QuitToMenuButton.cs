using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PauseMenu
{
	public class QuitToMenuButton : MonoBehaviour
	{
		private string _MAIN_MENU_SCENE_NAME = "Main Menu";
		
		void Start ()
		{
			gameObject.GetComponent<Button>().onClick.AddListener(OnClick);
		}

		private void OnClick()
		{
            var gameData = FindObjectOfType<GameNarrativeManager>().gameObject;
            try
            {
                DestroyImmediate(gameData);
            }
            catch (Exception)
            {
                Debug.LogWarning("Pause not running in game");
            }

            
            for (int i = 0; i < SceneManager.sceneCount; i++) {
                Scene scene = SceneManager.GetSceneAt (i);
                SceneManager.UnloadSceneAsync(scene);
            }

            SceneManager.LoadScene(_MAIN_MENU_SCENE_NAME);
		}
	}
}