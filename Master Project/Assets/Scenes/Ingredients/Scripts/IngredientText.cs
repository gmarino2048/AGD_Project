using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientText : MonoBehaviour {

	public GameObject textForIngredient;

	// Use this for initialization
	void Start () {
		textForIngredient.SetActive (false);
	}
	
	// Update is called once per frame
	void OnMouseEnter() {
		textForIngredient.SetActive (true);
	}
	void OnMouseExit() {
		textForIngredient.SetActive (false);
	}
}
