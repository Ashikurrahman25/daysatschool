using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class MatchTheColorGameManager : MonoBehaviour {
    public static MatchTheColorItem startedMatching;
    public static MatchTheColorGameManager Instance;

    List<int> toBeMatched=new List<int>();

    GameObject canvas;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        canvas = GameObject.Find("Canvas");
        Restart();
    }

    public void Matched(int index)
    {
        toBeMatched.Remove(index);
        if (toBeMatched.Count == 0)
            Finished();
    }

    public void Restart()
    {
        foreach (Transform t in transform.Find("AnimationHolder/BookHolder/AnimationHolder/Fruits"))
        {
            if(!toBeMatched.Contains(t.GetComponent<MatchTheColorItem>().index))
                toBeMatched.Add(t.GetComponent<MatchTheColorItem>().index);
            t.GetComponent<MatchTheColorItem>().enabled = true;
        }
        foreach (Transform t in transform.Find("AnimationHolder/BookHolder/AnimationHolder/Fruits"))
        {
            t.GetChild(0).GetComponent<UILineRenderer>().Points[1] = Vector2.zero;
        }
        foreach (Transform t in transform.Find("AnimationHolder/BookHolder/AnimationHolder/ColorShape"))
        {
            t.GetChild(0).GetComponent<UILineRenderer>().Points[1] = Vector2.zero;
            t.GetComponent<MatchTheColorItem>().enabled = true;
        }
    }

    void Finished()
    {
        Debug.Log("MATCH THE COLOR IGRA ZAVRSENA!!!");
        SchoolTestGameManager.Instance.MiniGameFinished();
        StartCoroutine(FadeOutAndDisable());
    }

    public void Help()
    {
        Transform fruit = null;
        Transform target = null;
        foreach (Transform t in transform.Find("AnimationHolder/BookHolder/AnimationHolder/Fruits"))
        {
            if (t.GetComponent<MatchTheColorItem>().enabled)
            {
                fruit = t;
                break;
            }
        }

        if(fruit!=null)
        {
            foreach (Transform t in transform.Find("AnimationHolder/BookHolder/AnimationHolder/ColorShape"))
            {
                if (t.GetComponent<MatchTheColorItem>().index==fruit.GetComponent<MatchTheColorItem>().index)
                {
                    target = t;
                    break;
                }
            }
        }
        if(fruit!=null&&target!=null)
        {
            StopAllCoroutines();
            StartCoroutine(MoveFromToAndFade(transform.Find("HandPointer"),fruit,target));
        }
    }
    IEnumerator MoveFromToAndFade(Transform t,Transform start, Transform target)
    {
        t.GetComponent<Image>().color = Color.white;
        float a = 0f;
        while(a<1f)
        {
            a += 0.01f;
            t.position = Vector3.Lerp(start.position, target.position, a);
            yield return new WaitForSeconds(0.01f);
        }
        t.GetComponent<Image>().color = new Color(1, 1, 1, 0);
    }


    IEnumerator FadeOutAndDisable()
    {
        float a = 1f;
        while(a>0)
        {
            a -= 0.01f;
            gameObject.GetComponent<CanvasGroup>().alpha = a;
            yield return new WaitForSeconds(0.01f);
        }

        this.gameObject.SetActive(false);
    }
}
