using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtSchoolGameManager : MonoBehaviour {

    public static ArtSchoolGameManager Instance;
    GameObject paintMenu;
    public GameObject finishButton;
    public GameObject teacher;
    public GameObject block;


    IEnumerator Start () 
    {
        Screen.orientation = ScreenOrientation.Portrait;
        Screen.orientation = ScreenOrientation.AutoRotation;
        Screen.autorotateToPortrait = true;
        Screen.autorotateToPortraitUpsideDown = true;
        Screen.autorotateToLandscapeLeft = false;
        Screen.autorotateToLandscapeRight = false;
        if (GlobalVariables.lastScreenOrientation != (ScreenOrientation)1 && GlobalVariables.lastScreenOrientation != (ScreenOrientation)2)
        {
            Debug.Log("Druga orijentacija");
            GlobalVariables.lastScreenOrientation = Screen.orientation;
        }

        paintMenu = GameObject.Find("Canvas/PaintMenu");

        yield return new WaitForSeconds(.5f);
        teacher.GetComponent<Animator>().Play("ShowStewardess");
        teacher.transform.Find("AnimationHolder").GetComponent<Animator>().Play("CharacterIdle_Pointing");
        yield return new WaitForSeconds(3f);
        teacher.GetComponent<Animator>().Play("HideStewardess");
        yield return new WaitForSeconds(1f);
        float a = 0f;
        while(a<1f)
        {
            a += 0.01f;
            paintMenu.GetComponent<CanvasGroup>().alpha = a;
            yield return new WaitForSeconds(0.01f);
        }
        paintMenu.GetComponent<CanvasGroup>().alpha = 1f;
        block.SetActive(false);
    }

    public void Painted()
    {
        finishButton.SetActive(true);
    }

}
