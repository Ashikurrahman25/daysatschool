using UnityEngine;
using System.Collections;
using UnityEngine.UI;

///<summary>
///<para>Scene:All/NameOfScene/NameOfScene1,NameOfScene2,NameOfScene3...</para>
///<para>Object:N/A</para>
///<para>Description: Sample Description </para>
///</summary>

public class DressingItem : MonoBehaviour {

	public Animator characterAnimator;

	public Image itemImageHolder;

	public bool alreadyDressed;

	public GameObject[] lastTwoItems;

	void Start()
	{
		characterAnimator = LevelManager.levelManager.characterHolder.transform.GetChild(0).GetChild(0).GetComponent<Animator>();
		alreadyDressed = false;
	}

	public bool CheckIfLastTwoItemsAreFinished()
	{
		for (int i = 0; i < lastTwoItems.Length; i++)
		{
			if (lastTwoItems[i].GetComponent<HoldItemOverItem>() != null)
			{
				if (!lastTwoItems[i].GetComponent<HoldItemOverItem>().CheckIfAllItemsAreFinished())
					return false;
			}
			else if (lastTwoItems[i].GetComponent<TapingItem>() != null)
			{
				if (!lastTwoItems[i].GetComponent<TapingItem>().CheckIfAllItemsAreFinished())
					return false;
			}
		}

		return true;
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.name == "DressingPlace" && !alreadyDressed && CheckIfLastTwoItemsAreFinished())
		{
			characterAnimator.Play("CharacterClothes", 0, 0);
			alreadyDressed = true;
			LevelManager.allItemsDone = true;
		}
		else if (coll.name == "DressingPlace")
		{
			for (int i = 0; i < lastTwoItems.Length; i++)
			{
				if (lastTwoItems[i].GetComponent<HoldItemOverItem>() != null)
				{
					if (!lastTwoItems[i].GetComponent<HoldItemOverItem>().CheckIfAllItemsAreFinished())
						lastTwoItems[i].GetComponent<HoldItemOverItem>().anim.Play("ItemBlinking", 0 , 0);
				}
				else if (lastTwoItems[i].GetComponent<TapingItem>() != null)
				{
					if (!lastTwoItems[i].GetComponent<TapingItem>().CheckIfAllItemsAreFinished())
						lastTwoItems[i].GetComponent<TapingItem>().anim.Play("ItemBlinking", 0 , 0);
				}
			}
		}
	}

	void OnTriggerExit2D(Collider2D coll)
	{
		if (coll.name == "DressingPlace")
		{
			for (int i = 0; i < lastTwoItems.Length; i++)
			{
				if (lastTwoItems[i].GetComponent<HoldItemOverItem>().anim != null)
					lastTwoItems[i].GetComponent<HoldItemOverItem>().anim.Play(lastTwoItems[i].name + "Idle", 0 , 0);
			}
		}
	}
}
