using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchoolLunchGameManager : MonoBehaviour {

    GameObject canvas;
	// Use this for initialization
	void Start () {
        Screen.orientation = ScreenOrientation.Portrait;
        Screen.orientation = ScreenOrientation.AutoRotation;
        Screen.autorotateToPortrait = true;
        Screen.autorotateToPortraitUpsideDown = true;
        Screen.autorotateToLandscapeLeft = false;
        Screen.autorotateToLandscapeRight = false;
        if (GlobalVariables.lastScreenOrientation != (ScreenOrientation)1 &&GlobalVariables.lastScreenOrientation!= (ScreenOrientation)2)
        {
            Debug.Log("Druga orijentacija");
            GlobalVariables.lastScreenOrientation = Screen.orientation;
        }

        canvas = GameObject.Find("Canvas");

        Timer.Schedule(this, 3f, delegate
            {
                Debug.Log("AAA");
                StartCoroutine(ShowFoodMakerScene());
            });
	}

    IEnumerator ShowFoodMakerScene()
    {
        Debug.Log("CORO SATARTA");
        float a = 0f;
        while(a<1f)
        {
            a += 0.01f;
            canvas.transform.Find("FoodShop").GetComponent<CanvasGroup>().alpha = a;
            yield return new WaitForSeconds(0.01f);
        }
        canvas.transform.Find("FoodShop").GetComponent<CanvasGroup>().alpha = 1;

    }
	
	
}
