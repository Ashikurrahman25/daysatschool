using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class WheelBrush : MonoBehaviour,  IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public static bool bWheelBrushEnabled = true;
	bool bEnableDrag = true;
	bool bDrag = false;
	bool bCleaning = false;
	
	 Animator anim ;
	float x;
	float y;

	ParticleSystem psDirt1;
	ParticleSystem psDirt2;
	ParticleSystem psDirt3;
	
	Vector3 diffPos = new Vector3(0,0,0);
	
	int dirtWheel1Status = 0;
	int dirtWheel2Status = 0;
	int dirtWheel3Status = 0;
	PointerEventData pointerData = new PointerEventData(EventSystem.current);

	public string LeftToBeCleaned = "";
	
	void Start()
	{
		anim = transform.GetComponent<Animator>();

		if(GameObject.Find("Wheel1/psDirt") !=null)  
		{
			psDirt1 = GameObject.Find("Wheel1/psDirt").GetComponent<ParticleSystem>();
			LeftToBeCleaned += "Wheel1,";
		}
		else dirtWheel1Status = 7;

		if(GameObject.Find("Wheel2/psDirt") !=null)
		{
			psDirt2 = GameObject.Find("Wheel2/psDirt").GetComponent<ParticleSystem>();
			LeftToBeCleaned += "Wheel2,";
		}
		else dirtWheel2Status = 7;

		if(GameObject.Find("Wheel3/psDirt") !=null)
		{
			psDirt3 = GameObject.Find("Wheel3/psDirt").GetComponent<ParticleSystem>();
			LeftToBeCleaned += "Wheel3,";
		}
		else dirtWheel3Status = 7;
		 
		if(psDirt1 != null) {psDirt1.Stop(); psDirt1.enableEmission = false; }
		if(psDirt2 != null) {psDirt2.Stop(); psDirt2.enableEmission = false; }
		if(psDirt3 != null) {psDirt3.Stop(); psDirt3.enableEmission = false; }

		InvokeRepeating("TestWheel",.1f,.1f);

	 
	}

	void TestWheel () 
	{
	
	if( bDrag  )
	{
		//if (  Vector2.Distance( pointerData.position, (Vector2)Input.mousePosition) >2  )//  &&   EventSystem.current.IsPointerOverGameObject()) 
		{          

				pointerData.position = Input.mousePosition;
				
				List<RaycastResult> objectsHit = new List<RaycastResult> ();
				EventSystem.current.RaycastAll(pointerData, objectsHit);
				
				for(int x = 0;x<objectsHit.Count;x++)
				{
					if(objectsHit[x].gameObject.name.StartsWith("WheelDirt" ))
					{
						if(!bCleaning) 
							StartCoroutine(CleanDirt(objectsHit[x].gameObject));
						 
						break;
					}
				}
			}
 
		}

		if(!bCleaning) 
		{
			if(psDirt1 != null) {psDirt1.Stop(); psDirt1.enableEmission = false; }
			if(psDirt2 != null) {psDirt2.Stop(); psDirt2.enableEmission = false; }
			if(psDirt3 != null) {psDirt3.Stop(); psDirt3.enableEmission = false; }
		}
	}

	IEnumerator CleanDirt(GameObject dirt)
	{
		bCleaning = true;
		//psBrush.enableEmission = true;
		string _name = dirt.name;
		yield return new WaitForSeconds(0.05f);
		Image Dirt = (Image) dirt.GetComponent<Image>();
		float status = 0;
		switch(_name)
		{

		case "WheelDirt01": 
			status = (dirtWheel1Status++); 
			if(psDirt1 != null) {  psDirt1.enableEmission = true; psDirt1.Play(); }
			if(psDirt2 != null) {psDirt2.Stop(); psDirt2.enableEmission = false; }
			if(psDirt3 != null) {psDirt3.Stop(); psDirt3.enableEmission = false; }
			CarWashLevelManager.Instance.AddStarWheelBrush(1,(int)status);
			break;
		case "WheelDirt02": 
			status = (dirtWheel2Status++);
			if(psDirt2 != null) {  psDirt2.enableEmission = true; psDirt2.Play(); }
			if(psDirt1 != null) {psDirt1.Stop(); psDirt1.enableEmission = false; }
			if(psDirt3 != null) {psDirt3.Stop(); psDirt3.enableEmission = false; }
			CarWashLevelManager.Instance.AddStarWheelBrush(2,(int)status);
			break;
		case "WheelDirt03": 
			status = (dirtWheel3Status++);
			if(psDirt1 != null) {psDirt1.Stop(); psDirt1.enableEmission = false; }
			if(psDirt2 != null) {psDirt2.Stop(); psDirt2.enableEmission = false; }
			if(psDirt3 != null) { psDirt3.enableEmission = true; ; psDirt3.Play(); }
			CarWashLevelManager.Instance.AddStarWheelBrush(3,(int)status);
			break;
		default:
			if(psDirt1 != null) {psDirt1.Stop(); psDirt1.enableEmission = false; }
			if(psDirt2 != null) {psDirt2.Stop(); psDirt2.enableEmission = false; }
			if(psDirt3 != null) {psDirt3.Stop(); psDirt3.enableEmission = false; }
			break;
		}
		
		 //Debug.Log(_name + "  "+status);
		
		if(status == 7)
		{

			status = (status * 5 - 5)/35f;
			for(int i =0;i<=5;i++)
			{
				yield return new WaitForSeconds(0.05f);
				Dirt.color = new Color(1,1,1,1 - status  -i/40f);
			}

			LeftToBeCleaned = LeftToBeCleaned.Replace(( dirt.transform.parent.parent.parent.parent.name + ","), "");
			GameObject.Destroy(dirt );

			 
			if(psDirt1 != null) {psDirt1.Stop(); psDirt1.enableEmission = false; }
			if(psDirt2 != null) {psDirt2.Stop(); psDirt2.enableEmission = false; }
			if(psDirt3 != null) {psDirt3.Stop(); psDirt3.enableEmission = false; }
			bCleaning = false;
		}
		else
		{
			status = (status * 5 - 5)/35f;
			for(int i =0;i<=5;i++)
			{
				yield return new WaitForSeconds(0.05f);
				Dirt.color = new Color(1,1,1,1 - status  -i/40f);
			}
		}

		bCleaning = false;

		 if( dirtWheel1Status ==7 && dirtWheel2Status ==7 && dirtWheel3Status ==7 ) 
		 {
			if(psDirt1 != null) {psDirt1.Stop(); psDirt1.enableEmission = false; }
			if(psDirt2 != null) {psDirt2.Stop(); psDirt2.enableEmission = false; }
			if(psDirt3 != null) {psDirt3.Stop(); psDirt3.enableEmission = false; }
			CancelInvoke("TestWheel");
		 }
	}
	


	
	public void OnBeginDrag(PointerEventData eventData)
	{
		if(!bEnableDrag) return;
		CancelInvoke( );
		InvokeRepeating("TestWheel",.1f,.1f);

		bDrag = true;
		
		diffPos = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition)   ;
		diffPos = new Vector3(diffPos.x,diffPos.y,0);
		if(anim == null) anim = transform.GetComponent<Animator>();
  		anim.SetBool("clean",true);
