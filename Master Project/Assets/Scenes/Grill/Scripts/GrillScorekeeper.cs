using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrillScorekeeper : MonoBehaviour {

    [Header("Target Number of Grilled Items")]
    public int Target; // The number of grill items that the player should grill.

    List<float> Scores; // The scores from each grilled object

    /// <summary>
    /// Runs at the start of the scene.
    /// </summary>
    void Awake()
    {
        Scores = new List<float>();
    }


    /// <summary>
    /// Finds the score of the players run through the minigame. This scorekeeper
    /// has a required number of items to grill without inflicting a penalty. If
    /// the player fails to meet this requirement, their score is reduced 
    /// proportionately to how many objects they failed to cook.
    /// </summary>
    /// <returns>The score from this minigame.</returns>
    public float Score () {

        float sum = 0;
        foreach (float score in Scores)
        {
            sum += score;
        }
        float average = sum / Scores.Count;

        // If enough objects grilled, take score as average
        if (Scores.Count >= Target) return average;

        // Otherwise inflict penalty linearly scaled to the score
        float multiplier = Scores.Count / Target;
        return average * multiplier;
    }

    /// <summary>
    /// Adds the score of a grilled object to this object's existing list of
    /// scores. These scores will be used to calculate the player's final score
    /// at the end of the scene.
    /// </summary>
    /// <param name="grilled">The object to add the score from.</param>
    public void AddScore (GrillObjectBehavior grilled) {
        Scores.Add(grilled.Score);
    }
}
