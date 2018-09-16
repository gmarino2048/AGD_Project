using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ChopManager : MonoBehaviour {

    [Header("Chop configurations")]
    public float ChopWidth = 0.25f;

    [SerializeField]
    public KnifeBehavior Reference;

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
		
	}

    void InsertChop (float position)
    {

    }

     
    HitOrMiss ValidPosition(float position)
    {
        List<Chop> cutWithin = AlreadyChopped.Where(chopValue =>
                                                        position >= chopValue.LowerBound &&
                                                        position <= chopValue.UpperBound)
                                             .ToList();
        if (cutWithin.Any())
        {
            return HitOrMiss.Collision;
        }

        float lowerBound = Reference.ItemToChop.LeftBound;
        float upperBound = Reference.ItemToChop.RightBound;

        return position < lowerBound || position > upperBound ? HitOrMiss.Miss : HitOrMiss.Hit;
    }

    void DrawChop (float position) 
    {

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
