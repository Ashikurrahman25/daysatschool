using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

	/**
  * Scene:All
  * Object:Canvas
  * Description: Skripta zaduzena za hendlovanje(prikaz i sklanjanje svih Menu-ja,njihovo paljenje i gasenje, itd...)
  **/
public class MenuManager : MonoBehaviour 
{
	
	public Menu currentMenu;
	public Menu currentPopUpMenu;
//	[HideInInspector]
//	public Animator openObject;
	public GameObject[] disabledObjects;
	GameObject ratePopUp, crossPromotionInterstitial,specialOfferPopUp;
	public bool popupOpened;
	public bool popupFullyOpened;
	private bool startInterstitialOpened;
	public GameObject soundButton;
	public GameObject loadingHolder;

    [Header ("PopUp Dialog")]
	public GameObject dialogPopup;
	public Text dialogMessage;
	public Text dialogTitle;

	// PopupHolders
    [Header ("PopUp holders")]
    public GameObject plastersPopup;

    public static bool justStarted = true;

    public static string activeMenu
    {
        get;
        set;
    }

//    public GameObject hammersPopup;
//    public GameObject thermometersPopup;
//    public GameObject magicPotionPanel;
//    public GameObject colorsPopup;
//    public GameObject stickersPopup;


//    public FacebookNativeAd gameplayNativeAd;
//    public FacebookNativeAd hammersNativeAd;
//    public FacebookNativeAd thermometersNativeAd;
//    public FacebookNativeAd magicPotionNativeAd;


//	public GameObject breathParticle;
	
	IEnumerator Start () 
	{
        transition = GameObject.Find("Transition").transform;
		if(Application.loadedLevelName=="MainScene")
		{
			crossPromotionInterstitial = GameObject.Find("PopUps/PopUpInterstitial");
			ratePopUp = GameObject.Find("PopUps/PopUpRate");
			specialOfferPopUp = GameObject.Find("PopUps/SpecialOfferPopUp");


		}
        yield return null;



		if (disabledObjects!=null) {
			for(int i=0;i<disabledObjects.Length;i++)
			{
				// Debug.Log("Gasi "+disabledObjects[i].name);
				if (disabledObjects[i] != null)
					disabledObjects[i].SetActive(false);
			}
		}
		
//		if(Application.loadedLevelName != "MapScene")
//			ShowMenu(currentMenu.gameObject);	
		
		if(Application.loadedLevelName=="MainScene")
		{
//			if (DateTime.Now.DayOfWeek != DayOfWeek.Sunday)
//			{
				if(PlayerPrefs.HasKey("alreadyRated"))
				{
					Rate.alreadyRated = PlayerPrefs.GetInt("alreadyRated");
				}
				else
				{
					Rate.alreadyRated = 0;
				}
				
				if(Rate.alreadyRated==0)
				{
					Rate.appStartedNumber = PlayerPrefs.GetInt("appStartedNumber");
					Debug.Log("appStartedNumber "+Rate.appStartedNumber);
					
					if(Rate.appStartedNumber>=6)
					{
						Rate.appStartedNumber=0;
						PlayerPrefs.SetInt("appStartedNumber",Rate.appStartedNumber);
						PlayerPrefs.Save();
						ShowPopUpMenu(ratePopUp);
						
					}
					else
					{
	//					if (!GlobalVariables.playCharacterUnlockAnimation)
//						if (GlobalVariables.showInhouseStartInterstitial)
//							ShowStartInterstitial();
//						else
//							mainSceneNativeAd.LoadAd();
					}
				}
				else
				{
	//				if (!GlobalVariables.playCharacterUnlockAnimation)
//					if (GlobalVariables.showInhouseStartInterstitial)
//						ShowStartInterstitial();
//					else
//						mainSceneNativeAd.LoadAd();
				}
//			}
//			else
//			{
//				if (!GlobalVariables.fullGameOwned || (!GlobalVariables.removeAdsOwned && !GlobalVariables.allItemsOwned && !GlobalVariables.allLevelsOwned))
//					ShowPopUpMenu(specialOfferPopUp);
//				else
//					mainSceneNativeAd.LoadAd();
//			}
		}

		popupOpened = false;
		popupFullyOpened = false;


        if (SceneManager.GetActiveScene().name == "MapScene")
        {
            if (!justStarted)
            {
            }

            justStarted = false;
        }


    }
	
