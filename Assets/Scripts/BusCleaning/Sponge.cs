using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Sponge : MonoBehaviour ,  IBeginDragHandler, IDragHandler, IEndDragHandler
{

	bool bEnableDrag = true;
	public bool bDrag = false;
	
	
	Animator anim ;
	float x;
	float y;
	
	
	Vector3 diffPos = new Vector3(0,0,0);
	
	 
	
	
	void Awake()
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
		
		anim.SetBool("clean",true);
		CarWashSoundManager.Instance.Play_Sound(CarWashSoundManager.Instance.Sponge);
	}
	
	public void OnDrag(PointerEventData eventData)
	{
		if( bEnableDrag &&  bDrag )
		{
			
			x = Input.mousePosition.x;
			y = Input.mousePosition.y;
			
			transform.position =     Camera.main.ScreenToWorldPoint(new Vector3(x ,y,100.0f)) + diffPos  ;
			if( transform.position.y > 3f) transform.position  = new Vector3(transform.position .x,3f,transform.position.z);   
//			if( transform.position.y < -2f) transform.position  = new Vector3(transform.position .x,-2f,transform.position.z);
			
			float rotZ =15* (  transform.position.y-2) + 5*transform.position.x;
			transform.rotation = Quaternion.Euler(new Vector3(0,0,rotZ));
			
		}
		
		
		
	}
	
	public void OnEndDrag(PointerEventData eventData)
	{
		if(  bEnableDrag &&  bDrag   )
		{
			x = Input.mousePosition.x;
			y = Input.mousePosition.y;
			
			transform.position =     Camera.main.ScreenToWorldPoint(new Vector3(x ,y,100.0f)) + diffPos  ;
			if( transform.position.y > 3.2f) transform.position  = new Vector3(transform.position .x,3.2f,transform.position.z);
			if( transform.position.y < -2f) transform.position  = new Vector3(transform.position .x,-2f,transform.position.z);
			
		}
		anim.SetBool("clean",false);
		CarWashSoundManager.Instance.Stop_Sound(CarWashSoundManager.Instance.Sponge);
		bDrag = false;

		TutorialBusCleaning.timeWaitToShowHelp = TutorialBusCleaning.PeriodToShowHelp;
		TutorialBusCleaning.bPause = false;
		
	}
	
	
 

}
