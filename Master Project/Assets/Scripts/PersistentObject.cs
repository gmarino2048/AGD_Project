using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentObject : MonoBehaviour
{
    private readonly string _MAIN_MENU_SCENE_NAME = "Main Menu";

    private bool isPersistent = false;

    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

	private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
	{
        if (scene.name == _MAIN_MENU_SCENE_NAME)
        {
            if (isPersistent)
            {
                Debug.Log("Destroying formerly persistent object");
                GameObject.DestroyImmediate(gameObject);
            }
            else
            {
                Debug.Log("Persisting object");
                DontDestroyOnLoad(gameObject);
                isPersistent = true;
            }
        }
	}
}