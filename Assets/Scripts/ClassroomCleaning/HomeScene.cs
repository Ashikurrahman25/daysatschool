using UnityEngine;
using UnityEngine.UI;
using System.Collections;
 

public class HomeScene : MonoBehaviour {

	//public Image SoundOn;
	public Image SoundOff;
	public Image BtnSoundIcon;
 
	public GameObject PopUpSecialOffer;
 
	public MenuManager menuManager;
	public static  HomeScene Instance;
	public KidsCarousel kidsCarousel;
	public ItemsSlider roomCarousel;
	Animator roomCarouselAnim;

	public GameObject BtnBack;
	public GameObject BtnTimeReward;
	public GameObject BtnWatchVideo;

	public GameObject BigStar;
	bool bBigStarVisible = false;

	public  Text txtStars;
 

	/*
	//NATIVE AD HOLDER HOME SCENA

*/
	public static bool bSkipSelectChild = false;
	//static bool bGoToSplash = true;

	 
	

	void Awake()
	{

		Input.multiTouchEnabled = false;



		Instance = this;
	}

	void Start () {

		Input.multiTouchEnabled = false;

	 
	
 
//		LevelTransition.Instance.ShowScene();

		BlockClicks.Instance.SetBlockAll(true);
		BlockClicks.Instance.SetBlockAllDelay(1f,false);

	/*	if(SoundManager.soundOn == 0) 
			BtnSoundIcon.sprite = SoundOff;
		else  
			BtnSoundIcon.sprite = SoundOn;
 
*/
		if(GameData.TotalStars>0) 
		{
			//unistava se velika zvezda
			GameObject.Destroy(BigStar);	 
		}
		 
		txtStars.text = GameData.TotalStars.ToString();

		roomCarouselAnim  = roomCarousel.transform.parent.GetComponent<Animator>();
		if(bSkipSelectChild)
		{
			roomCarouselAnim.SetBool("bDefaultVisible", true);
			//roomCarousel.transform.parent.GetComponent<ItemsSlider>().StartCoroutine("Init");
			kidsCarousel.gameObject.SetActive(false);
			BtnBack.SetActive(true);
			roomCarousel.b_Enabled = true;
		}
		else
		{
			BtnBack.SetActive(false);
			kidsCarousel.b_Enabled = true;
		}

		if(SoundManagerCleaningClassroom.soundOn == 1)
		{
			SoundOff.enabled = false;
			//SoundOn.enabled = true;
		}
		else
		{
			SoundOff.enabled = true;
			//SoundOn.enabled = false;
		}
	}

	
	public void ExitGame () {

			Application.Quit();

	}

 
//	public void btnPlayClick()
//	{
//		SoundManager.Instance.Play_ButtonClick();
//		StartCoroutine(LoadMap());
//	 
//		BlockClicks.Instance.SetBlockAll(true);
//		BlockClicks.Instance.SetBlockAllDelay(2f,false);
//	}
//	
//	IEnumerator LoadMap()
//	{
//		yield return new WaitForSeconds(.3f);
//		SceneManager.LoadScene("SelectRoom");
//		
//	}

 

//	public void btnHelpClick()
//	{
//		SoundManager.Instance.Play_ButtonClick();
//		menuManager.ShowPopUpMenu(PopUpHelp);
//	}
//
//	public void btnCloseHelpClick()
//	{
//		SoundManager.Instance.Play_ButtonClick();
//		menuManager.ClosePopUpMenu(PopUpHelp);
//	}
	 

	public void btnSoundClicked()
	{
 
		if(SoundManagerCleaningClassroom.soundOn == 1)
		{
			SoundOff.enabled = true;
			//SoundOn.enabled = false;
			SoundManagerCleaningClassroom.soundOn = 0;
			SoundManagerCleaningClassroom.Instance.MuteAllSounds();

		}
		else
		{
			SoundOff.enabled = false;
			//SoundOn.enabled = true;
			SoundManagerCleaningClassroom.soundOn = 1;
			SoundManagerCleaningClassroom.Instance.UnmuteAllSounds();

		}

		 
	

		PlayerPrefs.SetInt("SoundOn",SoundManagerCleaningClassroom.soundOn);
		PlayerPrefs.SetInt("MusicOn",SoundManagerCleaningClassroom.musicOn);
		PlayerPrefs.Save();

	 
		BlockClicks.Instance.SetBlockAll(true);
		BlockClicks.Instance.SetBlockAllDelay(.3f,false);
 
	}
 
	
	public void btnBuySpecialOfferClick(string btnID)
	{
		 
//		if(btnID == "SpecialOffer" &&  Shop.SpecialOffer == 0)  Shop.Instance.SendShopRequest(btnID);
//		
//		BlockClicks.Instance.SetBlockAll(true);
//		BlockClicks.Instance.SetBlockAllDelay(1f,false);
//		 
//		SoundManagerCleaningClassroom.Instance.Play_ButtonClick();
//		StartCoroutine("CloseSpecialOffer");
	}
	
//	IEnumerator CloseSpecialOffer()
//	{
//		yield return new WaitForSeconds(1);
//		menuManager.ClosePopUpMenu(PopUpSecialOffer );
//		
//	}