	/// <summary>
	/// Funkcija koja pali(aktivira) objekat
	/// </summary>
	/// /// <param name="gameObject">Game object koji se prosledjuje i koji treba da se upali</param>
	public void EnableObject(GameObject gameObject)
	{
		
		if (gameObject != null) 
		{
			if (!gameObject.activeSelf) 
			{
				gameObject.SetActive (true);
			}
		}
	}

	/// <summary>
	/// Funkcija koja gasi objekat
	/// </summary>
	/// /// <param name="gameObject">Game object koji se prosledjuje i koji treba da se ugasi</param>
	public void DisableObject(GameObject gameObject)
	{
		Debug.Log("Disable Object");
		if (gameObject != null) 
		{
			if (gameObject.activeSelf) 
			{
				gameObject.SetActive (false);
			}
		}
	}
	
	/// <summary>
	/// F-ja koji poziva ucitavanje Scene
	/// </summary>
	/// <param name="levelName">Level name.</param>
	public void LoadScene(string levelName )
	{
		if (levelName != "") {
			try {
				if (levelName == "MainScene" && Application.loadedLevelName == "CharacterSelect")
				{

					
					GlobalVariables.playLoadingDepart = false;
				}

				Application.LoadLevel (levelName);
			} catch (System.Exception e) {
				Debug.Log ("Can't load scene: " + e.Message);
			}
		} else {
			Debug.Log ("Can't load scene: Level name to set");
		}
	}
	
	/// <summary>
	/// F-ja koji poziva asihrono ucitavanje Scene
	/// </summary>
	/// <param name="levelName">Level name.</param>
	public void LoadSceneAsync(string levelName )
	{
		if (levelName != "") {
			try {
				Application.LoadLevelAsync (levelName);
			} catch (System.Exception e) {
				Debug.Log ("Can't load scene: " + e.Message);
			}
		} else {
			Debug.Log ("Can't load scene: Level name to set");
		}
	}
    public void OpenPPLink()
    {
        Application.OpenURL(AdsManager.Instance.privacyPolicyLink);
    }
	/// <summary>
	/// Funkcija za prikaz Menu-ja koji je pozvan kao Menu
	/// </summary>
	/// /// <param name="menu">Game object koji se prosledjuje i treba da se skloni, mora imati na sebi skriptu Menu.</param>
	public void ShowMenu(GameObject menu)
	{
		Debug.Log("ShowMwnu   " + menu);
		if (currentMenu != null)
		{
			currentMenu.IsOpen = false;
			currentMenu.gameObject.SetActive(false);
		}

		currentMenu = menu.GetComponent<Menu> ();
		menu.gameObject.SetActive (true);
		currentMenu.IsOpen = true;
		
	}

	/// <summary>
	/// Funkcija za zatvaranje Menu-ja koji je pozvan kao Meni
	/// </summary>
	/// /// <param name="menu">Game object koji se prosledjuje za prikaz, mora imati na sebi skriptu Menu.</param>
	public void CloseMenu(GameObject menu)
	{
		Debug.Log("CloseMenu");
		if (menu != null) 
		{
			menu.GetComponent<Menu> ().IsOpen = false;
			menu.SetActive (false);
		}
	}

    public void JustShowPopUpMenu(GameObject menu)
    {
        menu.gameObject.SetActive (true);

        currentPopUpMenu = menu.GetComponent<Menu> ();
        currentPopUpMenu.IsOpen = true;
    }

