using UnityEngine;
using System.Collections;

public class MiniGameConnectWires : MonoBehaviour {

	public Color[] Boje;
	public Cable3d[] kabloviLevo;
	public Cable3d[] kabloviDesno;
	 


	public static int ConnectedWires = 0;

	int[] RaspL = new int [4] { 0, 1, 2, 3};

	public Transform[] HelpPointerStartPositions  ;
	public Transform[] HelpPointerEndPositions  ;

	public MenuManager menuManager;
	
	//public GameObject PopUpTask;
	//public GameObject PopUpLevelComplete;
	public GameObject PopUpPause;
	public GameObject PopUpGameOver;



	float tsc = 1;
	//bool bEnableMove = false;
	bool bStartGame = false;
	float TimeToShowHelp = 1.5f;
	float CurrentTimeToShowHelp = 0;
	bool bShowHelp = false;

	public Animator animHandPointer;
	public GameObject  HandPointer;

	bool bTutorial = false;

	Vector3 handPointerPosition = Vector3.zero;
	
	int test = 0;
	//public static bool bRightClickedFirst = false;
	public static int ClickedIdBoje = -1;

	public ParticleSystem psShortCircut;
 

	void Awake () {
		Gameplay.bReturnFromMiniGame = true;
	}



	void Start  () {
	 	BlockClicks.Instance.SetBlockAll(true);
  		BlockClicks.Instance.SetBlockAllDelay(1.5f,false);

	

		bPause = true;
		//bRightClickedFirst = false;
		ClickedIdBoje = -1;
		 
	//	iTween.FadeUpdate(HandPointer,0,0);

		MoveCable.bEnableMove = false;
		ConnectedWires = 0;
		Input.multiTouchEnabled = false;
		StartCoroutine("ShowPopUp");

		ConnectedWires = 0;

		Color cTmp ;
		int iTmp ;
		for(int i = 0;i<100;i++)
		{
			int r1 = Mathf.FloorToInt(Random.Range(0,3.9f));
			cTmp = Boje[r1];
			int r2 = Mathf.FloorToInt(Random.Range(0,3.9f));

			Boje[r1] = Boje[r2];
			Boje[r2] = cTmp;
		}

		for(int i = 0;i<4;i++)
		{
			kabloviDesno[i].CableCol = (CableColor) i;
			kabloviDesno[i].Init();
		}

		for(int i = 0;i<100;i++)
		{
			int r1 = Mathf.FloorToInt(Random.Range(0,3.9f));
			iTmp = RaspL[r1];
			int r2 = Mathf.FloorToInt(Random.Range(0,3.9f));
			
			RaspL[r1] = RaspL[r2];
			RaspL[r2] = iTmp;
			
		}

		for(int i = 0;i<4;i++)
		{
			kabloviLevo[i].CableCol =  (CableColor)  RaspL[i];
			kabloviLevo[i].Init();
			 
		}

	 
	 
			InvokeRepeating ("TestHelpNeeded",4f,0.5f); 	//KADA JE ZAVRSEN PRVI TUTPORIJAL A IGRAC NE ZNA STA DA URADI
	 
		 


		CurrentTimeToShowHelp = 0;
		HandPointer.transform.position  = Vector3.zero;

		psShortCircut.GetComponent<Renderer>().sortingOrder = 20;
		psShortCircut.transform.GetChild(0).GetComponent<Renderer>().sortingOrder = 19;
		psShortCircut.transform.GetChild(1).GetComponent<Renderer>().sortingOrder = 19;
		psShortCircut.Stop();

		StartCoroutine("StartGame");
//		try{
//			if(	SoundManager.Instance.menuMusic.isPlaying)
//			{
//				SoundManager.Instance.Stop_MenuMusic();
//				SoundManager.Instance.Play_GameplayMusic();
//			}
//		}
//		catch{}
//		menuManager.EscapeButonFunctionStack.Push("btnPauseClicked");
	}
	
 
	void Update () {
	
	}



	
 
	
	
