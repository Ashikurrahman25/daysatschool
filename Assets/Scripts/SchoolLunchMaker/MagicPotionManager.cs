using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class MagicPotionManager : MonoBehaviour {

	public GameObject[] missionItems;

	// Potions indices and sprite images position must match
	public Sprite[] allPotions;

	// Used for starting the game (ItemDragAwake must be called)
	public GameObject[] potionObjects;

	public List<int> indices;
	public List<int> usedIndices;

	public float potionsDraggingSpeed;
	public float potionsReturningSpeed;

	public bool firstTimeStarted = false;

	public bool pouringSequenceStarted;

	public Image liquidImage1;
	public Image liquidImage2;
	public Image liquidImage3;

	public GameObject liquidParticle;
	
	public GameObject puffParticleHolder;

	public GameObject potionPouringPosition;

	private bool gameStarted = false;

	public GameObject clickBlocker;

	public GameObject blender;

    public GameObject glass;



	public bool gameFinished;


	public Image potionLockImage;
	public Sprite potionAutoUnlockedSprite;


	public static MagicPotionManager magicPotionManager;

    int juicesFinished;
    public void Awake()
    {
        Input.multiTouchEnabled = false;
    }

    void OnEnable()
	{
		if (!gameStarted)
		{
			//  8 jer cemo za sada imati 8 potiona
			indices = new List<int>();
			for (int i = 0; i < 8; i++)
			{
				indices.Add(i);
			}

			usedIndices = new List<int>();

			magicPotionManager = this;

			StartGame ();
		}


		UnblockClicks();
	}

	public void StartGame()
	{
		StartCoroutine(StartGameCoroutine());
		//RestartMissionPotions();
	}

	IEnumerator StartGameCoroutine()
	{
		yield return new WaitForSeconds(0.1f);

		

		gameStarted = true;

		gameFinished = false;


		

		RestartMissionPotions();
	}

	public void CheckIfPotionIsMission(int index)
	{
		bool usedAtLeastOneItem = false;

		for (int i = 0; i < missionItems.Length; i++)
		{
			if (missionItems[i].GetComponent<PotionScript>().potionIndex == index)
			{
				//missionItems[i].GetComponent<Image>().sprite = null;

				if (!missionItems[i].GetComponent<PotionScript>().potionUsed)
				{
					missionItems[i].GetComponent<PotionScript>().potionUsed = true;
					usedAtLeastOneItem = true;

					// Play used mission potion
					missionItems[i].GetComponent<Animator>().Play("MissionPotionUsed", 0, 0);

					// SoundManager.Instance.PlayBacteriaKillSound();

					CheckIfGameIsFinished();
				}
			}
		}

		if (!usedAtLeastOneItem)
			RestartMissionPotions(true);
		else
		{
			MagicPotionManager.magicPotionManager.pouringSequenceStarted = false;
		}
	}

	public void ChangeLiquidColor(Color c)
	{
		StartCoroutine(ChangeColorCoroutine(c));
        glass.transform.GetChild(0).GetComponent<Image>().color = c;
        ParticleSystem.MainModule main = blender.transform.Find("AnimationHolder/Liquid").GetComponent<ParticleSystem>().main;
        main.startColor = c;
        //blender.transform.Find("AnimationHolder/Liquid").GetComponent<ParticleSystem>(). = main;

        switch (juicesFinished)
        {
            case 0:
                GlobalVariables.juices[0] = c;
                break;

            case 1:
                GlobalVariables.juices[1]= c;

                break;

            case 2:
                GlobalVariables.juices[2] = c;

                break;

            case 3:
                GlobalVariables.juices[3] = c;

                break;
            default:
                break;
        }
	}

	IEnumerator ChangeColorCoroutine(Color c)
	{
		yield return new WaitForSeconds(0.2f);

		float timer = 0f;

		while (timer < 1f)
		{
			liquidImage1.color = Color.Lerp(liquidImage1.color, c,timer);
			liquidImage2.color = Color.Lerp(liquidImage3.color, c, timer);
			liquidImage3.color = Color.Lerp(liquidImage3.color, c, timer);
			liquidParticle.GetComponent<ParticleSystem>().startColor = c;
			puffParticleHolder.GetComponent<ParticleSystem>().startColor = c;
			yield return new WaitForSeconds(0.02f);
			timer += 0.02f;
		}
	}

//	public void RemoveUsedItemFromMissionItems(int index)
//	{
//		for (int i = 0; i < missionItems.Length; i++)
//		{
//			if (missionItems[i].GetComponent<PotionScript>().potionIndex == in)
//		}
//	}

	public void CheckIfGameIsFinished()
	{
		gameFinished = true;

		for (int i = 0; i < missionItems.Length; i++)
		{
			if (!missionItems[i].GetComponent<PotionScript>().potionUsed)
				gameFinished = false;
		}

		if (gameFinished)
			GameCompleted();
	}

    public void RestartMissionPotions(bool puff=false)
	{
        StartCoroutine (RestartMissionCoroutine(puff));
	}

    IEnumerator RestartMissionCoroutine(bool puff)
	{
        
		if (firstTimeStarted)
		{
			// closeButton.GetComponent<Button>().interactable = false;

			// Set this flag so we cant start automatic pouring if mission is restarting

			yield return new WaitForSeconds(0.7f);

			// Play puff particle
            if(puff)
			puffParticleHolder.GetComponent<ParticleSystem>().Play ();
            SoundManagerPotionMaker.PlaySound("WrongFruitBlenderedSound");

            yield return new WaitForSeconds(1.2f);
            ChangeLiquidColor(Color.white);

			missionItems[0].transform.parent.GetComponent<Animator>().Play ("SwapMissionPotions", 0, 0);
			
			yield return new WaitForSeconds(0.34f);
			
			indices.AddRange(usedIndices);
			usedIndices.Clear();
			
			for (int i = 0; i < missionItems.Length; i++)
			{
				int r = Random.Range(0, indices.Count);
				
				missionItems[i].GetComponent<Image>().sprite = allPotions[indices[r]];
				missionItems[i].GetComponent<PotionScript>().potionIndex = indices[r];
				missionItems[i].GetComponent<PotionScript>().potionUsed = false;
				missionItems[i].GetComponent<Animator>().Play("MissionPotionIdle", 0, 0);
				usedIndices.Add(indices[r]);
				indices.RemoveAt(r);
			}

			MagicPotionManager.magicPotionManager.pouringSequenceStarted = false;

//			closeButton.GetComponent<Button>().interactable = true;

		}
		else
		{
			firstTimeStarted = true;

			indices.AddRange(usedIndices);
			usedIndices.Clear();
			
			for (int i = 0; i < missionItems.Length; i++)
			{
				int r = Random.Range(0, indices.Count);
				
				missionItems[i].GetComponent<Image>().sprite = allPotions[indices[r]];
				missionItems[i].GetComponent<PotionScript>().potionIndex = indices[r];
				missionItems[i].GetComponent<PotionScript>().potionUsed = false;
				missionItems[i].GetComponent<Animator>().Play("MissionPotionIdle", 0, 0);
				usedIndices.Add(indices[r]);
				indices.RemoveAt(r);
			}
		}

		pouringSequenceStarted = false;
	}

	public void GameCompleted()
	{
		StartCoroutine(GameCompletedCoroutine());
        blender.transform.GetChild(0).GetComponent<Animator>().Play("BlenderPour");
        ++juicesFinished;
        GameObject canvas = GameObject.Find("Canvas");
        if(juicesFinished==4)
        {
            Timer.Schedule(this, 1f, delegate
                {
                    canvas.GetComponent<MenuManager>().PlayTransitionNoLoadScene();
                });
            Timer.Schedule(this, 3f, delegate
                {
                    this.gameObject.SetActive(false);
                    for(int i=1;i<=4;i++)
                    {
                        canvas.transform.Find("Kitchen/Table/Tray/Glass"+i.ToString()+"/Juice").GetComponent<Image>().color=GlobalVariables.juices[i-1];
                        canvas.transform.Find("Kitchen/Table/Tray/Glass"+i.ToString()+"/Juice").GetComponent<Image>().fillAmount=1;
                    }
                    canvas.transform.Find("TopUI/FinishButton").gameObject.SetActive(true);
                    canvas.transform.Find("Kitchen/Cook").gameObject.SetActive(false);
                });
        }
        else
        {
            RestartMissionPotions();
        }
	}
	
	IEnumerator GameCompletedCoroutine()
	{
        
		//SoundManager.Instance.PlaySuccessParticleSound();

		// PLay success particle
//		LevelManager.levelManager.successParticleHolder.GetComponent<ParticleSystem>().Play();


		// int itemIndex = item.targetItems.IndexOf(item.activeGameobject);

		// item.itemsDone[itemIndex] = true;

		// item.activeGameobject.GetComponent<BoxCollider2D>().enabled = false;
//		liquidImage1.transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = false;
//		liquidImage2.transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = false;
//		liquidImage3.transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = false;
//		blender.GetComponent<BoxCollider2D>().enabled = false;

		// Check if this phase is completed
		//if (item.CheckIfAllItemsAreFinished())
		//{
			// LevelManager.levelManager.phaseQuestsCompleted[LevelManager.levelManager.currentPhase]++;

			//item.tutorialCheckImageObjectHolder.transform.GetChild(0).GetComponent<Animator>().Play("CheckIconFinished", 0, 0);

			//item.finishedUsingItemMarkHolder.GetComponent<Animator>().Play("ItemDoneAnimation", 0, 0);

			//			if (LevelManager.levelManager.CheckIfPhaseIsFinished())
			//			{
			//				LevelManager.levelManager.successParticleHolder.particleSystem.Play();
			//			}
		//}

		// Set item graphics for item
//		if (item.GetComponent<ClickItem>() != null)
//			item.GetComponent<ClickItem>().SwapGraphicsWhenMiniGameIsCompleted();

//		GetComponent<ItemGame>().GameFinished();

//		yield return new WaitForSeconds(1.5f);

		//LevelManager.levelManager.transitionAnimationHolder.GetComponent<Animator>().Play("TransitionAnimation", 0 , 0);

		yield return new WaitForSeconds(0.45f);

		// FIXME za sada cu i da zatvorim prozor u koliko korisnik ne uradi to
//		GameObject.Find("Canvas").GetComponent<MenuManager>().ClosePopUpMenu(gameObject);

		// craneObject.GetComponent<CraneControlls>().StopAllCoroutines();
		Debug.Log("Game finished!");
		
		// FIXME za sada nakon pola sekunde neka otvori opet doctor menu i da pozove sledecu igru
		// Finished crane game
		//LevelManager.levelManager.menuManager.ShowMenu(LevelManager.levelManager.doctorMenu);
		//LevelManager.levelManager.FinishedUsingItem();
	}

	public void OnMiniGameClosed()
	{
//		// Set buttons and enable interaction
//		item.GetComponent<HoldItemOverItem>().buttonsHolder.SetActive(true);
//		item.GetComponent<HoldItemOverItem>().tutorialObjectHolder.SetActive(false);
//
//		LevelManager.levelManager.menuManager.magicPotionNativeAd.CancelLoading();
//		LevelManager.levelManager.menuManager.magicPotionNativeAd.HideNativeAd();
	}

	public void BlockClicks()
	{
		clickBlocker.SetActive(true);
	}

	public void UnblockClicks()
	{
		clickBlocker.SetActive(false);
	}

	public void VideoWatchedRewardPlayer()
	{
//		GameCompleted();
		MakePotionAutomatically();
	}

	public void MakePotionAutomatically()
	{
		StartCoroutine(MakePotionAutomaticallyCoroutine());
	}

	public bool CheckIfAtLeastOnePotionIsPouring()
	{
		for (int i = 0; i < potionObjects.Length; i++)
			if (potionObjects[i].GetComponent<PotionScript>().pouring)
				return false;

		return true;
	}

	public IEnumerator MakePotionAutomaticallyCoroutine()
	{
		if (!gameFinished && CheckIfAtLeastOnePotionIsPouring())
		{


			// First potion
			if (!missionItems[0].GetComponent<PotionScript>().potionUsed)
			{
				potionObjects[missionItems[0].GetComponent<PotionScript>().potionIndex].GetComponent<ItemDrag>().draggingSpeed = MagicPotionManager.magicPotionManager.potionsDraggingSpeed;
				potionObjects[missionItems[0].GetComponent<PotionScript>().potionIndex].GetComponent<PotionScript>().holdingThisPotion = true;
                potionObjects[missionItems[0].GetComponent<PotionScript>().potionIndex].GetComponent<ItemDrag>().backToStartPosition = false;
				potionObjects[missionItems[0].GetComponent<PotionScript>().potionIndex].GetComponent<BoxCollider2D>().enabled = true;

	//			potionObjects[missionItems[0].GetComponent<PotionScript>().potionIndex].GetComponent<PotionScript>().pouring = true;
	//			potionObjects[missionItems[0].GetComponent<PotionScript>().potionIndex].GetComponent<ItemDrag>().draggingSpeed = 0;
	//			potionObjects[missionItems[0].GetComponent<PotionScript>().potionIndex].GetComponent<BoxCollider2D>().enabled = false;
				potionObjects[missionItems[0].GetComponent<PotionScript>().potionIndex].GetComponent<PotionScript>().GoToPouringPositionAndPour();

				yield return new WaitForSeconds(2f);
			}

			// Second potion
			if (!missionItems[1].GetComponent<PotionScript>().potionUsed)
			{
				potionObjects[missionItems[1].GetComponent<PotionScript>().potionIndex].GetComponent<ItemDrag>().draggingSpeed = MagicPotionManager.magicPotionManager.potionsDraggingSpeed;
				potionObjects[missionItems[1].GetComponent<PotionScript>().potionIndex].GetComponent<PotionScript>().holdingThisPotion = true;
				potionObjects[missionItems[1].GetComponent<PotionScript>().potionIndex].GetComponent<ItemDrag>().backToStartPosition = false;
				potionObjects[missionItems[1].GetComponent<PotionScript>().potionIndex].GetComponent<BoxCollider2D>().enabled = true;

	//			potionObjects[missionItems[1].GetComponent<PotionScript>().potionIndex].GetComponent<PotionScript>().pouring = true;
	//			potionObjects[missionItems[1].GetComponent<PotionScript>().potionIndex].GetComponent<ItemDrag>().draggingSpeed = 0;
	//			potionObjects[missionItems[1].GetComponent<PotionScript>().potionIndex].GetComponent<BoxCollider2D>().enabled = false;
				potionObjects[missionItems[1].GetComponent<PotionScript>().potionIndex].GetComponent<PotionScript>().GoToPouringPositionAndPour();

				yield return new WaitForSeconds(2f);
			}

			// Third potion
			if (!missionItems[2].GetComponent<PotionScript>().potionUsed)
			{
				potionObjects[missionItems[2].GetComponent<PotionScript>().potionIndex].GetComponent<ItemDrag>().draggingSpeed = MagicPotionManager.magicPotionManager.potionsDraggingSpeed;
				potionObjects[missionItems[2].GetComponent<PotionScript>().potionIndex].GetComponent<PotionScript>().holdingThisPotion = true;
				potionObjects[missionItems[2].GetComponent<PotionScript>().potionIndex].GetComponent<ItemDrag>().backToStartPosition = false;
				potionObjects[missionItems[2].GetComponent<PotionScript>().potionIndex].GetComponent<BoxCollider2D>().enabled = true;

	//			potionObjects[missionItems[2].GetComponent<PotionScript>().potionIndex].GetComponent<PotionScript>().pouring = true;
	//			potionObjects[missionItems[2].GetComponent<PotionScript>().potionIndex].GetComponent<ItemDrag>().draggingSpeed = 0;
	//			potionObjects[missionItems[2].GetComponent<PotionScript>().potionIndex].GetComponent<BoxCollider2D>().enabled = false;
				potionObjects[missionItems[2].GetComponent<PotionScript>().potionIndex].GetComponent<PotionScript>().GoToPouringPositionAndPour();
			}
		}
	}
}
