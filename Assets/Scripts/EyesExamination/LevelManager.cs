using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System;

///<summary>
///<para>Scene:All/NameOfScene/NameOfScene1,NameOfScene2,NameOfScene3...</para>
///<para>Object:N/A</para>
///<para>Description: Sample Description </para>
///</summary>

public class LevelManager : MonoBehaviour {

	public int distanceBetweenItems;
	public int distanceBetweenPhases;

	public GameObject successParticleHolder;

	public List<int> phaseQuests; // index represents current phase, and data means how many quests has this phase
	public List<int> phaseQuestsCompleted; // index represents current phaze, and data means how many quests are completed
	public int currentPhase;

	public GameObject[] phasesHolder; // index represents current phase

	public GameObject previousPhaseButton;
	public GameObject nextPhaseButton;
	public GameObject cameraPhaseButton;
	public Animator phasesAnimator;

	// Items holder animator
	public Animator itemsHolderAnimator;

	// Active item holder so that he can be above everything
	public GameObject activeItem;

	// List of items that are outside of items holder
	public GameObject[] outerItems;
	public GameObject innerItemsHolder;

//	// Character holder
	public GameObject characterHolder;
//	public Image frontHandImageHolder;
//	public Image backHandImageHolder;
//	public Image[] fingerImageHolders;
//	public Image[] iceFingersHolders;
//	public Image[] fingerCallusesHolders;
//	public Image[] frontHandCutsHolders;
//	public Image[] frontHandCutsCremeHolders;
//	public Image[] glassImagesHolders;
//	public Image[] backHandCutsHolders;
//	public Image biteImageHolder;
//	public Image sewingWoundImageHolder;
//	public Image frontHandExtensionImageHolder;
//	public Image backHandExtensionImageHolder;
//	public Image[] zanoktice1Holders;
//	public Image[] zanoktice2Holders;
	public Animator characterAnimator;

//    public BadgesScript badges;

	// Array of character prefabs and sprites
	public GameObject[] characters;
//	public Sprite[] handBacks;
//	public Sprite[] handFronts;
//	public Sprite[] fingers;
//	public Sprite[] iceFingers;
//	public Sprite[] fingerCalluses;
//	public Sprite[] frontHandCuts;
//	public Sprite[] frontHandCremeCuts;
//	public Sprite[] glassImages;
//	public Sprite[] backHandCuts;
//	public Sprite[] biteImages;
//	public Color[] sewingWoundColours;
//	public Sprite[] frontHandExtensions;
//	public Sprite[] backHandExtensions;
//	public Sprite[] zanoktice1;
//	public Sprite[] zanoktice2;
//
//	public Sprite[] dressingSprites;

	// Settings button holder
	public GameObject settingsButtonHolder;

	// Flash animator
	public Animator flashAnimator;

	// Camera panel holder - it's not actually panel, it's just holder for camera button
	public GameObject cameraPanel;

	public MenuManager menuManager;

	// When taking picture turn hand button holder
	public GameObject turnHandButton;
	public GameObject nextPatientButton;

	// Clock animation holder
	public GameObject clockAnimationHolder;

	// Hand animator holder
	public Animator handAnimator;

	public static LevelManager levelManager;

	public static bool allItemsDone;

    public GameObject injuriesHolder;
    public GameObject injuries2Holder;
	public GameObject injuries3Holder;

	// List of all items
	public GameObject[] allItems;
    public GameObject[] zeroItemPhases; //Phases that start with popup, not with items

	// Finished item Using Markers
	public GameObject[] finishedItemsMarkers;

	// Buttons holders
	public GameObject mainButtonsHolder;
	public GameObject woundCloseButton;
	public GameObject bonesCloseButton;
//	public GameObject powderCloseButton;
	public GameObject throatCloseButton;

	// Tutorial holders
	public GameObject tutorialHolder;
	public GameObject tutorialHolder2;

	// CameraButtonItem
	public GameObject cameraButtonItem;

	// items holder FIXME mozda ovo da se nekako izegne, jer mi samo sluzi da ne bi mogao na on pause da mi pozove da otvori usta, tj. character idle
	public GameObject itemsHolder;

