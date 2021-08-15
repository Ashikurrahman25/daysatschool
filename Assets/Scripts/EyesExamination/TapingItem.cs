using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;

///<summary>
///<para>Scene:All/NameOfScene/NameOfScene1,NameOfScene2,NameOfScene3...</para>
///<para>Object:N/A</para>
///<para>Description: Sample Description </para>
///</summary>

public class TapingItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

	// FIXME za sada mora da budu targetovani objekti za item na koji se tapka u hijerarhiji sa ostalm itemima

	public List<GameObject> allTargetItems;
	public int targetItemsCountMin;
	public int targetItemsCountMax;
	public List<GameObject> targetItems;
	public List<bool> itemsDone;
	private List<int> targetIndices;

	public List<GameObject> allIndicatorImagesObjectHolders;
	public List<GameObject> indicatorImagesObjects;

	public int tapsNeeded;
	public int tapsOnItem;
	public bool readyForTapping; // when item is on position for taping
	public bool canTap;

	// Object and image holders for tutorial
	public GameObject tutorialObjectHolder;
	public GameObject buttonsHolder;
	public Image tutorialImageHolder;
	public Sprite thisItemTutorialImage;
	public GameObject tutorialCheckImageObjectHolder;

	public bool thisItemMakesCharacterSad;

	// If item that is holding is animated when player has taped on it, Animator and animation name if animation needs to be played
	public bool itemAnimatedWhenOverItem;
	public Animator anim;
	public string animationName;

	// If item is finished play particle
	public bool playFinishedItemUsingParticle;

	// Animate targeted object when targeted object is reached
	public bool ObjectAnimateWhenTargetReached;

	[HideInInspector]
	public GameObject activeGameobject;

