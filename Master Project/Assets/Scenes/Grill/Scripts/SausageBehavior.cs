using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SausageBehavior : MonoBehaviour {

    public float LifeCycle;

    [SerializeField]
    public SpriteRenderer Renderer;
    [SerializeField]
    public List<Sprite> Sprites;

    float TimeRemaining;

	/// <summary>
    /// Called once at start of scene
    /// </summary>
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (TimeRemaining > 0)
        {

            TimeRemaining -= Time.deltaTime;
        }
    }

    void Evaluate () {
        int length = Sprites.Count;
    }

    void SwapSprite () {

    }
}
