using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackingItem : DragableObject 
{

    int currentAnimPhase;

    void OnTriggerEnter2D (Collider2D other)
    {
//        Debug.Log("Collision " + other.tag + " " + transform.tag);
        if (other.tag == transform.tag) 
        {
            transform.SetParent (other.transform.GetChild(0));
            transform.localPosition = Vector2.zero;
            currentAnimPhase = other.transform.parent.GetComponent<Animator>().GetInteger("Phase");
            other.transform.parent.GetComponent<Animator>().SetInteger("Phase",currentAnimPhase+1);
            GameObject.Find("PackingLevelManager").GetComponent<PackingLevelManager>().ItemFound();
            if (check != null)
                check.SetActive(true);
            other.enabled = false;
            GetComponent<Collider2D> ().enabled = false;
            this.enabled = false;
        }
    }

    protected override void BeginDragMethod()
    {
        //PAVLE prekini tutorial
//        GameObject.Find("PackingLevelManager").GetComponent<PackingLevelManager>().tutorialController.StopTutorial();
    }
}
