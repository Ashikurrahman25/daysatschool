using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TutorialBusCleaning : MonoBehaviour {
	public static bool bTutorial = false;
	public static bool bPause = false;
	public static string tutorialState = "";

	string tutorialStateOld = "";
	public Animator animTutorial;
	Vector3 startPos = Vector3.zero;
	Vector3 endPos = Vector3.zero;

	public static float timeWaitToShowHelp = 3;
	public static float PeriodToShowHelp = 3;
	bool bHidden = true;
	//bool bTutButtons = true;


	void Start () {
		bPause = false;
		if(Application.loadedLevelName == "SelectCar" )
		{
			animTutorial = transform.GetComponent<Animator>();
			tutorialState = "select_car";
			 

// 			if(  GameData.TutorialShown == 0)  
// 			{
//				PeriodToShowHelp = 1;
//				timeWaitToShowHelp = 1;
// 			}
//			else 
//			{
				PeriodToShowHelp = 8;
				timeWaitToShowHelp = 8;
//			}
		 
			InvokeRepeating("TestHelpPointerSelectCar",0.1f,0.1f);
		}
 
		if(Application.loadedLevelName == "SchoolBusCleanUpScene" )
		{
			bPause = true;

//			if(GameData.TutorialShown == 0) 
//			{
//				timeWaitToShowHelp =5;
//				PeriodToShowHelp = 2.5f;
//			}
//			else  
//			{
//				timeWaitToShowHelp =10;
//				PeriodToShowHelp = 10;
//			}

			animTutorial = transform.GetComponent<Animator>();
			tutorialState = "";

			InvokeRepeating("TestHelpPointerGameplay",0.1f,0.1f);
		}
	}



	void TestHelpPointerGameplay()
	{
//		Debug.Log(MenuManager.activeMenu + "  " + tutorialState + "   "+ PeriodToShowHelp + "  " +  timeWaitToShowHelp);
        if(/*MenuManager.activeMenu == ""   && */ !bPause)
		{
			if(bHidden) 
			{
				 
				transform.rotation = Quaternion.identity;
				timeWaitToShowHelp -=.1f;
				if(timeWaitToShowHelp <=0)
				{
					StopAllCoroutines();
					timeWaitToShowHelp = PeriodToShowHelp;
					SwitchTutorialState();
				}
			}
		 
			 
		}
		else if(tutorialState == "")
		{
			//Debug.Log("TEST BUTTONS");
			timeWaitToShowHelp -=.1f;
			if(timeWaitToShowHelp <=0)
			{
				timeWaitToShowHelp =  PeriodToShowHelp;

				
				if( bHidden &&   tutorialStateOld  == "" &&  tutorialState =="" &&  CarWashLevelManager.Instance.nextTool >0 && CarWashLevelManager.Instance.nextTool<13 )
				{
					//Debug.Log("POKAZI DUGME");
					transform.rotation = Quaternion.Euler(0,0,120);
					if(CarWashLevelManager.Instance.nextTool == 1  && GameObject.Find("btnCleanLeaves")!=null )
							transform.position  = GameObject.Find("btnCleanLeaves").transform.position + new Vector3(.5f, 1f,0);
					else if(CarWashLevelManager.Instance.nextTool == 2  && GameObject.Find("btnCutBranches")!=null )
							transform.position = GameObject.Find("btnCutBranches").transform.position + new Vector3(.5f, 1f,0);
					else if(CarWashLevelManager.Instance.nextTool == 3  && GameObject.Find("btnCleanDirt")!=null )
							transform.position = GameObject.Find("btnCleanDirt").transform.position + new Vector3(.5f, 1f,0);
					else if(CarWashLevelManager.Instance.nextTool == 4  && GameObject.Find("btnCleanStains")!=null )
							transform.position = GameObject.Find("btnCleanStains").transform.position + new Vector3(.5f, 1f,0);
					else if(CarWashLevelManager.Instance.nextTool == 5  && GameObject.Find("btnWheelBrush")!=null )
							transform.position = GameObject.Find("btnWheelBrush").transform.position + new Vector3(.5f, 1f,0);
					else if(CarWashLevelManager.Instance.nextTool == 6  && GameObject.Find("btnBubbles")!=null )
							transform.position = GameObject.Find("btnBubbles").transform.position + new Vector3(.5f, 1f,0);

					else if(CarWashLevelManager.Instance.nextTool == 7  && GameObject.Find("btnRotatingBrush")!=null)
					{
							transform.position = GameObject.Find("btnRotatingBrush").transform.position + new Vector3(.5f, 1f,0);
					}

				//!!
					else if(CarWashLevelManager.Instance.nextTool == 8 )
					{
						if(GameObject.Find("btnWaxCar")!=null)
							transform.position = GameObject.Find("btnWaxCar").transform.position + new Vector3(.5f, 1f,0);

						else 	if(GameObject.Find("btnNext")!=null)
							transform.position = GameObject.Find("btnNext").transform.position + new Vector3(0.2f, 1f,0);
					}
					else if(CarWashLevelManager.Instance.nextTool == 9 && GameObject.Find("btnPolishCar")!=null )
							transform.position = GameObject.Find("btnPolishCar").transform.position + new Vector3(.5f, 1f,0);
					else if(CarWashLevelManager.Instance.nextTool == 10 && GameObject.Find("btnInflateTires")!=null )
							transform.position = GameObject.Find("btnInflateTires").transform.position + new Vector3(.5f, 1f,0);
					else if(CarWashLevelManager.Instance.nextTool == 11 && GameObject.Find("btnFuel")!=null )
										transform.position = GameObject.Find("btnFuel").transform.position + new Vector3(.5f,1f,0);

					else if(CarWashLevelManager.Instance.nextTool == 12 && GameObject.Find("btnNext")!=null )
					{
						transform.position = GameObject.Find("btnNext").transform.position + new Vector3(0.2f, 1f,0);
						//transform.localScale = new Vector3(-1,1,1);
					}
					else 
					{
						HidePointer();
						return;
					}
					startPos = transform.position;
					endPos = transform.position;
					StartCoroutine ("StartPointingAndHide");
					tutorialState = "buttons";
				}




			}
		}
		else if (tutorialState != "buttons")
		{
				 HidePointer();
		}
		tutorialStateOld = tutorialState;
	}
 
	void SwitchTutorialState() 
	{

		switch(tutorialState)
		{

			case "clean_leaves":
			if( GameObject.Find("TutPosStart_clean_leaves")!=null)
			{
				startPos = GameObject.Find("TutPosStart_clean_leaves").transform.position;
				LeafBlower lb = GameObject.Find("Tools/LeafBlower").GetComponent<LeafBlower>();
				if(lb.bLeaves1)  endPos = lb.psLeaves1.transform.position;
				else if(lb.bLeaves2)  endPos = lb.psLeaves2.transform.position;
				else if(lb.bLeaves3)  endPos = lb.psLeaves3.transform.position;
				else if(lb.bLeaves4)  endPos = lb.psLeaves4.transform.position;
				else if(lb.bLeaves5)  endPos = lb.psLeaves5.transform.position;
				StartPointing();
				StartCoroutine( "MovePoinnter" );
			}
			break;
			
		case "cut_branches":
			if( GameObject.Find("TutPosStart_cut_branches")!=null)
			{
				startPos = GameObject.Find("TutPosStart_cut_branches").transform.position;
				Shears sh = GameObject.Find("Tools/Shears").GetComponent<Shears>();
				
				string[] branchNames = sh.BranchesToCut.Split(new char[] {','} ,System.StringSplitOptions.RemoveEmptyEntries);
				if(branchNames.Length>0)
				{
					int ind = Mathf.FloorToInt( Random.Range(0,branchNames.Length));
					if(GameObject.Find(branchNames[ind])!=null)
					{
						endPos = GameObject.Find(branchNames[ind]).transform.position;
						StartPointing();
						StartCoroutine( "MovePoinnter" );
					}
				}
			}
			break;
			
		case "clean_dirt":
			if( GameObject.Find("TutPosStart_clean_dirt")!=null)
			{
				startPos = GameObject.Find("TutPosStart_clean_dirt").transform.position;
				WaterHose wh = GameObject.Find("Tools2/WaterHose").GetComponent<WaterHose>();
				
				string[] names = wh.LeftToBeCleaned.Split(new char[] {','} ,System.StringSplitOptions.RemoveEmptyEntries);
				if(names.Length>0)
				{
					int ind = Mathf.FloorToInt( Random.Range(0,names.Length));
					if(GameObject.Find(names[ind])!=null)
					{
						endPos = GameObject.Find(names[ind]).transform.position;
						transform.position = startPos;
						StartPointing();
						StartCoroutine( "MovePoinnter" );
					}
				}
			}
			break;
			
			
		case "clean_stains":
			if( GameObject.Find("TutPosStart_clean_stains")!=null)
			{
				startPos = GameObject.Find("TutPosStart_clean_stains").transform.position;
				WaterGun wg = GameObject.Find("Tools2/WaterGun").GetComponent<WaterGun>();
				
				string[] names = wg.LeftToBeCleaned.Split(new char[] {','},System.StringSplitOptions.RemoveEmptyEntries);
				if(names.Length>0)
				{
					int ind = Mathf.FloorToInt( Random.Range(0,names.Length));
					if(GameObject.Find(names[ind])!=null)
					{
						endPos = GameObject.Find(names[ind]).transform.position + new Vector3(.5f,-.5f,0);
						transform.position = startPos;
						StartPointing();
						StartCoroutine( "MovePoinnter" );
					}
				}
			}
			break;
			
			
		case "wheel_brush":
			if( GameObject.Find("TutPosStart_wheel_brush")!=null)
			{
				startPos = GameObject.Find("TutPosStart_wheel_brush").transform.position;
				WheelBrush wb = GameObject.Find("Tools/WheelBrush").GetComponent<WheelBrush>();
				
				string[] names = wb.LeftToBeCleaned.Split(new char[] {','},System.StringSplitOptions.RemoveEmptyEntries);
				if(names.Length>0)
				{
					int ind = Mathf.FloorToInt( Random.Range(0,names.Length));
					if(GameObject.Find(names[ind])!=null)
					{
						endPos = GameObject.Find(names[ind]).transform.position + new Vector3(.5f,-.5f,0);
						transform.position = startPos;
						StartPointing();
						StartCoroutine( "MovePoinnter" );
					}
				}
			}
			break;
			
		case "bubbles":
			if( GameObject.Find("TutPosStart_bubbles")!=null)
			{
				startPos = GameObject.Find("TutPosStart_bubbles").transform.position;
				StartCoroutine( "MovePoinnter_Bubbles" );
			}
			break;
			
		case "wax_car":
			if( GameObject.Find("TutPosStart_wax_car")!=null)
			{
				startPos = GameObject.Find("TutPosStart_wax_car").transform.position;
				CleaningCloth cc = GameObject.Find("Tools/CleaningCloth").GetComponent<CleaningCloth>();
				
				string[] names = cc.LeftToBeCleaned.Split(new char[] {','},System.StringSplitOptions.RemoveEmptyEntries);
				if(names.Length>0)
				{
					int ind = Mathf.FloorToInt( Random.Range(0,names.Length));
					if(GameObject.Find(names[ind])!=null)
					{
						endPos = GameObject.Find(names[ind]).transform.position + new Vector3(.5f,-.5f,0);
						transform.position = startPos;
						StartPointing();
						StartCoroutine( "MovePoinnter" );
					}
				}
			}
			break;
			
		case "polisher":
			if( GameObject.Find("TutPosStart_polisher")!=null)
			{
				startPos = GameObject.Find("TutPosStart_polisher").transform.position;
				Polisher pol = GameObject.Find("Tools/Polisher").GetComponent<Polisher>();
				
				string[] names = pol.LeftToBeCleaned.Split(new char[] {','},System.StringSplitOptions.RemoveEmptyEntries);
				if(names.Length>0)
				{
					int ind = Mathf.FloorToInt( Random.Range(0,names.Length));
					if(GameObject.Find(names[ind])!=null)
					{
						endPos = GameObject.Find(names[ind]).transform.position + new Vector3(.5f,-.5f,0);
						transform.position = startPos;
						StartPointing();
						StartCoroutine( "MovePoinnter" );
					}
				}
			}
			break;
			
		case "inflate_tires":
			if( GameObject.Find("TutPosStart_inflate_tires")!=null)
			{
				startPos = GameObject.Find("TutPosStart_inflate_tires").transform.position;
				TirePump tp = GameObject.Find("Tools/TirePump").GetComponent<TirePump>();
				
				string[] names = tp.LeftToBeCleaned.Split(new char[] {','},System.StringSplitOptions.RemoveEmptyEntries);
				if(names.Length>0)
				{
					int ind = Mathf.FloorToInt( Random.Range(0,names.Length));
					if(GameObject.Find(names[ind]+"/Valwe")!=null)
					{
						endPos = GameObject.Find(names[ind]+"/Valwe").transform.position + new Vector3(.5f,-.5f,0);
						transform.position = startPos;
						StartPointing();
						StartCoroutine( "MovePoinnter" );
					}
				}
			}
			break;
			
			
		case "fuel":
			if( GameObject.Find("TutPosStart_fuel")!=null)
			{
				startPos = GameObject.Find("TutPosStart_fuel").transform.position;
				endPos = GameObject.Find("FuelTank").transform.position + new Vector3(.5f,-.5f,0);
				transform.position = startPos;
				StartPointing();
				StartCoroutine( "MovePoinnter" );
			}
			break;
			
			
		default : break;
		}

	}


	/*
	void TestHelpPointerGameplay()
	{
		if(!bHidden && Application.loadedLevelName == "Gameplay" ) return;
         Debug.Log(MenuManager.activeMenu + "  " + tutorialState + "  th"+ PeriodToShowHelp);
		if(MenuManager.activeMenu == "" && !bPause)
		{
			timeWaitToShowHelp -=Time.deltaTime;
			if(timeWaitToShowHelp <=0)
			{
				timeWaitToShowHelp = PeriodToShowHelp;
				switch(tutorialState)
				{
					case "select_car":
						
						transform.position = new Vector3(0,-1,0);
						StartCoroutine( "StartPointingAndHide" );
					break;


					case "clean_leaves":
					if( GameObject.Find("TutPosStart_clean_leaves")!=null)
					{
						startPos = GameObject.Find("TutPosStart_clean_leaves").transform.position;
						LeafBlower lb = GameObject.Find("Tools/LeafBlower").GetComponent<LeafBlower>();
						if(lb.bLeaves1)  endPos = lb.psLeaves1.transform.position;
						else if(lb.bLeaves2)  endPos = lb.psLeaves2.transform.position;
						else if(lb.bLeaves3)  endPos = lb.psLeaves3.transform.position;
						else if(lb.bLeaves4)  endPos = lb.psLeaves4.transform.position;
						else if(lb.bLeaves5)  endPos = lb.psLeaves5.transform.position;
						StartPointing();
						StartCoroutine( "MovePoinnter" );
					}
					break;

				case "cut_branches":
					if( GameObject.Find("TutPosStart_cut_branches")!=null)
					{
						startPos = GameObject.Find("TutPosStart_cut_branches").transform.position;
						Shears sh = GameObject.Find("Tools/Shears").GetComponent<Shears>();

						string[] branchNames = sh.BranchesToCut.Split(new char[] {','} ,System.StringSplitOptions.RemoveEmptyEntries);
						if(branchNames.Length>0)
						{
							int ind = Mathf.FloorToInt( Random.Range(0,branchNames.Length));
							if(GameObject.Find(branchNames[ind])!=null)
							{
								endPos = GameObject.Find(branchNames[ind]).transform.position;
								StartPointing();
								StartCoroutine( "MovePoinnter" );
							}
						}
					}
					break;

				case "clean_dirt":
					if( GameObject.Find("TutPosStart_clean_dirt")!=null)
					{
						startPos = GameObject.Find("TutPosStart_clean_dirt").transform.position;
						WaterHose wh = GameObject.Find("Tools2/WaterHose/Nozzle").GetComponent<WaterHose>();
						
						string[] names = wh.LeftToBeCleaned.Split(new char[] {','} ,System.StringSplitOptions.RemoveEmptyEntries);
						if(names.Length>0)
						{
							int ind = Mathf.FloorToInt( Random.Range(0,names.Length));
							if(GameObject.Find(names[ind])!=null)
							{
								endPos = GameObject.Find(names[ind]).transform.position;
								transform.position = startPos;
								StartPointing();
								StartCoroutine( "MovePoinnter" );
							}
						}
					}
					break;


				case "clean_stains":
					if( GameObject.Find("TutPosStart_clean_stains")!=null)
					{
						startPos = GameObject.Find("TutPosStart_clean_stains").transform.position;
						WaterGun wg = GameObject.Find("Tools2/Compressor/WaterGun").GetComponent<WaterGun>();
						
						string[] names = wg.LeftToBeCleaned.Split(new char[] {','},System.StringSplitOptions.RemoveEmptyEntries);
						if(names.Length>0)
						{
							int ind = Mathf.FloorToInt( Random.Range(0,names.Length));
							if(GameObject.Find(names[ind])!=null)
							{
								endPos = GameObject.Find(names[ind]).transform.position + new Vector3(.5f,-.5f,0);
								transform.position = startPos;
								StartPointing();
								StartCoroutine( "MovePoinnter" );
							}
						}
					}
					break;


				case "wheel_brush":
					if( GameObject.Find("TutPosStart_wheel_brush")!=null)
					{
						startPos = GameObject.Find("TutPosStart_wheel_brush").transform.position;
						WheelBrush wb = GameObject.Find("Tools/WheelBrush").GetComponent<WheelBrush>();
						
						string[] names = wb.LeftToBeCleaned.Split(new char[] {','},System.StringSplitOptions.RemoveEmptyEntries);
						if(names.Length>0)
						{
							int ind = Mathf.FloorToInt( Random.Range(0,names.Length));
							if(GameObject.Find(names[ind])!=null)
							{
								endPos = GameObject.Find(names[ind]).transform.position + new Vector3(.5f,-.5f,0);
								transform.position = startPos;
								StartPointing();
								StartCoroutine( "MovePoinnter" );
							}
						}
					}
					break;

				case "bubbles":
					if( GameObject.Find("TutPosStart_bubbles")!=null)
					{
						startPos = GameObject.Find("TutPosStart_bubbles").transform.position;
						StartCoroutine( "MovePoinnter_Bubbles" );
					}
					break;
					
				case "wax_car":
					if( GameObject.Find("TutPosStart_wax_car")!=null)
					{
						startPos = GameObject.Find("TutPosStart_wax_car").transform.position;
						CleaningCloth cc = GameObject.Find("Tools/CleaningCloth").GetComponent<CleaningCloth>();
						
						string[] names = cc.LeftToBeCleaned.Split(new char[] {','},System.StringSplitOptions.RemoveEmptyEntries);
						if(names.Length>0)
						{
							int ind = Mathf.FloorToInt( Random.Range(0,names.Length));
							if(GameObject.Find(names[ind])!=null)
							{
								endPos = GameObject.Find(names[ind]).transform.position + new Vector3(.5f,-.5f,0);
								transform.position = startPos;
								StartPointing();
								StartCoroutine( "MovePoinnter" );
							}
						}
					}
					break;

				case "polisher":
					if( GameObject.Find("TutPosStart_polisher")!=null)
					{
						startPos = GameObject.Find("TutPosStart_polisher").transform.position;
						Polisher pol = GameObject.Find("Tools/Polisher").GetComponent<Polisher>();
						
						string[] names = pol.LeftToBeCleaned.Split(new char[] {','},System.StringSplitOptions.RemoveEmptyEntries);
						if(names.Length>0)
						{
							int ind = Mathf.FloorToInt( Random.Range(0,names.Length));
							if(GameObject.Find(names[ind])!=null)
							{
								endPos = GameObject.Find(names[ind]).transform.position + new Vector3(.5f,-.5f,0);
								transform.position = startPos;
								StartPointing();
								StartCoroutine( "MovePoinnter" );
							}
						}
					}
					break;

				case "inflate_tires":
					if( GameObject.Find("TutPosStart_inflate_tires")!=null)
					{
						startPos = GameObject.Find("TutPosStart_inflate_tires").transform.position;
						TirePump tp = GameObject.Find("Tools/TirePump").GetComponent<TirePump>();
						
						string[] names = tp.LeftToBeCleaned.Split(new char[] {','},System.StringSplitOptions.RemoveEmptyEntries);
						if(names.Length>0)
						{
							int ind = Mathf.FloorToInt( Random.Range(0,names.Length));
							if(GameObject.Find(names[ind]+"/Valwe")!=null)
							{
								endPos = GameObject.Find(names[ind]+"/Valwe").transform.position + new Vector3(.5f,-.5f,0);
								transform.position = startPos;
								StartPointing();
								StartCoroutine( "MovePoinnter" );
							}
						}
					}
					break;


				case "fuel":
					if( GameObject.Find("TutPosStart_fuel")!=null)
					{
						startPos = GameObject.Find("TutPosStart_fuel").transform.position;
						endPos = GameObject.Find("FuelTank").transform.position + new Vector3(.5f,-.5f,0);
						transform.position = startPos;
						StartPointing();
						StartCoroutine( "MovePoinnter" );
					}
					break;
					
					
					default : break;
				}
 
			}

			if(Application.loadedLevelName == "Gameplay" )
			{
				if(tutorialStateOld != tutorialState && (tutorialStateOld !="")) 
				{
					HidePointer();
					timeWaitToShowHelp = PeriodToShowHelp;
					StopAllCoroutines();
					Debug.Log (tutorialStateOld+ " *  " + tutorialState );
				}
				tutorialStateOld = tutorialState;
			}
		}
//		else
//		{
//			switch(tutorialState)
//			{
//			case "select_car":
//				timeWaitToShowHelp = PeriodToShowHelp;
//				HidePointer();
//				break;
//				
//			default : break;
//			}


			
			if(Application.loadedLevelName == "Gameplay"   && tutorialStateOld  == "" &&  tutorialState =="" && Gameplay.Instance.nextTool >0 && Gameplay.Instance.nextTool<12)
			{
				Debug.Log("POKAZI DUGME");
				
//				if(Gameplay.Instance.nextTool == 1)
//					transform.position = GameObject.Find("btnCleanLeaves").transform.position + new Vector3(.5f,-.5f,0);
//				if(Gameplay.Instance.lastCompletedToolAction == 2)
//					transform.position = GameObject.Find("btnCutBranches").transform.position + new Vector3(.5f,-.5f,0);
//				if(Gameplay.Instance.lastCompletedToolAction == 3)
//					transform.position = GameObject.Find("btnCleanDirt").transform.position + new Vector3(.5f,-.5f,0);
//				if(Gameplay.Instance.lastCompletedToolAction == 4)
//					transform.position = GameObject.Find("btnCleanStains").transform.position + new Vector3(.5f,-.5f,0);
//				if(Gameplay.Instance.lastCompletedToolAction == 5)
//					transform.position = GameObject.Find("btnWheelBrush").transform.position + new Vector3(.5f,-.5f,0);
//				if(Gameplay.Instance.lastCompletedToolAction == 6)
//					transform.position = GameObject.Find("btnBubbles").transform.position + new Vector3(.5f,-.5f,0);
//				if(Gameplay.Instance.lastCompletedToolAction == 7)
//					transform.position = GameObject.Find("btnRotatingBrush").transform.position + new Vector3(.5f,-.5f,0);
//				if(Gameplay.Instance.lastCompletedToolAction == 8)
//					transform.position = GameObject.Find("btnWaxCar").transform.position + new Vector3(.5f,-.5f,0);
//				if(Gameplay.Instance.lastCompletedToolAction == 9)
//					transform.position = GameObject.Find("btnPolishCar").transform.position + new Vector3(.5f,-.5f,0);
//				if(Gameplay.Instance.lastCompletedToolAction == 10)
//					transform.position = GameObject.Find("btnInflateTires").transform.position + new Vector3(.5f,-.5f,0);
//				if(Gameplay.Instance.lastCompletedToolAction == 11)
//					transform.position = GameObject.Find("btnFuel").transform.position + new Vector3(.5f,-.5f,0);
//				
//				StartPointingAndHide();
			}


			if(Application.loadedLevelName == "Gameplay" )
			{
				HidePointer();
				timeWaitToShowHelp = PeriodToShowHelp;
			 
			}
		}
	}

*/

	
	void TestHelpPointerSelectCar()
	{
//		Debug.Log(MenuManager.activeMenu + "  " + tutorialState + "   "+ PeriodToShowHelp + "  " +  timeWaitToShowHelp);
		if(/*MenuManager.activeMenu == ""   && */!bPause)
		{
			if(bHidden) 
			{
				timeWaitToShowHelp -=.1f;
				if(timeWaitToShowHelp <=0)
				{
					timeWaitToShowHelp = PeriodToShowHelp;
					switch(tutorialState)
					{
					case "select_car":
						transform.position = new Vector3(0,-1,0);
						StartCoroutine( "StartPointingAndHide" );
						break;
					}
				}
			}
		}
		else
		{
			switch(tutorialState)
			{
			case "select_car":
				timeWaitToShowHelp = PeriodToShowHelp;
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
		timeWaitToShowHelp = PeriodToShowHelp;
		bHidden = true;
	}
 
	void StartPointing()
	{
		bHidden = false;
		animTutorial.SetTrigger("moveStart");
		Debug.Log("StartPointing");
	}

	IEnumerator  MovePoinnter(  )
	{
		bHidden = false;
		float timeMove = 0;
		transform.position = Vector3.Lerp(startPos,endPos,timeMove);
		yield return new WaitForSeconds(.8f);
		animTutorial.SetTrigger("move");

		yield return new WaitForSeconds(.8f);
		while(timeMove<1)
		{
			timeMove+=2*Time.deltaTime;
			transform.position = Vector3.Lerp(startPos,endPos,timeMove);
			yield return new WaitForEndOfFrame();
		}
		yield return new WaitForSeconds(1);
		LiftPointer();
		Debug.Log("MovePoinnter");
	}

	void LiftPointer()
	{
		animTutorial.SetTrigger("moveEnd");
		Debug.Log("LiftPointer");
		bHidden = true;
		timeWaitToShowHelp = PeriodToShowHelp;
	}

	public void HidePointer()
	{
		if(!bHidden)
		{
			bHidden = true;
			Debug.Log("HidePointer");
			animTutorial.SetTrigger("Hide");
		 
		}
		StopAllCoroutines();
		timeWaitToShowHelp = PeriodToShowHelp;
	}


	IEnumerator  MovePoinnter_Bubbles(  )
	{
		StartPointing();
		float timeMove = 0;
		endPos = new Vector3(-3,1,0);
		transform.position = Vector3.Lerp(startPos,endPos,timeMove);
		yield return new WaitForSeconds(.8f);
		animTutorial.SetTrigger("move");
		
		yield return new WaitForSeconds(.8f);
		while(timeMove<1)
		{
			timeMove+=2*Time.deltaTime;
			transform.position = Vector3.Lerp(startPos,endPos,timeMove);
			yield return new WaitForEndOfFrame();
		}
		endPos = new Vector3(-1,-1,0);
		timeMove = 0;
		startPos = transform.position;
		while(timeMove<1)
		{
			timeMove+=2*Time.deltaTime;
			transform.position = Vector3.Lerp(startPos,endPos,timeMove);
			yield return new WaitForEndOfFrame();
		}

		endPos = new Vector3(1,1,0);
		timeMove = 0;
		startPos = transform.position;
		while(timeMove<1)
		{
			timeMove+=2*Time.deltaTime;
			transform.position = Vector3.Lerp(startPos,endPos,timeMove);
			yield return new WaitForEndOfFrame();
		}

		endPos = new Vector3(3,-1,0);
		timeMove = 0;
		startPos = transform.position;
		while(timeMove<1)
		{
			timeMove+=2*Time.deltaTime;
			transform.position = Vector3.Lerp(startPos,endPos,timeMove);
			yield return new WaitForEndOfFrame();
		}

		yield return new WaitForSeconds(1);
		LiftPointer();
		Debug.Log("MovePoinnter");
		timeWaitToShowHelp = PeriodToShowHelp;
		yield return new WaitForSeconds(.3f);
		bHidden = true;
	}

 
}
