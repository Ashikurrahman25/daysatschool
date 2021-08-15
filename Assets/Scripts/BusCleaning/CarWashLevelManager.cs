using UnityEngine;
using System.Collections;
using UnityEngine.UI;
 

public class CarWashLevelManager : MonoBehaviour {

	public ParticleSystem psDisableTool;

    ParticleSystem psLeaves0;
	ParticleSystem psLeaves1;
	ParticleSystem psLeaves2;
	ParticleSystem psLeaves3;
	ParticleSystem psLeaves4;
	ParticleSystem psLeaves5;
 
	int bubblesCount = 0;
	Transform bubblesHolder;
	Transform RotatingBrush;
	Transform GasPump;
	Transform FuelTank;
	Transform TirePump;
	 
	Transform cleningCloth;
	Transform sponge;
	Transform wheelBrush;
	Transform leafBlower;
	Transform shears;
	Transform polisher;

	WaterGun waterGun;
	WaterHose waterHose;
 
	public int starsCleanLeaves = 0; 
	public int starsCleanBranches = 0; 
	public int starsCleanDirt = 0; 
	public int starsCleanStains = 0; 
	public int starsCleanWheelBrush = 0; 
	public int starsBubbles = 0; 
	public int starsRotatingBrush = 0; 
	public int starsWaxCar = 0;
	public int starsPolishCar = 0;
	public int starsInflateTires = 0;
	public int starsFuel = 0;

	bool bStartInflatation = false;
	bool bStartWheelBrush = false;
	public static CarWashLevelManager Instance;

	public GameObject ButtonNext;
    public GameObject ButtonFinish;

	public int lastCompletedToolAction = 0;
	public string AllCompletedToolActions = ""; //"01,02,03,04,05,06,07,08,09,10,11";
	public string RemovedToolsList = ""; //"01,02,03,04,05,06,07,08,09,10,11";

	public GameObject ToolsButtonsHolder1;
	public GameObject ToolsButtonsHolder2;
//	public GameObject btnGasPedal;

//	public GameObject SpeedMeter;

	 
	public int nextTool = 1;

    public GameObject teacher;

	CanvasGroup BlockAll;

	Sponge spongeScript;
	Animator animRotatingBrush ;
    TutorialBusCleaning tutorial;
	void Awake()
	{
//        Screen.orientation = ScreenOrientation.Landscape;
//		GameData.CollectedStars = 0;


//		btnGasPedal.SetActive(false);
		Time.timeScale = 1;
		Instance = this;
        ButtonNext.SetActive(false);
//        ButtonFinish.SetActive(false);
		ToolsButtonsHolder1.SetActive(true);
		ToolsButtonsHolder2.SetActive(false);

		psDisableTool.GetComponent<Renderer>().sortingLayerName = "UI";
		psDisableTool.Stop();
		BlockAll = GameObject.Find("BlockAll").GetComponent<CanvasGroup>();
		BlockAll.blocksRaycasts = true;
		StartCoroutine(SetBlockAll(2f,false));
		StartCoroutine("EnableTools1");

//		SpeedMeter.SetActive(false);
	}

	IEnumerator EnableTools1()
	{
		yield return new WaitForSeconds(.1f);
		ToolsButtonsHolder1.SetActive(false);
		yield return new WaitForSeconds(3.7f);

		ToolsButtonsHolder1.SetActive(true);
		ToolsButtonsHolder2.SetActive(false);
		HighlightNextToolButton();
	}

 
	public void StartAndStopLeaves()
	{
//		if(GameObject.Find("CAR/Leaves/Leaves0")!=null)
//		{
//			if(psLeaves0==null) psLeaves0 = (ParticleSystem) GameObject.Find("CAR/Leaves/Leaves0").GetComponent<ParticleSystem>();
//			psLeaves0.Simulate(0.05f,false,true); 
//		}
		if(GameObject.Find("CAR/Leaves/Leaves1")!=null)
		{
			if(psLeaves1==null) psLeaves1 = (ParticleSystem) GameObject.Find("CAR/Leaves/Leaves1").GetComponent<ParticleSystem>();
			 psLeaves1.Simulate(0.05f,false,true); 
		}
		if(GameObject.Find("CAR/Leaves/Leaves2")!=null)
		{
			if(psLeaves2==null) psLeaves2 = (ParticleSystem) GameObject.Find("CAR/Leaves/Leaves2").GetComponent<ParticleSystem>();
			psLeaves2.Simulate(0.05f,false,true); 
		}
		if(GameObject.Find("CAR/Leaves/Leaves3")!=null)
		{
			if(psLeaves3==null) psLeaves3 = (ParticleSystem) GameObject.Find("CAR/Leaves/Leaves3").GetComponent<ParticleSystem>();
			psLeaves3.Simulate(0.05f,false,true); 
			 
		}
		if( GameObject.Find("CAR/Leaves/Leaves4")!=null)
		{
			if(psLeaves4==null) psLeaves4 = (ParticleSystem) GameObject.Find("CAR/Leaves/Leaves4").GetComponent<ParticleSystem>();
			psLeaves4.Simulate(0.05f,false,true); 
			 
		}
		if(GameObject.Find("CAR/Leaves/Leaves5")!=null)
		{
			if(psLeaves5==null) psLeaves5 = (ParticleSystem) GameObject.Find("CAR/Leaves/Leaves5").GetComponent<ParticleSystem>();
			psLeaves5.Simulate(0.05f,false,true);  
		}

	}
 
