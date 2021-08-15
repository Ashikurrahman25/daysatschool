using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ItemDragEvents : MonoBehaviour,IBeginDragHandler, IDragHandler, IEndDragHandler {

    public bool isDraggable = true;    //FIXME Enable dragging from game manager

    [Header ("Targets")]
    public List<Collider2D> targets;  // Array of targets for this item
    
    [Header("Drag behaviour")]
    public bool lepring;
    public float draggingSpeed;
    
    [Header("Resize and Rotate")]
    public bool scaleWhenGrabbed;
    public Vector3 scaleVector;
    public bool resizeWhenGrabbed;
    public Vector2 resizeVector;
    public bool rotateWenGrabbed;
    public Vector3 rotationVector;

    int currentTargetIndex;
    Transform startParent;
    Vector3 startPos;
    Vector3 startScale;
    Vector2 startDeltaSize;
    Quaternion startRot;
    bool[] targetDone;
    Vector3 tempVector;
    Vector3 targetPosition;
    bool onTargetPlace;
    PointerEventData currentEventData;

    #region UnityEvent-s
    [Header("Drag events")]
    public UnityEvent itemBeginDrag;
    public UnityEvent itemEndDrag;
    public UnityEvent itemDrag;
    [Header("Trigger events")]
    public UnityEvent itemTriggerEnter;
    public UnityEvent itemTriggerExit;
    public float triggerStayFireRate = 0.2f;   //TODO Here you can set fire rate of TriggerStay
    public UnityEvent itemTriggerStay;
    #endregion

    void Start()
    {
        startPos = transform.localPosition;
        startParent = transform.parent;
        startScale = transform.localScale;
        startDeltaSize = GetComponent<RectTransform>().sizeDelta;
        startRot = transform.rotation;
        targetDone = new bool[targets.Count];
    }

    #region Drag interfaces implementation
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (scaleWhenGrabbed)
            transform.localScale = scaleVector; 
        if (resizeWhenGrabbed)
            GetComponent<RectTransform>().sizeDelta = resizeVector;
        if (rotateWenGrabbed)
            transform.rotation = Quaternion.Euler(rotationVector);
        transform.SetParent(GameObject.Find("Canvas").transform);

        currentEventData = eventData;
        itemBeginDrag.Invoke();
    }
    //TODO add maximum height (or drag borders)
    public void OnDrag(PointerEventData eventData)
    {
        if (!isDraggable)
            return;
        tempVector = Camera.main.ScreenToWorldPoint(eventData.position);
        tempVector.z = 90;
        targetPosition = tempVector;

        if (lepring)
        {
            transform.position = Vector3.Lerp(transform.position,targetPosition, draggingSpeed * Time.fixedTime / 1.5f);
            if (Vector3.Distance(transform.localPosition, targetPosition) < 0.2f)
            {
                transform.position = targetPosition;
            }
        }
        else
        {
            transform.position = targetPosition;
        }
        itemDrag.Invoke();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(startParent);
        itemEndDrag.Invoke();
    }
    #endregion

    #region Trigger events
    /// <summary>
    /// Raises the TriggerEnter2D event. Takes care of rubbing counter and event.
    /// </summary>
    /// <param name="other">Other.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (TargetHit(other))
        { 
            other.GetComponent<DragSystemEffects>().enabled = true;
            itemTriggerEnter.Invoke();
            onTargetPlace = true;
            StartCoroutine(CTriggerStay());
        }
    }

    IEnumerator CTriggerStay()
    {
        while (onTargetPlace)
        {
            itemTriggerStay.Invoke();
            Debug.Log("TriggerStay");
            yield return new WaitForSeconds(triggerStayFireRate);
        }
        yield return null;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other == targets[currentTargetIndex])
        {
            other.GetComponent<DragSystemEffects>().enabled = false;
            onTargetPlace = false;
            StopCoroutine("CTriggerStay");
            itemTriggerExit.Invoke();
        }
    }
    #endregion

    #region SelfEffects
    public void SetItemDraggable(bool b)
    {
        isDraggable = b;
    }

    /// <summary>
    /// Returns item to start position and reset its transform state (parent,scale,size,rotation)
    /// </summary>
    public void BackToStartPosition()
    {
//        currentEventData.dragging = false;
        currentEventData.pointerDrag = null;    // triggers OnEndDrag
        transform.SetParent(startParent);
        if (scaleWhenGrabbed)
            transform.localScale = startScale;
        if (resizeWhenGrabbed)
            GetComponent<RectTransform>().sizeDelta = startDeltaSize;
        if (rotateWenGrabbed)
            transform.rotation = startRot;
        StartCoroutine(LerpToTargetPos(startPos));
    }

    /// <summary>
    /// Set the target as parent and lerp item to the position of the target. Keeps transform state (scale,size,rotation)
    /// </summary>
    public void GoToTargetPosition()
    {
//        currentEventData.dragging = false;
        currentEventData.pointerDrag = null;    // triggers OnEndDrag
        transform.SetParent(targets[currentTargetIndex].transform);
        StartCoroutine(LerpToTargetPos(Vector3.zero));
    }
    //FIXME public Vector3 to set value. Bool or Method to apply the value
    public void ScaleWhenGrabbed()
    {
        
    }

    public void ResizeWhenGrabbed()
    {
        
    }

    public void RotateWhenGrabbed()
    {
        
    }
    #endregion 

    #region Helpers
    /// <summary>
    /// Helper method.
    /// </summary>
    /// <returns><c>true</c> if "other" is a valid target, <c>false</c> otherwise.</returns>
    /// <param name="other">Other.</param>
    bool TargetHit(Collider2D other)
    {
        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i] == other)
            {
                currentTargetIndex = i;
                return true;
            }
        }

        return false;
    }

    IEnumerator LerpToTargetPos(Vector3 localTargetPos)
    {
        yield return new WaitForFixedUpdate();
        while (true)
        {
            yield return new WaitForFixedUpdate();
            transform.localPosition = Vector3.Lerp(transform.localPosition,localTargetPos, 30 * Time.deltaTime / 1.5f);
            if (Vector3.Distance(transform.localPosition, localTargetPos) < 0.2f)
            {
                transform.localPosition = localTargetPos;
                break;
            }
        }
    }
    #endregion

}
