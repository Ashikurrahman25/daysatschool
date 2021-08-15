using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
 

public class Gameplay : MonoBehaviour {
	public static Gameplay Instance;


	public  int GamePhase = 0; //1- sakuplljanje otpadaka, 2- sredjivanje i ciscenje alatom, 3- dekoracija

	public int GamePhase1Items = 0; //SAKUPLJANJE OTPADAKA
	public int GamePhase2Items = 0; //SREDJIVANJE I CISCENJE ALATON

	public MiniGamePainting mgPainting;

	public GameObject[] HideWhenMiniGame;
	bool[] statusHideWhenMiniGame;
	 

	public GameObject[] MiniGameStartButtons;
	public MenuCleaningTools LeftMenuCleaning;
	public MenuDecorations LeftMenuDecorations;
	public GameProgressBar gameProgressBar;

	public GameObject RightMenuButtons ;
	public GameObject CleaningTopMenuButtons ;
 
	public GameObject ButtonFinish;

	Animator TrashCan;

	public static int roomNo = 1;
	public static int childNo = 0;
	public static bool bReturnFromMiniGame = false;

//	public GameplayChildController Child;
	public GameObject[] DestroyIfDecorating;
	public GameObject[] ShowIfDecorating;
 
	public bool Room6WashingMachineFull = false;
	public bool Room6CommodeClosed = false;

	public RawImage ScreenshotImage;

	public Transform DragItemParent;
    public GameObject teacher;

	void Awake()
	{
		Instance = this; 
	}

	IEnumerator Start () 
	{
		BlockClicks.Instance.SetBlockAll(true);
		BlockClicks.Instance.SetBlockAllDelay(1.5f,false);
		if( roomNo == 1) 
		{
			StartCoroutine("WaitToInitMiniGamePainting");
			yield return new WaitForSeconds(.1f);
		}

//		yield return new WaitForSeconds(.5f);
		if(MenuDecorations.LevelDecorations[Gameplay.roomNo-1] != "" || bReturnFromMiniGame ) //povratak na scenu - dekoracija
		{
			 
			
			bReturnFromMiniGame = false;
//			LeftMenuDecorations.RestoreDecorations(); //postavljanje dekoracija koje su snimljene po napustanju scene

			GamePhase =   3;

//			CleaningTopMenuButtons.SetActive(false);
			RightMenuButtons.SetActive(true);

			ShowHidePainitingButtons(true);

			LeftMenuDecorations.gameObject.SetActive(true);
			LeftMenuDecorations.ShowDecorationsMenu();

			ButtonFinish.SetActive(true);
			 
			GameObject.Destroy(LeftMenuCleaning.gameObject);
			GameObject.Destroy(gameProgressBar.gameObject);
			foreach(GameObject go in DestroyIfDecorating)
			{
				if(go!=null)GameObject.Destroy(go);
			}
		}
		else //normalno ucitavanje scene
		{

			foreach(GameObject go in ShowIfDecorating)
			{
				if(go!=null) go.SetActive(false);
			}
			//if(Tutorial.bTutorial) LeftMenuCleaning.
			ButtonFinish.SetActive(false);
			ShowHidePainitingButtons(false);
			GamePhase =   0;
			StartNextPhase();
		}
        yield return new WaitForSeconds(.5f);
        teacher.GetComponent<Animator>().Play("ShowStewardess");
        teacher.transform.Find("AnimationHolder").GetComponent<Animator>().Play("CharacterIdle_Pointing");
        yield return new WaitForSeconds(3f);
        teacher.GetComponent<Animator>().Play("HideStewardess");

        //AdsManager.Instance.IsVideoRewardAvailable();

		 
//		LevelTransition.Instance.ShowScene();
	}
	 
	IEnumerator WaitToInitMiniGamePainting()
	{
		 if(mgPainting!=null)
		{
			mgPainting.gameObject.SetActive(true); 
			mgPainting.GetComponent<RectTransform>().anchoredPosition = new Vector2(10000,10000);
			mgPainting.paintEngine.drawEnabled = false;
			yield return new WaitForSeconds(5);

			mgPainting.gameObject.SetActive(false); 
			mgPainting.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
			mgPainting.bInit = true;
		}
		yield return null;
	}


	IEnumerator Phase1Delay()
	{
		yield return new WaitForSeconds(1);
		gameProgressBar.ShowProgressBar();
		if(roomNo <5)
		{
			TrashCan = GameObject.Find("TrashCan").GetComponent<Animator>();
			TrashCan.SetBool("Show",true);
		}
	}

