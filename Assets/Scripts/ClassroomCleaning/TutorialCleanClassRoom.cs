using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TutorialCleanClassRoom : MonoBehaviour {
	
    public static bool bTutorial = false;
    public static bool bPause = false;
    public static string tutorialState = "";

    string tutorialStateOld = "";
    Animator animTutorial;
    Vector3 startPos = Vector3.zero;
    Vector3 endPos = Vector3.zero;

    public static float timeLeftToShowHelp = 2.5f;
    public static float ShowHelpPeriod = 3f;
    bool bHidden = true;



    public Transform[]  Phase1StartPositions;
    public Transform Phase1EndPosition;

    [Header("Phase 2A Tool")]
    public int phase2ATool = 1;
    [HideInInspector()]
    public Transform  Phase2StartPosition;
    public Transform[] Phase2EndPositions;

    [Header("Phase 2B Tool (if help item requires 2nd tool)")]
    public int phase2BTool = -1;
    [HideInInspector()]
    public Transform  Phase2bStartPosition;
    public Transform[] Phase2bEndPositions;

    Image DragImage;

    [Header("Override help positions")]
    bool bOverrideTutorial = false;
    public Transform OverrideStartPosition;
    public Transform OverrideEndPosition;


    public static TutorialCleanClassRoom Instance;

    Color visibleCol = new Color(1,1,1,.5f);
    Color hiddenCol = new Color(1,1,1,0);
    void Awake()
    {
        Instance = this;
    }

    void Start () {

        bPause = false;
//        if(Application.loadedLevelName.StartsWith( "Room") )
        {
            animTutorial = transform.GetComponent<Animator>();
            DragImage = transform. Find("DragItem").GetComponent<Image>();
            DragImage.color = hiddenCol;

            //          if(  GameData.TutorialShown == 0)  
            //          {
            ShowHelpPeriod = 3;
            timeLeftToShowHelp = 3;
            //          }
            //          else 
            //          {
            //              PeriodToShowHelp = 8;
            //              timeWaitToShowHelp = 8;
            //          }

            InvokeRepeating("TestGameplayTutorial",2f,0.1f);
        }


    }



    void TestGameplayTutorial()
    {
        //Debug.Log(bPause);
        //      Debug.Log(MenuManager.activeMenu + "  " + tutorialState + "   "+ PeriodToShowHelp + "  " +  timeWaitToShowHelp);
        if(!bPause && Gameplay.Instance!=null)
        {
            if(bHidden) 
            {
                timeLeftToShowHelp -=.1f;
                if(timeLeftToShowHelp <=0)
                {
                    tutorialState = Gameplay.Instance.GamePhase.ToString();
                    if(bOverrideTutorial) tutorialState = "OVERRIDE";
                    timeLeftToShowHelp = ShowHelpPeriod;
                    switch(tutorialState)
                    {
                        case "1": //situacija kada se prikazuje sakupljanje otpadaka
                            endPos = Phase1EndPosition.position;
                            Transform dragPosP1 = Phase1StartPositions[ Random.Range(0,Phase1StartPositions.Length)];
                            startPos =  dragPosP1.position;
                            transform.position = startPos;

                            if(DragImage!=null)
                            {
                                DragImage.sprite =   dragPosP1.GetComponent<Image>().sprite;
                                DragImage.color = hiddenCol;
                                DragImage.transform.position = dragPosP1.position;
                                DragImage.transform.localScale = dragPosP1.localScale;
                                DragImage.rectTransform.sizeDelta = dragPosP1.GetComponent<RectTransform>().sizeDelta;
                            }


                            StartPointing();
                            StartCoroutine( "MovePoinnter" );

                            break;

                        case "2": //situacija kada se prikazuje ciscenje ili popravka

                            endPos = Phase2EndPositions[ Random.Range(0,Phase2EndPositions.Length)].position; 

                            Transform dragPosP2 = Phase2StartPosition.GetChild(0).GetChild(0);
                            //Debug.Log(dragPosP2.name);

                            startPos = dragPosP2.position;
                            transform.position = startPos;




                            if(DragImage!=null)
                            {
                                DragImage.sprite =   dragPosP2.GetComponent<Image>().sprite;
                                DragImage.color = hiddenCol;
                                DragImage.transform.position = dragPosP2.position;
                                DragImage.transform.localScale = dragPosP2.localScale;
                                DragImage.rectTransform.sizeDelta = dragPosP2.GetComponent<RectTransform>().sizeDelta;
                            }



                            StartPointing();
                            StartCoroutine( "MovePoinnter" );
                            break;


                        case "OVERRIDE": //kada se zaobilazi tutorijal...mogucnost da se direktno iz koda podese lokacije na koje se pokazuje

                            endPos =OverrideEndPosition.position;
                            startPos =  OverrideStartPosition.position;
                            transform.position = startPos;

                            if(DragImage!=null)
                            {
                                DragImage.sprite =   OverrideStartPosition.GetComponent<Image>().sprite;
                                DragImage.color = hiddenCol;
                                DragImage.transform.position = OverrideStartPosition.position;
                                DragImage.transform.localScale = OverrideStartPosition.localScale;
                                DragImage.rectTransform.sizeDelta = OverrideStartPosition.GetComponent<RectTransform>().sizeDelta;
                            }


                            StartPointing();
                            StartCoroutine( "MovePoinnter" );


                            break;
                    }
                }
            }
        }
        else
        {
            switch(tutorialState)
            {
                case "select_room":
                    timeLeftToShowHelp = ShowHelpPeriod;
                    StopAllCoroutines();
                    HidePointer();
                    break;

                default : break;
            }
        }

    }

    IEnumerator  StartPointingAndHide(  )
    {
        bHidden = false;
        animTutorial.ResetTrigger("Hide");
        animTutorial.SetTrigger("moveStart");
        yield return new WaitForSeconds(5);
        animTutorial.ResetTrigger("moveStart");
        animTutorial.SetTrigger("Hide");
        timeLeftToShowHelp = ShowHelpPeriod;
        bHidden = true;
    }

    void StartPointing()
    {
        bHidden = false;
        animTutorial.SetTrigger("moveStart");
        //Debug.Log("StartPointing");
    }

    IEnumerator  MovePoinnter(  )
    {
        bHidden = false;
        float timeMove = 0;
        transform.position = Vector3.Lerp(startPos,endPos,timeMove);
        yield return new WaitForSeconds(.8f);
        DragImage.color = visibleCol;
        animTutorial.SetTrigger("move");

        yield return new WaitForSeconds(.8f);
        while(timeMove<1)
        {
            timeMove+=2*Time.deltaTime;
            transform.position = Vector3.Lerp(startPos,endPos,timeMove) ;

            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(.2f);
        DragImage.color = hiddenCol;
        LiftPointer();

    }

    void LiftPointer()
    {
        animTutorial.SetTrigger("moveEnd");

        bHidden = true;
        timeLeftToShowHelp = ShowHelpPeriod;
    }

    public void HidePointer()
    {
        if(!bHidden)
        {
            bHidden = true;
            animTutorial.SetTrigger("Hide");
        }

        if(DragImage!=null)
        {
            DragImage.sprite =   null;
            DragImage.color = hiddenCol;
        }
        StopAllCoroutines();
        timeLeftToShowHelp = ShowHelpPeriod;
    }


    public void AnimEventMoveEnd()
    {
        HidePointer();

    }

    public void SwapTutorialTool()
    {
        Phase2StartPosition =  Phase2bStartPosition;
        Phase2EndPositions  = Phase2bEndPositions;
        phase2BTool = -2;
    }

    public void StartOverrideTutorial( Transform _Start, Transform _End)
    {
        bOverrideTutorial = true;

        OverrideStartPosition = _Start;
        OverrideEndPosition = _End;
        TutorialCleanClassRoom.bPause = false;
        ShowHelpPeriod = 3;
        timeLeftToShowHelp = 1;
    }

    public void StopOverridingTutorial()
    {
        bOverrideTutorial = false;
        TutorialCleanClassRoom.bPause = true;
        ShowHelpPeriod = 10000;
        timeLeftToShowHelp = 10000;
    }


}