	IEnumerator Start () {
        
		tutorial = GameObject.Find("Tutorial").GetComponent<TutorialBusCleaning>();
//		Shop.Instance.txtDispalyStars =  null;


		starsCleanLeaves = 0; 
		starsCleanBranches = 0; 
		starsCleanDirt = 0; 
		starsCleanStains = 0; 
		starsCleanWheelBrush = 0; 
		starsBubbles = 0; 
		starsRotatingBrush = 0; 
		starsWaxCar = 0;
		starsPolishCar = 0;
		starsInflateTires = 0;
		starsFuel = 0;







        Vector3 scale = Vector3.one;// * Screen.width*3f/(4f*Screen.height);

        bubblesHolder = GameObject.Find("CAR").transform.Find("Bubbles").transform;
		RotatingBrush = GameObject.Find("RotatingBrush2").transform;
		animRotatingBrush = RotatingBrush.GetChild(0).GetComponent<Animator>();


		GasPump = GameObject.Find("GasPump").transform;
		GasPump.localScale = scale;
		FuelTank  = GameObject.Find("FuelTank").transform;

//		TirePump = GameObject.Find("Tools/TirePump").transform;
//		TirePump.localScale = scale;


		//waterHose = GameObject.Find("Tools/WaterHose").transform;
		//waterHose.localScale = scale;

		cleningCloth = GameObject.Find("Tools/CleaningCloth").transform;
		cleningCloth.localScale = scale;

		sponge = GameObject.Find("Tools/Sponge").transform;
		sponge.localScale = scale;


//		wheelBrush = GameObject.Find("Tools/WheelBrush").transform;
//		wheelBrush.localScale = scale;

		leafBlower = GameObject.Find("Tools/LeafBlower").transform;
		leafBlower.localScale = scale;

		shears = GameObject.Find("Tools/Shears").transform;
		shears.localScale = new Vector3(-scale.x,scale.y,scale.z);

		polisher = GameObject.Find("Tools/Polisher").transform;
		polisher.localScale = scale;


 	 
        waterGun = GameObject.Find("Tools2").transform.Find("WaterGun").GetComponent<WaterGun>();
        waterHose = GameObject.Find("Tools2").transform.Find("WaterHose").GetComponent<WaterHose>();


		DisableAllTools();
		yield return new WaitForSecondsRealtime(.5f);
		 
//		LevelTransition.Instance.ShowScene();
//		LevelTransition.bFirstStart = false;

        yield return new WaitForSeconds(.5f);
        teacher.GetComponent<Animator>().Play("ShowStewardess");
        teacher.transform.Find("AnimationHolder").GetComponent<Animator>().Play("CharacterIdle_Pointing");
        yield return new WaitForSeconds(3f);
        teacher.GetComponent<Animator>().Play("HideStewardess");
	}

	void DisableAllTools()
	{
		TutorialBusCleaning.tutorialState = "";
        TutorialBusCleaning.timeWaitToShowHelp = TutorialBusCleaning.PeriodToShowHelp;
        TutorialBusCleaning.bPause = true;

        CancelInvoke("TestBubbles");
		FuelTank.gameObject.SetActive(false);
		GasPump.SendMessage("DisableGasPump", SendMessageOptions.DontRequireReceiver);   
		//TirePump.gameObject.SetActive(false);
//		TirePump.SendMessage("DisableTirePump", SendMessageOptions.DontRequireReceiver);  
		cleningCloth.gameObject.SetActive(false);
		sponge.gameObject.SetActive(false);

		StartCoroutine("HideRotatingBrush");


//		wheelBrush.gameObject.SetActive(false);
		leafBlower.gameObject.SetActive(false);
		shears.gameObject.SetActive(false);
		polisher.gameObject.SetActive(false);
		waterGun.Deactivate();
        if(waterHose!=null)
		    waterHose.Deactivate();


		CarWashSoundManager.Instance.Stop_Sound(CarWashSoundManager.Instance.WaterHose);
//		CarWashSoundManager.Instance.Stop_Sound(CarWashSoundManager.Instance.WheelBrush);
		CarWashSoundManager.Instance.Stop_Sound(CarWashSoundManager.Instance.Compressor);
		CarWashSoundManager.Instance.Stop_Sound(CarWashSoundManager.Instance.Air);
		CarWashSoundManager.Instance.Stop_Sound(CarWashSoundManager.Instance.Sponge);
		CarWashSoundManager.Instance.Stop_Sound(CarWashSoundManager.Instance.Fuel);
		CarWashSoundManager.Instance.Stop_Sound(CarWashSoundManager.Instance.TirePump);
		CarWashSoundManager.Instance.Stop_Sound(CarWashSoundManager.Instance.Shears);
		CarWashSoundManager.Instance.Stop_Sound(CarWashSoundManager.Instance.RotatingBrush);
	}

	IEnumerator PauseLeaves()
	{
		yield return new WaitForFixedUpdate();
		//yield return new WaitForEndOfFrame();
	
		if(psLeaves1!=null) psLeaves1.Pause();
		if(psLeaves2!=null)psLeaves2.Pause();
		if(psLeaves3!=null)psLeaves3.Pause();
		if(psLeaves4!=null)psLeaves4.Pause();
		if(psLeaves5!=null)psLeaves5.Pause();
	 
	}
	
	 
	public void InflateTyres()
	{

		if(TirePump.gameObject.activeSelf) return;
		//StartCoroutine("InflateTyres1");
		DisableAllTools();
		TirePump.transform.position = Vector3.zero;
		TirePump.gameObject.SetActive(true);
		TutorialBusCleaning.tutorialState = "inflate_tires";
		TutorialBusCleaning.bPause = false;
		tutorial.HidePointer();
		SoundManager.instance.Play_ButtonClick();
	}


	public void CleanDirt()
	{

		if(waterHose.spritesCompressor.Count>0 &&  waterHose.spritesCompressor[0].enabled&&waterHose.gameObject.activeSelf) return;
		//StartCoroutine("ShowWater");
		DisableAllTools();
        waterHose.gameObject.SetActive(true);
	    waterHose.Activate();
        waterHose.GetComponent<SpriteRenderer>().enabled = true;
		TutorialBusCleaning.tutorialState = "clean_dirt";
		TutorialBusCleaning.bPause = false;
		tutorial.HidePointer();
		SoundManager.instance.Play_ButtonClick();
	}

	 

	public void WaxCar()
	{
		if(cleningCloth.gameObject.activeSelf) return;
		DisableAllTools();
		cleningCloth.transform.position = Vector3.zero;
		cleningCloth.gameObject.SetActive(true);
		TutorialBusCleaning.tutorialState = "wax_car";
		TutorialBusCleaning.bPause = false;
		tutorial.HidePointer();
		SoundManager.instance.Play_ButtonClick();
	}



