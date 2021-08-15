using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemHoldForPeriodOfTIme : DragableObject 
{

    [Header ("Item Speciffic")]
    public bool rub;
    public float timeToHold;
    public EChangeOpacity opacityMultiplyer = EChangeOpacity.No;

    bool onPlace = false;   // We set this OnTriggerEnter() and use it OnTriggerStay() in order to minimize targetHit() calls
    Image otherImage;
    GameObject effectsHolder;

    //timer
    float[] timePerTarget;
    float lastTriggerTime;
    float timeDifference;
    Vector3 lastFramePos;

    void Awake()
    {
        timePerTarget = new float[targets.Length];
        lastFramePos = new Vector3 (-1000, -1000, -1000);
    }

    /// <summary>
    /// When we hit the collider, we check if it is one of the targets.
    /// </summary>
    /// <param name="other">Other.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (TargetHit(other))
        {
            onPlace = true;
            otherImage = other.GetComponent<Image>();
            if (rub)
                other.GetComponent<Rigidbody2D>().sleepMode = RigidbodySleepMode2D.StartAwake;
            else
                other.GetComponent<Rigidbody2D>().sleepMode = RigidbodySleepMode2D.NeverSleep;
            if (other.transform.childCount > 0)
            {
                effectsHolder = other.transform.Find("EffectsHolder").gameObject;
                effectsHolder.SetActive(true);
            }
            if(itemAnimator != null)
                itemAnimator.Play("ItemOnPlace",0,1f);
        }
    }

    void FixedUpdate()
    {
        if (onPlace)
        {
            if (rub)
            {
                if (lastFramePos == transform.position)
                    return;
                lastFramePos = transform.position;
            }
            timePerTarget[currentTargetIndex] += Time.fixedDeltaTime;
            Debug.Log("TimeForTarget: " + timePerTarget[currentTargetIndex]);

            if(otherImage != null)
                otherImage.color += new Color(0,0,0,(int) opacityMultiplyer*(Time.fixedDeltaTime/timeToHold));
            if (timePerTarget[currentTargetIndex] >= timeToHold)
            {
                targets[currentTargetIndex].enabled = false;
                TargetDone();
            }
        }
    }

//        //FIXME fix seconds when rub is true
//    void OnTriggerStay2D(Collider2D other)
//    {
//        if (rub)
//        {   
//            timeDifference = Time.fixedDeltaTime;
//        }
//        else
//        {
//            timeDifference = Time.time - lastTriggerTime;
//            lastTriggerTime = Time.time;
//        }
//
//        if (onPlace)
//        {
//            timePerTarget[currentTargetIndex] += timeDifference;
//            Debug.Log("TimeForTarget: " + timePerTarget[currentTargetIndex]);
//            
//            if(otherImage != null)
//                otherImage.color += new Color(0,0,0,(int) opacityMultiplyer*(timeDifference/timeToHold));
//            if (timePerTarget[currentTargetIndex] >= timeToHold)
//            {
//                targets[currentTargetIndex].enabled = false;
//                TargetDone();
//            }
//        }
//    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other == targets[currentTargetIndex])
        {
            onPlace = false;
            if (effectsHolder != null)
                effectsHolder.SetActive(false);
            if(itemAnimator != null)
                itemAnimator.Play("ItemIdle",0,1f);
            other.GetComponent<Rigidbody2D>().Sleep();
        }
    }

    protected override void EndDragMethod()
    {
        if (effectsHolder != null)
            effectsHolder.SetActive(false);
        if(itemAnimator != null)
            itemAnimator.Play("ItemIdle",0,1f);
        targets[currentTargetIndex].GetComponent<Rigidbody2D>().Sleep();
    }
}

public enum EChangeOpacity
{
    No = 0,
    FadeIn = 1,
    FadeOut = -1
}