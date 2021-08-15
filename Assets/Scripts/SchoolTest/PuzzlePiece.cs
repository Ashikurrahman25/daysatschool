using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzlePiece : MonoBehaviour 
{
    public Transform PartsHolder;
    public int index;
    private Vector2 sizeDelta;
    public PuzzleGameManager puzzleGameManager;

    //public Transform lastTrigger;
    public void ScaleToTarget()
    {
        //if(lastTrigger==null)
        sizeDelta = transform.GetComponent<RectTransform>().sizeDelta;
        transform.GetComponent<RectTransform>().sizeDelta = PartsHolder.GetChild(0).GetComponent<RectTransform>().sizeDelta;

        Debug.LogError("HAHAH");
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("PuzzleSlot"))
        {
            if (collider.GetComponent<PuzzleSlot>().desiredSprite == GetComponent<Image>().sprite)
            {
                transform.GetComponent<ItemDrag>().enabled = false;
                transform.position = collider.transform.position;
                transform.transform.SetParent(collider.transform);
                puzzleGameManager.CheckForFinish();
            }


            //lastTrigger = collider.transform;
        }
    }

    //public void OnTriggerExit2D(Collider2D collider)
    //{
        //lastTrigger = null;
    //}

    public void DragEndedCallback()
    {
        //if (lastTrigger != null)
        //{

        //    if (!(lastTrigger.childCount > 0))
        //    {
        //        transform.GetComponent<ItemDrag>().StopAllCoroutines();
        //        transform.SetParent(lastTrigger, false);
        //        transform.GetComponent<RectTransform>().anchorMax = new Vector2(.5f, .5f);
        //        transform.GetComponent<RectTransform>().anchorMin = new Vector3(.5f, .5f);
        //        transform.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
        //        PuzzleGameManager.Instance.CheckForFinish();
        //    }
        //}
        //else
        transform.GetComponent<RectTransform>().sizeDelta = sizeDelta;
    }
}
