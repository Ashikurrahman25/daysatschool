using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisCheckManager : MonoBehaviour {

    [Header ("Setup")]
    public Transform aim;
    public float aimHorizontalSpeed;
    public float aimVerticalSpeed;
    public float hitDistance;

    [Header ("References")]
    public List<Transform> targetsX;
    public List<Transform> targetsY;

    [Header("Borders")]
    public Transform borderTop;
    public Transform borderBot;
    public Transform borderLeft;
    public Transform borderRight;

    Coroutine currentCoroutine;
    WaitForFixedUpdate wffu = new WaitForFixedUpdate();
    Vector3 speedVector;
    Vector3 directionVector;
    Vector3 moveVector;
    List<Transform> donedTargets = new List<Transform>();

    bool checking;

    void OnEnable()
    {
        speedVector = new Vector3(aimHorizontalSpeed,aimVerticalSpeed,0);
        directionVector = Vector3.right;
        aim.position = borderLeft.transform.position;
        Invoke("MoveAim",1f);
//        GameObject.Find("Canvas/MainPanel/CharacterHolder")
    }

    void OnDisable()
    {
        CancelInvoke();
        if(currentCoroutine != null)
            StopCoroutine(currentCoroutine);
    }

    public void CheckForHit()
    {
        if (checking)
            return;
        else
            checking = true;
         
        if(currentCoroutine != null)
            StopCoroutine(currentCoroutine);

        // first we try to hit all X targets then Y
        if (targetsX.Count > 0)
        {
            for (int i = 0; i < targetsX.Count; i++)
            {
                if (Vector3.Distance(aim.position, targetsX[i].position) < hitDistance)
                {
                    foreach (Transform hitMark in targetsX[i])
                    {
                        hitMark.gameObject.SetActive(true);
                    }
                    donedTargets.Add(targetsX[i]);
                    targetsX.RemoveAt(i);
                    TargetHit(true);
                    return;
                }
            }

            TargetMiss();
        }
        else if (targetsY.Count > 0)
        {
            for (int i = 0; i < targetsY.Count; i++)
            {
                if (Vector3.Distance(aim.position, targetsY[i].position) < hitDistance)
                {
                    foreach (Transform hitMark in targetsY[i])
                    {
                        hitMark.gameObject.SetActive(true);
                    }
                    donedTargets.Add(targetsY[i]);
                    targetsY.RemoveAt(i);
                    TargetHit(false);
                    return;
                }
            }

            TargetMiss();
        }
    }

    void TargetHit(bool isX)
    {
        SoundManagerEyeExamination.PlaySound("MiniGameCorrect");
        if (isX)
        {
            Debug.Log("We have a hit on X!");
            if (targetsX.Count == 0)
            {
                Invoke("SwitchAxis",0.9f);
            }
            Invoke("MoveAim",1f);
        }
        else
        {
            Debug.Log("We have a hit on Y!");
            if (targetsY.Count == 0)
            {
                Debug.Log("Game Over!");
                Invoke("EndGame",1f);
            }
            else
                Invoke("MoveAim",1f);
        }

    }

    void TargetMiss()
    {
        Debug.Log("Miss!");

        SoundManagerEyeExamination.PlaySound("MiniGameWrong");
        Invoke("MoveAim",0.3f);
    }

    void SwitchAxis()
    {
        foreach (Transform target in donedTargets)
        {
            foreach (Transform hitMark in target)
            {
                hitMark.gameObject.SetActive(false);
            }
            target.gameObject.SetActive(false);
        }
        donedTargets.Clear();

        foreach (Transform target in targetsY)
        {
            target.gameObject.SetActive(true);
        }

        aim.GetChild(0).gameObject.SetActive(false);
        aim.GetChild(1).gameObject.SetActive(true);
        aim.position = borderTop.position;
        directionVector = Vector3.down;

    }

    void EndGame()
    {
        GetComponent<ItemGame>().GameFinished();
        GameObject.Find("Canvas").GetComponent<MenuManager>().ClosePopUpMenu(gameObject);
    }

    #region Aim movement
    void MoveAim()
    {
        checking = false;
        moveVector = Vector3.Scale(directionVector,speedVector);

        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);
        
        currentCoroutine = StartCoroutine(MoveAimCoroutine());
    }

    IEnumerator MoveAimCoroutine()
    {
        Debug.Log("MoveAim crtn started called");
        while (BorderCheck())
        {
            yield return wffu;
            aim.position += moveVector;
        }
    }

    bool BorderCheck()
    {
        if (directionVector == Vector3.up)
        {
            if (aim.position.y >= borderTop.position.y)
                RedirectVector( Vector3.down);
        }
        else if (directionVector == Vector3.down)
        {
            if (aim.position.y <= borderBot.position.y)
                RedirectVector( Vector3.up);
        }
        else if (directionVector == Vector3.left)
        {
            if (aim.position.x <= borderLeft.position.x)
                RedirectVector( Vector3.right);
        }
        else if (directionVector == Vector3.right)
        {
            if (aim.position.x >= borderRight.position.x)
                RedirectVector(Vector3.left);
        }

        return true;
    }

    void RedirectVector(Vector3 newDirection)
    {
        directionVector = newDirection;
        moveVector = Vector3.Scale(directionVector,speedVector);
    }
    #endregion
}
