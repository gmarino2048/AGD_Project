using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnifeBehavior : MonoBehaviour {

    [Header("Screen Information")]
    [SerializeField]
    public Camera Camera;
    public float XOffset;
    public float YOffset;
    public float ZIndex;

    [Header("Scene References")]
    [SerializeField]
    public ChopObjectBehavior ItemToChop;
    public float BufferWidth;

    private float LeftBound;
    private float RightBound;

    enum Direction 
    {
        Left,
        Right
    }

	// Use this for initialization
	void Start () 
    {
        // Set the inital position of the knife
        float initialX = Camera.transform.position.x + XOffset;
        float initialY = Camera.transform.position.y + YOffset;

        Vector3 knifePosition = new Vector3(initialX, initialY, ZIndex);
        gameObject.transform.position = knifePosition;

        // Set the bounds for motion

	}
	
	// Update is called once per frame
	void Update () 
    {
        float currentX = transform.position.x;
        float newX = GetNextPosition(currentX, Time.deltaTime);
	}

    void SetBounds()
    {
        if (ItemToChop != null)
        {
            LeftBound = ItemToChop.LeftBound - BufferWidth;
            RightBound = ItemToChop.RightBound + BufferWidth;
        }
        else
        {
            throw new MissingComponentException("Could not find Choppable Item");
        }
    }

    float GetNextPosition (float currentPosition, float elapsedTime) 
    {

    }
}
