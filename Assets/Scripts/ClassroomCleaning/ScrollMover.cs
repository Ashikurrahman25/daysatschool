using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScrollMover : MonoBehaviour,IPointerClickHandler {

    public ScrollRect scroll;
    public bool vertical;
    public bool decrement;

    #region IPointerClickHandler implementation
    public void OnPointerClick(PointerEventData eventData)
    {
        Vector2 old = scroll.normalizedPosition;
        if(!decrement)
        {
            if (vertical)
                old.y += 0.2f;
            else
                old.x += 0.2f;
        }
        else
        {
            if (vertical)
                old.y -= 0.2f;
            else
                old.x -= 0.2f;
        }

        scroll.normalizedPosition = old;
    }
    #endregion
}
