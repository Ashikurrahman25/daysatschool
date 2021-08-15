using UnityEngine;
using System.Collections;
using UnityEngine.UI;

///<summary>
///<para>Scene:All/NameOfScene/NameOfScene1,NameOfScene2,NameOfScene3...</para>
///<para>Object:N/A</para>
///<para>Description: Sample Description </para>
///</summary>

public class GlobalVariables : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		DontDestroyOnLoad(gameObject);
	}
//    public static int InterstitialNextCounter=1;

    public static Color[] juices=new Color[4];
    public static ScreenOrientation lastScreenOrientation = ScreenOrientation.Landscape;
    public static ScreenOrientation mapScreenOrientation = ScreenOrientation.Landscape;

    public static string mgDrawShaper_SavedProgres
    {
        get;
        set;
    }

    public static int selectedCharacterIndex
    {
        get;
        set;
    }

    public static bool playLoadingDepart
    {
        get;
        set;
    }

    public static bool unlockNewLevelWhenLastLevelPassed
    {
        get;
        set;
    }

    public static string[] characterLocks
    {
        get;
        set;
    }

    public static int lastUnlockedCharacterIndex
    {
        get;
        set;
    }

    public static bool playCharacterUnlockAnimation
    {
        get;
        set;
    }

    public void DisableLog(string msg)
    {
        Debug.unityLogger.logEnabled = false;
    }
}
