using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseStatus : MonoBehaviour {

	// Use this for initialization
	public static bool paused = false;

	public GameObject pauseMenu;

	public GameObject pauseButton;

	void Start() {
		pauseMenu.SetActive (false);
	}
		
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)) {
			if (paused) {
				Resume ();
			} 
			else {
				Pause ();
			}
		}
	}

	public void Resume () {
		pauseMenu.SetActive(false);
		Time.timeScale = 1;
		paused = false;
	}


	void Pause() {
		pauseMenu.SetActive (true);
		Time.timeScale = 0;
		paused = true;
	}

	public void QuitGame() {
		Application.Quit ();
	}
}
