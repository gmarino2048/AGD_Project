using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spoonScript : MonoBehaviour {

    //distance of the mouse from the center of the spoon
    private Vector3 offset;

    //where mouse is
    private Vector3 mousePosition;

    //where mouse was last frame
    private Vector3 prevMousePosition;

    //how far the spoon traveled
    public float travelDistance = 0;

    //angle of mouse relative to center
    public float angle;

    //how far the mouse is from the center
    public float mouseRadius;

    //where the center is
    private Vector3 center;

    //x location of spoon
    public float spoonX;

    //y location of spoon
    public float spoonY;

    //if time is up
    public bool timerDone = false;


    // Use this for initialization
    void Start () {
        center = new Vector3(0, 0, 0);
	}
	
	
    /// <summary>
    /// keeps track of distance traveled
    /// </summary>
	void Update () {

        travelDistance += Vector3.Distance(transform.position, prevMousePosition);


        prevMousePosition = transform.position;
    }

    /// <summary>
    /// keeps the spoon for auto-centering on the mouse making it look disconnected from the previous frame
    /// </summary>
    void OnMousePressed()
    {
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
    }

    /// <summary>
    /// moves the spoon
    /// </summary>
    void OnMouseDrag()
    {
        if (!timerDone)
        {
            Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
            mousePosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
            //transform.position = mousePosition;

            mouseRadius = Vector3.Distance(mousePosition, center);

            angle = Mathf.Atan2(mousePosition.y, mousePosition.x);

            if (mouseRadius < 2)
            {
                spoonX = Mathf.Cos(angle) * mouseRadius;
                spoonY = Mathf.Sin(angle) * mouseRadius;
            }
            else
            {
                spoonX = Mathf.Cos(angle) * 2;
                spoonY = Mathf.Sin(angle) * 2;
            }

            transform.position = new Vector3(spoonX, spoonY, 0);

        }

    }
}