	public void CleanLeaves()
	{
		if(leafBlower.gameObject.activeSelf) return;
		DisableAllTools();
		leafBlower.transform.position = Vector3.zero;
		leafBlower.gameObject.SetActive(true);
		TutorialBusCleaning.tutorialState = "clean_leaves";
		TutorialBusCleaning.bPause = false;
		tutorial.HidePointer();
		SoundManager.instance.Play_ButtonClick();
		 
	}
 

	public void CleanStains()
	{
		if(waterGun.spritesCompressor.Count>0 &&  waterGun.spritesCompressor[0].enabled) return;
	 
		SoundManager.instance.Play_ButtonClick();
		DisableAllTools();
        waterGun.gameObject.SetActive(true);
		waterGun.Activate();
        waterGun.GetComponent<SpriteRenderer>().enabled = true;
		TutorialBusCleaning.tutorialState = "clean_stains";
		TutorialBusCleaning.bPause = false;
		tutorial.HidePointer();
		//StartCoroutine("CleanStains1");
	}

	 

	 
	public void CutBranches()
	{
		if(shears.gameObject.activeSelf) return;
		 
		DisableAllTools();
		shears.transform.position = Vector3.zero;
		shears.gameObject.SetActive(true);
		TutorialBusCleaning.tutorialState = "cut_branches";
		TutorialBusCleaning.bPause = false;
		tutorial.HidePointer();
		SoundManager.instance.Play_ButtonClick();
	 
			 
	}
	
 
	public void Bubbles()
	{
		if(sponge.gameObject.activeSelf) return;
		//starsBubbles = 0;
		//starsRotatingBrush = 0;
		//GameObject.Find ("btnBubbles").GetComponent<ButtonSelectTool>().ResetAllStars();
		 
		SoundManager.instance.Play_ButtonClick();
		DisableAllTools();
		sponge.transform.position = Vector3.zero;
		sponge.gameObject.SetActive(true);
		if(spongeScript == null) spongeScript = sponge.GetComponent<Sponge>();
		foreach(Transform ch in bubblesHolder)
		{
			if(ch.tag == "ANCHOR") {
				ch.GetComponent<Animator>().ResetTrigger("disable");
				ch.GetComponent<Animator>().SetTrigger("activate");
				ch.GetComponent<CircleCollider2D>().radius = 55;
			}
		}
		InvokeRepeating("TestBubbles",0,0.1f);
		TutorialBusCleaning.tutorialState = "bubbles";
		TutorialBusCleaning.bPause = false;
		tutorial.HidePointer();
	}

	void TestBubbles()
	{
		if(!spongeScript.bDrag) return;
		if( bubblesCount< bubblesHolder.childCount && Input.GetMouseButton(0) )
		{

			Vector3 mousePos = Input.mousePosition;
			mousePos.z = 10;
			
			Vector3 screenPos =    new Vector3(sponge.position.x, sponge.position.y,0);                      //Camera.main.ScreenToWorldPoint(mousePos);
			RaycastHit2D[] hit = Physics2D.RaycastAll(screenPos,Vector2.zero);



			for(int i = 0; i<hit.Length;i++)
			{
				if( hit[i].transform!=null &&  hit[i].transform.tag == "ANCHOR" && hit[i].transform.name == "BP")
				{
					hit[i].collider.transform.GetComponent<Animator>().SetTrigger("show");
					bubblesCount++;
					 
					if((bubblesHolder.childCount/5f* (starsBubbles+.5f) < bubblesCount)  && starsBubbles<4) AddStarBubbles();
					else if(starsBubbles==4 && bubblesCount == bubblesHolder.childCount) AddStarBubbles();
					if(starsRotatingBrush > 0 ) { 
						GameObject.Find ("btnRotatingBrush").GetComponent<ButtonSelectTool>().ResetAllStars();
						GameObject.Find ("btnBubbles").GetComponent<ButtonSelectTool>().ResetAllStars();
						starsBubbles = 0;
						starsRotatingBrush = 0;

						while(   ((bubblesHolder.childCount/5f* (starsBubbles+.5f) < bubblesCount)  && starsBubbles<4)  
						      || (starsBubbles==4 && bubblesCount == bubblesHolder.childCount) ) 
							AddStarBubbles();
					}
				}
			}
		}

		if(bubblesCount >= 1 && starsRotatingBrush>0)
		{
			starsRotatingBrush = 0;
			if( AllCompletedToolActions.Contains("07,")) AllCompletedToolActions = AllCompletedToolActions.Replace("07,","");

			GameObject.Find ("btnRotatingBrush").GetComponent<ButtonSelectTool>().ResetAllStars();
			GameObject.Find ("btnBubbles").GetComponent<ButtonSelectTool>().ResetAllStars();
			starsBubbles = 0;
//			Debug.Log("preostalo "+bubblesCount);
			while(   ((bubblesHolder.childCount/5f* (starsBubbles+.5f) < bubblesCount)  && starsBubbles<4)  
			      || (starsBubbles==4 && bubblesCount == bubblesHolder.childCount) ) 
				AddStarBubbles();
		}


		if(bubblesCount == bubblesHolder.childCount)
		{
			CancelInvoke("TestBubbles");
			Debug.Log("ALL");
		}
	}

	public void ClearBubbles()
	{
		//if(RotatingBrush.gameObject.activeSelf) return;
		if(TutorialBusCleaning.tutorialState == "rotating_brush") return;
		//SoundManager.Instance.Play_ButtonClick();
		DisableAllTools();
		 
		StartCoroutine("ShowRotatingBrush");
	}

	IEnumerator HideRotatingBrush()
	{
		StopCoroutine("ShowRotatingBrush");

		animRotatingBrush.SetBool("bClean",false);
		 
		yield return new WaitForEndOfFrame();
		RotatingBrush.gameObject.SetActive(false);
	}


