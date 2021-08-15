using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckVisualAcuityManager : MonoBehaviour {

    public int itemsToFind;
    public Transform itemsHolder;
    public ItemGame itemGame;
    public MenuManager menuManager;
    public HoldItemOverItem itemForThisGame;
    public LevelManager levelManager;
    public Transform eyeGlassesHolder;
    public Animator mainAnimator;

    [Space(5)]
    [Header("Board Image and sprites")]
    public Image boardImage; 
    public Sprite[] boardSprites; // 0 = correct answer

    int correctItemIndex;
    int itemsFound;

	void Start () 
    {
        itemsFound = 0;
        RefreshCorrectItemIndex();  
    }

    void OnEnable()
    {
        foreach (Transform item in itemsHolder)
        {
            item.GetComponent<ItemDragScript>().Invoke("ItemDragAwake",0.5f);
        }
    }

    void RefreshCorrectItemIndex()
    {
        correctItemIndex = Random.Range(0,eyeGlassesHolder.childCount);
        boardImage.sprite = boardSprites[correctItemIndex+1];
        Debug.Log("Correct item index is: " + correctItemIndex);
    }

    public int GetCorrectItemIndex()
    {
        return correctItemIndex;
    }

    public void ItemFound()
    {
        itemsFound++;

        if (itemsFound != itemsToFind)
        {
            for (int i = 0; i < itemsHolder.childCount; i++)
            {
                itemsHolder.GetChild(i).GetComponent<ItemDragScript>().enabled = true;
                itemsHolder.GetChild(i).GetComponent<ItemDragScript>().returnToStartPosition = true;
                itemsHolder.GetChild(i).GetComponent<ItemDragScript>().returningToStartPosition = true;
            }

            Invoke("RefreshCorrectItemIndex",mainAnimator.GetCurrentAnimatorStateInfo(0).length);
            SoundManagerEyeExamination.PlaySound("MiniGameCorrect");
            Debug.Log("Item found, go next");
        }
        else
        {
            Invoke("FinishGame",mainAnimator.GetCurrentAnimatorStateInfo(0).length);
        }

        mainAnimator.SetLayerWeight(1, 0);
        mainAnimator.SetInteger("Counter" , itemsFound);
    }

    void FinishGame()
    {
        GameObject.Find("Canvas").GetComponent<MenuManager>().ClosePopUpMenu(gameObject);
        itemGame.GameFinished();
    }

    public void WrongItem()
    {
        Debug.Log("Wrong item");
        SoundManagerEyeExamination.PlaySound("MiniGameWrong");
        mainAnimator.SetLayerWeight(1, 1);
        mainAnimator.Play("WrongItem",1,0);
    }

    public void SetBoardImage(int itemIndex)
    {
        boardImage.sprite = boardSprites[Mathf.Abs(correctItemIndex - itemIndex)];
    }
}
