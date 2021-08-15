using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI.Extensions;
using UnityEngine.UI;

public class MatchTheColorItem : MonoBehaviour,IDragHandler,IBeginDragHandler,IEndDragHandler ,IPointerEnterHandler
{
    public int index;

    private UILineRenderer lineRenderer;
    private Vector3 ratio;
    Vector3 pos = new Vector3();

    private bool matched=false;
    void Awake()
    {
        lineRenderer = transform.GetChild(0).GetComponent<UILineRenderer>();
        Resolution deviceRes = Screen.currentResolution;
        GameObject canvas = GameObject.Find("Canvas");
        ratio = new Vector3(Screen.width / canvas.GetComponent<CanvasScaler>().referenceResolution.x, Screen.height / canvas.GetComponent<CanvasScaler>().referenceResolution.y,1f);
        Debug.Log(canvas.GetComponent<CanvasScaler>().referenceResolution);
        Debug.Log(deviceRes);
        Debug.Log(ratio);
    }

    #region IBeginDragHandler implementation

    public void OnBeginDrag(PointerEventData eventData)
    {
        lineRenderer.Points[1] = new Vector2(0, 0);
        MatchTheColorGameManager.startedMatching = this;
    }

    #endregion

    #region IDragHandler implementation

    public void OnDrag(PointerEventData eventData)
    {
        if (!matched)
        {
            Vector3 posBeforeCalc = (Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position));
            pos.x = posBeforeCalc.x / ratio.x;
            pos.y = posBeforeCalc.y / ratio.x;
            pos.z = 0;

            lineRenderer.Points[1] = pos;
            lineRenderer.SetVerticesDirty();
        }
    }

    #endregion
	
    #region IEndDragHandler implementation

    public void OnEndDrag(PointerEventData eventData)
    {
        MatchTheColorGameManager.startedMatching = null;
        lineRenderer.Points[1] = Vector2.zero;
        lineRenderer.SetVerticesDirty();
    }

    #endregion

    #region IPointerEnterHandler implementation

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("POINTER ENTER");
        if(MatchTheColorGameManager.startedMatching!=null)
        {
            if(MatchTheColorGameManager.startedMatching!=this)
            {
                if(MatchTheColorGameManager.startedMatching.index==index)
                {
                    matched = true;
//                    Vector2 endLinePos = V2(Camera.main.WorldToScreenPoint(transform.GetChild(1).position) - Camera.main.WorldToScreenPoint(MatchTheColorGameManager.startedMatching.transform.GetChild(0).position));
                    Vector2 endLinePos = V2(Camera.main.WorldToScreenPoint(transform.GetChild(0).position) - Camera.main.WorldToScreenPoint(MatchTheColorGameManager.startedMatching.transform.GetChild(0).position));

                    endLinePos.y /= ratio.x;
                    endLinePos.x /= ratio.x;
                    MatchTheColorGameManager.startedMatching.lineRenderer.Points[1] = endLinePos;
                    MatchTheColorGameManager.startedMatching.lineRenderer.SetVerticesDirty();
                    MatchTheColorGameManager.Instance.Matched(index);
                    MatchTheColorGameManager.startedMatching.enabled = false;
                    MatchTheColorGameManager.startedMatching = null;
                    this.enabled = false;
                }
            }
        }
    }

    #endregion

    Vector2 V2(Vector3 v3)
    {
        return new Vector2(v3.x, v3.y);
    }
}
