using UnityEngine;
using System.Collections;

public class WashWater : MonoBehaviour {
	//public RectTransform Water;
	 
	public static void WashWithWater (  float washedPercent ) 
	{
		RectTransform Water = (RectTransform) GameObject.Find("Car/MaskWater/Water").GetComponent<RectTransform>();
		 
		if(washedPercent < 1 && washedPercent >0)
		{
			Water.anchoredPosition =  new Vector2(0,380 - .8f*washedPercent);
			Water.sizeDelta = new Vector2(600,180+washedPercent*3.2f);

		}

	}
}
