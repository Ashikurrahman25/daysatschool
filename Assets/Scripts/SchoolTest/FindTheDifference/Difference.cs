using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Difference : MonoBehaviour,IPointerClickHandler 
{

    #region IPointerClickHandler implementation

    public void OnPointerClick(PointerEventData eventData)
    {
        transform.GetChild(0).GetComponent<Image>().enabled = true;
        transform.GetChild(0).GetComponent<Image>().color = Color.white;
        FindTheDifferenceGameManager.Instance.FoundDifference();
        this.enabled = false;
    }

    #endregion
}