	public void ButtonFinishClicked()
	{
		Gameplay.bReturnFromMiniGame = true;
		BlockClicks.Instance.SetBlockAll(true);
		 
		StartCoroutine(WaitToLoadLevel("Room4",1));
	}
	
//	public void btnReplyClicked()
//	{
//		BlockAll.blocksRaycasts = true;
//		StartCoroutine(SetBlockAll(1,false));
//
//		SoundManager.Instance.Play_ButtonClick();
//		 
//		//StartCoroutine(WaitToLoadLevel("MiniGame1",1));
//		StartCoroutine(WaitToLoadLevel("CustomerScene",1));
//	}
	
	
	public static bool bPause = false;
//	public void btnPauseClicked()
//	{
//		BlockAll.blocksRaycasts = true;
//		StartCoroutine(SetBlockAll(1,false));
//
//		SoundManager.Instance.Play_ButtonClick();
//		if(bStartGame)
//		{
//			bPause = true;
//			 
//			menuManager.ShowPopUpMenu (PopUpPause);
//			//StartCoroutine(SetBlockAll(0f,true));
//		}
//		
//	}
	
//	public void btnPlayClicked()
//	{
//		BlockAll.blocksRaycasts = true;
//		StartCoroutine(SetBlockAll(1,false));
//
//		SoundManager.Instance.Play_ButtonClick();
//		bPause = false;
//		 
//		menuManager.ClosePopUpMenu (PopUpPause);
//		//StartCoroutine(SetBlockAll(1.5f,false));
//	}
//	
	
	public void btnLoadNextClicked()
	{
		Gameplay.bReturnFromMiniGame = true;
		BlockClicks.Instance.SetBlockAll(true);
		 StartCoroutine(WaitToLoadLevel("Room4",1));
		 
	}


	//**************************************************
	
	
	IEnumerator WaitToLoadLevel(string leveName, float timeW = 0)
	{
		yield return new WaitForSeconds(timeW);
		//Application.LoadLevel( leveName );
//		LevelTransition.Instance.HideSceneAndLoadNext(leveName);
	}
	
	
//	
//	public void btnFinishClicked()
//	{
//		StartCoroutine(ShowEndMenu());
//	}
//	
	IEnumerator ShowEndMenu()
	{
		 
		CancelInvoke("TestHelpNeeded");
		HidePointer();



		MoveCable.bEnableMove = false;
		yield return new WaitForSeconds(.5f);
		btnLoadNextClicked();
	}
	
	IEnumerator StartGame()
	{
		yield return new WaitForSeconds(0.2f);
		bStartGame = true;
//		if(LevelTransition.Instance!=null) LevelTransition.Instance.ShowScene();
		MoveCable.bEnableMove = true;

		yield return new WaitForSeconds(1.5f);
		bPause = false;
		if(bTutorial)  
		{
				ShowHelpLeftWire();
		}
		else CurrentTimeToShowHelp = 0;
	}
	
	IEnumerator ShowPopUp()
	{
		MoveCable.bEnableMove = false;
		yield return new WaitForSeconds(.1f);
//		menuManager.ShowPopUpMenu(PopUpTask);
	}

	IEnumerator ShowGameOverMenu()
	{
		Gameplay.bReturnFromMiniGame = true;
		CancelInvoke("TestHelpNeeded");
		HidePointer();

		yield return new WaitForSeconds(1.5f);
		MoveCable.bEnableMove = false;
		yield return new WaitForSeconds(.5f);
		//menuManager.ShowPopUpMenu(PopUpGameOver);
		ConnectedWires = 0;
		StartCoroutine(WaitToLoadLevel("Room4",0.1f));

	}





	public void ShortCircut(Vector3 _position)
	{
		psShortCircut.transform.position = _position;
		psShortCircut.Play();
	
		//SoundManager.Instance.WireSparks.Play();
	}


