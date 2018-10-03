using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {

    public static bool Gamepaused = false;
    public GameObject pausemenu;

    private void Awake()
    {
        pausemenu.SetActive(false);
    }
    private void Update()
    {
        if (Gamepaused)
        {
            pause();
        }
        else
        {
            resume();
        }
    }
    public void PauseHandler()
    {
        //Output this to console when the Button is clicked
        Debug.Log("Pause");
        Gamepaused = true;
    }

    public void pause()
    {
        pausemenu.SetActive(true);
        Time.timeScale = 0f;
        Gamepaused = true;
    }
    public void resume()
    {
        pausemenu.SetActive(false);
        Time.timeScale = 1f;
        Gamepaused = false;
    }
}