	IEnumerator ShowRotatingBrush()
	{
		TutorialBusCleaning.tutorialState = "rotating_brush";
		tutorial.HidePointer();

		Vector3 pom = RotatingBrush.position;
		RotatingBrush.position = new Vector3(-1000,0,0);
		yield return new WaitForSeconds(0.1f);
		starsRotatingBrush = 0;
		RotatingBrush.gameObject.SetActive(true);
		yield return new WaitForEndOfFrame();
		animRotatingBrush.SetBool("bClean",true);
		RotatingBrush.position = pom;
		 
		GameObject.Find ("btnRotatingBrush").GetComponent<ButtonSelectTool>().ResetAllStars();
		CancelInvoke("TestBubbles");
		float timeClening = 0;

		yield return new WaitForSeconds(2f);
		CarWashSoundManager.Instance.Play_Sound(CarWashSoundManager.Instance.RotatingBrush );

		while(timeClening <12f)
		{
			float posX =   RotatingBrush.Find("rod1/rod2/part1Holder/CleaningPosition").position.x;
			yield return new WaitForSeconds(0.1f);
			timeClening+=0.1f;
			if(timeClening>2)
			{
				foreach(Transform ch in bubblesHolder)
				{
					 
					if( ch.GetComponent<Image>().enabled &&  ch.tag == "ANCHOR" && ch.transform.position.x<posX) {
						ch.GetComponent<Animator>().SetTrigger("disable");
						 
						bubblesCount--;
//						Debug.Log(bubblesCount);
					}
				}
			}
			if(starsRotatingBrush<5 &&  (starsRotatingBrush+.5f)<  (timeClening-3f )) AddStarRotatingBrush();

		}
		yield return new WaitForSeconds(1f);
		CarWashSoundManager.Instance.Stop_Sound(CarWashSoundManager.Instance.RotatingBrush );
		yield return new WaitForSeconds(.8f);
		animRotatingBrush.SetBool("bClean",false);
		yield return new WaitForEndOfFrame();
		RotatingBrush.gameObject.SetActive(false);
		bubblesCount = 0;
		TutorialBusCleaning.tutorialState = "";
	 
	}

	public void WheelBrush()
	{
		if(wheelBrush.gameObject.activeSelf) return;
		SoundManager.instance.Play_ButtonClick();
		DisableAllTools();
		wheelBrush.transform.position = Vector3.zero;
		wheelBrush.gameObject.SetActive(true);
		TutorialBusCleaning.tutorialState = "wheel_brush";
		TutorialBusCleaning.bPause = false;
		tutorial.HidePointer();
	}


	public void Fuel()
	{
		if(GasPump.gameObject.activeSelf) return;
		SoundManager.instance.Play_ButtonClick();
		GasPump.transform.  GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
		DisableAllTools();
		GasPump.gameObject.SetActive(true);
		FuelTank.gameObject.SetActive(true);
		TutorialBusCleaning.tutorialState = "fuel";
		TutorialBusCleaning.bPause = false;
		tutorial.HidePointer();
	}

	public void RESET_ALL()
	{
		StopAllCoroutines();
		//Application.LoadLevel("Gameplay");
//		LevelTransition.Instance.HideScene("Gameplay");
	}

	public void btnHomeClicked()
	{
//		GameData.TutorialOver();
		Time.timeScale = 1;
		MoveVehicle.bMoveDown = false;
		MoveVehicle.bTestDrive = false;

		DisableAllTools();

		SoundManager.instance.Play_ButtonClick();
		StopAllCoroutines();
		//Application.LoadLevel("SelectCar");
//		LevelTransition.Instance.HideScene("SelectCar");
		BlockAll.blocksRaycasts = true;
		StartCoroutine(SetBlockAll(1f,false));
//		GameData.IncrementHomeButtonClickedCount();

	}

	public void PolishCar()
	{
		if(polisher.gameObject.activeSelf) return;
		SoundManager.instance.Play_ButtonClick();
		DisableAllTools();
		polisher.position =  Vector3.zero;
		polisher.gameObject.SetActive(true);
		TutorialBusCleaning.tutorialState = "polisher";
		TutorialBusCleaning.bPause = false;
		tutorial.HidePointer();
	}


	
	
	//*******************************************************************************

	public void AddStarLeaves()
	{
		starsCleanLeaves++;
		if(starsCleanLeaves==5) { Tool5Stars();  }
		GameObject.Find ("btnCleanLeaves").GetComponent<ButtonSelectTool>().AddStar(starsCleanLeaves);
//		if(  !AllCompletedToolActions.Contains("01") || lastCompletedToolAction == 0  || (lastCompletedToolAction == 1 && starsCleanLeaves<=5) )
//		{
//			if(  !AllCompletedToolActions.Contains("01")) 
//				AllCompletedToolActions+="01,";
//
//			if(lastCompletedToolAction == 0  )
//			{
//				lastCompletedToolAction = 1;
//
//			}
//		}
//		if(TestFirstActions()) ButtonNext.SetActive(true);
//        if( starsCleanLeaves ==5 ) { HighlightNextToolButton();  }
        if( starsCleanLeaves ==5 ) 
        {
            if(  !AllCompletedToolActions.Contains("01") || lastCompletedToolAction == 0  || (lastCompletedToolAction == 1 && starsCleanLeaves<=5) )
            {
                if(  !AllCompletedToolActions.Contains("01")) 
                    AllCompletedToolActions+="01,";

                if(lastCompletedToolAction == 0  )
                {
                    lastCompletedToolAction = 1;

                }
            }
            if(TestFirstActions()) ButtonNext.SetActive(true);
            HighlightNextToolButton();  
        }
	}
 
	public void AddStarBranch()
	{
		starsCleanBranches++;
		if(starsCleanBranches==5) { Tool5Stars();  }
		GameObject.Find ("btnCutBranches").GetComponent<ButtonSelectTool>().AddStar(starsCleanBranches);
//		if(lastCompletedToolAction <= 1 || (lastCompletedToolAction == 2 && starsCleanBranches<=5) )
//		{
//			if(  !AllCompletedToolActions.Contains("02")) 
//				AllCompletedToolActions+="02,";
//			
//			if(lastCompletedToolAction == 1  )
//			{
//				lastCompletedToolAction = 2;
//
//			}
//		}
//		if(TestFirstActions()) ButtonNext.SetActive(true);
//        if( starsCleanBranches ==5 ) { HighlightNextToolButton(); }
        if( starsCleanBranches ==5 ) 
        {
            if(lastCompletedToolAction <= 1 || (lastCompletedToolAction == 2 && starsCleanBranches<=5) )
            {
                if(  !AllCompletedToolActions.Contains("02")) 
                    AllCompletedToolActions+="02,";

                if(lastCompletedToolAction == 1  )
                {
                    lastCompletedToolAction = 2;

                }
            }
            if(TestFirstActions()) ButtonNext.SetActive(true);
            HighlightNextToolButton(); 
        }
	}

