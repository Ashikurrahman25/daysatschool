using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StickerTool : MonoBehaviour {

	// true if sticker tool false if color tool
	public bool isStickerTool;

	public Color toothColor;
	public Image stickerImageHolder;

	void Start()
	{
		if (!isStickerTool)
			toothColor = Color.white;

		Destroy(GetComponent<HoldItemOverItem>());
	}

	public void OnTriggerEnter2D(Collider2D coll)
	{
		if (GetComponent<ItemDragScript>().isHoldingItem)
		{
			if (isStickerTool && coll.tag == "Sticker")
				coll.GetComponent<Image>().sprite = stickerImageHolder.sprite;

			if (!isStickerTool && coll.tag == "Tooth")
			{
				coll.GetComponent<Image>().color = toothColor;
			}
		}
	}
}
