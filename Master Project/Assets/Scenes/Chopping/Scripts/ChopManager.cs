using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ChopManager : MonoBehaviour {

    [Header("Chop configurations")]
    public float ChopWidth = 0.25f;

    [SerializeField]
    public KnifeBehavior Reference;
    [SerializeField]
    public KeyCode InputKey = KeyCode.Space;

    public struct Chop 
    {
        public float LowerBound;
        public float ActualPosition;
        public float UpperBound;
    }

    public List<Chop> AlreadyChopped { get; private set; }

    public enum HitOrMiss
    {
        Hit,
        Miss,
        Collision
    }

	void Start () 
    {
        AlreadyChopped = new List<Chop>();
	}

	void Update () 
    {
        // Listen for space bar input
        if (Input.GetKeyDown(InputKey))
        {
            Debug.Log(Reference.transform.position.x);
            InsertChop(Reference.transform.position.x);
        }
	}

    void InsertChop (float position)
    {
        Chop currentChop = new Chop
        {
            LowerBound = position - ChopWidth,
            ActualPosition = position,
            UpperBound = position + ChopWidth
        };


        // Draw the chop in the debug frame
        // TODO: REMOVE LATER
        DrawChop(currentChop);

        PrintHitOrMiss(ValidPosition(currentChop));

        AlreadyChopped.Add(currentChop);
    }

     
    HitOrMiss ValidPosition(Chop current)
    {
        List<Chop> cutWithin = AlreadyChopped.Where(chopValue =>
                                                    current.ActualPosition >= chopValue.LowerBound &&
                                                    current.ActualPosition <= chopValue.UpperBound)
                                             .ToList();

        if (cutWithin.Any())
        {
            return HitOrMiss.Collision;
        }

        float lowerBound = Reference.ItemToChop.LeftBound;
        float upperBound = Reference.ItemToChop.RightBound;

        return current.ActualPosition < lowerBound || current.ActualPosition > upperBound 
                      ? HitOrMiss.Miss : HitOrMiss.Hit;
    }

    void DrawChop (Chop current) 
    {
        float upperY = 10;
        float lowerY = -10;

        // Draw Center Line
        Vector3 centerStart = new Vector3(current.ActualPosition, upperY, 0);
        Vector3 centerEnd = new Vector3(current.ActualPosition, lowerY, 0);

        Color centerColor = Color.red;

        Debug.DrawLine(centerStart, centerEnd, centerColor, Mathf.Infinity, false);

        // Draw Left line
        Vector3 leftStart = new Vector3(current.ActualPosition - ChopWidth, upperY, 0);
        Vector3 leftEnd = new Vector3(current.ActualPosition - ChopWidth, lowerY, 0);

        Color leftColor = Color.green;

        Debug.DrawLine(leftStart, leftEnd, leftColor, Mathf.Infinity, false);

        // Draw Right Line
        Vector3 rightStart = new Vector3(current.ActualPosition + ChopWidth, upperY, 0);
        Vector3 rightEnd = new Vector3(current.ActualPosition + ChopWidth, lowerY, 0);

        Color rightColor = Color.green;

        Debug.DrawLine(rightStart, rightEnd, rightColor, Mathf.Infinity, false);
    }

    void PrintHitOrMiss (HitOrMiss status) {
        string message;

        switch (status){
            case HitOrMiss.Hit:
                message = "Valid Position";
                break;

            case HitOrMiss.Collision:
                message = "Knife Cut Collision";
                break;

            default:
                message = "Target Missed";
                break;
        }

        Debug.Log(message);
    }
}