	public void TestEndGame()
	{
		//doslo je do kratkog spoja
		if(ConnectedWires ==-1) 
		{
 			
			StartCoroutine(ShowGameOverMenu());
		}
		//spojeni svi kablovi
		else if(ConnectedWires ==4) 
			StartCoroutine(ShowEndMenu());
		//ako je tutorial u toku - aktiviranje sledece pomoci
		//TODO
		else if(bTutorial) 
		{
			bShowHelp = true;
			ShowHelpLeftWire();
		}
		//resetovanje periaoda aktiviranja pomoci ako je tutorial zavrsen
		else
		{
			bShowHelp = false;
			CurrentTimeToShowHelp = 0;
			HidePointer();
		}
	}


	
	//**************************************************
	//TUTORIJAL

 

	public void ShowHelpLeftWire()
	{
		bool bPronadjen = false;

		while(!bPronadjen)
		{
		 	test = Mathf.FloorToInt(Random.Range(0,4));
			if(!kabloviLevo[test].bIskoriscen )
			{
				bPronadjen = true;

//				if(ClickedIdBoje ==-1) 	iTween.MoveTo( HandPointer , HelpPointerStartPositions[test].position,1f);
				ShowPointer();
				ClickedIdBoje = (int) kabloviLevo[test].CableCol;
			
			}
		}
		

		
	}
	 
	public void ShowHelpRightWire(int _IDBoje )
	{
		ClickedIdBoje = _IDBoje;
		 
		if(!bTutorial &&  !bShowHelp)
		{
			CurrentTimeToShowHelp = 0;
			HidePointer();
			return;
		}
	 
		for(int i= 0;i<4;i++)
		{
			if((int)kabloviDesno[i].CableCol ==  _IDBoje)
			{
				int pos = int.Parse( kabloviDesno[i].transform.name.Substring(5,1))-1;
				handPointerPosition = HelpPointerEndPositions[pos].position;
			}
		}
	 
		ShowPointer();
//		iTween.MoveTo( HandPointer     ,   iTween.Hash("position",   handPointerPosition,   "time", 1f,     "delay", 0 ,    "oncomplete","PointerCompleteMoving" ,     "oncompletetarget",gameObject));

	}

	 



	public void ShowLeftHelpAgain( )
	{
	 
		if(!bTutorial )
		{
			ClickedIdBoje =-1;
			CurrentTimeToShowHelp = 0;
			HidePointer();
			return;
		}


		for(int i= 0;i<4;i++)
		{
			if((int) kabloviLevo[i].CableCol ==  ClickedIdBoje ) test = i;
		}
//		iTween.MoveTo( HandPointer , HelpPointerStartPositions[test].position,1f);
		ShowPointer();
	}









	public void PointerCompleteMoving()
	{

		if(!bTutorial) 
		{
			CurrentTimeToShowHelp = 0;
			StartCoroutine(WaitToHidePointer(1));

		}
	}

	IEnumerator WaitToHidePointer(float timeW)
	{
		yield return new WaitForSeconds(timeW);
		HidePointer();
		
	}
	
	public void HidePointer()
	{
		CurrentTimeToShowHelp = 0;
//		iTween.FadeTo(HandPointer,0,0.3f);
	}
	
	public void ShowPointer()
	{
		if(bTutorial || bShowHelp) //ako nije prikazan treci tutorijal
		{
			HandPointer.SetActive(true);
//			iTween.FadeTo(HandPointer,1,0.3f);
		}
	}
	
 
	//AKO JE IGRAC ZABORAVIO STA TREBA DA URADI POKERECE SE KURSOR KOJI POKAZUJE POMOC
	void TestHelpNeeded()
	{
		if(bStartGame)
		{
			if(CurrentTimeToShowHelp ==0) bShowHelp = false;
			CurrentTimeToShowHelp+=0.5f;
			
			if(TimeToShowHelp  == CurrentTimeToShowHelp)
			{
				bShowHelp = true;
				if(ClickedIdBoje ==-1)
					ShowHelpLeftWire();
				else
				{
				
					ShowHelpRightWire(ClickedIdBoje);
				}
			}
		}
	}

	 


}