//		CarWashSoundManager.Instance.Play_Sound(CarWashSoundManager.Instance.WheelBrush);
 
		TutorialBusCleaning.bPause = true;
	}
	
	public void OnDrag(PointerEventData eventData)
	{
		if( bEnableDrag &&  bDrag )
		{
			
			x = Input.mousePosition.x;
			y = Input.mousePosition.y;
			
			transform.position =     Camera.main.ScreenToWorldPoint(new Vector3(x ,y,100.0f)) + diffPos  ;
			if( transform.position.y > 2.5f) transform.position  = new Vector3(transform.position .x,2.5f,transform.position.z);    
			if( transform.position.y < -2f) transform.position  = new Vector3(transform.position .x,-2f,transform.position.z);
			
			//float rotZ =15* (  transform.position.y) + 5*transform.position.x;
			//transform.rotation = Quaternion.Euler(new Vector3(0,0,rotZ));
		}
	}
	
	public void OnEndDrag(PointerEventData eventData)
	{
		if(  bEnableDrag &&  bDrag   )
		{
			x = Input.mousePosition.x;
			y = Input.mousePosition.y;
			
			transform.position =     Camera.main.ScreenToWorldPoint(new Vector3(x ,y,100.0f)) + diffPos  ;
			if( transform.position.y > 3f) transform.position  = new Vector3(transform.position .x,3f,transform.position.z);
			if( transform.position.y < -2f) transform.position  = new Vector3(transform.position .x,-2f,transform.position.z);
			
		}
	 	anim.SetBool("clean",false);
//		CarWashSoundManager.Instance.Stop_Sound(CarWashSoundManager.Instance.WheelBrush);
		bDrag = false;
		if(psDirt1 != null) {psDirt1.Stop(); psDirt1.enableEmission = false; }
		if(psDirt2 != null) {psDirt2.Stop(); psDirt2.enableEmission = false; }
		if(psDirt3 != null) {psDirt3.Stop(); psDirt3.enableEmission = false; }
		bCleaning = false;
		TutorialBusCleaning.timeWaitToShowHelp = TutorialBusCleaning.PeriodToShowHelp;
		TutorialBusCleaning.bPause = false;
	}

 
	void OnDisable() {
		bCleaning = false;
		if(anim!=null)anim.SetBool("clean",false);
		bDrag = false; 
		if(psDirt1 != null) {psDirt1.Stop(); psDirt1.enableEmission = false; }
		if(psDirt2 != null) {psDirt2.Stop(); psDirt2.enableEmission = false; }
		if(psDirt3 != null) {psDirt3.Stop(); psDirt3.enableEmission = false; }
		CancelInvoke( );
	}

}
	