	// Dentist
	public GameObject waterInMouth;

	public GameObject sidetoolsHolder;

	public GameObject pictureSavedPopup;

	[HideInInspector]
	public bool toothOneSet;
	[HideInInspector]
	public bool toothTwoSet;

	public bool CheckIfPhaseIsFinished()
	{
		if (phaseQuests[currentPhase] == phaseQuestsCompleted[currentPhase])
		{
            Debug.Log("Badge earned for phase: " + currentPhase);
//            badges.EarnBadge(currentPhase);
			return true;
		}
		else
			return false;
	}

    public void CheckFromPopUpClose()
    {
        CheckIfPhaseIsFinished();
    }

	public IEnumerator ActivatePhase()
	{

		yield return new WaitForSeconds(0.1f);

		for (int i = 0; i < phasesHolder.Length; i++)
		{
			if (i == currentPhase)
				phasesHolder[i].SetActive(true);
			else
			{
				if (phasesHolder[i] != phasesHolder[currentPhase])
					phasesHolder[i].SetActive(false);
			}
		}
	}

    void Update()
    {
        if (Input.GetKeyDown("n"))
            NextPhaseClicked();
    }

	public void NextPhaseClicked()
	{
        DeactivateInnerItemsMovement();
        currentPhase++;
        phasesAnimator.SetInteger("Counter",currentPhase);
        ResetCheckMarks();

        Invoke("CheckIfGameIsFinished", 0.5f);
    }

    void CheckIfGameIsFinished()
    {
        switch (currentPhase)
        {
            case 1:
                injuriesHolder.SetActive(false);
                characterAnimator.Play("CharacterEZoom2", 0, 0);
                StartCoroutine(ActivateInjuries2());
                break;
            case 3:
                injuries2Holder.SetActive(false);
                characterAnimator.Play("CharacterEZoom2Reverse",0,0);
                StartCoroutine(ActivateInjuries3());
                break;
            case 4:
                menuManager.JustShowPopUpMenu(zeroItemPhases[0]);
                break;
            case 5:
                //FIXME set eyes to correct position for this phase
                characterAnimator.enabled = false;
                GameObject.Find("Canvas/MainPanel/CharacterHolder").transform.GetChild(0).Find("AnimationHolder/HipHolder/BodyHolder/NeckHolder/HeadHolder/NormalHeadHolder/EyesNHolder/EyeLeftNHolder/EyeBallLeftNImage").position = GameObject.Find("Canvas/MarkerLeftEye").transform.position;
                GameObject.Find("Canvas/MainPanel/CharacterHolder").transform.GetChild(0).Find("AnimationHolder/HipHolder/BodyHolder/NeckHolder/HeadHolder/NormalHeadHolder/EyesNHolder/EyeRightNHolder/EyeBallRightNImage").position = GameObject.Find("Canvas/MarkerRightEye").transform.position;
                break;
            case 6:
                menuManager.JustShowPopUpMenu(zeroItemPhases[1]);
                characterAnimator.enabled = true;
                characterAnimator.Play("CharacterENormalIdle",0,0);
                characterHolder.GetComponent<Animator>().Play("CharacterZoomAnimationReverse", 0, 0);
                break;
            case 7:
                Debug.Log("Game Over ==> Next Patient");
                nextPatientButton.SetActive(true);
                break;
            default:
                break;
        }
        Invoke("ActivateInnerItemsMovement",1.5f);
    }

    IEnumerator ActivateInjuries2()
    {
        yield return new WaitForSeconds(1.3f);
        injuries2Holder.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        injuries2Holder.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        injuries2Holder.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
        injuries2Holder.transform.GetChild(2).GetChild(3).gameObject.SetActive(true);
        injuries2Holder.transform.GetChild(2).GetChild(4).gameObject.SetActive(true);
        injuries2Holder.transform.GetChild(2).GetChild(5).gameObject.SetActive(true);
        yield return new WaitForEndOfFrame();

    }

    IEnumerator ActivateInjuries3()
    {
        yield return new WaitForSeconds(1.3f);
        injuries3Holder.SetActive(true);
    }