	public void CarouselSelected()
	{
		kidsCarousel.b_Enabled = false;
		roomCarousel.b_Enabled = false;

		if(kidsCarousel.gameObject.activeSelf)
		{
			//Debug.Log("SelectedChild"+kidsCarousel.SelectedChild);
			StartCoroutine("ShowCarouselRooms");
		}
		else
		{
			DragToLocation.ToClean= 0;
			DragToLocation.ToClean_L2= 0;
			Clean.CleanAndFixItemsCount = 0;
			bSkipSelectChild = true;

			if(GameData.CheckedRooms[(Gameplay.roomNo-1)]) 
			{
				MenuDecorations.LevelDecorations[(Gameplay.roomNo-1)] = ""; 
				GameData.CheckedRooms[(Gameplay.roomNo-1)] = false;
			}

			//RESETOVANJE MINI IGARA

			if( Gameplay.roomNo == 2 ) 
			{
				GlobalVariables.mgDrawShaper_SavedProgres = "";
				MiniGame_DrawShapes.screenshot = null;
			}

			if( Gameplay.roomNo == 4 ) MiniGameConnectWires.ConnectedWires = 0;


//			LevelTransition.Instance.HideSceneAndLoadNext("Room" + Gameplay.roomNo);
		}
	}

	IEnumerator ShowCarouselRooms()
	{
		BlockClicks.Instance.SetBlockAll(true);
		yield return new WaitForSeconds(.1f);

 
		if(BigStar !=null)
		{
			bBigStarVisible = true;
			BigStar.GetComponentInChildren<Animator>().SetTrigger("tShow");
			yield return new WaitForSeconds(2f);
			AddStar();
		}
		 
		yield return new WaitForSeconds(.1f);
		GameObject.Find("RoomSelect").GetComponent<Animator>().SetTrigger("tShow");

		yield return new WaitForSeconds(0.2f);
		BtnBack.SetActive(true);
		yield return new WaitForSeconds(0.3f);
		kidsCarousel.gameObject.SetActive(false);
		//yield return new WaitForSeconds(0.2f);
		roomCarousel.b_Enabled = true;
		BlockClicks.Instance.SetBlockAll(false);
		yield return new WaitForSeconds(.8f);
		bBigStarVisible = false;
		if(BigStar !=null)
		{
			yield return new WaitForSeconds(2.2f);
			GameObject.Destroy(	BigStar );
		}
		 


	}

	public void btnShowKidsCarousel()
	{
		kidsCarousel.b_Enabled = false;
		roomCarousel.b_Enabled = false;

		BlockClicks.Instance.SetBlockAll(true);
		BlockClicks.Instance.SetBlockAllDelay(1.5f,false);
		
		StartCoroutine("ShowKidsCarouselAndHideSelectRooms");
	}

	IEnumerator ShowKidsCarouselAndHideSelectRooms()
	{
		 
		kidsCarousel.gameObject.SetActive(true);
	
		 

		yield return new WaitForSeconds(0.2f);
		roomCarouselAnim.SetBool("bDefaultVisible", false);
		roomCarouselAnim.SetTrigger("tHide");
		yield return new WaitForSeconds(0.2f);
		BtnBack.SetActive(false);
		yield return new WaitForSeconds(0.4f);
		kidsCarousel.b_Enabled = true;
	}

	public void AddStar()
	{
		GameData.TotalStars++;
		GameData.SetStarsToPP();
		txtStars.text = GameData.TotalStars.ToString();
		roomCarousel.SetRooms();
	}

	public void EndWatchingVideo()
	{
		GameData.WatchVideoCounter = 0;
		AddStar();
		GameObject.Destroy(BtnWatchVideo);
	}


	//********************************************


	public  void LevelTransitionOn()
	{
		 
		if(Application.loadedLevelName == "HomeScene") 
		{
			//Debug.Log("HIDE HOME SCENE NATIVE AD");


		}
		 
	}
	
	public   void LevelTransitionOff()
	{
		if(Application.loadedLevelName == "HomeScene") 
		{
			//Debug.Log("SHOW HOME SCENE NATIVE AD"); 

		}
	}

	
	public void RemoveAdsBought( )
	{
/*		if(NativeAdHolder_KidsCarousel !=null)   NativeAdHolder_KidsCarousel.HideBothAds();
		bVisible_NativeAdHolder_KidsCarousel = false;
		
		if(NativeAdHolder_RoomCarousel !=null)   NativeAdHolder_RoomCarousel.HideBothAds();
		bool bVisible_NativeAdHolder_RoomCarousel = false;
*/		
		 
	}









}
