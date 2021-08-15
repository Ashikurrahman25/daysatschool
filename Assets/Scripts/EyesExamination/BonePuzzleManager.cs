using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BonePuzzleManager : MonoBehaviour {

	public GameObject[] boneParts;

//	public Image progressBar;
//	public float progressBarFillSpeed;
//	private float onePointFillAmount;

	public float bonePartsReturningSpeed;
	public float bonePartsDraggingSpeed;

	public int maxPoints;
	public static int points;

	public HoldItemOverItem rendgenItem;

	private bool gameStarted = false;

    public GameObject closeButtonFinishedAnimationHolder;

    public Button closeButton;

    public Transform BonesInRentgenHolder;

	public static BonePuzzleManager bonePuzzleManager;

//    Collider2D[] targetColliders; //TODO if we want this to work fine, we need suport in HoldItemOverItem - GoesToAppropriatePositionAndAnimate has to include collider offset

	void OnEnable()
	{
		// FIXME za test ove igre ovde startgame
		if (!gameStarted)
		{
			StartGame();
			bonePuzzleManager = this;
		}
	}

	public void StartGame()
	{
		StartCoroutine(StartGameCoroutine());
	}

	IEnumerator StartGameCoroutine()
	{
//        targetColliders = rendgenItem.targetItems[0].GetComponents<Collider2D>();
//        Debug.Log("Pokupio kolajdere: " + targetColliders.Length);

		yield return new WaitForSeconds (0.02f);

		gameStarted = true;
		points = 0;

//		if (LevelManager.levelManager.currentPhase == 2)
//		{
//			nativeAdHolder.GetComponent<FacebookNativeAd>().CancelLoading();
//			nativeAdHolder.GetComponent<FacebookNativeAd>().HideNativeAd();
//		}

		// Vector3 bonePartsStartPosition = new Vector3(-260, -400, 0);

		for (int i = 0; i < boneParts.Length; i++)
		{
			//boneParts[i].transform.localPosition = bonePartsStartPosition;
			//bonePartsStartPosition += new Vector3 (120, 0, 0);
			boneParts[i].GetComponent<BonePartScript>().boneMatched = false;
			boneParts[i].GetComponent<BonePartScript>().holdingThisPart = false;

//			if (boneParts[i].GetComponent<BonePartScript>().startPosition == null)
//				boneParts[i].GetComponent<BonePartScript>().startPosition = boneParts[i].transform.localPosition;
//			else
			boneParts[i].transform.localPosition = boneParts[i].GetComponent<BonePartScript>().startPosition;

			// boneParts[i].GetComponent<BoxCollider2D>().enabled = false;
			boneParts[i].GetComponent<ItemDragScript>().enabled = true;
			boneParts[i].GetComponent<ItemDragScript>().draggingSpeed = bonePartsDraggingSpeed;
		}

//		progressBar.fillAmount = 0;
//		onePointFillAmount = 1.0f / maxPoints;
	}

	bool CheckForLevelFinished()
	{
//		for (int i = 0; i < boneParts.Length; i++)
//		{
//			if (!boneParts[i].GetComponent<BonePartScript>().boneMatched)
//				return false;
//		}
//
//		Debug.Log("Game finished!");
//		return true;
		if (points == maxPoints)
		{
			Debug.Log("Game finished points!");
			return true;
		}
		else
			return false;
	}

    public void UpdateProgressBar(int boneIndex)
	{
		points++;
        BonesInRentgenHolder.GetChild(boneIndex).gameObject.SetActive(true);
//        targetColliders[boneIndex].enabled = false;
//		progressBar.fillAmount += progressBarFillSpeed;
		StartCoroutine(FillProgressBar());
	}

	public void GameCompleted()
	{
		StartCoroutine(GameCompletedCoroutine());
	}
	
	IEnumerator GameCompletedCoroutine()
	{
        //SoundManager.Instance.PlaySuccessParticleSound();
        SoundManagerEyeExamination.PlaySound("BigParticle");

        // PLay success particle
        //LevelManager.levelManager.successParticle.particleSystem.Play();
        closeButton.interactable = false;

		yield return new WaitForSeconds(0.2f);

		//LevelManager.levelManager.transitionAnimationHolder.GetComponent<Animator>().Play ("TransitionAnimation", 0, 0);

		// Remove progress bar
//		progressBar.enabled = false;
//		progressBar.transform.parent.GetComponent<Image>().enabled = false;

		GetComponent<ItemGame>().GameFinished();

		int itemIndex = rendgenItem.targetItems.IndexOf(rendgenItem.activeGameobject);

		rendgenItem.itemsDone[itemIndex] = true;

        // Show particles aroud close button;
//        closeButtonFinishedAnimationHolder.SetActive(true);

		// Check if this phase is completed
		if (rendgenItem.CheckIfAllItemsAreFinished())
		{
			// LevelManager.levelManager.phaseQuestsCompleted[LevelManager.levelManager.currentPhase]++;

            rendgenItem.finishedUsingItemMarkHolder.GetComponent<Animator>().Play("ItemDoneAnimation", 0, 0);

//			if (LevelManager.levelManager.CheckIfPhaseIsFinished())
//			{
//				LevelManager.levelManager.successParticleHolder.particleSystem.Play();
//			}
		}

        yield return new WaitForSeconds(1f); 

        rendgenItem.GetComponent<ItemDragScript>().enabled = true;
        rendgenItem.GetComponent<ItemDragScript>().isHoldingItem = false;
        rendgenItem.GetComponent<ItemDragScript>().returnToStartPosition = true;
        rendgenItem.GetComponent<ItemDragScript>().returningToStartPosition = true;

        closeButton.interactable = true;

        //yield return new WaitForSeconds(0.45f);
        
        // craneObject.GetComponent<CraneControlls>().StopAllCoroutines();
        Debug.Log("Game finished!");
        
        // FIXME restart game na kraju test
        //StartGame();

        //yield return new WaitForSeconds (0.03f);

        // Finished bones game
        //LevelManager.levelManager.menuManager.ShowMenu(LevelManager.levelManager.doctorMenu);
        //LevelManager.levelManager.FinishedUsingItem();
    }
    
    IEnumerator FillProgressBar()
    {
//      while (progressBar.fillAmount < points * onePointFillAmount - 0.01f)
//      {
//          progressBar.fillAmount += progressBarFillSpeed;
//          yield return new WaitForSeconds(0.02f);
//          
//          if (progressBar.fillAmount >= 1)
//              break;
//      }
        yield return new WaitForEndOfFrame();

        if (CheckForLevelFinished())
            GameCompleted();
    }

    public void RendgenBack()
    {
//        rendgenItem.GetComponent<ItemDragScript>().enabled = true;
//        rendgenItem.GetComponent<ItemDragScript>().isHoldingItem = false;
//        rendgenItem.GetComponent<ItemDragScript>().returnToStartPosition = true;
//        rendgenItem.GetComponent<ItemDragScript>().returningToStartPosition = true;
    }
}
