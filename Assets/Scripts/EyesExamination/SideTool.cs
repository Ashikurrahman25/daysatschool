using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class SideTool : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

	public GameObject buttonsHolder;
	public Animator anim;
	public float activeAnimationLength;

	public GameObject itemsHolder;

	public GameObject target;

	public bool doingWork; // Savke brat finta

	public GameObject indicator;

	public string soundName;

	public bool playTargetAnimation;

	void Awake()
	{
		doingWork = false;

		if (indicator != null)
			indicator.SetActive(false);
		
		GetComponent<ItemDragScript>().ItemDragAwake();
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (indicator != null && !doingWork)
			indicator.SetActive(true);
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (indicator != null)
			indicator.SetActive(false);
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject == target)
			GoToPositionAndAnimate(target.transform, activeAnimationLength);
	}

	public void GoToPositionAndAnimate(Transform targetPosition, float timeOnPosition)
	{
		StartCoroutine(GoToPositionAndAnimateCoroutine(targetPosition, timeOnPosition));
	}

	IEnumerator GoToPositionAndAnimateCoroutine(Transform targetPosition, float timeOnPosition)
	{
		doingWork = true;

		if (indicator != null)
			indicator.SetActive(false);
		
		// Set buttons and disable interaction
		buttonsHolder.SetActive(false);
		//		tutorialObjectHolder.SetActive(true);
		StartCoroutine(DisableMovementForAllItemsAfterTime(0f));

		// Cekamo jedan frame i disablujemo holder sa itemima
		yield return new WaitForEndOfFrame();
		itemsHolder.SetActive(false);

		GetComponent<ItemDragScript>().returnToStartPosition = false;
		GetComponent<ItemDragScript>().isHoldingItem = true;

		// If item is water removal tool disable target boxcollider
		if (name == "WaterRemovalTool")
			target.GetComponent<BoxCollider2D>().enabled = false;

		// Vector2 newTargetPosition = new Vector2(targetPosition.GetComponent<RectTransform>().anchoredPosition.x - Screen.width / 2f, targetPosition.GetComponent<RectTransform>().anchoredPosition.y);

		while (Vector2.Distance(new Vector2(transform.localPosition.x, transform.localPosition.y), new Vector2(targetPosition.localPosition.x/* - transform.parent.localPosition.x*/, targetPosition.localPosition.y/* - transform.parent.localPosition.y*/)) > 10f)
		{
			transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(targetPosition.localPosition.x - transform.parent.localPosition.x, targetPosition.localPosition.y - transform.parent.localPosition.y, 0), 1600f);
			yield return new WaitForSeconds(0.02f);
		}

		//transform.localPosition = targetPosition.localPosition;
		transform.localPosition = new Vector3(targetPosition.localPosition.x - transform.parent.localPosition.x, targetPosition.localPosition.y - transform.parent.localPosition.y, 0);


		//		transform.position = targetPosition.position;

		LevelManager.levelManager.DeactivateInnerItemsMovement();

		//		// Go to position
		//		while (transform.position != targetPosition.position)
		//		{
		//			transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, Time.deltaTime * 10f);
		//			GetComponent<ItemDragScript>().isHoldingItem = true;
		//		}

		// Play animaton
		anim.Play(gameObject.name + "Active", 0, 0);

		if (playTargetAnimation)
			target.transform.GetChild(0).GetComponent<Animator>().Play(target.name + "Active", 0, 0);

        if (soundName != null)
            SoundManagerEyeExamination.PlaySound(soundName);

        // Wait for animation to finish
        yield return new WaitForSeconds(activeAnimationLength);

		//		// Set buttons and enable interaction
		//		buttonsHolder.SetActive(true);
		//		tutorialObjectHolder.SetActive(false);

		// Play item idle animation and set holding to false
		anim.Play(gameObject.name + "Idle", 0, 0);
		GetComponent<ItemDragScript>().isHoldingItem = false;
		GetComponent<ItemDragScript>().returnToStartPosition = true;
		GetComponent<ItemDragScript>().returningToStartPosition = true;

//		for (int i = 0; i < indicatorImagesObjects.Count; i++)
//		{
//			if (!itemsDone[i])
//				indicatorImagesObjects[i].SetActive(false);
//		}

		buttonsHolder.SetActive(true);

		// Ukljucujemo iteme, cekamo jedan frejm i enablujemo ih
		itemsHolder.SetActive(true);
		yield return new WaitForEndOfFrame();

		doingWork = false;

		EnableMovementForAllItems();
//		LevelManager.levelManager.ActivateInnerItemsMovement();
	}

	public IEnumerator DisableMovementForAllItemsAfterTime(float time)
	{
		yield return new WaitForSeconds(time);

		// FIXME nije mi odgovaralo za Gas masku, a ne znam ni sto sam stavio da se vrati scale na 1,1,1
		// transform.localScale = new Vector3(1f, 1f, 1f);

		foreach (Transform t in itemsHolder.transform)
		{
			if (t.GetComponent<ItemDragScript>() != null)
				t.GetComponent<ItemDragScript>().enabled = false;
		}

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
		foreach (Transform t in itemsHolder.transform)
		{
			if (t.GetComponent<ItemDragScript>() != null && t.GetComponent<VideoItem>() == null) // Dodata provera za video iteme
			{
				t.GetComponent<ItemDragScript>().enabled = true;
				t.GetComponent<ItemDragScript>().returningToStartPosition = true;
			}
		}

		foreach (Transform t in transform.parent)
		{
			if (t.GetComponent<ItemDragScript>() != null)
				t.GetComponent<ItemDragScript>().enabled = true;
		}

		for (int i = 0; i < LevelManager.levelManager.outerItems.Length; i++)
		{
			LevelManager.levelManager.outerItems[i].GetComponent<ItemDragScript>().enabled = true;
			LevelManager.levelManager.outerItems[i].GetComponent<ItemDragScript>().returningToStartPosition = true;
		}
	}
}
