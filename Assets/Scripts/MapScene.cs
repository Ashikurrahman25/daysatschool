using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class MapScene : MonoBehaviour 
{

    public static bool justStarted=true;
    void Awake()
    {
        if(justStarted)
        {
            justStarted = false;
            GameObject.Find("Transition").GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("Transition/Logo").GetComponent<SpriteRenderer>().enabled = false;
        }

        if (Screen.orientation != ScreenOrientation.LandscapeLeft && Screen.orientation != ScreenOrientation.LandscapeRight)
            Screen.orientation = GlobalVariables.mapScreenOrientation;
        Screen.orientation = ScreenOrientation.AutoRotation;
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToPortraitUpsideDown = false;
        if(GlobalVariables.lastScreenOrientation!=ScreenOrientation.LandscapeLeft && GlobalVariables.lastScreenOrientation!= ScreenOrientation.LandscapeRight)
        {
            Debug.Log("Druga orijentacija");
            Debug.Log(GlobalVariables.lastScreenOrientation.ToString());
            Debug.Log(Screen.orientation.ToString());
            GlobalVariables.lastScreenOrientation = Screen.orientation;
        }
        PlayerPrefs.DeleteKey("EyeExamination");
        PlayerPrefs.Save();

        if(PlayerPrefs.GetInt("SoundOn")==2)
        {
            GameObject.Find("Canvas").transform.Find("Menus/MainMenu/ButtonsHolder/AnimationHolder/SettingsMenuHomeScene/ButtonSound/SoundOffIcon").gameObject.SetActive(true);
        }
    }

    public void SaveMapOrientation()
    {
        GlobalVariables.mapScreenOrientation = Screen.orientation;
    }
}
