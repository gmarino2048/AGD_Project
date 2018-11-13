using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementHelper : MonoBehaviour {

    [Header("Food Object Sprite")]
    public Sprite FoodObject;
    public SpriteRenderer Renderer;

    [Header("Z-Placement Index")]
    public float ZIndex = -2;

	// Use this for initialization
	void Start () {
        Renderer.sprite = FoodObject;
	}
	
    
	// Update is called once per frame
	void Update () {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector3 spritePosition = new Vector3(worldPosition.x, worldPosition.y, ZIndex);
        Renderer.transform.position = spritePosition;
	}
}
