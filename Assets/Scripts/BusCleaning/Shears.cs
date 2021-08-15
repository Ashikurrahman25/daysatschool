using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Shears : MonoBehaviour ,  IBeginDragHandler, IDragHandler, IEndDragHandler
{
	
	bool bEnableDrag = true;
	bool bDrag = false;
	bool bEnableCut = true; 
	float x;
	float y;
	
	Animator anim;
	public string BranchesToCut = "branch1,branch2,branch3,branch4,branch5,";
	Vector3 diffPos = new Vector3(0,0,0);
	Vector3 offPos = new Vector3(1f,.6f,0);
	Vector3 offPos2= new Vector3(-.5f,.5f,0);
	 
	void OnTriggerStay2D(Collider2D other) {
		if(bEnableCut &&  bDrag)
		{
			CarWashSoundManager.Instance.Play_Sound(CarWashSoundManager.Instance.Shears);
			anim.SetTrigger("Cut");
			StartCoroutine("CutBranch",other.transform);
			bEnableDrag = false;
		}

	}

	IEnumerator CutBranch(Transform branch)
	{
		bEnableCut = false;

		Vector3 v1 = transform.position;
		Vector3 v2 = branch.position - offPos2;
		branch.GetComponent<Collider2D>().enabled = false;

		for(float j = 0;j<=1;j+=0.1f)
		{
			transform.position  =   Vector3.Lerp   (v1,v2,j );
			yield return new WaitForFixedUpdate();
		}


		yield return new WaitForSeconds(.2f);
		CarWashSoundManager.Instance.Stop_Sound(CarWashSoundManager.Instance.Shears);
		CarWashSoundManager.Instance.Play_Sound(CarWashSoundManager.Instance.Shears);
		branch.GetComponent<Branch>().bCut = true;
		CarWashLevelManager.Instance.AddStarBranch();
		BranchesToCut = BranchesToCut.Replace((branch.name+","), "");
		yield return new WaitForSeconds(.3f);

		bEnableCut = true; 
		bEnableDrag =  true;
		diffPos = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition) - diffPos +offPos  ;
		diffPos = new Vector3(diffPos.x,diffPos.y,0);
	}

	void Start()
	{
		anim = transform.GetComponent<Animator>();
		 
	}
	
 
	
	public void OnBeginDrag(PointerEventData eventData)
	{
		if(!bEnableDrag) return;
		
		bDrag = true;
	 
		TutorialBusCleaning.bPause = true;
		diffPos = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition)   ;
		diffPos = new Vector3(diffPos.x,diffPos.y,0);
		
	}
	
	public void OnDrag(PointerEventData eventData)
	{
//		if( bEnableDrag &&  bDrag )
//		{
//			diffPos = 0.9f*diffPos;
//			x = Input.mousePosition.x;
//			y = Input.mousePosition.y;
//
//			transform.position =     Camera.main.ScreenToWorldPoint(new Vector3(x ,y,100.0f)) + diffPos -offPos ;
//			if( transform.position.y > 3f) transform.position  = new Vector3(transform.position .x,3f,transform.position.z);
//			if( transform.position.y < -3f) transform.position  = new Vector3(transform.position .x,-3f,transform.position.z);
//			
//		}
	}
	
	public void OnEndDrag(PointerEventData eventData)
	{
		if(  bEnableDrag &&  bDrag   )
		{
			x = Input.mousePosition.x;
			y = Input.mousePosition.y;
			
			//transform.position =     Camera.main.ScreenToWorldPoint(new Vector3(x ,y,100.0f)) + diffPos  -offPos ;
			if( transform.position.y >3.5f) transform.position  = new Vector3(transform.position .x,3.5f,transform.position.z);
			if( transform.position.y < -3f) transform.position  = new Vector3(transform.position .x,-3f,transform.position.z);
			
		}
		
		bDrag = false;
		TutorialBusCleaning.timeWaitToShowHelp = TutorialBusCleaning.PeriodToShowHelp;
		TutorialBusCleaning.bPause = false;
	}

	void Update()
	{
		if( bEnableDrag &&  bDrag )
		{
			 
			x = Input.mousePosition.x;
			y = Input.mousePosition.y;
			
			transform.position =     Vector3.Lerp (transform.position, Camera.main.ScreenToWorldPoint(new Vector3(x ,y,100.0f))  - offPos ,5* Time.deltaTime);
            if( transform.position.y > 2f) transform.position  = new Vector3(transform.position .x,2f,transform.position.z);    
//			if( transform.position.y < -3f) transform.position  = new Vector3(transform.position .x,-3f,transform.position.z);
			
		}
	}

	void OnDisable()
	{
		bDrag = false;
	}
	
}