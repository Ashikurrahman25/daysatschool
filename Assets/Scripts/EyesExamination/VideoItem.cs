using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

// This script is used for selectinf features for some elements (color, sticker, etc...).
// Script is working if user just taps on this item, than popup is opened for selecting feature, 
// else if he is holding finger on item than dragging is enabled.
public class  VideoItem : MonoBehaviour, IPointerDownHandler {

	public int toolIndex;

	private bool canClickOnVideoItem = true;
	public GameObject videoItemLockedImage;

	void Awake()
	{
		canClickOnVideoItem = true;
	}

	IEnumerator ClickOnVideoItemDelay()
	{
		// Disable clicking on item for a time delay
		canClickOnVideoItem = false;



		yield return new WaitForSeconds(1.4f);

		canClickOnVideoItem = true;
	}

	public void UnlockVideoItem()
	{
		// Remove video item image
		videoItemLockedImage.SetActive(false);

		// Enable ItemDrag and HildingItemOverItem scripts
		if (GetComponent<HoldItemOverItem>() != null)
			GetComponent<HoldItemOverItem>().enabled = true;
		else if (GetComponent<TapingItem>() != null)
			GetComponent<TapingItem>().enabled = true;

		GetComponent<ItemDragScript>().enabled = true;

		PlayerPrefs.SetString("Tool" + toolIndex.ToString(), "Raspakovka");

		// Turn off script
		Destroy(this);
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (canClickOnVideoItem)
		{
			StartCoroutine(ClickOnVideoItemDelay());
		}
	}
}
