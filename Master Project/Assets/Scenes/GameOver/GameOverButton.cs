using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverButton : MonoBehaviour {

    /// <summary>
    /// The name of the next scene.
    /// </summary>
    public string nextSceneName;

    public void Awake()
    {
        var button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(OnButtonClicked);
    }

    public void OnButtonClicked()
    {
        Debug.Log("grrrr");
        SceneManager.LoadScene(nextSceneName, LoadSceneMode.Single);
    }
}