//	public GameObject finishedUsingItemMarkHolder;

	public bool hideTargetGraphics;

	public bool swapItemGraphics;

	public GameObject activeItemGraphicsHolder;
	public GameObject inactiveItemGraphicsHoler;

	public bool setItemScaleWhenTargetReached;
	public Vector3 itemReachedScale;

	// FIXME ovde dodati bool ako uopste ne zelimo da pustamo sound ili cu ostaviti prazan string ako ne pustam sound
	public string itemSound;

	public GameObject tappingAnimationHolder;

	public void SetItemAwake()
	{
		targetIndices = new List<int>();
		int numberOfTargets;

		if (targetItemsCountMin == targetItemsCountMax)
			numberOfTargets = targetItemsCountMin;
		else
			numberOfTargets = UnityEngine.Random.Range(targetItemsCountMin, targetItemsCountMax);

		// Turn off all indicator images objects
		for (int i = 0; i < allIndicatorImagesObjectHolders.Count; i++)
		{
			allIndicatorImagesObjectHolders[i].SetActive(false);
		}

		// Set taps on 0
		tapsOnItem = 0;
		canTap = true;

		// Set items done
		itemsDone = new List<bool>();

		// Add random targets
		for (int i = 0; i < numberOfTargets; i++)
		{
			int r = UnityEngine.Random.Range(0, allTargetItems.Count);

			targetItems.Add(allTargetItems[r]);
			allTargetItems.RemoveAt(r);

			targetIndices.Add(r);

			// Set indicator images
			if (allIndicatorImagesObjectHolders.Count > 0)
			{
				indicatorImagesObjects.Add(allIndicatorImagesObjectHolders[r]);
				allIndicatorImagesObjectHolders.RemoveAt(r);
			}
				
			// Set lists for progress time and item done variables
			itemsDone.Add(false);
		}

		// Destroy all other targets
		for (int i = 0; i < allTargetItems.Count; i++)
		{
			Destroy(allTargetItems[i]);
		}

		// If target need swaping
		if (swapItemGraphics)
		{
			activeItemGraphicsHolder.SetActive(false);
			inactiveItemGraphicsHoler.SetActive(true);
		}
	}

	// Check if we finished all items
	public bool CheckIfAllItemsAreFinished()
	{
		for (int i = 0; i < itemsDone.Count; i++)
		{
			if (itemsDone[i] == false)
				return false;
		}

		return true;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (!readyForTapping)
		{
			if (GetComponent<ItemDragScript>().enabled && !CheckIfAllItemsAreFinished())
			{
				// Turn on all indicator images if length is more than 0
				if (indicatorImagesObjects.Count > 0)
				{
					for (int i = 0; i < indicatorImagesObjects.Count; i++)
					{
						if (!itemsDone[i])
							indicatorImagesObjects[i].SetActive(true);
					}
				}

				// Hide buttons and show tutorial for this item
				buttonsHolder.SetActive(false);
				tutorialImageHolder.sprite = thisItemTutorialImage;
				tutorialObjectHolder.SetActive(true);

				// If item is finished show mark image
				if (!CheckIfAllItemsAreFinished())
				{
					tutorialCheckImageObjectHolder.transform.GetChild(0).GetComponent<Animator>().Play("CheckIconInvis", 0, 0);
				}
				else
				{
					tutorialCheckImageObjectHolder.transform.GetChild(0).GetComponent<Animator>().Play("CheckIconVisible", 0, 0);
				}

				// PLay character sad animation if needed
				if (thisItemMakesCharacterSad && !LevelManager.allItemsDone)
					LevelManager.levelManager.characterAnimator.Play("CharacterSad", 0, 0);

				// Set injuries for this item to be last in hierarchy
				targetItems[0].transform.parent.SetParent(targetItems[0].transform.parent.parent);

				// If item target need swaping
				if (swapItemGraphics)
				{
					activeItemGraphicsHolder.SetActive(true);
					inactiveItemGraphicsHoler.SetActive(false);
				}
			}
		}
		else
		{
			if (readyForTapping && canTap)
			{
				if (itemAnimatedWhenOverItem)
				{
					StartCoroutine(TapedCoroutine());
				}
			}
		}
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (!readyForTapping)
		{
			if (GetComponent<ItemDragScript>().enabled)
			{
				// Turn off all indicator images if length is more than 0
				if (indicatorImagesObjects.Count > 0)
				{
					for (int i = 0; i < indicatorImagesObjects.Count; i++)
					{
						indicatorImagesObjects[i].SetActive(false);
					}
				}

				// Hide buttons and show tutorial for this item
				tutorialObjectHolder.SetActive(false);
				buttonsHolder.SetActive(true);

				// PLay character idle animation if needed
				if (thisItemMakesCharacterSad && !LevelManager.allItemsDone)
					LevelManager.levelManager.characterAnimator.Play("CharacterIdle", 0, 0);
			}
		}
	}

	IEnumerator TapedCoroutine()
	{
		canTap = false;

		if (tapsOnItem < tapsNeeded - 1)
		{

            if (itemSound != "")
                SoundManagerEyeExamination.PlaySound(itemSound);

            anim.Play(animationName, 0, 0);

			tappingAnimationHolder.SetActive(false);
		}
		else
		{
            if (itemSound != "")
                SoundManagerEyeExamination.PlaySound(itemSound);

            anim.Play(gameObject.name + "TapingFinal", 0, 0);

			tappingAnimationHolder.SetActive(false);
		}

		yield return new WaitForSeconds(0.7f);

		tapsOnItem++;

		if (tapsOnItem == tapsNeeded)
		{
			// Wait for animation to finish
			// yield return new WaitForSeconds(0.4f);

			//		// Set buttons and enable interaction
			//		buttonsHolder.SetActive(true);
			//		tutorialObjectHolder.SetActive(false);

			int itemIndex = targetItems.IndexOf(activeGameobject);
			itemsDone[itemIndex] = true;

			// Play item idle animation and set holding to false
			anim.Play(gameObject.name + "Idle", 0, 0);
			GetComponent<ItemDragScript>().isHoldingItem = false;
			GetComponent<ItemDragScript>().returnToStartPosition = true;

			for (int i = 0; i < indicatorImagesObjects.Count; i++)
			{
				if (!itemsDone[i])
					indicatorImagesObjects[i].SetActive(false);
			}

			if (CheckIfAllItemsAreFinished())
			{
				tutorialCheckImageObjectHolder.transform.GetChild(0).GetComponent<Animator>().Play("CheckIconVisible", 0, 0);

				LevelManager.levelManager.phaseQuestsCompleted[LevelManager.levelManager.currentPhase]++;

				tutorialCheckImageObjectHolder.transform.GetChild(0).GetComponent<Animator>().Play("CheckIconFinished", 0, 0);

//				finishedUsingItemMarkHolder.GetComponent<Animator>().Play("ItemDoneAnimation", 0, 0);

				if (LevelManager.levelManager.CheckIfPhaseIsFinished())
				{
                    // Play particle sound
                    SoundManagerEyeExamination.PlaySound("BigParticle");
                    LevelManager.levelManager.successParticleHolder.GetComponent<ParticleSystem>().Play();

//					// Show next phase button
//					if (LevelManager.levelManager.currentPhase < LevelManager.levelManager.phaseQuests.Count - 1)
//						LevelManager.levelManager.nextPhaseButton.SetActive(true);
//					else
//						LevelManager.levelManager.CameraButtonPhaseClicked();
////						LevelManager.levelManager.cameraPhaseButton.SetActive(true);
				}

				// Hide buttons and show tutorial for this item
				tutorialObjectHolder.SetActive(false);
				buttonsHolder.SetActive(true);

				// PLay character idle animation if needed
				if (thisItemMakesCharacterSad && !LevelManager.allItemsDone)
					LevelManager.levelManager.characterAnimator.Play("CharacterIdle", 0, 0);

				// Reset ready for taping so tutorial will be shown again
				readyForTapping = false;
			}

			// If target graphics needs to be hidden, hide it.. same rule find animatorholder and turn off object
			if (hideTargetGraphics)
			{
				yield return new WaitForSeconds(1f);
				activeGameobject.transform.Find("AnimationHolder").gameObject.SetActive(false);
			}

			// PLay particle
			if (playFinishedItemUsingParticle)
			{
                SoundManagerEyeExamination.PlaySound("SmallParticle");
                activeGameobject.transform.Find("ParticleHolder").GetComponent<ParticleSystem>().Play ();
			}

			EnableMovementForAllItems();
			LevelManager.levelManager.ActivateInnerItemsMovement();
		}
		else
		{
			canTap = true;
			tappingAnimationHolder.SetActive(true);
		}
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (GetComponent<ItemDragScript>().isHoldingItem)
		{
			if (targetItems.Contains(coll.gameObject))
			{
				int itemIndex = targetItems.IndexOf(coll.gameObject);

				// Set active gameobject
				activeGameobject = coll.gameObject;

				// If its just drag item than check is done and play animation if animated
				if (!itemsDone[itemIndex])
				{
					if (indicatorImagesObjects.Count > 0)
					{
						for (int i = 0; i < indicatorImagesObjects.Count; i++)
							indicatorImagesObjects[i].SetActive(false);
					}
						
					tappingAnimationHolder.SetActive(true);

					GoToPositionAndAnimate(coll.gameObject.transform, 0.3f);
				}
			}
		}
	}

	// Item goes to a certain position, playes certain animation and returns to startposition FIXME (ne treba popravka)
	public void GoToPositionAndAnimate(Transform targetPosition, float timeOnPosition)
	{
		StartCoroutine(GoToPositionAndAnimateCoroutine(targetPosition, timeOnPosition));
	}

	IEnumerator GoToPositionAndAnimateCoroutine(Transform targetPosition, float timeOnPosition)
	{
		// Set buttons and disable interaction
		buttonsHolder.SetActive(false);
		tutorialObjectHolder.SetActive(true);
		StartCoroutine(DisableMovementForAllItemsAfterTime(0f));

		GetComponent<ItemDragScript>().returnToStartPosition = false;
		GetComponent<ItemDragScript>().isHoldingItem = true;

		// Vector2 newTargetPosition = new Vector2(targetPosition.GetComponent<RectTransform>().anchoredPosition.x - Screen.width / 2f, targetPosition.GetComponent<RectTransform>().anchoredPosition.y);

		while (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(targetPosition.position.x, targetPosition.position.y)) > 10f)
		{
			transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, 0.2f);
			yield return new WaitForSeconds(0.02f);
		}

		// Animate targeted object if needed
		if (ObjectAnimateWhenTargetReached)
		{
			activeGameobject.transform.Find("AnimationHolder").GetComponent<Animator>().Play(activeGameobject.name + "Active", 0, 0);
		}

		readyForTapping = true;
			
		transform.position = targetPosition.position;

		// Set item scale when target reach if needed
		if (setItemScaleWhenTargetReached)
			transform.localScale = itemReachedScale;

		// Show tapping animation
		transform.GetChild(1).gameObject.SetActive(true);

		LevelManager.levelManager.DeactivateInnerItemsMovement();
	}

	public IEnumerator DisableMovementForAllItemsAfterTime(float time)
	{
		yield return new WaitForSeconds(time);

//		transform.localScale = new Vector3(1f, 1f, 1f);

		foreach (Transform t in transform.parent)
		{
			if (t.GetComponent<ItemDragScript>() != null)
				t.GetComponent<ItemDragScript>().enabled = false;
		}

		for (int i = 0; i < LevelManager.levelManager.outerItems.Length; i++)
		{
			LevelManager.levelManager.outerItems[i].GetComponent<ItemDragScript>().enabled = false;
		}
	}

	public void EnableMovementForAllItems()
	{
		foreach (Transform t in transform.parent)
		{
			if (t.GetComponent<ItemDragScript>() != null)
			{
				t.GetComponent<ItemDragScript>().enabled = true;
				t.GetComponent<ItemDragScript>().returningToStartPosition = true;
			}
		}

		for (int i = 0; i < LevelManager.levelManager.outerItems.Length; i++)
		{
			LevelManager.levelManager.outerItems[i].GetComponent<ItemDragScript>().enabled = true;
			LevelManager.levelManager.outerItems[i].GetComponent<ItemDragScript>().returningToStartPosition = true;
		}
	}
}
