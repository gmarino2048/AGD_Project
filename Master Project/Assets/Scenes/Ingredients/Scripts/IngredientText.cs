using UnityEngine;
using UnityEngine.UI;

namespace Ingredients
{
	public class IngredientText : MonoBehaviour
	{
		private DishPreparationManager _DishPreparationManager;
		private DragIngredient _IngredientInfo;
		private Text _ToolTipText;

		// Use this for initialization
		void Awake()
		{
			_DishPreparationManager = GameObject.FindObjectOfType<DishPreparationManager>();
			_IngredientInfo = this.gameObject.GetComponent<DragIngredient>();
			_ToolTipText = GameObject.FindGameObjectWithTag("ToolTip").GetComponent<Text>();
		}
		
		// Update is called once per frame
		void OnMouseEnter()
		{
			_ToolTipText.text = _DishPreparationManager.GetIngredientDisplayName(_IngredientInfo.ingredientType);
			_ToolTipText.transform.position = Input.mousePosition;
			_ToolTipText.enabled = true;
		}

		void OnMouseExit()
		{
			_ToolTipText.enabled = false;
		}
	}
}