	/// <summary>
	/// Funkcija za prikaz Menu-ja koji je pozvan kao PopUp-a
	/// </summary>
	/// /// <param name="menu">Game object koji se prosledjuje za prikaz, mora imati na sebi skriptu Menu.</param>
	public void ShowPopUpMenu(GameObject menu)
	{
		Debug.Log(menu.name);
		Debug.Log("popupOpened: "    + popupOpened);
		Debug.Log("popupFullyOpened: "    + popupFullyOpened);

		if (true)
		{
			popupOpened = true;

			menu.gameObject.SetActive (true);

			currentPopUpMenu = menu.GetComponent<Menu> ();
			currentPopUpMenu.IsOpen = true;


            // FIXME za sada cu da stavim bilo koji menu ako se otvor i ako je gameplay scena da se zaustavi prikazivanje gameplay native ada
			if (Application.loadedLevelName == "Level")
			{
//			
				
			}
//		
			
		}

		if ((menu.name == "VideoNotAvailable" || menu.name == "WatchVideoDialog") && Application.loadedLevelName == "Level")
		{
            // Gasimo odgovarajuci native
			if (plastersPopup.activeInHierarchy)
			{
//				plastersNativeAd.CancelLoading();
//                plastersNativeAd.HideBothAds();
			}
//			else if (hammersPopup.activeInHierarchy)
//			{
//				hammersNativeAd.CancelLoading();
//				hammersNativeAd.HideNativeAd();
//			}
//			else if (thermometersPopup.activeInHierarchy)
//			{
//				thermometersNativeAd.CancelLoading();
//				thermometersNativeAd.HideNativeAd();
//			}
			else if (dialogPopup.activeInHierarchy)
			{
//				dialogNativeAd.CancelLoading();
//                dialogNativeAd.HideBothAds();
			}
//			else if (colorsPopup.activeInHierarchy)
//			{
//				colorsPopup.transform.Find("AnimationHolder/Body/NativeAdHolder").GetComponent<FacebookNativeAd>().CancelLoading();
//				colorsPopup.transform.Find("AnimationHolder/Body/NativeAdHolder").GetComponent<FacebookNativeAd>().HideNativeAd();
//			}
//			else if (stickersPopup.activeInHierarchy)
//			{
//				stickersPopup.transform.Find("AnimationHolder/Body/NativeAdHolder").GetComponent<FacebookNativeAd>().CancelLoading();
//				stickersPopup.transform.Find("AnimationHolder/Body/NativeAdHolder").GetComponent<FacebookNativeAd>().HideNativeAd();
//			}

			menu.gameObject.SetActive (true);

//			if (magicPotionPanel.activeInHierarchy)
//			{
//				magicPotionNativeAd.CancelLoading();
//				magicPotionNativeAd.HideNativeAd();
//			}

			currentPopUpMenu = menu.GetComponent<Menu> ();
			currentPopUpMenu.IsOpen = true;
		}
	}

	IEnumerator WaitForPopupToBeFullyOpened()
	{
//		// If scene is level disable bad breath particle
//		if (Application.loadedLevelName == "Level")
//		{
//			breathParticle.SetActive(false);
//		}

		yield return new WaitForSeconds(1.2f);
		
		if (popupOpened)
			popupFullyOpened = true;
	}

	/// <summary>
	/// Funkcija za zatvaranje Menu-ja koji je pozvan kao PopUp-a, poziva inace coroutine-u, ima delay zbog animacije odlaska Menu-ja
	/// </summary>
	/// /// <param name="menu">Game object koji se prosledjuje i treba da se skloni, mora imati na sebi skriptu Menu.</param>
	public void ClosePopUpMenu(GameObject menu)
	{
		Debug.Log("ClosePopUpMenu");
		StartCoroutine("HidePopUp",menu);

//		if (GlobalVariables.quitGame)
//			Application.Quit();
	}

