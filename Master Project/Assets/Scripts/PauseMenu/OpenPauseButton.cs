using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PauseMenu
{
	public class OpenPauseButton : MonoBehaviour
	{
		public GameObject pauseMenuPrefab;

		void Start ()
		{
			gameObject.GetComponent<Button>().onClick.AddListener(OnClick);
		}

		private void OnClick()
		{
			if (pauseMenuPrefab == null)
			{
				Debug.Log("No prefab for pause menu");
			}

			Instantiate(pauseMenuPrefab);
		}
	}
}