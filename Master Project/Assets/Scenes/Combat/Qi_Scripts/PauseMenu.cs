using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	// Use this for initialization
	public static bool paused = false;

	public GameObject pauseMenu;
    public Button pause;
    public Button resume;

    private void Awake()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
	}

	public void Resume () {
		pauseMenu.SetActive(false);
		Time.timeScale = 1;
		paused = false;
	}


	public void Pause() {
		pauseMenu.SetActive (true);
		Time.timeScale = 0;
		paused = true;
	}
}
