using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Polisher : MonoBehaviour  ,  IBeginDragHandler , IDragHandler, IEndDragHandler 
{
	bool bEnableDrag = true;
	bool bDrag = false;
	
	
	Animator anim ;
	float x;
	float y;

	ParticleSystem psSparkles1;
	ParticleSystem psSparkles2;
	ParticleSystem psSparkles3;
	ParticleSystem psSparkles4;
	ParticleSystem psSparkles5;
 
	Vector3 diffPos = new Vector3(0,0,0);
	Transform target; 

	public int wax1Status = -1;
	public int wax2Status = -1;
	public int wax3Status = -1;
	public int wax4Status = -1;
	public int wax5Status = -1;
	PointerEventData pointerData  ;

	public string LeftToBeCleaned = "";
	void Awake()
	{

		pointerData = new PointerEventData(EventSystem.current);
		anim = transform.GetComponent<Animator>();


	}

	void Start () 
	{
		if(GameObject.Find("CAR/Sparkles")!=null)
		{
			psSparkles1 = GameObject.Find("CAR/Sparkles/sp1").GetComponent<ParticleSystem>();
			psSparkles2 = GameObject.Find("CAR/Sparkles/sp2").GetComponent<ParticleSystem>();
			psSparkles3 = GameObject.Find("CAR/Sparkles/sp3").GetComponent<ParticleSystem>();
			psSparkles4 = GameObject.Find("CAR/Sparkles/sp4").GetComponent<ParticleSystem>();
			psSparkles5 = GameObject.Find("CAR/Sparkles/sp5").GetComponent<ParticleSystem>();
		}
	}

	public void StopSparcles()
	{
		if(psSparkles1!=null) psSparkles1.enableEmission = false ;
		if(psSparkles2!=null) psSparkles2.enableEmission = false ;
		if(psSparkles3!=null) psSparkles3.enableEmission = false ;
		if(psSparkles4!=null) psSparkles4.enableEmission = false ;
		if(psSparkles5!=null) psSparkles5.enableEmission = false ;

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
						StartCoroutine(CleanWax(objectsHit[x].gameObject.name));
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
		TutorialBusCleaning.bPause = true;

		diffPos = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition)   ;
		diffPos = new Vector3(diffPos.x,diffPos.y,0);
		
		 
		anim.SetBool("bPolish",true);
		CarWashSoundManager.Instance.Play_Sound(CarWashSoundManager.Instance.Compressor);
	}
	
	public void OnDrag(PointerEventData eventData)
	{
		if( bEnableDrag &&  bDrag )
		{
			
			x = Input.mousePosition.x;
			y = Input.mousePosition.y;
			
			transform.position =     Camera.main.ScreenToWorldPoint(new Vector3(x ,y,100.0f)) + diffPos  ;
			if( transform.position.y > 2.5f) transform.position  = new Vector3(transform.position .x,2.5f,transform.position.z);
//			if( transform.position.y < -3f) transform.position  = new Vector3(transform.position .x,-3f,transform.position.z);
			
			//float rotZ =   15* (  transform.position.y) + 3*transform.position.x;
			 
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
			if( transform.position.y > 2.5f) transform.position  = new Vector3(transform.position .x,2.5f,transform.position.z);
			if( transform.position.y < -2f) transform.position  = new Vector3(transform.position .x,-2f,transform.position.z);
		}
		anim.SetBool("bPolish",false);
		bDrag = false;
		TutorialBusCleaning.timeWaitToShowHelp = TutorialBusCleaning.PeriodToShowHelp;
		TutorialBusCleaning.bPause = false;

		CancelInvoke( );
		CarWashSoundManager.Instance.Stop_Sound(CarWashSoundManager.Instance.Compressor);
	}
	
	
	
	
	IEnumerator CleanWax(string _name)
	{
        Debug.Log("CleaningWax");
		yield return new WaitForSeconds(0.05f);
		Image Wax = (Image) GameObject.Find("CAR/Mask/Wax/"+_name).GetComponent<Image>();
		float status = -1;
		
		switch(_name)
		{
		case "Wax1":  if(wax1Status >=0)  status = (wax1Status++); break;
		case "Wax2":  if(wax2Status >=0)  status = (wax2Status++); break;
		case "Wax3":  if(wax3Status >=0)  status = (wax3Status++); break;
		case "Wax4":  if(wax4Status >=0)  status = (wax4Status++); break;
		case "Wax5":  if(wax5Status >=0)  status = (wax5Status++); break;
		}
		
		//Debug.Log(_name + "  "+status);
		float alpha = Wax.color.a;
		float alphaNew =2;

		if(status == 3)
		{
			status = (status * 5 - 5)/15f;
			for(int i =0;i<=5;i++)
			{
				yield return new WaitForSeconds(0.05f);
				alphaNew = 1 - status  -i/20f;
				if(alphaNew < alpha) Wax.color = new Color(1,1,1,alphaNew);
			}
			
			// GameObject.Destroy(Wax.gameObject);
		
			if(_name == "Wax1" ) { psSparkles1.enableEmission = true ;   CarWashLevelManager.Instance.AddStarPolishCar(); wax1Status = -1; }
			if(_name == "Wax2" ) { psSparkles2.enableEmission = true ;   CarWashLevelManager.Instance.AddStarPolishCar(); wax2Status = -1; }
			if(_name == "Wax3" ) { psSparkles3.enableEmission = true ;   CarWashLevelManager.Instance.AddStarPolishCar(); wax3Status = -1; }
			if(_name == "Wax4" ) { psSparkles4.enableEmission = true ;   CarWashLevelManager.Instance.AddStarPolishCar(); wax4Status = -1; }
			if(_name == "Wax5" ) { psSparkles5.enableEmission = true ;   CarWashLevelManager.Instance.AddStarPolishCar(); wax5Status = -1; }
			LeftToBeCleaned = LeftToBeCleaned.Replace(( _name +","), "");
		}
		else  if(status >=0)
		{
			status = (status * 5 - 5)/15f;
			for(int i =0;i<=5;i++)
			{
				yield return new WaitForSeconds(0.05f);
				alphaNew = 1 - status  -i/20f;
				if(alphaNew < alpha)  Wax.color = new Color(1,1,1, alphaNew);
			}
		}
	}
	
	

	void OnDisable() {
		CancelInvoke( );
	}

	void OnEnable()
	{
		if(wax1Status >=0) LeftToBeCleaned = "Wax1,";
		if(wax2Status >=0) LeftToBeCleaned += "Wax2,";
		if(wax3Status >=0) LeftToBeCleaned += "Wax3,";
		if(wax4Status >=0) LeftToBeCleaned += "Wax4,";
		if(wax5Status >=0) LeftToBeCleaned += "Wax5,";
		 
	}






 

}
