using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDragAndDrop : DragableObject {

    [Header ("Item Speciffic")]
    public bool disableTarget = true;  // Set to false if you need to drag multiple instances of this item to the same target

    Transform tempParent;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (TargetHit(other))
        {
            if (disableTarget)
                targets[currentTargetIndex].enabled = false;
            tempParent = targets[currentTargetIndex].transform;
            backToStartPosition = false;
            TargetDone();
            this.enabled = false;
        }
    }

    protected override void EndDragMethod()
    {
        if(tempParent != null)
        {
            transform.SetParent (tempParent);
//            StopCoroutine("LerpToTargetPos");
//            StartCoroutine(LerpToTargetPos(Vector3.zero));
            transform.localPosition = Vector3.zero;
        }
    }
}
