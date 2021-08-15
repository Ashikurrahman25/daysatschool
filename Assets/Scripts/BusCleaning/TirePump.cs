using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TirePump : MonoBehaviour ,  IBeginDragHandler , IDragHandler, IEndDragHandler  //, IPointerDownHandler, IPointerUpHandler
{
	bool bEnableDrag = true;
	bool bDrag = false;
	
	bool bPumping = false;
	Animator anim ;
	float x;
	float y;
	
	 
	Vector3 diffPos = new Vector3(0,0,0);
	//Vector3 startPos = new Vector3(0,0,0);
	
	Transform PumpTip;
	Transform Wheel1;
	Transform Wheel2;
	Transform Wheel3;
	
	RectTransform NozzleMask;
	RectTransform Nozzle;

	int Wheel1Inflated = 0;
	int Wheel2Inflated = 0;
	int Wheel3Inflated = 0;

	float  wheelOffsetYScale =.5f;//pomeranje nije uvek 8 piksela

	public string LeftToBeCleaned = "";

	void Start () {


		PumpTip = GameObject.Find("PumpTip").transform;
		anim =  transform.GetComponent<Animator>();
		if( GameObject.Find("Wheel1/Valwe")!=null ) { Wheel1 =  GameObject.Find("Wheel1/Valwe").transform;  LeftToBeCleaned = "Wheel1,"; }
		if( GameObject.Find("Wheel2/Valwe")!=null ) { Wheel2 =  GameObject.Find("Wheel2/Valwe").transform;  LeftToBeCleaned += "Wheel2,"; }
		if( GameObject.Find("Wheel3/Valwe")!=null ) { Wheel3 =  GameObject.Find("Wheel3/Valwe").transform;  LeftToBeCleaned += "Wheel3,"; }





		 
		 
	}
	
	 
	void Update () {
		if( Wheel1!=null && !bPumping &&   Wheel1Inflated<8  &&  Vector2.Distance(Wheel1/*.parent*/.position, PumpTip.position) <.75f)
		{
			StartCoroutine("StartP",Wheel1);
		 
		}
		if( Wheel2!=null && !bPumping &&  Wheel2Inflated<8 && Vector2.Distance(Wheel2.parent.position, PumpTip.position) <.75f)
		{
			StartCoroutine("StartP",Wheel2);
			
		}
		if( Wheel3!=null && !bPumping &&  Wheel3Inflated<8 && Vector2.Distance(Wheel3.parent.position, PumpTip.position) <.75f)
		{
			StartCoroutine("StartP",Wheel3);
			
		}

 
//		if( bEnableDrag &&  bDrag )
//		{
//			
//			x = Input.mousePosition.x;
//			y = Input.mousePosition.y;
//			
//			//transform.position =     Camera.main.ScreenToWorldPoint(new Vector3(x ,y,100.0f)) + diffPos  ;
//			transform.position =     Vector3.Lerp (transform.position, Camera.main.ScreenToWorldPoint(new Vector3(x ,y,100.0f))    ,5* Time.deltaTime);
//
//			if( transform.position.y > 2.5f) transform.position  = new Vector3(transform.position .x,2.5f,transform.position.z);
//			if( transform.position.y < -2f) transform.position  = new Vector3(transform.position .x,-2f,transform.position.z);
//		}
	}
	
	IEnumerator StartP (Transform wheel)
	{
		bDrag = false;
		bEnableDrag = false;
		bPumping = true;
		TutorialBusCleaning.bPause = true;
		GameObject.Find("Tutorial").SendMessage("HidePointer");

		Vector3 v1 = transform.position;
		Vector3 v2 = transform.position + (wheel.position - PumpTip.position);
		
		for(float j = 0;j<=1;j+=0.1f)
		{
			transform.position  =   Vector3.Lerp   (v1,v2,j );
			yield return new WaitForFixedUpdate();
		}
		
		yield return new WaitForSeconds(.1f);
		StartCoroutine("InflateTyre",wheel.parent);
		anim.SetBool("active", true);
		yield return new WaitForSeconds(1f);
		bEnableDrag = true;
		
	}
	
	IEnumerator Remove()
	{

		anim.SetBool("active", false);
		TutorialBusCleaning.bPause = false;
		bEnableDrag = true;
		bDrag = true;
		
		diffPos = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition)   ;
		diffPos = new Vector3(diffPos.x,diffPos.y,0);
		 
		yield return new WaitForSeconds(.5f);
		bPumping = false;
		StopAllCoroutines( );

	}
	
	public void OnBeginDrag(PointerEventData eventData)
	{
		TutorialBusCleaning.bPause = true;
		if(!bEnableDrag) return;
		if(bPumping)  
			StartCoroutine("Remove");
		else
		{
			bDrag = true;
			
			diffPos = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition)   ;
			diffPos = new Vector3(diffPos.x,diffPos.y,0);
	 
		}
		
		
	}
	
	public void OnDrag(PointerEventData eventData)
 	{
		if( bEnableDrag &&  bDrag )
		{
 
			x = Input.mousePosition.x;
			y = Input.mousePosition.y;
			
			transform.position =     Camera.main.ScreenToWorldPoint(new Vector3(x ,y,100.0f)) + diffPos  ;
			 
			if( transform.position.y > 2f) transform.position  = new Vector3(transform.position .x,2f,transform.position.z);
			if( transform.position.y < -2f) transform.position  = new Vector3(transform.position .x,-2f,transform.position.z);
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
		
		bDrag = false;
		if(!bPumping)
		{ 
			TutorialBusCleaning.timeWaitToShowHelp = TutorialBusCleaning.PeriodToShowHelp;
			TutorialBusCleaning.bPause = false;
		}
	}


	IEnumerator InflateTyre(Transform wheel)
	{
 
		RectTransform Wheel = (RectTransform) wheel.Find("AnimationHolder/Mask/Wheel").GetComponent<RectTransform>();
		Image FlatTire = (Image) wheel.Find("AnimationHolder/FlatTire").GetComponent<Image>();
		//FlatTire.enabled = true;
		
	 
		if(wheel.name == "Wheel1") 
		{
			 
			while( Wheel1Inflated  <8  )
			{
				
				Wheel1Inflated++;
				CarWashLevelManager.Instance.AddStarInflateTires(1,Wheel1Inflated);
				yield return new WaitForSeconds(0.23f);		
				Wheel.anchoredPosition = new Vector2(0,(-8+Wheel1Inflated)*wheelOffsetYScale);
				FlatTire.transform.localScale = Vector3.one*(16-Wheel1Inflated)/16f;
				yield return new WaitForSeconds(0.6f);
				if(Wheel1Inflated == 8) LeftToBeCleaned = LeftToBeCleaned.Replace(( "Wheel1,"), "");
			}
			StartCoroutine("Remove");
		 
		}
		else if(wheel.name == "Wheel2") 
		{
			 
			while( Wheel2Inflated  <8  )
			{
				Wheel2Inflated++;
				CarWashLevelManager.Instance.AddStarInflateTires(2,Wheel2Inflated);
				yield return new WaitForSeconds(0.23f);
				Wheel.anchoredPosition = new Vector2(0,(-8+Wheel2Inflated)*wheelOffsetYScale);
				FlatTire.transform.localScale = Vector3.one*(16-Wheel2Inflated)/16f;
				 yield return new WaitForSeconds(0.6f);

				if(Wheel2Inflated == 8) LeftToBeCleaned = LeftToBeCleaned.Replace(( "Wheel2,"), "");
			}
			StartCoroutine("Remove");
			 
		}
		else if(wheel.name == "Wheel3") 
		{
			 
			while( Wheel3Inflated  <8  )
			{
				Wheel3Inflated++;
				CarWashLevelManager.Instance.AddStarInflateTires(3,Wheel3Inflated);
				yield return new WaitForSeconds(0.23f);
				Wheel.anchoredPosition = new Vector2(0,(-8+Wheel3Inflated)*wheelOffsetYScale);
				FlatTire.transform.localScale = Vector3.one*(16-Wheel3Inflated)/16f;
				yield return new WaitForSeconds(0.6f);
				if(Wheel3Inflated == 8) LeftToBeCleaned = LeftToBeCleaned.Replace(( "Wheel3,"), "");
			}
			StartCoroutine("Remove");
				
			 
		}



		
		yield return new WaitForSeconds(0.1f);
		
		
	}

	public void PlayTireSound()
	{
		CarWashSoundManager.Instance.Stop_Sound(CarWashSoundManager.Instance.TirePump);
		CarWashSoundManager.Instance.Play_Sound(CarWashSoundManager.Instance.TirePump);
	}

	void OnDisable()
	{
		if(	Wheel1 != null && Wheel1Inflated >= 8)
		{
			RectTransform Wheel = (RectTransform) Wheel1.parent.Find("AnimationHolder/Mask/Wheel").GetComponent<RectTransform>();
			Wheel.anchoredPosition =  Vector2.zero;
		}

		if(	Wheel2 != null && Wheel2Inflated >= 8)
		{
			RectTransform Wheel = (RectTransform) Wheel2.parent.Find("AnimationHolder/Mask/Wheel").GetComponent<RectTransform>();
			Wheel.anchoredPosition =  Vector2.zero;
		}

		if(	Wheel3 != null && Wheel3Inflated >= 8)
		{
			RectTransform Wheel = (RectTransform) Wheel3.parent.Find("AnimationHolder/Mask/Wheel").GetComponent<RectTransform>();
			Wheel.anchoredPosition =  Vector2.zero;
		}
		bDrag = false;
	}

	public void  DisableTirePump()
	{
		bDrag = false;
		bPumping = false;
		bEnableDrag = true;
		gameObject.SetActive(false);
	}
	 
}