	/// <summary>
	/// Couorutine-a za zatvaranje Menu-ja koji je pozvan kao PopUp-a
	/// </summary>
	/// /// <param name="menu">Game object koji se prosledjuje, mora imati na sebi skriptu Menu.</param>
	IEnumerator HidePopUp(GameObject menu)
	{
//		SoundManager.Instance.PlayPopupDepartSound();
        //PAVLE SM MM
//        SoundManager.PlaySound("PopupDepart");

		menu.GetComponent<Menu> ().IsOpen = false;

//		Debug.Log(menu.name.IndexOf("NailPanel"));

		if (menu.name == "PlastersPopup")
		{
//            plastersNativeAd.HideBothAds();
		}
		else if (menu.name == "PopUpDialog")
		{
//            dialogNativeAd.HideBothAds();
		}


		if (menu.name.IndexOf("NailPanel") != -1)
			yield return new WaitForSeconds(0.2f);
		else
			yield return new WaitForSeconds(0.3f); // FIXME prepravio sa 1.2 na 0.3

		Debug.Log(menu.name);

		// FIXME za sada cu da stavim bilo koji menu ako se zatvori i ako je gameplay scena da se prikaze gameplay native ad
		if (Application.loadedLevelName == "Level" && menu.name != "VideoNotAvailable")
		{
			// Debug.Log("nije moguceeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee");
//			if (LevelManager.levelManager.currentPhase < 3)
//				gameplayNativeAd.LoadAd();
		}
//		else if (Application.loadedLevelName == "CharacterSelect")
//			mainSceneNativeAd.LoadAd();

//		popupOpened = false;
//		popupFullyOpened = false;

		// FIXME FIXME
		// if menu is one of the popup bought menus set popup to be opened and set last opened popup to be shop popup
		if ((menu.name == "VideoNotAvailable" || menu.name == "WatchVideoDialog") && Application.loadedLevelName == "Level")
		{
//			Debug.Log("Minja close test");
//				popupOpened = true;
//				popupFullyOpened = true;
//	
            if (plastersPopup.activeInHierarchy)
            {
                currentPopUpMenu = plastersPopup.GetComponent<Menu>();
//                plastersNativeAd.LoadAdWithDelay(0.4f);
            }
//			else if (hammersPopup.activeInHierarchy)
//			{
//				currentPopUpMenu = 	hammersPopup.GetComponent<Menu>();
//				hammersNativeAd.LoadAdWithDelay(0.4f);
//			}
//			else if (thermometersPopup.activeInHierarchy)
//			{
//				currentPopUpMenu = thermometersPopup.GetComponent<Menu>();
//				thermometersNativeAd.LoadAdWithDelay(0.4f);
//			}
//			else if (colorsPopup.activeInHierarchy)
//			{
//				currentPopUpMenu = colorsPopup.GetComponent<Menu>();
//				colorsPopup.transform.Find("AnimationHolder/Body/NativeAdHolder").GetComponent<FacebookNativeAd>().LoadAdWithDelay(0.4f);
//			}
//			else if (stickersPopup.activeInHierarchy)
//			{
//				currentPopUpMenu = stickersPopup.GetComponent<Menu>();
//				stickersPopup.transform.Find("AnimationHolder/Body/NativeAdHolder").GetComponent<FacebookNativeAd>().LoadAdWithDelay(0.4f);
//			}
			else
			{
				popupOpened = false;
				popupFullyOpened = false;
			}
//			else if (magicPotionPanel.activeInHierarchy)
//			{
//				currentPopUpMenu = magicPotionPanel.GetComponent<Menu>();
//			}
			// currentPopUpMenu = GameObject.Find("PopUpShop").GetComponent<Menu> ();
		}
		else
		{
			popupOpened = false;
			popupFullyOpened = false;
		}

//		// If scene is level disable bad breath particle
//		if (Application.loadedLevelName == "Level")
//		{
//			breathParticle.SetActive(true);
//		}

//		if (Application.loadedLevelName == "HospitalSelect")
//			GameObject.Find("BackButton").GetComponent<Button>().enabled = true;

		menu.SetActive (false);
	}

	/// <summary>
	/// Funkcija za prikaz poruke preko Log-a, prilikom klika na dugme
	/// </summary>
	/// /// <param name="message">poruka koju treba prikazati.</param>
	public void ShowMessage(string message)
	{
		Debug.Log(message);
	}
    
    public void Action_NextSceneInterstitial()
    {

    }



	/// <summary>
	/// Funkcija za prikaz CrossPromotion ExitInterstitial-a
	/// </summary>

	/// <summary>
	/// Funkcija koja podesava naslov dialoga kao i poruku u dialogu i ova f-ja se poziva iz skripte
	/// </summary>
	/// <param name="messageTitleText">naslov koji treba prikazati.</param>
	/// <param name="messageText">custom poruka koju treba prikazati.</param>
	public void ShowPopUpMessage(string messageTitleText, string messageText)
	{
		transform.Find("PopUps/PopUpMessage/AnimationHolder/Body/HeaderHolder/TextHeader").GetComponent<Text>().text=messageTitleText;
		transform.Find("PopUps/PopUpMessage/AnimationHolder/Body/ContentHolder/TextBG/TextMessage").GetComponent<Text>().text=messageText;
		ShowPopUpMenu(transform.Find("PopUps/PopUpMessage").gameObject);

	}

