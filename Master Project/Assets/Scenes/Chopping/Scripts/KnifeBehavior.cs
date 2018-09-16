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

    Direction CurrentDirection;

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
        float currentY = transform.position.y;
        float currentZ = transform.position.z;

        float newX = GetNextPosition(currentX, Time.deltaTime);

        Vector3 newPosition = new Vector3(newX, currentY, currentZ);
        transform.position = newPosition;
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
        // Check if we should switch direction
        if (currentPosition <= LeftBound) {
            CurrentDirection = Direction.Right;
        }
        else if (currentPosition >= RightBound){
            CurrentDirection = Direction.Left;
        }
    }

    float expLerp (float start, float stop, float current, float t){

    }
}
