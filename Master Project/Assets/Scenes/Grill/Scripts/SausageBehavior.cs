using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SausageBehavior : MonoBehaviour {

    [Header("Cook time options")]
    public float LifeCycle; // The number of seconds this object takes to cook.
    public float Target; // The target number of seconds to cook for.

    [Header("Animations")]
    [SerializeField]
    public List<Sprite> Animation; // Sequence of Sprites to be rendered as animation.
    [SerializeField]
    public List<Sprite> Loop; // Plays over and over when the object has finished its main animation.

    SpriteRenderer Animator; // Used to render the different sprites for animations.
    float TimeRemaining; // The time remaining to cook the object for until it is burnt.
    float SecondsOver; // The time used to calculate 

    public float Score { get; private set; } // The Score of the object
	
	void Start () {
        // Get the sprite Renderer for this object
        Animator = GetComponent<SpriteRenderer>();

        // Set the time remaining
        TimeRemaining = LifeCycle;
        SecondsOver = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (TimeRemaining > 0){

        }
    }


    /// <summary>
    /// Scales the animation to take as long as the sausage cooks
    /// </summary>
    /// <returns>The index of the sprite which needs to be displayed.</returns>
    int AnimationScaler () {
        float progress = TimeRemaining / LifeCycle;
        int currentSprite = Mathf.FloorToInt(progress * Animation.Count);
        return currentSprite;
    }

    float LoopScaler (float fps) {

    }

    void SwapSprite (List<Sprite> sprites, int spriteNumber) {
        if (spriteNumber < sprites.Count){
            Animator.sprite = sprites[spriteNumber];
        }
    }

    float GetScore () {
        float cookTime = LifeCycle - TimeRemaining;

        // Severely overcooked
        if (cookTime >= LifeCycle - 0.01) {
            return 0f;
        }

        // Overcooked
        if (cookTime >= Target) {
            float distance = cookTime - Target;
            return 1 / (1 + distance);
        }

        // Undercooked
        float distance = Target - cookTime;
        return 1 / (1 + distance);
    }

    void OnMouseDown()
    {
        Score = GetScore();
        Debug.Log(Score);
    }
}