	/// <summary>
	/// Funkcija koja podesava naslov CustomMessage-a, i ova f-ja se poziva preko button-a zajedno za f-jom ShowPopUpMessageCustomMessageText u redosledu: 1-ShowPopUpMessageTitleText 2-ShowPopUpMessageCustomMessageText
	/// </summary>
	/// <param name="messageTitleText">naslov koji treba prikazati.</param>
	public void ShowPopUpMessageTitleText(string messageTitleText)
	{
		transform.Find("PopUps/PopUpMessage/AnimationHolder/Body/HeaderHolder/TextHeader").GetComponent<Text>().text=messageTitleText;
	}

	/// <summary>
	/// Funkcija koja podesava poruku CustomMessage, i poziva meni u vidu pop-upa, ova f-ja se poziva preko button-a zajedno za f-jom ShowPopUpMessageTitleText u redosledu: 1-ShowPopUpMessageTitleText 2-ShowPopUpMessageCustomMessageText
	/// </summary>
	/// <param name="messageText">custom poruka koju treba prikazati.</param>
	public void ShowPopUpMessageCustomMessageText(string messageText)
	{
		transform.Find("PopUps/PopUpMessage/AnimationHolder/Body/ContentHolder/TextBG/TextMessage").GetComponent<Text>().text=messageText;		
		ShowPopUpMenu(transform.Find("PopUps/PopUpMessage").gameObject);
	}

	/// <summary>
	/// Funkcija koja podesava naslov dialoga kao i poruku u dialogu i ova f-ja se poziva iz skripte
	/// </summary>
	/// <param name="dialogTitleText">naslov koji treba prikazati.</param>
	/// <param name="dialogMessageText">custom poruka koju treba prikazati.</param>
	public void ShowPopUpDialog(string dialogTitleText, string dialogMessageText)
	{
		transform.Find("PopUps/PopUpMessage/AnimationHolder/Body/HeaderHolder/TextHeader").GetComponent<Text>().text=dialogTitleText;
		transform.Find("PopUps/PopUpMessage/AnimationHolder/Body/ContentHolder/TextBG/TextMessage").GetComponent<Text>().text=dialogMessageText;
		ShowPopUpMenu(transform.Find("PopUps/PopUpMessage").gameObject);
	}

	/// <summary>
	/// Funkcija koja podesava naslov dialoga, ova f-ja se poziva preko button-a zajedno za f-jom ShowPopUpDialogCustomMessageText u redosledu: 1-ShowPopUpDialogTitleText 2-ShowPopUpDialogCustomMessageText
	/// </summary>
	/// <param name="dialogTitleText">naslov koji treba prikazati.</param>
	public void ShowPopUpDialogTitleText(string dialogTitleText)
	{
		transform.Find("PopUps/PopUpMessage/AnimationHolder/Body/HeaderHolder/TextHeader").GetComponent<Text>().text=dialogTitleText;
	}

	/// <summary>
	/// Funkcija koja podesava poruku dialoga i poziva meni u vidu pop-upa, ova f-ja se poziva preko button-a zajedno za f-jom ShowPopUpDialogTitleText u redosledu: 1-ShowPopUpDialogTitleText 2-ShowPopUpDialogCustomMessageText
	/// </summary>
	/// <param name="dialogMessageText">custom poruka koju treba prikazati.</param>
	public void ShowPopUpDialogCustomMessageText(string dialogMessageText)
	{
		transform.Find("PopUps/PopUpDialog/AnimationHolder/Body/ContentHolder/TextBG/TextMessage").GetComponent<Text>().text=dialogMessageText;		
		ShowPopUpMenu(transform.Find("PopUps/PopUpMessage").gameObject);
	}

	void Awake()
	{


		StartCoroutine(StartSceneWithLoadingHolderCoroutine());
	}

	public void ToggleSound()
	{
        this.transform.Find("Menus/MainMenu/ButtonsHolder/AnimationHolder/SettingsMenuHomeScene/ButtonSound/SoundOffIcon").gameObject.SetActive(!SoundManager.instance.ToggleSound());
    }