    void ResetCheckMarks()
    {
        for (int i = 0; i < finishedItemsMarkers.Length; i++)
        {
            finishedItemsMarkers[i].GetComponent<Animator>().Play("ItemNotDone",0,0);
        }
    }

	IEnumerator SetItemMarkers()
	{
		yield return new WaitForSeconds(0.7f);

	}

	public void DeactivateOuterItemsMovement()
	{
		for (int i = 0; i < outerItems.Length; i++)
		{
			if (outerItems[i].GetComponent<ItemDragScript>() != null)
				outerItems[i].GetComponent<ItemDragScript>().enabled = false;
		}
	}

	public void DeactivateInnerItemsMovement()
	{
		foreach(Transform t in innerItemsHolder.transform)
		{
			if (t.GetComponent<ItemDragScript>() != null)
				t.GetComponent<ItemDragScript>().enabled = false;
		}
	}

	public void ActivateInnerItemsMovement()
	{
		foreach(Transform t in innerItemsHolder.transform)
		{
			if (t.GetComponent<ItemDragScript>() != null)
				t.GetComponent<ItemDragScript>().enabled = true;
		}
	}

	// Open and close settings
	public void OpenSettings()
	{
		if (settingsButtonHolder.transform.GetChild(0).GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
			settingsButtonHolder.transform.GetChild(0).GetComponent<Animator>().Play("OpenSettings", 0 , 0);
	}

	public void CloseSettings()
	{
		if (settingsButtonHolder.transform.GetChild(0).GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
			settingsButtonHolder.transform.GetChild(0).GetComponent<Animator>().Play("CloseSettings", 0 , 0);
	}

	public void CameraButtonPhaseClicked()
	{
		cameraPhaseButton.GetComponent<CanvasGroup>().alpha = 0;
		cameraPhaseButton.GetComponent<CanvasGroup>().interactable = false;

		nextPatientButton.SetActive(true);
	}

	public void NextPatientButtonClicked()
	{
		Application.LoadLevel("CharacterSelect");
	}

	public void PlayClockAnimation()
	{
		StartCoroutine(PlayClockAnimationCoroutine());
	}

	public IEnumerator PlayClockAnimationCoroutine()
	{
        // Play sound 
        SoundManagerEyeExamination.PlaySound("SmallClockSound");

        clockAnimationHolder.SetActive(true);
		clockAnimationHolder.transform.GetChild(0).GetComponent<Animator>().Play("SmallClockActive", 0 , 0);

		yield return new WaitForSeconds(3.9f);

		clockAnimationHolder.SetActive(false);
	}

	// Turn hand
	public void ToggleTurnHandButton()
	{
		if (handAnimator.GetCurrentAnimatorStateInfo(0).IsName("FrontToBackTurn"))
			handAnimator.Play("BackToFrontTurn", 0, 0);
		else if (handAnimator.GetCurrentAnimatorStateInfo(0).IsName("BackToFrontTurn"))
			handAnimator.Play("FrontToBackTurn", 0, 0);

		// FIXME test da vidim kad opalim character clothes animaciju
		characterAnimator.Play("CharacterClothes", 0, 0);
	}

	int numberOfPhases;
	List<string> phasesQuests = new List<string>();
	List<int> thisCharacterItemsIndices = new List<int>();
	List<int> itemDependOnThisItemIndices = new List<int>();
	List<int> itemNeedToBeDoneBeforeIndices = new List<int>();

    void Awake()
	{
		allItemsDone = false;

		// Dentist tooth setting
		toothOneSet = false;
		toothTwoSet = false;

		// Creation of phases and elements
		numberOfPhases = 0;
		List<string> phasesQuests = new List<string>();
		string[] thisCharacterItemsIndicesStringValue;
		string[] itemDependOnThisItemStringValue;
		string[] itemNeedToBeDoneBeforeStringValue;

		TextAsset textAsset = (TextAsset) Resources.Load("Levels");  
		XmlDocument xmldoc = new XmlDocument ();
		xmldoc.LoadXml ( textAsset.text );

		XmlNodeList appNodes = xmldoc.SelectNodes("/xml/level");

		foreach (XmlNode node in appNodes)
		{
			if (node.Attributes["id"].Value == (GlobalVariables.selectedCharacterIndex + 1).ToString())
			{
				numberOfPhases = Int32.Parse(node.SelectSingleNode("numberOfPhases").InnerText);
				string[] phasesQuestsString = node.SelectSingleNode("itemsPerPhase").InnerText.Split(',');
				thisCharacterItemsIndicesStringValue = node.SelectSingleNode("itemsList").InnerText.Split(',');
				itemDependOnThisItemStringValue = node.SelectSingleNode("itemDependsOnThisItemList").InnerText.Split(',');
				itemNeedToBeDoneBeforeStringValue = node.SelectSingleNode("itemThatNeedsToBeDoneBeforeThisOne").InnerText.Split(',');

				for (int i = 0; i < phasesQuestsString.Length; i++)
				{
					phasesQuests.Add(phasesQuestsString[i]);
				}

				for (int i = 0; i < thisCharacterItemsIndicesStringValue.Length; i++)
				{
					thisCharacterItemsIndices.Add(Int32.Parse(thisCharacterItemsIndicesStringValue[i]));
					itemDependOnThisItemIndices.Add(Int32.Parse(itemDependOnThisItemStringValue[i]));
					itemNeedToBeDoneBeforeIndices.Add(Int32.Parse(itemNeedToBeDoneBeforeStringValue[i]));
				}
				break;
			}
		}

		//inicijalizacija itema	
		int tempItemsPlaced = 0;
        int tempParse = 0;
        for (int i = 0; i < numberOfPhases; i++)
        {
            tempParse = Int32.Parse(phasesQuests[i]);

            if (tempParse > 0)
            {
                phaseQuests.Add(tempParse);
                phaseQuestsCompleted.Add(0);
                for (int j = tempItemsPlaced; j < tempItemsPlaced+tempParse; j++)
                {
//                    Debug.Log("Phase: " + i + " Item in AllItems" + thisCharacterItemsIndices[j] + " no of items " + tempParse);
                    if (allItems[thisCharacterItemsIndices[j]].GetComponent<HoldItemOverItem>() != null)
                    {
                        allItems[thisCharacterItemsIndices[j]].GetComponent<HoldItemOverItem>().SetItemAwake();
                        allItems[thisCharacterItemsIndices[j]].GetComponent<ItemDragScript>().ItemDragAwake();
                    }
                    else if (allItems[thisCharacterItemsIndices[j]].GetComponent<TapingItem>() != null)
                    {
                        allItems[thisCharacterItemsIndices[j]].GetComponent<TapingItem>().SetItemAwake();
                        allItems[thisCharacterItemsIndices[j]].GetComponent<ItemDragScript>().ItemDragAwake();
                    }
                }
            }
            else
            {
                phaseQuests.Add(1);
                phaseQuestsCompleted.Add(0);
            }

            tempItemsPlaced+=tempParse;
        }
        


		// Destroy all other items, but before destroying them destroy their targets.
		for (int i = 0; i < allItems.Length; i++)
		{
			if (!thisCharacterItemsIndices.Contains(i))
			{
				if (allItems[i].GetComponent<HoldItemOverItem>() != null)
				{
					for (int j = 0; j < allItems[i].GetComponent<HoldItemOverItem>().allTargetItems.Count; j++)
					{
						Destroy(allItems[i].GetComponent<HoldItemOverItem>().allTargetItems[j]);
					}

					Destroy(allItems[i]);
				}
				else if (allItems[i].GetComponent<TapingItem>() != null)
				{
					for (int j = 0; j < allItems[i].GetComponent<TapingItem>().allTargetItems.Count; j++)
					{
						Destroy(allItems[i].GetComponent<TapingItem>().allTargetItems[j]);
					}

					Destroy(allItems[i]);
				}
			}
		}

		// Instantiate character
		Vector3 characterOriginalScale = characters[GlobalVariables.selectedCharacterIndex].transform.localScale;
		Vector2 characterOriginalPosition = characters[GlobalVariables.selectedCharacterIndex].GetComponent<RectTransform>().anchoredPosition;
		GameObject go = Instantiate(characters[GlobalVariables.selectedCharacterIndex], Vector3.zero, characters[GlobalVariables.selectedCharacterIndex].transform.rotation) as GameObject;

		// Set parent to character holder
		go.transform.SetParent(characterHolder.transform);

		// Set character scale and position
		go.transform.localScale = characterOriginalScale;
		go.transform.localPosition = characterOriginalPosition;

		// Set character animator
		characterAnimator = go.transform.GetChild(0).GetComponent<Animator>();


		injuriesHolder.SetActive(false);

		// Set buttons depending if remove ads has been bought
		SetButtonsAcordingToRemoveAds();

		// Set items so be invisible and not interactable
		itemsHolderAnimator.GetComponent<CanvasGroup>().alpha = 0;
		itemsHolderAnimator.GetComponent<CanvasGroup>().interactable = false;

		levelManager = this;
	}

	public void SetButtonsAcordingToRemoveAds()
	{
//		if (GlobalVariables.removeAdsOwned)
//		{
//			mainButtonsHolder.GetComponent<RectTransform>().anchoredPosition = new Vector2(settingsButtonHolder.GetComponent<RectTransform>().anchoredPosition.x, 130f);
//			throatCloseButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(-62f, -59f);
//			tutorialHolder.GetComponent<RectTransform>().anchoredPosition = new Vector2(106f, -140f);
//			tutorialHolder2.GetComponent<RectTransform>().anchoredPosition = new Vector2(106f, -140f);

//			GameObject.Find("PhaseOneTreatmentBoard").GetComponent<RectTransform>().anchoredPosition = new Vector2(276f, 555f);
//			GameObject.Find("PhaseTwoTreatmentBoard").GetComponent<RectTransform>().anchoredPosition = new Vector2(276f, 555f);
//		}
	}

	// ---------------------------- Dentist functions -----------------------------------
	public GameObject startGameHandClickHolder;

	public void CharacterZoomIn()
	{
		StartCoroutine(CharacterZoomInCoroutine());
	}

	public IEnumerator CharacterZoomInCoroutine()
	{
		// Destroy button for character zoom
		Destroy(GameObject.Find("CharacterZoomButton"));

        // FIXME Play "Say AAAA sound"
        SoundManagerEyeExamination.PlaySound("PressurePumpSound");

        Destroy(startGameHandClickHolder);

		// Wait for sound to finish
		yield return new WaitForSeconds(1f);

		// Play zoom in and open mouth animation in delays
		characterHolder.GetComponent<Animator>().Play("CharacterZoomAnimation", 0, 0);

		yield return new WaitForSeconds(0.2f);

		// Set items active
		itemsHolder.SetActive(true);

		// Open mouth and show items
		characterAnimator.Play("CharacterEZoom1", 0, 0);
		itemsHolderAnimator.Play("ShowItems", 0, 0);

		yield return new WaitForSeconds(0.14f);



		yield return new WaitForSeconds(0.3f);

		// Enable clicking on items by removing cover
		Destroy(GameObject.Find("ItemsCoverImage"));

		// Set next phase button active
//		nextPhaseButton.SetActive(true);

		// FIXME First tools arrive
        GameObject.Find("EyesInjuries").SetActive(false);
		injuriesHolder.SetActive(true);

		// Find water in mouth and set it for the item
//		waterInMouth = GameObject.Find("WaterRemovalPlace");
//		sidetoolsHolder.transform.GetChild(1).GetComponent<SideTool>().target = waterInMouth;
	}

//	public void ActivateWaterInMouth()
//	{
//		if (!waterInMouth.GetComponent<BoxCollider2D>().enabled)
//		{
//			// Play filling animatiomn
//			waterInMouth.transform.GetChild(0).GetComponent<Animator>().Play("WaterFill", 0, 0);
//
//			// Enable collider for cleaning water
//			waterInMouth.GetComponent<BoxCollider2D>().enabled = true;
//		}
//	}

	// Promenljive i funkcije za prikazivanje video reklame
	public GameObject showVideoRewardPopup;

	public void ShowVideoRewardPopup()
	{
		menuManager.ShowPopUpMenu(showVideoRewardPopup);
	}

	public void ShowVideo()
	{

	}
}