	public void StartNextPhase()
	{
//		Debug.Log("NEXT PHASE");
		GamePhase++;
		if(GamePhase == 1)
		{
//			CleaningTopMenuButtons.SetActive(true);
//			RightMenuButtons.SetActive(false);

			GamePhase1Items = DragToLocation.ToClean + DragToLocation.ToClean_L2 ;
			gameProgressBar.TotalSteps = GamePhase1Items;

            StartCoroutine("Phase1Delay");
		}

        else if(GamePhase == 2)
		{
			//FAZA POPRAVKE I SREDJIVANJA
//			CleaningTopMenuButtons.SetActive(false);
			RightMenuButtons.SetActive(true);

			GamePhase2Items = Clean.CleanAndFixItemsCount;
			gameProgressBar.TotalSteps = GamePhase2Items;
			gameProgressBar.ShowProgressBar();


			LeftMenuCleaning.gameObject.SetActive(true);
			LeftMenuCleaning.ShowMenu();

			//RESET TUTORIAL TIME
			 
            TutorialCleanClassRoom.timeLeftToShowHelp = 3;
            TutorialCleanClassRoom.ShowHelpPeriod = 3;
            TutorialCleanClassRoom.bPause = false;

			//ZABRANA KLIKOVA NA SVE OSIM NA PRVI ITEM
		}

        else  if(GamePhase ==3) 
		{
			//FAZA DEKORACIJE
			ShowHidePainitingButtons(true);

			LeftMenuDecorations.gameObject.SetActive(true);
			LeftMenuDecorations.ShowDecorationsMenu();
			ButtonFinish.SetActive(true);
			CleaningFinished();
			GameObject.Destroy(LeftMenuCleaning.gameObject,2);

			foreach(GameObject go in ShowIfDecorating)
			{
				if(go!=null) go.SetActive(true);
			}
		}
	}

	//***********************************
 
	public void CollectingItemsFinished( )
	{
		SoundManagerCleaningClassroom.Instance.StopAndPlay_Sound(SoundManagerCleaningClassroom.Instance.CleaningFinished);
		TrashCan.SetBool("Show",false);
        StartNextPhase();
	}

	public void DetergentFinished()
	{
		GameObject.Find("CanvasBG/SceneGraphics/WashingMachine/Lid").GetComponent<Image>().enabled = false;
		 //Animacija ves masine
		GameObject.Find("CanvasBG/SceneGraphics/WashingMachine").GetComponent<Animator>().SetBool("bWashing", true);
		GameObject.Find("CanvasBG/SceneGraphics/WashingMachine/ParticlesWashingMachine").GetComponent<ParticleSystem>().Play();
        TutorialCleanClassRoom.Instance.StopOverridingTutorial();

		SoundManagerCleaningClassroom.Instance.StopAndPlay_Sound(SoundManagerCleaningClassroom.Instance.CleaningFinished);
	}

	IEnumerator CloseWashingMachine()
	{
		yield return new WaitForFixedUpdate();
		 
		GameObject.Destroy(GameObject.Find("CanvasBG/SceneGraphics/WashingMachine/WashingMachineTarget"));
 
		GameObject.Find("CanvasBG/SceneGraphics/WashingMachine/DoorOpened").GetComponent<Image>().enabled = false;
		GameObject.Find("CanvasBG/SceneGraphics/WashingMachine/DoorClosed").GetComponent<Image>().enabled = true;
		GameObject.Find("CanvasBG/SceneGraphics/WashingMachine/Lid").GetComponent<Image>().enabled = true;

		if( Room6CommodeClosed && Room6WashingMachineFull) 
		{
			GameObject.Find("Detergent").GetComponent<DragToLocation>().enabled = true;
            TutorialCleanClassRoom.Instance.StartOverrideTutorial(GameObject.Find("DetergentBottle").transform,  GameObject.Find("CanvasBG/SceneGraphics/WashingMachine/Lid").transform);
		}
	}

	int wmCycle  = 0;
	public void WashingMachineAnimationCycleFinished()
	{
		wmCycle++;
		if(wmCycle == 8)  
		{
			GameObject.Find("CanvasBG/SceneGraphics/WashingMachine").GetComponent<Animator>().SetBool("bWashing",false);
			GameObject.Find("CanvasBG/SceneGraphics/WashingMachine/DoorOpened").GetComponent<Image>().enabled = true;
			GameObject.Find("CanvasBG/SceneGraphics/WashingMachine/DoorClosed").GetComponent<Image>().enabled = false;
			StartCoroutine("MoveClaenLaundry");

		}
	}