	public void AddStarDirt()
	{
		starsCleanDirt++;
		if(starsCleanDirt==5) { Tool5Stars();  }
		GameObject.Find ("btnCleanDirt").GetComponent<ButtonSelectTool>().AddStar(starsCleanDirt);
//		if( lastCompletedToolAction <= 2 || (lastCompletedToolAction == 3 && starsCleanDirt<=5) )
//		{
//			if(  !AllCompletedToolActions.Contains("03")) 
//				AllCompletedToolActions+="03,";
//			
//			if(lastCompletedToolAction == 2  )
//			{
//				lastCompletedToolAction = 3;
//
//			}
//		}
//		if(TestFirstActions()) ButtonNext.SetActive(true);
//        if(starsCleanDirt ==5 ) { HighlightNextToolButton();  }
        if(starsCleanDirt ==5 )
        {
            if( lastCompletedToolAction <= 2 || (lastCompletedToolAction == 3 && starsCleanDirt<=5) )
            {
                if(  !AllCompletedToolActions.Contains("03")) 
                    AllCompletedToolActions+="03,";

                if(lastCompletedToolAction == 2  )
                {
                    lastCompletedToolAction = 3;

                }
            }
            if(TestFirstActions()) ButtonNext.SetActive(true);
            HighlightNextToolButton();  
        }
	}

	public void AddStarStains()
	{
		starsCleanStains++;
		if(starsCleanStains==5) { Tool5Stars();  }
		GameObject.Find ("btnCleanStains").GetComponent<ButtonSelectTool>().AddStar(starsCleanStains);
//		if(lastCompletedToolAction <= 3  || (lastCompletedToolAction == 4 && starsCleanStains<=5) )
//		{
//
//			if(  !AllCompletedToolActions.Contains("04")) 
//				AllCompletedToolActions+="04,";
//			
//			if(lastCompletedToolAction == 3  )
//			{
//				lastCompletedToolAction = 4;
//
//			}
//		}
//		if(TestFirstActions()) ButtonNext.SetActive(true);
//        if(starsCleanStains ==5 ){ HighlightNextToolButton(); }
        if(starsCleanStains ==5 )
        {
            if(lastCompletedToolAction <= 3  || (lastCompletedToolAction == 4 && starsCleanStains<=5) )
            {

                if(  !AllCompletedToolActions.Contains("04")) 
                    AllCompletedToolActions+="04,";

                if(lastCompletedToolAction == 3  )
                {
                    lastCompletedToolAction = 4;

                }
            }
            if(TestFirstActions()) ButtonNext.SetActive(true);
            HighlightNextToolButton(); 
        }
	}

	public void AddStarWheelBrush(int wheelNo, int whellStatus)
	{
		 
		if( !bStartWheelBrush || 
		   ( MoveVehicle.wheelsCount == 2 && wheelNo ==1 && (whellStatus == 4 || whellStatus == 7) )  ||  
		   ( MoveVehicle.wheelsCount == 2 && wheelNo ==2 && (whellStatus == 4 || whellStatus == 7) )     
			||
		   ( MoveVehicle.wheelsCount == 3 && wheelNo == 1 && ( whellStatus == 4 || whellStatus == 7) )  ||  
		   (MoveVehicle. wheelsCount == 3 && wheelNo == 2 && (  whellStatus == 7) )  ||  
		   ( MoveVehicle.wheelsCount == 3 && wheelNo == 3 && (  whellStatus == 7) )   )   
				
		{
			bStartWheelBrush =true;
			starsCleanWheelBrush++;
			if(starsCleanWheelBrush==5) { Tool5Stars();  }
			GameObject.Find ("btnWheelBrush").GetComponent<ButtonSelectTool>().AddStar(starsCleanWheelBrush);
//			if(  lastCompletedToolAction <= 4   || (lastCompletedToolAction == 5 && starsCleanWheelBrush<=5) )
//			{
//				if(  !AllCompletedToolActions.Contains("05")) 
//					AllCompletedToolActions+="05,";
//				
//				if(lastCompletedToolAction == 4 )
//				{
//					lastCompletedToolAction = 5;
//
//				}
//			}
//			if(TestFirstActions()) ButtonNext.SetActive(true);
//            if( starsCleanWheelBrush ==5 ){ HighlightNextToolButton();  }
            if( starsCleanWheelBrush ==5 )
            {
                bStartWheelBrush =true;
                starsCleanWheelBrush++;
                if(starsCleanWheelBrush==5) { Tool5Stars();  }
                GameObject.Find ("btnWheelBrush").GetComponent<ButtonSelectTool>().AddStar(starsCleanWheelBrush);
                if(  lastCompletedToolAction <= 4   || (lastCompletedToolAction == 5 && starsCleanWheelBrush<=5) )
                {
                    if(  !AllCompletedToolActions.Contains("05")) 
                        AllCompletedToolActions+="05,";

                    if(lastCompletedToolAction == 4 )
                    {
                        lastCompletedToolAction = 5;

                    }
                }
                if(TestFirstActions()) ButtonNext.SetActive(true);
                HighlightNextToolButton(); 
            }
		}
	}

	public void  AddStarBubbles()
	{
		starsBubbles++;
		if(starsBubbles==5) { Tool5Stars();  }
		GameObject.Find ("btnBubbles").GetComponent<ButtonSelectTool>().AddStar(starsBubbles);
//		if(lastCompletedToolAction <= 5  || (lastCompletedToolAction == 6 && starsBubbles<=5) )
//		{
//			if(  !AllCompletedToolActions.Contains("06")) 
//				AllCompletedToolActions+="06,";
//			
//			if(lastCompletedToolAction == 5  )
//			{
//				lastCompletedToolAction = 6;
//
//				if(TestFirstActions()) ButtonNext.SetActive(true);
//			}
//
//
//		}
//		if(TestFirstActions()) ButtonNext.SetActive(true);
//      if(starsBubbles ==5 ) { HighlightNextToolButton(); }
        if(starsBubbles ==5 ) 
        {
            if(lastCompletedToolAction <= 5  || (lastCompletedToolAction == 6 && starsBubbles<=5) )
            {
                if(  !AllCompletedToolActions.Contains("05")) 
                    AllCompletedToolActions+="05,";

                if(lastCompletedToolAction == 5  )
                {
                    lastCompletedToolAction = 6;

                    if(TestFirstActions()) ButtonNext.SetActive(true);
                }


            }
            if(TestFirstActions()) ButtonNext.SetActive(true);
            HighlightNextToolButton(); 
        }
	}

