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
    public float LoopFPS; // The Frames per second value that the end loop animation runs at.
    [SerializeField]
    public Vector2 SpriteSize; // Used to get the width and height of the sprite to render.

    SpriteRenderer Animator; // Used to render the different sprites for animations.
    float TimeRemaining; // The time remaining to cook the object for until it is burnt.
    float SecondsOver; // The time used to calculate the loop position of the final animation.
    int FramePosition; // The last index of the animation shown.

    [Header("Scorekeeping")]
    [SerializeField]
    public GrillScorekeeper Scorekeeper; // The object responsible for scoring all the grilled items.
    public float Score { get; private set; } // The Score of the object
	

    /// <summary>
    /// Called on instantiation. Sets up the object to run the proper animation
    /// sequence to start when the object is instantiated.
    /// </summary>
	void Start () {
        // Get the sprite Renderer for this object
        Animator = GetComponent<SpriteRenderer>();
        Animator.drawMode = SpriteDrawMode.Simple;
        Animator.size = SpriteSize;
        Animator.sprite = Animation[0];

        // Set the time remaining
        TimeRemaining = LifeCycle;
        SecondsOver = 0;

        // Initialize first frame's position
        FramePosition = 0;

        // Get the scene scorekeeper
        Scorekeeper = GetComponentInParent<GrillScorekeeper>();
	}

    // Update is called once per frame

	void Update () {
        if (TimeRemaining > 0){
            int temp = AnimationScaler();
            if (temp != FramePosition) {
                SwapSprite(Animation, temp);
                FramePosition = temp;
            }
            // Otherwise do nothing

            TimeRemaining -= Time.deltaTime;
        }
        else {
            int temp = LoopScaler(LoopFPS);
            if (temp != FramePosition) {
                SwapSprite(Loop, temp);
                FramePosition = temp;
            }
            // Otherwise do nothing

            SecondsOver += Time.deltaTime;
        }
    }


    /// <summary>
    /// Scales the animation to take as long as the sausage cooks. This function
    /// automatically scales the time between frames so that the animation
    /// takes as long to run as the object does to cook.
    /// </summary>
    /// <returns>The index of the sprite which needs to be displayed.</returns>
    int AnimationScaler () {
        float progress = TimeRemaining / LifeCycle;
        int currentSprite = Mathf.FloorToInt(progress * Animation.Count);
        return currentSprite;
    }


    /// <summary>
    /// Scales the looped portion to run at a given fps set by the developer.
    /// The specified animation under loop will loop forever after the grill 
    /// object is completely overcooked.
    /// </summary>
    /// <returns>The current index of the sprite within the loop
    /// animation.</returns>
    /// <param name="fps">The frames per second of the animation.</param>
    int LoopScaler (float fps) {
        float timeInSeconds = Loop.Count / fps;
        float progress = SecondsOver % timeInSeconds;

        float currentSprite = Loop.Count * (progress / timeInSeconds);
        int index = Mathf.FloorToInt(currentSprite);
        return index;
    }


    /// <summary>
    /// Changes the sprite value of the SpriteRenderer from one object to 
    /// another in order to simulate an animation of the grilled object.
    /// In theory the objects should be all the same size, so that is not a
    /// concern.
    /// </summary>
    /// <param name="sprites">The animation list of sprites to choose
    /// from.</param>
    /// <param name="spriteNumber">The index of the sprite to be 
    /// displayed.</param>
    void SwapSprite (List<Sprite> sprites, int spriteNumber) {
        if (spriteNumber < sprites.Count)
        {
            Animator.sprite = sprites[spriteNumber];
        }
        else Debug.Log("Error: Requested Sprite is out of bounds");
    }

    /// <summary>
    /// Calculates the score of this grillable object as a float between 0 and
    /// 1, inclusive.
    /// </summary>
    /// <returns>The score for this object.</returns>
    float GetScore () {
        float cookTime = LifeCycle - TimeRemaining;

        // Severely overcooked
        if (cookTime >= LifeCycle - 0.01) {
            return 0f;
        }

        // Overcooked
        if (cookTime >= Target) {
            float timeRemoved = cookTime - Target;
            return 1 / (1 + timeRemoved);
        }

        // Undercooked
        float distance = Target - cookTime;
        return 1 / (1 + distance);
    }

    /// <summary>
    /// Registers this object to add itself to the scorekeeping list and
    /// remove itself from the scene.
    /// </summary>
    void OnMouseDown()
    {
        Score = GetScore();
        Debug.Log(Score);
        Scorekeeper.AddScore(this);
        Destroy(this);
    }
}