	void Update()
	{
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(transform.Find("PopUps/PopUpDialog")!=null&& transform.Find("PopUps/PopUpDialog").gameObject.activeSelf)
            {
                transform.Find("PopUps/PopUpDialog/AnimationHolder/Body/ButtonsHolder/ButtonNo").GetComponent<Button>().onClick.Invoke();
            }
            else
            {
                GameObject buttonHome = GameObject.Find("ButtonHome");
                if (buttonHome != null)
                    buttonHome.GetComponent<Button>().onClick.Invoke();
            }

            if (SceneManager.GetActiveScene().name == "MapScene")
                Application.Quit();

            if(SceneManager.GetActiveScene().name=="PackingHome")
            {
                if(transform.Find("PopUps/PackingDonePopUp").gameObject.activeSelf)
                {
                }
            }
        }
        
	}

	public void OpenLevelScene(string levelName)
	{
		Application.LoadLevel(levelName);
	}

	// Play button click sound
	public void PlayButtonClickSound()
	{
        //PAVLE SM MM ButtonClick
//		SoundManager.PlaySound("ButtonClick");
	}

	public void LoadSceneWithLoading(string sceneName)
	{
		StartCoroutine(LoadSceneWithLoadingCoroutine(sceneName));
	}

	IEnumerator LoadSceneWithLoadingCoroutine(string sceneName)
	{
		if (loadingHolder != null)
		{
			if (sceneName == "CharacterSelect")
			{
//				gameplayNativeAd.HideBothAds();
//				gameplayNativeAd.gameObject.SetActive(true);
				// Just in any casedsa call again after 0.1 sec
				yield return new WaitForSeconds(0.1f);
//				gameplayNativeAd.HideBothAds();
			}

			loadingHolder.GetComponent<CanvasGroup>().blocksRaycasts = true;

			// Play loading scene arrive animation
			loadingHolder.transform.GetChild(0).GetComponent<Animator>().Play("LoadingArriving", 0, 0);

			yield return new WaitForSeconds (2f);

			Application.LoadLevel(sceneName);
		}
	}

	IEnumerator StartSceneWithLoadingHolderCoroutine()
	{
		if (loadingHolder != null && GlobalVariables.playLoadingDepart)
		{
			loadingHolder.GetComponent<CanvasGroup>().blocksRaycasts = true;

			// Play loading scene depart animation
			loadingHolder.transform.GetChild(0).GetComponent<Animator>().Play("LoadingDeparting", 0, 0);

			yield return new WaitForSeconds (1f);

			if (!GlobalVariables.playCharacterUnlockAnimation)
				loadingHolder.GetComponent<CanvasGroup>().blocksRaycasts = false;
		}
	}

	public void HomeButtonPressed()
	{
		GlobalVariables.playLoadingDepart = true;

        // Home interstitial
        Debug.Log("We call interstitial: Home");


		if (Application.loadedLevelName == "Level")
			StartCoroutine(LoadSceneWithLoadingCoroutine("MainScene"));

	}



	public void GoToNextPatient()
	{
		GlobalVariables.playLoadingDepart = true;

//		StartCoroutine("ShowPatientFinishedInterstitial");

		if (Application.loadedLevelName == "Level")
			StartCoroutine(LoadSceneWithLoadingCoroutine("CharacterSelect"));

	}

	public void NextPatientPressed()
	{
		// Check for last unlocked character and unlock one if needed
		if (GlobalVariables.unlockNewLevelWhenLastLevelPassed)
		{
			// If current character index is larger than loast unlocked character than unlock nex one
			if (GlobalVariables.selectedCharacterIndex == GlobalVariables.lastUnlockedCharacterIndex)
			{
				switch (GlobalVariables.selectedCharacterIndex + 1)
				{
				case 1:
					GlobalVariables.characterLocks[GlobalVariables.selectedCharacterIndex + 1] = "sugi";
					break;
				case 2:
					GlobalVariables.characterLocks[GlobalVariables.selectedCharacterIndex + 1] = "bugi";
					break;
				case 3:
					GlobalVariables.characterLocks[GlobalVariables.selectedCharacterIndex + 1] = "botko";
					break;
				case 4:
                        GlobalVariables.characterLocks[GlobalVariables.selectedCharacterIndex + 1] = "smotko";
					break;
				case 5:
					GlobalVariables.characterLocks[GlobalVariables.selectedCharacterIndex + 1] = "tatko";
					break;
				case 6:
					GlobalVariables.characterLocks[GlobalVariables.selectedCharacterIndex + 1] = "ratko";
					break;
				case 7:
					GlobalVariables.characterLocks[GlobalVariables.selectedCharacterIndex + 1] = "bulatko";
					break;
				case 8:
					GlobalVariables.characterLocks[GlobalVariables.selectedCharacterIndex + 1] = "bekrim";
					break;
				case 9:
					GlobalVariables.characterLocks[GlobalVariables.selectedCharacterIndex + 1] = "gazmen";
					break;
				}

//				if (GlobalVariables.lastUnlockedCharacterIndex + 1 < GlobalVariables.characterLocks.Length)
//				{
//					GlobalVariables.lastUnlockedCharacterIndex = GlobalVariables.lastUnlockedCharacterIndex + 1;
//					string characters = string.Join(",", GlobalVariables.characterLocks);
//					PlayerPrefs.SetString("Karakteri", characters);
//					PlayerPrefs.Save();
//
//					GlobalVariables.playCharacterUnlockAnimation = true;
//				}
			}
		}
		else
		{
			// Just unlock next character
			switch (GlobalVariables.lastUnlockedCharacterIndex + 1)
			{
			case 1:
				GlobalVariables.characterLocks[GlobalVariables.lastUnlockedCharacterIndex + 1] = "sugi";
				break;
			case 2:
				GlobalVariables.characterLocks[GlobalVariables.lastUnlockedCharacterIndex + 1] = "bugi";
				break;
			case 3:
				GlobalVariables.characterLocks[GlobalVariables.lastUnlockedCharacterIndex + 1] = "botko";
				break;
			case 4:
				GlobalVariables.characterLocks[GlobalVariables.lastUnlockedCharacterIndex + 1] = "smotko";
				break;
			case 5:
				GlobalVariables.characterLocks[GlobalVariables.lastUnlockedCharacterIndex + 1] = "tatko";
				break;
			case 6:
				GlobalVariables.characterLocks[GlobalVariables.lastUnlockedCharacterIndex + 1] = "ratko";
				break;
			case 7:
				GlobalVariables.characterLocks[GlobalVariables.lastUnlockedCharacterIndex + 1] = "bulatko";
				break;
			case 8:
				GlobalVariables.characterLocks[GlobalVariables.lastUnlockedCharacterIndex + 1] = "bekrim";
				break;
			case 9:
				GlobalVariables.characterLocks[GlobalVariables.lastUnlockedCharacterIndex + 1] = "gazmen";
				break;
			}

			GlobalVariables.lastUnlockedCharacterIndex = GlobalVariables.lastUnlockedCharacterIndex + 1;
			string characters = string.Join(",", GlobalVariables.characterLocks);
			PlayerPrefs.SetString("Karakteri", characters);
			PlayerPrefs.Save();

			GlobalVariables.playCharacterUnlockAnimation = true;
		}

		GlobalVariables.playLoadingDepart = true;



		StartCoroutine(LoadSceneWithLoadingCoroutine("CharacterSelect"));
	}

	public void PlayButtonPressed()
	{
		GlobalVariables.playLoadingDepart = false;


		loadingHolder.GetComponent<CanvasGroup>().blocksRaycasts = true;

		Application.LoadLevel("CharacterSelect");
	}

	// If video is not available for plaster, hammer or thermometer, show appropriate native ad
	public void VideoNotAvailableShowAppropriateNativeAd()
	{

	}

    Transform transition;


    public void LoadSceneWithTransition(string scene,float delay=2f)
    {
        transition.GetComponent<SpriteRenderer>().enabled = true;
        transition.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        transition.GetComponent<Animator>().SetTrigger("arrive");
        Timer.Schedule(this, delay, () =>
            {
                SceneManager.LoadScene(scene);
            });
    }

    public void LoadSceneWithTransition(string scene)
    {
        LoadSceneWithTransition(scene,2f);
    }

    void LoadSceneWithTransitionAndInterstitial(string scene,float delay)
    {
        transition.GetComponent<SpriteRenderer>().enabled = true;
        transition.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        transition.GetComponent<Animator>().SetTrigger("arrive");
        Timer.Schedule(this, delay, () =>
            {
                AdsManager.Instance.ShowInterstitial();
                SceneManager.LoadScene(scene);
            });
    }

    public void LoadSceneWithNextInterstitial(string scene)
    {
        LoadSceneWithTransitionAndInterstitial(scene, 2f);
    }

    public void PlayTransitionNoLoadScene()
    {
        transition.GetComponent<SpriteRenderer>().enabled = true;
        transition.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        transition.GetComponent<Animator>().SetTrigger("arrive");
    }


    

  
}