	public void  AddStarRotatingBrush()
	{
		starsRotatingBrush++;
		//if(starsRotatingBrush == 5) { Tool5Stars();  }
		GameObject.Find ("btnRotatingBrush").GetComponent<ButtonSelectTool>().AddStar(starsRotatingBrush);
//		if( lastCompletedToolAction <= 6 || (lastCompletedToolAction == 7 && starsRotatingBrush<=5) )
//		{
//			if(  !AllCompletedToolActions.Contains("07")) 
//				AllCompletedToolActions+="07,";
//			
//			if(lastCompletedToolAction == 6  )
//			{
//				lastCompletedToolAction = 7;
//
//			}
//		}
//		if(TestFirstActions()) ButtonNext.SetActive(true);
//        if( starsRotatingBrush ==5 ) { HighlightNextToolButton();  }
		if( starsRotatingBrush ==5 ) 
        {
            if( lastCompletedToolAction <= 6 || (lastCompletedToolAction == 7 && starsRotatingBrush<=5) )
            {
                if(  !AllCompletedToolActions.Contains("06")) 
                    AllCompletedToolActions+="06,";

                if(lastCompletedToolAction == 6  )
                {
                    lastCompletedToolAction = 7;

                }
            }
            if(TestFirstActions()) ButtonNext.SetActive(true);
            HighlightNextToolButton();  
        }
	}

	bool TestFirstActions() // da li je obavljen prvi skup akcija za ciscenje
	{
		 
		for(int i = 1;i<=6;i++)
		{
			if( RemovedToolsList.Contains(i.ToString().PadLeft(2,'0'))) continue;
			if( !AllCompletedToolActions.Contains(i.ToString().PadLeft(2,'0'))  ) return false;

			lastCompletedToolAction = i;
		}
		return true;
	}

	bool TestSecondActions() // da li je obavljen drgi skup akcija za ciscenje (donja garaza)
	{
		
		for(int i = 7;i<=9;i++)
		{
			if( RemovedToolsList.Contains(i.ToString().PadLeft(2,'0'))) continue;
			if( !AllCompletedToolActions.Contains(i.ToString().PadLeft(2,'0'))  ) return false;
			
			lastCompletedToolAction = i;
		}
		return true;
	}

//*********************************************************
	public void  AddStarWaxCar()
	{
		starsWaxCar++;
		if(starsWaxCar==5) { Tool5Stars();  }
		GameObject.Find ("btnWaxCar").GetComponent<ButtonSelectTool>().AddStar(starsWaxCar);
		if( lastCompletedToolAction <= 7 || (lastCompletedToolAction == 8 && starsWaxCar<=5) )
		{
			if(  !AllCompletedToolActions.Contains("07")) 
				AllCompletedToolActions+="07,";
			if(lastCompletedToolAction == 7  )
			{
				lastCompletedToolAction = 8;
			}

		}
		if( starsWaxCar ==5 ) { HighlightNextToolButton(); }

		if(starsPolishCar>0) 
		{
			starsPolishCar = 0;
			GameObject.Find ("btnPolishCar").GetComponent<ButtonSelectTool>().ResetAllStars();
		}
		 
	}

	public void  AddStarPolishCar()
	{
//		starsPolishCar++;
//		if(starsPolishCar==5) { Tool5Stars();  }
//		GameObject.Find ("btnPolishCar").GetComponent<ButtonSelectTool>().AddStar(starsPolishCar);
//		if( lastCompletedToolAction <= 8 || (lastCompletedToolAction == 9 && starsPolishCar<=5) )
//		{
//			if(  !AllCompletedToolActions.Contains("09")) 
//				AllCompletedToolActions+="09,";
//			 
//			if(lastCompletedToolAction == 8  )
//			{
//				lastCompletedToolAction = 9;
//			}
//
//		}
//		if( starsPolishCar ==5 )
//        {
//            HighlightNextToolButton();
//            if(TestSecondActions())
//                AllItemsDone();
//        }
        starsPolishCar++;
        GameObject.Find ("btnPolishCar").GetComponent<ButtonSelectTool>().AddStar(starsPolishCar);
        if(starsPolishCar==5)
        {
            Tool5Stars(); 
            if( lastCompletedToolAction <= 8 || (lastCompletedToolAction == 9 && starsPolishCar<=5) )
            {
                if(  !AllCompletedToolActions.Contains("08")) 
                    AllCompletedToolActions+="08,";
                
                if(lastCompletedToolAction == 8  )
                {
                    lastCompletedToolAction = 9;
                }
                
            }
            HighlightNextToolButton();
            if(TestSecondActions())
                AllItemsDone();
        }
	}

