using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PauseMenu
{
	public class ClosePauseMenuButton : MonoBehaviour
	{
		public GameObject pauseMenu;
		
		void Start ()
		{
			Time.timeScale = 0;
			gameObject.GetComponent<Button>().onClick.AddListener(OnClick);
		}

		private void OnClick()
		{
			Time.timeScale = 1;
		}
	}
}