	IEnumerator MoveClaenLaundry()
	{

		Image ImageL = GameObject.Find("CanvasBG/SceneGraphics/Decorations/P32/CleanLaundry").GetComponent<Image>();
		ImageL.enabled = true;
		Vector3 targetL = GameObject.Find("CanvasBG/SceneGraphics/Decorations/P32/CleanLaundryTarget").transform.position;
		float timeMove = 0;
		yield return new WaitForSeconds(0.5f);
		while  (timeMove  < .5f )
		{
			yield return new WaitForFixedUpdate();
			timeMove += (Time.fixedDeltaTime/2);
			ImageL.transform.position = Vector3.Lerp (ImageL.transform.position, targetL , timeMove)  ;
		}
		ImageL.transform.SetParent(GameObject.Find("CanvasBG/SceneGraphics/Decorations/P32/CleanLaundryTarget").transform);
		yield return new WaitForFixedUpdate();

		GameObject DetergentHolder2 = GameObject.Find("CanvasBG/SceneGraphics/DecorationsStatic");
		GameObject.Find("CanvasBG/SceneGraphics/WashingMachine/Detergent").transform.SetParent (DetergentHolder2.transform);
		GameObject.Find("CanvasBG/SceneGraphics/WashingMachine/Detergent2").transform.SetParent (DetergentHolder2.transform);

	}

	IEnumerator HideDrawer()
	{
		CanvasGroup drawer = GameObject.Find("Drawer").GetComponent<CanvasGroup>();
		drawer.transform.Find("ParticlesDrawer").GetComponent<ParticleSystem>().Play();
 		
		float timeFade = 0;

		while  (drawer.alpha  >0)
		{
			yield return new WaitForFixedUpdate();
			drawer.alpha-= Time.fixedDeltaTime;
		}

		GameObject.Destroy(drawer.gameObject);
	 
	}



 

	public void ChangeProgressBar()
	{
//		Debug.Log("CHANGE");
		gameProgressBar.IncreaseBar();
	}
 
	//*******************************
	//LEFT MENU 
	public void ObjectCleaned(int toolNO)
	{
		LeftMenuCleaning.ObjectCleaned(toolNO);
		if(LeftMenuCleaning.IsCleaningFinished())
		{
			StartCoroutine("ShowLeftMenuDecorationsWithDelay");
			GameData.TotalStars++;
			GameData.SetStarsToPP();
		}

		gameProgressBar.IncreaseBar();
        if(TutorialCleanClassRoom.Instance!=null && TutorialCleanClassRoom.Instance.phase2BTool == -1 && TutorialCleanClassRoom.Instance.gameObject.activeSelf)
		{
			CleaningTool.OneToolEnabledNo = -1;
//            TutorialCleanClassRoom.bPause = true;
            TutorialCleanClassRoom.Instance.StopAllCoroutines();
            TutorialCleanClassRoom.Instance.CancelInvoke();
            TutorialCleanClassRoom.Instance.gameObject.SetActive(false);
		}
        else if(TutorialCleanClassRoom.Instance!=null && TutorialCleanClassRoom.Instance.phase2BTool  != toolNO &&  TutorialCleanClassRoom.Instance.phase2BTool >   -1  )
		{
			//Debug.Log("SWAP TUT " + Tutorial.Instance.phase2BTool  +  " ,  " + toolNO );
            TutorialCleanClassRoom.Instance.SwapTutorialTool();
			//Debug.Log("SWAPED TUT " + Tutorial.Instance.phase2BTool    );
		}
	}
 
	public void CleaningFinished()
	{
        Debug.Log("Cleaning Finished");
		LeftMenuCleaning.HideMenu();
		gameProgressBar.HideProgressBar();
        ButtonFinish.SetActive(true);
        TutorialCleanClassRoom.bPause = false;
        TutorialCleanClassRoom.timeLeftToShowHelp = 3;
        TutorialCleanClassRoom.ShowHelpPeriod = 3;
	}


	IEnumerator ShowLeftMenuDecorationsWithDelay()
	{
		yield return new WaitForSeconds(1);
		StartNextPhase();


	}

	//*********************************

	void ShowHidePainitingButtons(bool active)
	{
		for(int i=0; i<MiniGameStartButtons.Length;i++)
		{
 
			MiniGameStartButtons[i].SetActive(active);
		}
	}








	//*****************************************************************
	 

	 
 

	public void ShowRoom()
	{
		for(int i=0; i<HideWhenMiniGame.Length;i++)
		{
			if(HideWhenMiniGame[i] != null ) HideWhenMiniGame[i].SetActive(statusHideWhenMiniGame[i]);

		}

		if(GamePhase == 3) 
		{
			LeftMenuDecorations.ShowDecorationsMenu();

			if(GameData.MiniGamePlayed[(Gameplay.roomNo-1)]  &&  LeftMenuDecorations.listaGlavnihMenija == "" )
			{
				ShowChildGoToNextLevel();
			}
		}
	}