	public void  AddStarInflateTires(int wheelNo, int whellStatus)
	{
		if( !bStartInflatation || 
			( MoveVehicle.wheelsCount == 2 && wheelNo ==1 && (whellStatus == 4 || whellStatus == 8) )  ||  
				( MoveVehicle.wheelsCount == 2 && wheelNo ==2 && (whellStatus == 4 || whellStatus == 8) )     
				||
				( MoveVehicle.wheelsCount == 3 && wheelNo == 1 && ( whellStatus == 4 || whellStatus == 8) )  ||  
				(MoveVehicle. wheelsCount == 3 && wheelNo == 2 && (  whellStatus == 8) )  ||  
				( MoveVehicle.wheelsCount == 3 && wheelNo == 3 && (  whellStatus == 8) )   )   
		{
			bStartInflatation =true;
			starsInflateTires++;
//			if(starsInflateTires==5) { Tool5Stars();  }
//			GameObject.Find ("btnInflateTires").GetComponent<ButtonSelectTool>().AddStar(starsInflateTires);
//			if( lastCompletedToolAction <= 9 || (lastCompletedToolAction == 10 && starsInflateTires<=5) )
//			{
//				if(  !AllCompletedToolActions.Contains("10")) 
//					AllCompletedToolActions+="10,";
//			 
//				if(lastCompletedToolAction == 9  )
//				{
//					lastCompletedToolAction = 10;
//				}
//			}
//			if( starsInflateTires ==5 ) 
//            {
//                HighlightNextToolButton();  
//                if(TestSecondActions())
//                    AllItemsDone();
//            } 
            GameObject.Find ("btnInflateTires").GetComponent<ButtonSelectTool>().AddStar(starsInflateTires);
            if(starsInflateTires==5) 
            { 
                Tool5Stars();
                if( lastCompletedToolAction <= 9 || (lastCompletedToolAction == 10 && starsInflateTires<=5) )
                {
                    if(  !AllCompletedToolActions.Contains("10")) 
                        AllCompletedToolActions+="10,";
                    
                    if(lastCompletedToolAction == 9  )
                    {
                        lastCompletedToolAction = 10;
                    }
                }
                HighlightNextToolButton();  
                if(TestSecondActions())
                    AllItemsDone();
            }
        }
	}

	public void  AddStarFuel()
	{

//		starsFuel++;
//		if(starsFuel==5) { Tool5Stars();  }
//		GameObject.Find ("btnFuel").GetComponent<ButtonSelectTool>().AddStar(starsFuel);
//		if( lastCompletedToolAction <= 10 || (lastCompletedToolAction == 11 && starsFuel<=5) )
//		{
//			if(  !AllCompletedToolActions.Contains("11")) 
//				AllCompletedToolActions+="11,";
//			 
//			if(lastCompletedToolAction == 10  )
//			{
//				lastCompletedToolAction = 11;
//			}
//            if (TestSecondActions())
//                AllItemsDone();
//		}
//	 
//		if( starsFuel ==5 ) { HighlightNextToolButton(); }

        starsFuel++;
        GameObject.Find ("btnFuel").GetComponent<ButtonSelectTool>().AddStar(starsFuel);
        if(starsFuel==5) 
        { 
            Tool5Stars();  
            if( lastCompletedToolAction <= 10 || (lastCompletedToolAction == 11 && starsFuel<=5) )
            {
                if(  !AllCompletedToolActions.Contains("09")) 
                    AllCompletedToolActions+="09,";
                
                if(lastCompletedToolAction == 10  )
                {
                    lastCompletedToolAction = 11;
                }
                if (TestSecondActions())
                    AllItemsDone();
            }
            HighlightNextToolButton(); 
        }

	}
	

	void Tool5Stars()
	{
        if (leafBlower.gameObject.activeSelf)
        {
            psDisableTool.transform.position = leafBlower.position;
            if (GameObject.Find("btnCleanLeaves") != null)
                GameObject.Find("btnCleanLeaves").GetComponent<Button>().enabled = false;
        }
        else if (shears.gameObject.activeSelf)
        {
            psDisableTool.transform.position = shears.position;

            if (GameObject.Find("btnCutBranches") != null)
                GameObject.Find("btnCutBranches").GetComponent<Button>().enabled = false;
        }
        //		else if(wheelBrush.gameObject.activeSelf) psDisableTool.transform.position = wheelBrush.position;
        else if (sponge.gameObject.activeSelf)
        {
            psDisableTool.transform.position = sponge.position;
            if (GameObject.Find("btnBubbles") != null)
                GameObject.Find("btnBubbles").GetComponent<Button>().enabled = false;
        }

        else if (polisher.gameObject.activeSelf)
        {
            psDisableTool.transform.position = polisher.position;
            if (GameObject.Find("btnPolishCar") != null)
                GameObject.Find("btnPolishCar").GetComponent<Button>().enabled = false;
        }
        else if (cleningCloth.gameObject.activeSelf)
        {
            psDisableTool.transform.position = cleningCloth.position;
            if (GameObject.Find("btnWaxCar") != null)
                GameObject.Find("btnWaxCar").GetComponent<Button>().enabled = false;
        }
        else if (GasPump.gameObject.activeSelf)
        {
            psDisableTool.transform.position = GasPump.position;
            if (GameObject.Find("btnFuel") != null)
                GameObject.Find("btnFuel").GetComponent<Button>().enabled = false;
        }
        else if (waterGun.transform.gameObject.activeSelf)
        {
            psDisableTool.transform.position = waterGun.transform.position;
            if (GameObject.Find("btnCleanStains") != null)
                GameObject.Find("btnCleanStains").GetComponent<Button>().enabled = false;
        }
        else if (waterHose.transform.gameObject.activeSelf)
        {
            psDisableTool.transform.position = waterHose.transform.position;
            if (GameObject.Find("btnCleanDirt") != null)
                GameObject.Find("btnCleanDirt").GetComponent<Button>().enabled = false;
        }


		CarWashSoundManager.Instance.Play_Sound(CarWashSoundManager.Instance.Correct);
		DisableAllTools();
		 
		psDisableTool.Stop();
		psDisableTool.Play();
	}
	 

