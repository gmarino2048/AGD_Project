using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Combat
{
    public class PlayByPlayController : MonoBehaviour
    {
        [Header("Play By Play Text")]
        public Text PlayByPlayText;

        [Header("Text Scroll Display")]
        public int DelayFrames = 5;

        public void Clear ()
        {
            PlayByPlayText.text = string.Empty;
        }

        public IEnumerator Display (string text)
        {
            int i = 1;
            while (i < text.Length)
            {
                string temp = text.Substring(0, i);
                PlayByPlayText.text = temp;

                
                for (int j = 0; j < DelayFrames; j++) yield return new WaitForEndOfFrame();

                i++;
            }

            PlayByPlayText.text = text;
        }
    }
}