using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PackingLevelManager : MonoBehaviour 
{
    //Set in editor TODO should be set from configuration file
    #region Config
    public TutorialController tutorialController;
    public int PackingVersion;
    public int numberOfItems;
    #endregion

    MenuManager menuManager;
    int itemsInPlace;

    private Transform canvas;

    void Awake()
    {
        if (Screen.orientation != ScreenOrientation.Portrait && Screen.orientation != ScreenOrientation.PortraitUpsideDown)
            Screen.orientation = ScreenOrientation.Portrait;
        Screen.orientation = ScreenOrientation.AutoRotation;
        Screen.autorotateToPortrait = true;
        Screen.autorotateToPortraitUpsideDown = true;
        Screen.autorotateToLandscapeLeft = false;
        Screen.autorotateToLandscapeRight = false;
        if (GlobalVariables.lastScreenOrientation != ScreenOrientation.Portrait && GlobalVariables.lastScreenOrientation != ScreenOrientation.PortraitUpsideDown)
        {
            Debug.Log("Druga orijentacija");
            GlobalVariables.lastScreenOrientation = Screen.orientation;
        }
    }

    void Start()
    {
        canvas = GameObject.Find("Canvas").transform;
        menuManager = GameObject.Find("Canvas").GetComponent<MenuManager>();
        itemsInPlace = 0;
        tutorialController.DragItemToTarget(0,5f);
    }

    public void ItemFound()
    {
        Debug.Log("Item found!");
        itemsInPlace++;
        if (numberOfItems == itemsInPlace)
            Invoke("AllItemsFound",1f);
        tutorialController.StopTutorial();

        SoundManagerPackingAndDressUp.Instance.Play_ItemInPlace();
    }

    #region EndGameMethods
    /// <summary>
    /// This is called when all items are found. Show reward animation, popup or something... 
    /// </summary>
    void AllItemsFound()
    {
        Debug.Log("SVI ITEMI PRONADJENI");

        menuManager.ShowPopUpMenu(canvas.Find("PopUps/PackingDonePopUp").gameObject);

        SoundManagerPackingAndDressUp.Instance.Play_LevelFinished();
    }
    /// <summary>
    /// This method is used to send data to MiniGameManager. 
    /// </summary>
    public void PackingFinished()
    { 
        Debug.Log("PAKOVANJE ZAVRSENO");
        canvas.GetComponent<MenuManager>().LoadSceneWithTransition("DressUpBoy");
    }
    #endregion
}