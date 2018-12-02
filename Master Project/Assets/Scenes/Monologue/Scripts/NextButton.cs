using UnityEngine;
using UnityEngine.UI;

namespace Monologue
{
    public class NextButton : MonoBehaviour
    {
        public bool shouldStopTyping = false;

        private MonologueSceneManager _SceneManager;

        void Awake()
        {
            _SceneManager = GameObject.FindObjectOfType<MonologueSceneManager>();

            gameObject.GetComponent<Button>().onClick.AddListener(OnClick);
        }

        public void OnClick()
        {
            if (!_SceneManager.ShowNextEntry(shouldStopTyping))
            {
                _SceneManager.EndScene();
            }
        }
    }
}
