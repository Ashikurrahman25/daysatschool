using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GasPump : MonoBehaviour ,  IBeginDragHandler, IDragHandler, IEndDragHandler
{
	bool bEnableDrag = true;
	bool bDrag = false;

	bool bTanking = false;
	
	float x;
	float y;
	 
 
	Vector3 diffPos = new Vector3(0,0,0);
	//Vector3 startPos = new Vector3(0,0,0);
 
	Transform NozzleTip;
	Transform FuelTank;

	RectTransform NozzleMask;
	RectTransform Nozzle;

	void Start () {
		FuelTank =  GameObject.Find("FuelTank").transform;
		NozzleTip = GameObject.Find("GasPump/NozzleMask/NozzleTip").transform;

		NozzleMask = GameObject.Find("GasPump/NozzleMask").transform.GetComponent<RectTransform>();
		Nozzle = GameObject.Find("GasPump/NozzleMask/Nozzle").transform.GetComponent<RectTransform>();
	}
	
	 
	void Update () {
		if(!bTanking && Vector2.Distance(FuelTank.position, NozzleTip.position) <0.3f)
		{
			StartCoroutine("Insert");
		}

		if( bEnableDrag &&  bDrag )
		{
			
			x = Input.mousePosition.x;
			y = Input.mousePosition.y;
			
			transform.position =   Vector3.Lerp( transform.position,  Camera.main.ScreenToWorldPoint(new Vector3(x ,y,100.0f)),5*Time.deltaTime);//  Camera.main.ScreenToWorldPoint(new Vector3(x ,y,100.0f)) + diffPos  ;
			
			if( transform.position.y > 2.2f) transform.position  = new Vector3(transform.position .x,2.2f,transform.position.z);
//			if( transform.position.y < -1f) transform.position  = new Vector3(transform.position .x,-1f,transform.position.z);

			if( transform.position.x > 4.5f) transform.position  = new Vector3( 4.5f,transform.position.y,transform.position.z);
			if( transform.position.x < -4.5f) transform.position  = new Vector3(-4.5f,transform.position .y,transform.position.z);
		}
	}

	IEnumerator Insert ()
	{
		Debug.Log("Insert");
		bDrag = false;
		bEnableDrag = false;
		bTanking = true;
		Vector3 v1 = transform.position;
		Vector3 v2 = transform.position + (FuelTank.position - NozzleTip.position);

		for(float j = 0;j<=1;j+=0.1f)
		{
			transform.position  =   Vector3.Lerp   (v1,v2,j );
			yield return new WaitForFixedUpdate();
		}

		yield return new WaitForSeconds(.1f);
		 
		v1 = transform.position;
		v2 = transform.position + (NozzleTip.position - GameObject.Find("NozzleEnd").transform.position);
		for(int i = 0;i<=48;i+=4)
		{
			Nozzle.anchoredPosition = new Vector2(8-i,0);
			NozzleMask.anchoredPosition = new Vector2(i,0);

			transform.position  =   Vector3.Lerp   (v1,v2,i/48f);
			yield return new WaitForFixedUpdate();

		}
		bTanking = true;
		TutorialBusCleaning.bPause = true;
		GameObject.Find("Tutorial").SendMessage("HidePointer");

		StartCoroutine("CountTime");
		CarWashSoundManager.Instance.Play_Sound(CarWashSoundManager.Instance.Fuel);
		yield return new WaitForSeconds(1f);
		bEnableDrag = true;

	}

	IEnumerator Remove()
	{
		Debug.Log("Remove");
		bEnableDrag = false;
		CarWashSoundManager.Instance.Stop_Sound(CarWashSoundManager.Instance.Fuel);
		StopCoroutine("CountTime");
		Vector3 v1 = transform.position;
		Vector3 v2 = transform.position - (NozzleTip.position - GameObject.Find("NozzleEnd").transform.position)+new Vector3(.6f,.6f,0);
		for(int i = 48;i>=0;i-=4)
		{
			Nozzle.anchoredPosition = new Vector2(8-i,0);
			NozzleMask.anchoredPosition = new Vector2(i,0);
			
			transform.position  =   Vector3.Lerp   (v2,v1,i/48f);
			yield return new WaitForFixedUpdate();
			
		}
		//bEnableDrag = true;
		bDrag = true;

		diffPos = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition)   ;
		diffPos = new Vector3(diffPos.x,diffPos.y,0);


		yield return new WaitForSeconds(.02f);
		bEnableDrag = true;
		bTanking = false;
		Debug.Log("RemoveEnd");
		TutorialBusCleaning.bPause = false;
	}

	int fuel = 0;
	IEnumerator CountTime()
	{
		while(fuel<5)
		{
			yield return new WaitForSeconds(1f);
			fuel++;
			CarWashLevelManager.Instance.AddStarFuel();

		}
		yield return new WaitForSeconds(0.1f);
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		if(!bEnableDrag) return;
		if(bTanking) 
		{
			StartCoroutine("Remove");
			 
		}
		else
		{
			bDrag = true;
			
			diffPos = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition)   ;
			diffPos = new Vector3(diffPos.x, diffPos.y,0);
 
		}
		 
		TutorialBusCleaning.bPause = true;
		
	}
	
	public void OnDrag(PointerEventData eventData)
	{
//		if( bEnableDrag &&  bDrag )
//		{
// 
//			x = Input.mousePosition.x;
//			y = Input.mousePosition.y;
//			
//			transform.position =   Vector3.Lerp( transform.position,  Camera.main.ScreenToWorldPoint(new Vector3(x ,y,100.0f)),Time.deltaTime);//  Camera.main.ScreenToWorldPoint(new Vector3(x ,y,100.0f)) + diffPos  ;
//			 
//			if( transform.position.y > 2f) transform.position  = new Vector3(transform.position .x,2f,transform.position.z);
//			if( transform.position.y < -1f) transform.position  = new Vector3(transform.position .x,-1f,transform.position.z);
//		}
	}
	
	public void OnEndDrag(PointerEventData eventData)
	{
		if(  bEnableDrag &&  bDrag   )
		{
			x = Input.mousePosition.x;
			y = Input.mousePosition.y;
			
			//transform.position =     Camera.main.ScreenToWorldPoint(new Vector3(x ,y,100.0f)) + diffPos  ;
			if( transform.position.y > 2.2f) transform.position  = new Vector3(transform.position .x,2.2f,transform.position.z);
			if( transform.position.y < -1f) transform.position  = new Vector3(transform.position .x,-1f,transform.position.z);

			if( transform.position.x > 4.5f) transform.position  = new Vector3( 4.5f,transform.position.y,transform.position.z);
			if( transform.position.x < -4.5f) transform.position  = new Vector3(-4.5f,transform.position .y,transform.position.z);
		}
		bDrag = false;
		if(!bTanking)
		{
			TutorialBusCleaning.timeWaitToShowHelp = TutorialBusCleaning.PeriodToShowHelp;
			TutorialBusCleaning.bPause = false;
		}
	}

	public void  DisableGasPump()
	{
		bDrag = false;
		bTanking = false;
		bEnableDrag = true;
	
		if(Nozzle !=null && NozzleMask!=null)
		{
			Nozzle.anchoredPosition = new Vector2(0,0);
			NozzleMask.anchoredPosition = new Vector2(0,0);
				
			//transform.position  =  transform.position - (NozzleTip.position - GameObject.Find("NozzleEnd").transform.position)+new Vector3(.6f,.6f,0);
		
		}
		gameObject.SetActive(false);
	}

}
