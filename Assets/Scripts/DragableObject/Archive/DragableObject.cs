using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// Dragable object v1.1
/// </summary>
public class DragableObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public bool isDragable = true;


    [Header("Drag behaviour")]
    public bool backToStartPosition;
    public bool lepring;
    public float draggingSpeed;

    [Header ("Targets")]
    public bool progressiveTargets; // If "true", targets will be turned on one by one.
    public Collider2D[] targets;  // Array of targets for this item

    [Header("Offset")]
    public bool offset;
    public Vector3 offsetVector;
    public Transform dragPoint;

    [Header("Resize and Rotate")]
    public bool scaleWhenGrabbed;
    public Vector3 scaleVector;
    public bool resizeWhenGrabbed;
    public Vector2 resizeVector;
    public bool rotateWenGrabbed;
    public Vector3 rotationVector;

    [Header("Check item when doned")]
    public GameObject check;

    protected Animator itemAnimator;
    protected int currentTargetIndex;

    Transform parent;
    Vector3 startPos;
    Vector3 startScale;
    Vector2 startDeltaSize;
    Quaternion startRot;
    bool[] targetDone;
    Vector3 tempVector;
    Vector3 targetPosition;

    Vector3 topLimit;

    #region UnityEventFunctions
    void Start()
    {
        startPos = transform.localPosition;
        parent = transform.parent;
        startScale = transform.localScale;
        startDeltaSize = GetComponent<RectTransform>().sizeDelta;
        startRot = transform.rotation;
        targetDone = new bool[targets.Length];
        itemAnimator = GetComponent<Animator>();
        if (progressiveTargets)
        {
            for (int i = 1; i < targets.Length; i++)
            {
                targets[i].enabled = false;
            }
        }

        topLimit = GameObject.Find("Canvas/TopLimit").transform.position;
    }

//    //FIXME move to ondrag
//    void Update()
//    {
//        if (itemIsMoving)
//        {
//            if (lepring)
//            {
//                transform.position = Vector3.Lerp(transform.position,targetPosition, draggingSpeed * Time.deltaTime / 1.5f);
//                if (Vector3.Distance(transform.localPosition, targetPosition) < 0.2f)
//                {
//                    transform.position = targetPosition;
//                }
//            }
//            else
//            {
//                transform.position = targetPosition;
//            }
//        }
//    }

    public void OnBeginDrag(PointerEventData eventData)
    {

        if (!isDragable)
            return;
        if(offset && dragPoint!=null)
            offsetVector = transform.position - dragPoint.position;
        if (scaleWhenGrabbed)
            transform.localScale = scaleVector;	
        if (resizeWhenGrabbed)
            GetComponent<RectTransform>().sizeDelta = resizeVector;
        if (rotateWenGrabbed)
            transform.rotation = Quaternion.Euler(rotationVector);

        transform.SetParent(GameObject.Find("Canvas").transform);
        BeginDragMethod();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragable)
            return;
        tempVector = Camera.main.ScreenToWorldPoint(eventData.position);
        if (offset)
            tempVector += offsetVector;
        tempVector.z = 90;
//        transform.position = tempVector;

        if (tempVector.y > topLimit.y)
            tempVector.y = topLimit.y;

        targetPosition = tempVector;

        if (lepring)
        {
            transform.position = Vector3.Lerp(transform.position,targetPosition, draggingSpeed * Time.deltaTime / 1.5f);
            if (Vector3.Distance(transform.localPosition, targetPosition) < 0.2f)
            {
                transform.position = targetPosition;
            }
        }
        else
        {
            transform.position = targetPosition;
        }

        DragMethod();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
//        Debug.Log("EndDrag executed");
        DropItem();
    }

    void OnApplicationPause(bool p)
    {
        if (!p)
            return;
        {			
            DropItem();
        }
    }
    #endregion

    /// <summary>
    /// Methods that we override from derived class.
    /// </summary>
    #region OverrideMethods
    protected virtual void BeginDragMethod()
    {
        
    }

    protected virtual void DragMethod()
    {
        
    }

    protected virtual void EndDragMethod()
    {
        
    }
    #endregion
    
    /// <summary>
    /// Helper method.
    /// </summary>
    /// <returns><c>true</c> if "other" is a valid target, <c>false</c> otherwise.</returns>
    /// <param name="other">Other.</param>
    protected bool TargetHit(Collider2D other)
    {
        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i] == other)
            {
                currentTargetIndex = i;
                return true;
            }
        }

        return false;
    }
    
    /// <summary>
    /// Called when target is done. If all targets are doned, calls FinishItem().
    /// </summary>
    protected void TargetDone()
    {
        Debug.Log("Target Done");
        targetDone[currentTargetIndex] = true;

        if (progressiveTargets && currentTargetIndex < targets.Length-1)
        {
            targets[currentTargetIndex].enabled = false;
            targets[currentTargetIndex+1].enabled = true;
        }
        
        for (int i = 0; i < targetDone.Length; i++)
        {
            if (!targetDone[i])
                return;
        }

        FinishItem();
    }
    
    void FinishItem()
    {
        Debug.Log("Finish item");
        DropItem();
        isDragable = false;
        if (check != null)
            check.SetActive(true);
    }
    
    void DropItem()
    {
        if (!isDragable)
            return;     // prevents double-drop when item is finished
        transform.SetParent(parent);
        if (backToStartPosition)
        {
            if (scaleWhenGrabbed)
                transform.localScale = startScale;
            if (resizeWhenGrabbed)
                GetComponent<RectTransform>().sizeDelta = startDeltaSize;
            if (rotateWenGrabbed)
                transform.rotation = startRot;
            if (lepring)
            {
                StopCoroutine("LerpToTargetPos");
                StartCoroutine(LerpToTargetPos(startPos));
            }
            else
            {
                transform.localPosition = startPos;
            }
        }
        EndDragMethod();
    }

    protected IEnumerator LerpToTargetPos(Vector3 localTargetPos)
    {
        yield return new WaitForEndOfFrame();
        while (true)
        {
            yield return new WaitForEndOfFrame();
            transform.localPosition = Vector3.Lerp(transform.localPosition,localTargetPos, 30 * Time.deltaTime / 1.5f);
            if (Vector3.Distance(transform.localPosition, localTargetPos) < 0.2f)
            {
                transform.localPosition = localTargetPos;
                break;
            }
        }
    }
}
