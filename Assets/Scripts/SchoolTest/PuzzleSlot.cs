using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleSlot : MonoBehaviour {

    public Sprite desiredSprite;

    public bool RightSprite()
    {
        bool ret = false;

        if(transform.childCount>0)
        {
            if (desiredSprite == transform.GetChild(0).GetComponent<Image>().sprite)
                ret = true;
        }

        return ret;
    }
}
