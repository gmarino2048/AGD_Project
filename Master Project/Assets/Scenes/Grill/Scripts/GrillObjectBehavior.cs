using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Controls the actions of a grill object, and makes sure that its animations are
/// handled correctly.
/// </summary>
public class GrillObjectBehavior : MonoBehaviour {

    [Header("Animations")]
    [SerializeField]
    Animator Master; // The animator used to animate the grill object.

    [Header("Main Animation")]
    AnimationClip Main; // The main animation for the cooking sequence.
    public string MainName; // The name of the main animation.
    public float MainTime; // The duration of the main animation.
    public float Target; // The desired amount of cooking time.

    [Header("Loop Animation")]
    AnimationClip Loop; // The loop for the burnt grill object.
    public string LoopName; // The name of the loop animation clip.
    public float LoopTime; // The total time that the loop animation takes.

    bool Switch; // Tells the main function when to switch the animator speed.
    float TimeRemaining; // The time remaining to cook the object for until it is burnt.
    float SecondsOver; // The time used to calculate the loop position of the final animation.

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
        Master = GetComponent<Animator>();

        Main = Master.runtimeAnimatorController.animationClips
                     .Where(clip => clip.name == MainName).FirstOrDefault();

        Loop = Master.runtimeAnimatorController.animationClips
                     .Where(clip => clip.name == LoopName).FirstOrDefault();

        // Set the animator speed to take the correct amount of time
        float multiplier = Main.length / MainTime;
        Master.speed = multiplier;

        // Set the time remaining
        TimeRemaining = MainTime;
        SecondsOver = 0;
        Switch = false;

        // Get the scene scorekeeper
        Scorekeeper = GetComponentInParent<GrillScorekeeper>();
	}


    /// <summary>
    /// Runs once every frame.
    /// </summary>
	void Update()
    {
        if (TimeRemaining > 0) {
            TimeRemaining -= Time.deltaTime;
        }
        else {
            if (Switch)
            {
                float multiplier = LoopTime / Loop.length;
                Master.speed = multiplier;
                Switch = false;
            }


            SecondsOver += Time.deltaTime;
        }
    }

    /// <summary>
    /// Calculates the score of this grillable object as a float between 0 and
    /// 1, inclusive.
    /// </summary>
    /// <returns>The score for this object.</returns>
    float GetScore () {
        float cookTime = MainTime - TimeRemaining;

        // Severely overcooked
        if (cookTime >= MainTime - 0.01) {
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
