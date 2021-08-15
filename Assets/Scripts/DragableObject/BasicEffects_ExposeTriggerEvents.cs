using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BasicEffects_ExposeTriggerEvents : MonoBehaviour {

    public UnityEvent triggerEnter;
    public UnityEvent triggerStay;
    public UnityEvent triggerExit;

    Collider2D otherCollider;

    void Start()
    {
        if(GetComponent<Collider2D>() == null)
            throw new System.Exception("There is no 'Collieder2D' attached to this GameObject! ");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        otherCollider = other;
        triggerEnter.Invoke();
    }

    void OnTriggerStay2D()
    {
        triggerStay.Invoke();
    }

    void OnTriggerExit2D()
    {
        otherCollider = null;
        triggerExit.Invoke();
    }

    public void Effect_DestroyOther()
    {
        if (otherCollider != null)
            Destroy(otherCollider.gameObject);
    }

}
