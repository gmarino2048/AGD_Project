using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientDirectiveScript : MonoBehaviour {

    BoxCollider2D boxCollider;


	// Use this for initialization
	void Start () {
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
    }
	
	// Update is called once per frame
	void Update () {

        Collider2D[] overlap = Physics2D.OverlapAreaAll(boxCollider.bounds.min, boxCollider.bounds.max);
        if (overlap.Length > 1){
            overlap[0].transform.position = this.transform.position;
           // Debug.Log(string.Format("Found {0} overlapping object(s)", overlap.Length - 1));
        }
            
    }

	public void OnCollisionEnter2D(Collision2D col) {
		//Debug.Log ("Collision");
		if (col.gameObject.tag == "ItemOne") {
			Debug.Log ("first");
		}
		else if (col.gameObject.tag == "ItemTwo") {
			Debug.Log ("second");
		}

		else if (col.gameObject.tag == "ItemThree") {
			Debug.Log ("third");
		}

		else if (col.gameObject.tag == "ItemFour") {
			Debug.Log ("fourth");
		}
	}
}
