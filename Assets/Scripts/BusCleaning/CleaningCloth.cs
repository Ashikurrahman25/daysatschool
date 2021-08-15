using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class CleaningCloth :  MonoBehaviour  ,  IBeginDragHandler , IDragHandler, IEndDragHandler 
{
	bool bEnableDrag = true;
	bool bDrag = false;

	Animator anim ;
	float x;
	float y;

	Vector3 diffPos = new Vector3(0,0,0);

	int wax1Status = 0;
	int wax2Status = 0;
	int wax3Status = 0;
	int wax4Status = 0;
	int wax5Status = 0;

	PointerEventData pointerData  ;
	public Polisher polisher;
	public string LeftToBeCleaned = "";
	void Awake()
	{
		LeftToBeCleaned = "Wax1,Wax2,Wax3,Wax4,Wax5,";
		 pointerData = new PointerEventData(EventSystem.current);
		anim = transform.GetComponent<Animator>();
		//InvokeRepeating("TestWax",.5f,.5f);
	}

	void TestWax () {
 	
		if( bDrag  )
		{
			// if (EventSystem.current.IsPointerOverGameObject()) 
			{          

				pointerData.position = Input.mousePosition;

				List<RaycastResult> objectsHit = new List<RaycastResult> ();
				EventSystem.current.RaycastAll(pointerData, objectsHit);

				for(int x = 0;x<objectsHit.Count;x++)
				{
					if(objectsHit[x].gameObject.name.StartsWith("Wax"))
					{
						StartCoroutine(AddWax(objectsHit[x].gameObject.name));
						break;
					}
				}
			}
		}
	}
	
	public void OnBeginDrag(PointerEventData eventData)
	{
		if(!bEnableDrag) return;
		InvokeRepeating("TestWax",.1f,.5f);
		bDrag = true;
		
		diffPos = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition)   ;
		diffPos = new Vector3(diffPos.x,diffPos.y,0);
		 
		anim.SetBool("clean",true);
		CarWashSoundManager.Instance.Play_Sound(CarWashSoundManager.Instance.Sponge);
		 
		TutorialBusCleaning.bPause = true;
	}
	
	public void OnDrag(PointerEventData eventData)
	{
		if( bEnableDrag &&  bDrag )
		{
			 
			x = Input.mousePosition.x;
			y = Input.mousePosition.y;
			
			transform.position =     Camera.main.ScreenToWorldPoint(new Vector3(x ,y,100.0f)) + diffPos  ;
            if( transform.position.y > 3.5f) transform.position  = new Vector3(transform.position .x,3.5f,transform.position.z);    
//			if( transform.position.y < -2f) transform.position  = new Vector3(transform.position .x,-2f,transform.position.z);
			
			float rotZ =15* (  transform.position.y) + 5*transform.position.x;
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
		bDrag = false;
		TutorialBusCleaning.timeWaitToShowHelp = TutorialBusCleaning.PeriodToShowHelp;
		TutorialBusCleaning.bPause = false;

		//CancelInvoke("TestClean" );   
		CancelInvoke();
		CarWashSoundManager.Instance.Stop_Sound(CarWashSoundManager.Instance.Sponge);
	}



 
   IEnumerator AddWax(string _name)
	{
		yield return new WaitForSeconds(0.05f);
		Image Wax = (Image) GameObject.Find("CAR/Mask/Wax/"+_name).GetComponent<Image>();
		float status = 0;
		polisher.StopSparcles();


		switch(_name)
		{
		case "Wax1": status = (wax1Status++); polisher.wax1Status =0; break;
		case "Wax2": status = (wax2Status++); polisher.wax2Status =0; break;
		case "Wax3": status = (wax3Status++); polisher.wax3Status =0; break;
		case "Wax4": status = (wax4Status++); polisher.wax4Status =0; break;
		case "Wax5": status = (wax5Status++); polisher.wax5Status =0; break;
		}

		//Debug.Log(_name + "  "+status);

		if(status == 3)
		{
			status = (status * 5 - 5)/15f;
			for(int i =0;i<=5;i++)
			{
				yield return new WaitForSeconds(0.05f);
				//Wax.color = new Color(1,1,1,1 - status  -i/20f);
				Wax.color = new Color(1,1,1, status  + i/20f);
			}

			LeftToBeCleaned = LeftToBeCleaned.Replace(( _name+","), "");
			CarWashLevelManager.Instance. AddStarWaxCar();
		}
		else
		{
			status = (status * 5 - 5)/15f;
			for(int i =0;i<=5;i++)
			{
				yield return new WaitForSeconds(0.05f);
				//Wax.color = new Color(1,1,1,1 - status  -i/20f);
				Wax.color = new Color(1,1,1, status  + i/20f);
			}

		}
	}
 
	void OnDisable() {
		CancelInvoke( );
	}
	
	
	
	
}
