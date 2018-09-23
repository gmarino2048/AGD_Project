using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spoon : MonoBehaviour {

    private Vector3 offset;

    private Vector3 mousePosition;
    private Vector3 prevMousePosition;

    public float travelDistance = 0;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        travelDistance += Vector3.Distance(mousePosition, prevMousePosition);


        prevMousePosition = mousePosition;
    }

    void OnMousePressed()
    {
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
    }

    void OnMouseDrag()
    {
        Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        mousePosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
        transform.position = mousePosition;
    }
}
