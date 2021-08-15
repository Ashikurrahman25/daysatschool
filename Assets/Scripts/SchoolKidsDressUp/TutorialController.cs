using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour {

    public Transform[] tapTargets;
    public ItemTargetPair[] itemTargetPair;

    Transform animHolder;
    Animator anim;

    float distance;
    Vector3 itemPos;
    Vector3 targetPos;

    Coroutine crtn;
    Coroutine moveCrtn;
    WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

    void Start()
    {
        animHolder = transform.GetChild(0);
        anim = animHolder.GetComponent<Animator>();
        animHolder.gameObject.SetActive(false);
    }

    public void TapTheTarget(int index, float delay)
    {
        if (crtn != null)
            StopCoroutine(crtn);
        crtn = StartCoroutine(ShowTapTarget(index,delay));
    }

    IEnumerator ShowTapTarget(int i, float d)
    {
        yield return new WaitForSeconds(d);
        animHolder.position = tapTargets[i].position;
        animHolder.gameObject.SetActive(true);
        anim.Play("TutorialTapOnItem",0,0);
    }

    public void DragItemToTarget(int index, float delay)
    {

        if (crtn != null)
            StopCoroutine(crtn);

        crtn = StartCoroutine(ShowDragToTarget(index, delay));
    }

    IEnumerator ShowDragToTarget(int i, float d)
    {
        yield return new WaitForSeconds(d);
        itemPos = itemTargetPair[i].item.position;
        targetPos = itemTargetPair[i].target.position;
        distance = Vector3.Distance(itemPos,targetPos);
        animHolder.position = itemPos;

        animHolder.gameObject.SetActive(true);
        anim.Play("TutorialDragItem",0,0);
        if (moveCrtn != null)
            StopCoroutine(moveCrtn);
        moveCrtn = StartCoroutine("MoveItemTowardsTarget");
    }

    public void StopTutorial()
    {
        CancelInvoke();
        if (moveCrtn != null)
            StopCoroutine(moveCrtn);
        if (crtn != null)
            StopCoroutine(crtn);
        anim.enabled = false;
        animHolder.gameObject.SetActive(false);
    }

    IEnumerator MoveItemTowardsTarget()
    {
        yield return new WaitForSeconds(0.6f);
        while (Vector3.Distance(animHolder.position,targetPos) > 0.1f)
        {
            animHolder.position = Vector3.MoveTowards(animHolder.position, targetPos, Time.fixedDeltaTime*distance);
            Debug.Log("distance"+distance);
            yield return waitForFixedUpdate;
        }
        animHolder.position = targetPos;
        yield return new WaitForSeconds(0.8f);
        animHolder.position = itemPos;
        anim.Play("TutorialDragItem",0,0);

        if(moveCrtn != null)
            StopCoroutine(moveCrtn);
        moveCrtn = StartCoroutine("MoveItemTowardsTarget");
    }
}

[System.Serializable]
public struct ItemTargetPair
{
    public Transform item;
    public Transform target;
}