	void HighlightNextToolButton()
	{

		tutorial.HidePointer();
//		if(GameData.TutorialShown == 0) Tutorial.timeWaitToShowHelp = 1f;
		nextTool = 0;//   lastCompletedToolAction;
		bool bPronadjeno= false;
		while(!bPronadjeno)
		{
			nextTool ++;
			if(!RemovedToolsList.Contains (nextTool.ToString().PadLeft(2,'0')) && !AllCompletedToolActions.Contains (nextTool.ToString().PadLeft(2,'0') ) ) bPronadjeno = true;
		}
		if(lastCompletedToolAction == 11) nextTool = 12;
		 
		Animator animGlowButton = null;
		if(nextTool <12)
		{

			switch(nextTool)
			{
			case 1:
				if(GameObject.Find ("btnCleanLeaves") !=null) animGlowButton = 	GameObject.Find ("btnCleanLeaves").GetComponent<Animator>();
				break;
			case 2:
				if(GameObject.Find ("btnCutBranches") !=null) animGlowButton = GameObject.Find ("btnCutBranches").GetComponent<Animator>();
				break;
			case 3:
				if(GameObject.Find ("btnCleanDirt") !=null) animGlowButton = GameObject.Find ("btnCleanDirt").GetComponent<Animator>();
				break;
			case 4:
				if(GameObject.Find ("btnCleanStains") !=null) animGlowButton = GameObject.Find ("btnCleanStains").GetComponent<Animator>();
				break;	
			case 5:
				if(GameObject.Find ("btnWheelBrush") !=null) animGlowButton = GameObject.Find ("btnWheelBrush").GetComponent<Animator>();
				break;
			case 6:
				if(GameObject.Find ("btnBubbles") !=null) animGlowButton = GameObject.Find ("btnBubbles").GetComponent<Animator>();
				break;
			case 7:
				if(GameObject.Find ("btnRotatingBrush") !=null)  animGlowButton = GameObject.Find ("btnRotatingBrush").GetComponent<Animator>();
				break;
				//case 8: se aktivira na next button
			case 8:
				if(GameObject.Find ("btnWaxCar") !=null) animGlowButton = GameObject.Find ("btnWaxCar").GetComponent<Animator>();
				break;
			case 9:
					if(GameObject.Find ("btnPolishCar") !=null) animGlowButton = GameObject.Find ("btnPolishCar").GetComponent<Animator>();
				break;
			case 10:
				if(GameObject.Find ("btnInflateTires") !=null) animGlowButton = GameObject.Find ("btnInflateTires").GetComponent<Animator>();
				break;
			case 11:
				if(GameObject.Find ("btnFuel") !=null) animGlowButton = GameObject.Find ("btnFuel").GetComponent<Animator>();
				break;
				
			}
		}
		else
		{
			TutorialBusCleaning.tutorialState = "";
			TutorialBusCleaning.bPause = true;
			TutorialBusCleaning.timeWaitToShowHelp = TutorialBusCleaning.PeriodToShowHelp;
		}
		if(animGlowButton !=null) StartCoroutine("ActivateGlowAnim",animGlowButton);

	}
	IEnumerator ActivateGlowAnim(Animator anim)
	{
		yield return new WaitForSeconds(1);
		anim.SetTrigger("Glow");

	}

	public void btnNextClicked()
	{
		SoundManager.instance.Play_ButtonClick();
		DisableAllTools();
		ButtonNext.SetActive(false);
		ToolsButtonsHolder1.SetActive(false);
//        ToolsButtonsHolder2.SetActive(false);
        ToolsButtonsHolder2.SetActive(true);

		tutorial.HidePointer();
		TutorialBusCleaning.timeWaitToShowHelp = 100000;

//		StartCoroutine("NextGarage");
//		BlockAll.blocksRaycasts = true;
//		StartCoroutine(SetBlockAll(3f,false));
	}

	IEnumerator NextGarage()
	{
		SoundManager.instance.Play_ButtonClick();
		yield return null;
		if (lastCompletedToolAction <=7) 
		{ 



			MoveVehicle.bMoveDown = true;
 			yield return new WaitForSeconds( MoveVehicle.MoveDownTime*2 + 2);

			ToolsButtonsHolder1.SetActive(false);
			ToolsButtonsHolder2.SetActive(true);
			nextTool = 8;
			StartCoroutine("NextToolsButtons");
		}
		else if  (lastCompletedToolAction >7)
		{
//			GameData.TutorialOver();
//			GameData.IncrementCarCompletedCount();
//			 StartCoroutine("DriveCar");
		} 
	}
	IEnumerator NextToolsButtons()
	{
		yield return new WaitForSeconds(.5f);
//		Animator animGlowButton = GameObject.Find ("btnWaxCar").GetComponent<Animator>();
//		if(animGlowButton !=null) StartCoroutine("ActivateGlowAnim",animGlowButton);
		nextTool = 7;
		HighlightNextToolButton();

		TutorialBusCleaning.timeWaitToShowHelp = TutorialBusCleaning.PeriodToShowHelp;
		TutorialBusCleaning.tutorialState = "";
		TutorialBusCleaning.bPause = true;
	
	}

//	IEnumerator DriveCar()
//	{
//		 
////		GameData.CollectedStars =  starsCleanLeaves + starsCleanBranches + starsCleanDirt + 
////			starsCleanStains + starsCleanWheelBrush + starsBubbles + starsRotatingBrush +
////				starsWaxCar + starsPolishCar + starsInflateTires + starsFuel;
////
////		GameData.TotalStars  += GameData.CollectedStars;
////
////		GameData.SetStarsToPP();
////		GameData.GetStarsFromPP();
//		CarWashSoundManager.Instance.Play_Sound(CarWashSoundManager.Instance.CarStart);
//		yield return new WaitForSeconds(.5f);
//
//		Animator animGarageDoor = GameObject.Find ("GarageDoorExit").GetComponent<Animator>();
//		GameObject.Find ("psExhaustSmoke").GetComponent<ParticleSystem>().enableEmission = true;
//		if(ItemsSlider.ActiveItemNo == 22) GameObject.Find ("psExhaustSmoke2").GetComponent<ParticleSystem>().enableEmission = true;
//		if(animGarageDoor !=null) animGarageDoor.SetTrigger("tOpen");
//		yield return new WaitForSeconds(.5f);
//
//		MoveVehicle.bTestDrive = true;
//		yield return new WaitForSeconds( 5f);
//		btnGasPedal.SetActive(true);
//		SpeedMeter.SetActive(true);
//	}
	 
	IEnumerator SetBlockAll(float time, bool blockRays)
	{
		if(BlockAll == null) BlockAll = GameObject.Find("BlockAll").GetComponent<CanvasGroup>();
		yield return new WaitForSeconds(time);
		BlockAll.blocksRaycasts = blockRays;
		
	}
    #region EndGameMethods
    void AllItemsDone()
    {
        Debug.Log("Game finished!");

        CarWashFinished();
    }

    public void CarWashFinished()
    {
        Debug.Log("BUS WASH FINISHED");
        ToolsButtonsHolder1.SetActive(false);
        ToolsButtonsHolder2.SetActive(false);
        ButtonFinish.SetActive(true);
    }
    #endregion
}
