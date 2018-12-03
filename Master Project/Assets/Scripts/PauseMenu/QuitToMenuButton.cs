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
            PersistentObject persistent = FindObjectOfType<PersistentObject>();
            try
            {
                DestroyImmediate(persistent.gameObject);
            }
            catch (Exception)
            {
                Debug.LogWarning("Pause not running in game");
            }

            SceneManager.LoadScene(_MAIN_MENU_SCENE_NAME);
		}
	}
}