using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyesExaminationGameManager : MonoBehaviour {
    public static EyesExaminationGameManager Instance;
        

    public GameObject miniGameAcuityTest, miniGameShortVisionTest, miniGameColorBlindnessTest, miniGameEyeCalibrationTest;
    public EyesExaminationPhases currentPhase;
    public GameObject tutorialHolder;

    private Transform canvas;

    private int charactersFinished;

    void Awake()
    {
        Instance = this;
        if (Screen.orientation != ScreenOrientation.Portrait && Screen.orientation != ScreenOrientation.PortraitUpsideDown)
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
    }

	void Start () 
    {
        charactersFinished = PlayerPrefs.GetInt("EyeCharacter");
        canvas = GameObject.Find("Canvas").transform;
        canvas.transform.Find("MainPanel/CharacterHolder").GetChild(Random.Range(0,4)).gameObject.SetActive(true);
        canvas.GetComponent<MenuManager>().ShowMenu(miniGameAcuityTest);
        Timer.Schedule(this,2f,delegate() 
        {
            tutorialHolder.SetActive(true);
        });
	}

    public void MiniGameFinished()
    {
        switch (currentPhase)
        {

            case EyesExaminationPhases.VisualAcuityTest:
                currentPhase = EyesExaminationPhases.ShortVisiontest;
                canvas.GetComponent<MenuManager>().ShowMenu(miniGameShortVisionTest);
                break;

            case EyesExaminationPhases.ShortVisiontest:
                currentPhase = EyesExaminationPhases.ColorBlindnessTest;
                canvas.GetComponent<MenuManager>().ShowMenu(miniGameColorBlindnessTest);
//                miniGameColorBlindnessTest.GetComponent<QuizGameManager>().SendMessage("Awake");
//                miniGameColorBlindnessTest.GetComponent<QuizGameManager>().SendMessage("Start");

                break;

            case  EyesExaminationPhases.ColorBlindnessTest:
                currentPhase = EyesExaminationPhases.EyeCalibration;
                canvas.GetComponent<MenuManager>().ShowMenu(miniGameEyeCalibrationTest);
//                miniGameEyeCalibrationTest.GetComponent<AxisCheckManager>().SendMessage("Start");
                break;

            case EyesExaminationPhases.EyeCalibration:
                currentPhase = EyesExaminationPhases.VisualAcuityTest;
                FinishedGame();
                break;
            default:
                break;
        }

    }

    public void FinishedGame()
    {

        canvas.transform.Find("TopUI/FinishButton").gameObject.SetActive(true);

//        ++charactersFinished;
//        if(charactersFinished==4)
//        {
//        canvas.GetComponent<MenuManager>().LoadSceneWithTransition("MapScene");
//        }
//        else
//        {
//            PlayerPrefs.SetInt("EyeCharacter", charactersFinished);
//            PlayerPrefs.Save();
//            canvas.GetComponent<MenuManager>().LoadSceneWithTransition("EyeExaminationScene");
//        }
    }
	
}
[System.Serializable]
public enum EyesExaminationPhases
{
    VisualAcuityTest,
    ShortVisiontest,
    ColorBlindnessTest,
    EyeCalibration
}