	public void ChildClickNextLevel()
	{
		if(GameData.ItemsDataList[roomNo].unlocked)
		{
			//TODO DA LI JE USLOV ZA PRELAZAK ISPUNJEN
			//Debug.Log("OTKLJUCAN SLEDECI NIVO");
			DragToLocation.ToClean= 0;
			DragToLocation.ToClean_L2= 0;
			Clean.CleanAndFixItemsCount = 0;
			roomNo ++;
			 
//			LevelTransition.Instance.HideSceneAndLoadNext("Room" + roomNo.ToString());
		}
		else
		{
			//Debug.Log("NIJE OTKLJUCAN SLEDECI NIVO, POVRATAK");
			DragToLocation.ToClean= 0;
			DragToLocation.ToClean_L2= 0;
			Clean.CleanAndFixItemsCount = 0;
			roomNo ++;
			ItemsSlider.ActiveItemNo = roomNo;
//			LevelTransition.Instance.HideSceneAndLoadNext("HomeScene");
		}

	}

	public void FinishLevel()
	{
		DragToLocation.ToClean= 0;
		DragToLocation.ToClean_L2= 0;
		ItemsSlider.ActiveItemNo = roomNo;
		GameData.IncrementButtonCheckClickedCount();
//		LevelTransition.Instance.HideSceneAndLoadNext("HomeScene");
		 
	}


	public void ShowChildGoToNextLevel()
	{
//		//Debug.Log("SHOW INFO NEXT LEVEL");
//		Child.StopAllCoroutines();
//		Child.StartCoroutine("ActivcateNextLevelInfo");
//		if(!GameData.CheckedRooms[(roomNo-1)])
//		{
//			GameData.CheckedRooms[(roomNo-1)] = true;
//			GameData.WatchVideoCounter++;
//		}
	}

	public void IncreaseProgressBarFixingAndClening()
	{
		
	}

	//----------------button replay decoration----------------

	public void ButtonReplayClicked()
	{
		DragToLocation.ToClean= 0;
		DragToLocation.ToClean_L2= 0;
		Clean.CleanAndFixItemsCount = 0;
		bReturnFromMiniGame = false;

		//if(GameData.CheckedRooms[(Gameplay.roomNo-1)]) 
		//{
			MenuDecorations.LevelDecorations[(Gameplay.roomNo-1)] = ""; 
			GameData.CheckedRooms[(Gameplay.roomNo-1)] = false;
		//}
		 
		
		if( Gameplay.roomNo == 2 ) 
		{
			GlobalVariables.mgDrawShaper_SavedProgres = "";
			MiniGame_DrawShapes.screenshot = null;
		}
		if( Gameplay.roomNo == 4 ) MiniGameConnectWires.ConnectedWires = 0;
		
//		LevelTransition.Instance.HideSceneAndLoadNext("Room" + Gameplay.roomNo);

		GameData.IncrementButtonRepeatClickedCount();
	}





	//******************************************
	public void ButtonMiniGamePaintingClicked()
	{
		if(!mgPainting.bInit) return;
//		EscapeButtonManager.AddEscapeButonFunction("btnClosePaintingClick");
		statusHideWhenMiniGame = new bool[HideWhenMiniGame.Length];
		mgPainting.gameObject.SetActive(true);
		for(int i=0; i<HideWhenMiniGame.Length;i++)
		{
			if(HideWhenMiniGame[i] != null ) 
			{
				statusHideWhenMiniGame[i] = HideWhenMiniGame[i].activeSelf;
				HideWhenMiniGame[i].SetActive(false);
			}
		}
 
		GameData.MiniGamePlayed[(roomNo-1)] = true;
	}

	//******************************************
	//ESC BUTTON POZIVI

	public void btnClosePaintingClick()
	{

		mgPainting.btnClosePaintingClick();
	}

	public void btnHomeClicked()
	{
 
		MenuDecorations.LevelDecorations[(Gameplay.roomNo-1)] = MenuDecorations.LevelDecorationsStart;

		StopAllCoroutines();
		ItemsSlider.ActiveItemNo = Gameplay.roomNo;
//		LevelTransition.Instance.HideSceneAndLoadNext("HomeScene");
		BlockClicks.Instance.SetBlockAll(true);
		BlockClicks.Instance.SetBlockAllDelay(1f,false);

		GameData.IncrementButtonHomeClickedCount();
	}
	//*******************************************
}
