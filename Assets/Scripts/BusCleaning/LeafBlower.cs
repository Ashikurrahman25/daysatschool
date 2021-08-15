using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class LeafBlower : MonoBehaviour ,  IBeginDragHandler, IDragHandler, IEndDragHandler
{
	
	bool bEnableDrag = true;
	bool bDrag = false;
 
	public ParticleSystem psLeaves1;
	public ParticleSystem psLeaves2;
	public ParticleSystem psLeaves3;
	public ParticleSystem psLeaves4;
	public ParticleSystem psLeaves5;

	float x;
	float y;

	Image Air;
	Transform target;
	 
	Vector3 diffPos = new Vector3(0,0,0);
 
	public bool bLeaves1 = true;
	public bool bLeaves2 = true;
	public bool bLeaves3 = true;
	public bool bLeaves4 = true;
	public bool bLeaves5 = true;

 

	void Awake()
	{
		Air = transform.Find("Air").GetComponent<Image>();
		Air.enabled = false;

		target = transform.Find("Target").transform;
	 
		CarWashSoundManager.Instance.Stop_Sound(CarWashSoundManager.Instance.Air);
	}
	
	void TestClean () {
		
		if( bDrag  )
		{
//			Debug.Log("lisce pozicija:     " + psLeaves1.transform.position.ToString() + "     pozicija blowera:     " + target.position.ToString());
			if( bLeaves1 &&  Vector2.Distance( psLeaves1.transform.position,target.position) < 1.3f) 		   { psLeaves1.Play();  bLeaves1= false; GameObject.Destroy(psLeaves1.gameObject,1.5f); CarWashLevelManager.Instance.AddStarLeaves();  }
			else if(  bLeaves2 &&  Vector2.Distance( psLeaves2.transform.position,target.position) < 1.3f) { psLeaves2.Play();  bLeaves2= false;  GameObject.Destroy(psLeaves2.gameObject,1.5f); CarWashLevelManager.Instance.AddStarLeaves(); }
			else if( bLeaves3 && Vector2.Distance( psLeaves3.transform.position,target.position) < 1.3f)   { psLeaves3.Play();  bLeaves3= false; GameObject.Destroy(psLeaves3.gameObject,1.5f); CarWashLevelManager.Instance.AddStarLeaves(); }
			else if( bLeaves4 &&  Vector2.Distance( psLeaves4.transform.position,target.position) < 1.3f)  { psLeaves4.Play();  bLeaves4= false;  GameObject.Destroy(psLeaves4.gameObject,1.5f); CarWashLevelManager.Instance.AddStarLeaves(); }
			else if( bLeaves5 &&  Vector2.Distance( psLeaves5.transform.position,target.position) < 1.3f)  { psLeaves5.Play();  bLeaves5= false;  GameObject.Destroy(psLeaves5.gameObject,1.5f); CarWashLevelManager.Instance.AddStarLeaves(); }
			else return;
			CarWashSoundManager.Instance.Play_Sound(CarWashSoundManager.Instance.Leaves);
		}
	}

	void Start()
	{
		if(GameObject.Find("CAR/Leaves/Leaves0")!=null ) psLeaves2 = (ParticleSystem) GameObject.Find("CAR/Leaves/Leaves0").GetComponent<ParticleSystem>();
		psLeaves1 = (ParticleSystem) GameObject.Find("CAR/Leaves/Leaves1").GetComponent<ParticleSystem>();
		if(GameObject.Find("CAR/Leaves/Leaves2")!=null ) psLeaves2 = (ParticleSystem) GameObject.Find("CAR/Leaves/Leaves2").GetComponent<ParticleSystem>();
		psLeaves3 = (ParticleSystem) GameObject.Find("CAR/Leaves/Leaves3").GetComponent<ParticleSystem>();
		psLeaves4 = (ParticleSystem) GameObject.Find("CAR/Leaves/Leaves4").GetComponent<ParticleSystem>();
		psLeaves5 = (ParticleSystem) GameObject.Find("CAR/Leaves/Leaves5").GetComponent<ParticleSystem>();
	}
	 
	
	
	
	public void OnBeginDrag(PointerEventData eventData)
	{
		if(!bEnableDrag) return;
		InvokeRepeating("TestClean",.3f,.3f);
		bDrag = true;
		
		diffPos = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition)   ;
		diffPos = new Vector3(diffPos.x,diffPos.y,0);
		 
		TutorialBusCleaning.bPause = true;
	}
	
	public void OnDrag(PointerEventData eventData)
	{
		if( bEnableDrag &&  bDrag )
		{
			Air.enabled = true;
			x = Input.mousePosition.x;
			y = Input.mousePosition.y;
			
			transform.position =     Camera.main.ScreenToWorldPoint(new Vector3(x ,y,100.0f)) + diffPos  ;
            if( transform.position.y > 3f) transform.position  = new Vector3(transform.position .x,3f,transform.position.z);    
//			if( transform.position.y < -3f) transform.position  = new Vector3(transform.position .x,-3f,transform.position.z);

			CarWashSoundManager.Instance.Play_Sound(CarWashSoundManager.Instance.Air);
		}
	}
	
	public void OnEndDrag(PointerEventData eventData)
	{
		if(  bEnableDrag &&  bDrag   )
		{
			Air.enabled = false;

			x = Input.mousePosition.x;
			y = Input.mousePosition.y;
			
			transform.position =     Camera.main.ScreenToWorldPoint(new Vector3(x ,y,100.0f)) + diffPos  ;
			if( transform.position.y > 3.5f) transform.position  = new Vector3(transform.position .x,3.5f,transform.position.z);
			if( transform.position.y < -3f) transform.position  = new Vector3(transform.position .x,-3f,transform.position.z);
			CarWashSoundManager.Instance.Stop_Sound(CarWashSoundManager.Instance.Air);
		}
	 
		bDrag = false;
		TutorialBusCleaning.timeWaitToShowHelp = TutorialBusCleaning.PeriodToShowHelp;
		TutorialBusCleaning.bPause = false;

		CancelInvoke ("TestClean"); 
	}
	
}

