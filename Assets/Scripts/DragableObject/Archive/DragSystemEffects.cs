using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DragSystemEffects : MonoBehaviour {

    public UnityEvent targetDone;

    #region UnityEvent-s
//    [Header("Trigger events")]
//    public UnityEvent itemTriggerEnter;
//    public UnityEvent itemTriggerExit;
//    public float triggerStayFireRate = 0.2f;   //TODO Here you can set fire rate of TriggerStay
//    public UnityEvent itemTriggerStay;
    #endregion

    Image tempImage;
    int rubCounter;
    bool triggerStayFlag = false;

    //FIXME OnEnable, Awake or Start?
    void OnEnable()
    {
        rubCounter = 0;
        tempImage = GetComponent<Image>();
    }

    #region Trigger events
//    /// <summary>
//    /// Raises the TriggerEnter2D event. Takes care of rubbing counter and event.
//    /// </summary>
//    /// <param name="other">Other.</param>
//    void OnTriggerEnter2D(Collider2D other)
//    {
//        if (other.GetComponent<ItemDragEvents>() != null)
//        { 
////            Collider2D[] trgts = other.GetComponent<ItemDragEvents>().targets.ToArray();
////            foreach (Collider2D trgt in trgts)
////            {
////                Debug.Log("Target colliders: " + trgt);
////            }
////
////            Debug.Log("This collider: " + GetComponent<Collider2D>());
//
//            if (other.GetComponent<ItemDragEvents>().targets.Contains(GetComponent<Collider2D>()))
//            {
//                itemTriggerEnter.Invoke();
//                triggerStayFlag = true;
//                StartCoroutine(CTriggerStay());
//            }
//        }
//    }
//
//    IEnumerator CTriggerStay()
//    {
//        while (triggerStayFlag)
//        {
//            itemTriggerStay.Invoke();
//            Debug.Log("TriggerStay");
//            yield return new WaitForSeconds(triggerStayFireRate);
//        }
//        yield return null;
//    }
//    
//    void OnTriggerExit2D(Collider2D other)
//    {
//        if (other.GetComponent<ItemDragEvents>().targets.Contains(GetComponent<Collider2D>()))
//        {
//            triggerStayFlag = false;
//            StopCoroutine("CTriggerStay");
//            itemTriggerExit.Invoke();
//        }
//    }
    #endregion

    /// <summary>
    /// Fades the image component of the game object.
    /// </summary>
    /// <param name="fadeAmount">Positive value for "FadeIn" effect, negative for "FadeOut".</param>
    public void FadeImage(float fadeAmount)
    {
        if (tempImage != null)
        {
            if ((fadeAmount > 0 && tempImage.color.a < 1) || (fadeAmount < 0 && tempImage.color.a > 0))
            {
                GetComponent<Image>().color += new Color(0, 0, 0, fadeAmount);
            }
            else
            {
                targetDone.Invoke();
                Debug.Log("Aplha limit reached: " + tempImage.color.a); //FIXME TargetDone event?
            }
        }
        else
            Debug.Log("Image component missing!");
    }

    public void Rubbing(int rubs)
    {
        if (rubCounter < rubs)
        {
            rubCounter++;
        }
        else
        {
            targetDone.Invoke();
            Debug.Log("Rub limit reached: " + rubCounter);  //FIXME TargetDone event?
        }
    }

    public void EnableTarget()
    {
        gameObject.SetActive(true);
    }
}
