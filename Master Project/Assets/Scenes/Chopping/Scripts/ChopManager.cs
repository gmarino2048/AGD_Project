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
            InsertChop(Reference.transform.position.x);
	}

    void InsertChop (float position)
    {
        Chop currentChop = new Chop
        {
            LowerBound = position - ChopWidth,
            ActualPosition = position,
            UpperBound = position + ChopWidth
        };

        AlreadyChopped.Add(currentChop);

        DrawChop(currentChop);

        PrintHitOrMiss(ValidPosition(currentChop));
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

        Vector3[] centerPositions = {new Vector3(current.ActualPosition, upperY, 0),
            new Vector3(current.ActualPosition, lowerY, 0)};

        LineRenderer centerLine = GetComponent<LineRenderer>();
        centerLine.positionCount = 2;
        centerLine.SetPositions(centerPositions);
        centerLine.startColor = Color.red;
        centerLine.endColor = Color.red;
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
