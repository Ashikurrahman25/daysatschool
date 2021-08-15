using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

///<summary>
///<para>Scene:All/NameOfScene/NameOfScene1,NameOfScene2,NameOfScene3...</para>
///<para>Object:N/A</para>
///<para>Description: Sample Description </para>
///</summary>

public class ItemDragScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

	public float draggingSpeed;
	
	public bool isHoldingItem;
	
	public Vector3 startPosition;
	
	public Vector3 offsetVector;

	public bool scaleWhenGrabbed;
	public Vector3 scaleVector;

//	[HideInInspector]
    public Vector3 originalScale = Vector3.one;
	
	public bool returnToStartPosition;

 	public bool returningToStartPosition;

	// These variables are used for selecting features for some elements (color, sticker, etc...).
	// Script is working if user just taps on this item, than popup is opened for selecting feature, 
	// else if he is holding finger on item than dragging is enabled.
	public bool selectorType; // true if tap on item opens popup
	public float selectorTimeDelay;
	public Sprite selectedSprite;
	public GameObject selectorPopup;
	public float selectorStartTime;
	public Image selectorImage;

    Vector3 topLimit;

    IEnumerator Start()
    {
        
        if (gameObject.name == "GasMask")
            topLimit = Camera.main.WorldToScreenPoint(GameObject.Find("TopLimit").transform.position - new Vector3 (0,2,0));
        startPosition = transform.localPosition;
        yield return new WaitForSeconds(0.4f);
        topLimit = Camera.main.WorldToScreenPoint(GameObject.Find("TopLimit").transform.position);
    }

	public void ItemDragAwake()
	{
		if (selectorType)
			selectorStartTime = 0;

		isHoldingItem = false;
		startPosition = transform.localPosition;
		returningToStartPosition = false;
        //
		originalScale = transform.localScale;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
        if (((GetComponent<HoldItemOverItem>() != null/* && !GetComponent<HoldItemOverItem>().CheckIfAllItemsAreFinished()*/) || (GetComponent<TapingItem>() != null/* && !GetComponent<TapingItem>().CheckIfAllItemsAreFinished()*/)) || GetComponent<PotionScript>() != null || GetComponent<BonePartScript>() != null || GetComponent<StickerTool>() != null || GetComponent<SideTool>() != null || GetComponent<CheckVisualAcuityItem>() != null)
		{
			isHoldingItem = true;
			if (scaleWhenGrabbed)
				transform.localScale = scaleVector;

			selectorStartTime = Time.timeSinceLevelLoad;
		}
        topLimit = Camera.main.WorldToScreenPoint(GameObject.Find("TopLimit").transform.position);
        //Selector features
        //		if (selectorType)
        //		{
        //			isHoldingItem = true;
        //
        //			if (scaleWhenGrabbed)
        //				transform.localScale = scaleVector;
        //			
        //			selectorStartTime = Time.timeSinceLevelLoad;
        //		}

        //returningToStartPosition = false;
    }
	
	public void OnPointerUp(PointerEventData eventData)
	{
        Debug.Log("OnPointerUp opalio");
		isHoldingItem = false;

		if (GetComponent<SideTool>() == null || (GetComponent<SideTool>() != null && !GetComponent<SideTool>().doingWork))
		{
            //
			if (scaleWhenGrabbed)
				transform.localScale = originalScale;

			returningToStartPosition = true;
		}

		if (selectorType && selectorStartTime + selectorTimeDelay > Time.timeSinceLevelLoad && Vector3.Distance(transform.localPosition, startPosition) < 250f)
		{
			LevelManager.levelManager.menuManager.ShowPopUpMenu(selectorPopup);
		}
	}

	public void OnApplicationPause(bool paused)
	{
		isHoldingItem = false;
        //
		if (scaleWhenGrabbed)
			transform.localScale = originalScale;

		returningToStartPosition = true;
	}

	void Update()
	{
		if (isHoldingItem)
		{
			Vector3 screenPoint = (Vector3)Input.mousePosition + offsetVector;
			screenPoint.z = 100f;
            Debug.Log("SCreen Point " + screenPoint.y + "   TOP point " + topLimit.y);
            if (screenPoint.y >= topLimit.y)
                screenPoint.y = topLimit.y;

			transform.position = Vector3.Lerp(transform.position, Camera.main.ScreenToWorldPoint(screenPoint), draggingSpeed * Time.deltaTime);
		}

		if (!isHoldingItem && returnToStartPosition && returningToStartPosition)
		{
			Vector3 screenPoint = startPosition;
			screenPoint.z = 10f;
			
			transform.localPosition = Vector3.Lerp(transform.localPosition, screenPoint, draggingSpeed * Time.deltaTime / 1.5f);

			if (Vector3.Distance(transform.localPosition, screenPoint) < 0.2f)
			{
				transform.localPosition = screenPoint;
				returningToStartPosition = false;
                //
				transform.localScale = originalScale;

                // If item target need swaping
                if (GetComponent<HoldItemOverItem>() != null && GetComponent<HoldItemOverItem>().swapItemGraphics)
                {
                    GetComponent<HoldItemOverItem>().activeItemGraphicsHolder.SetActive(false);
                    GetComponent<HoldItemOverItem>().inactiveItemGraphicsHoler.SetActive(true);
                }
                else if (GetComponent<TapingItem>() != null && GetComponent<TapingItem>().swapItemGraphics)
                {
                    GetComponent<TapingItem>().activeItemGraphicsHolder.SetActive(false);
                    GetComponent<TapingItem>().inactiveItemGraphicsHoler.SetActive(true);
                }
            }
		}
	}
}