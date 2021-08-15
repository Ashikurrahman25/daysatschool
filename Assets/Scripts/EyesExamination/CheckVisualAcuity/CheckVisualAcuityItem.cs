using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CheckVisualAcuityItem : MonoBehaviour {

    public GameObject targetItem;

    CheckVisualAcuityManager cvaManager;

    void Start()
    {
        cvaManager = transform.parent.parent.parent.GetComponent<CheckVisualAcuityManager>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == targetItem)
        {
            transform.position = other.transform.position;
            SetItemsEnabled(false);
            cvaManager.SetBoardImage(transform.GetSiblingIndex());
            GetComponent<Animator>().Play("ItemOnTarget",0,0);
            EyesExaminationGameManager.Instance.tutorialHolder.SetActive(false);
        }
    }

    /// <summary>
    /// Checks the item. Call from animation event
    /// </summary>
    public void CheckTheItem()
    {
        Invoke("EnableItems",1f);

        if (transform.GetSiblingIndex() == cvaManager.GetCorrectItemIndex())
            cvaManager.ItemFound();
        else
            cvaManager.WrongItem();
    }

    /// <summary>
    /// Calls SetItemsEnabled with "true" parameter so we can call it with invoke.
    /// </summary>
    void EnableItems()
    {
        SetItemsEnabled(true);
    }

    void SetItemsEnabled(bool b)
    {
        Debug.Log("Items enabled: " + b);
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            transform.parent.GetChild(i).GetComponent<ItemDragScript>().isHoldingItem = false;
            transform.parent.GetChild(i).GetComponent<ItemDragScript>().enabled = b;
            transform.parent.GetChild(i).GetComponent<ItemDragScript>().returnToStartPosition = b;
            transform.parent.GetChild(i).GetComponent<ItemDragScript>().returningToStartPosition = b;
        }
    }
}
