using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragIngredient : MonoBehaviour
{

    //distance of the mouse from the center of the spoon
    private Vector3 offset;

    //where mouse is
    private Vector3 mousePosition;

    //where mouse was last frame
    private Vector3 prevMousePosition;

    //how far the spoon traveled
    public float travelDistance = 0;


    // Use this for initialization
    void Start()
    {

    }


    /// <summary>
    /// keeps track of distance traveled
    /// </summary>
    void Update()
    {

        travelDistance += Vector3.Distance(mousePosition, prevMousePosition);


        prevMousePosition = mousePosition;
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
        Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        mousePosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
        transform.position = mousePosition;
    }
		
}