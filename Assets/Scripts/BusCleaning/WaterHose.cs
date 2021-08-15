using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;
 
public class WaterHose : MonoBehaviour ,  IBeginDragHandler //, IDragHandler, IEndDragHandler 
{
	 
	public Transform Prefab1;
//	public Transform StartSegment;
//	public Transform EndSegment;

	int startSegmentNo = 3;
	int segmentCount =  20;//30;
	Vector3 offset = new Vector3(0.08f,0,0);

 
	public List <SpriteRenderer> spritesCompressor = new List<SpriteRenderer>() ; 

	float washedPercent = 0;
	PointerEventData pointerData  ;
	
	//-------------------------------------------------
	bool bEnableDrag = true;
	bool bDrag = false;
	
	
	Animator anim ;
	float x;
	float y;
	
	
	Vector3 diffPos = new Vector3(0,0,0);
	 
	Transform testPos;
	
	int dirt1Status = 0;
	int dirt2Status = 0;
	int dirt3Status = 0;
	int dirt4Status = 0;
	int dirt5Status = 0;
	
	public string LeftToBeCleaned = "Dirt1,Dirt2,Dirt3,Dirt4,Dirt5,";

	Vector3 destinationPos;
	private Vector3 difVelocity = Vector3.zero;
	float maxSpeed = 9f;
	float minSpeed = 5f;
	 
 
	Image Water;
	RectTransform WaterTR;

	void Start () 
	{
		Water = (Image) GameObject.Find("CAR/MaskWater/Water").GetComponent<Image>();
		WaterTR = (RectTransform) GameObject.Find("CAR/MaskWater/Water").GetComponent<RectTransform>();

		anim = transform.GetComponent<Animator>();
		transform.position  = new Vector3(3,-.5f,0);
		//transform.position  = new Vector3(3.5f, .5f,0);
		destinationPos = transform.position;

	 	 
		testPos = transform.Find("splash/testPos").transform;
		pointerData = new PointerEventData(EventSystem.current);
	 

//		Rigidbody2D pomRB = StartSegment.GetComponent<Rigidbody2D>();
//		GameObject pomGO = StartSegment.gameObject;
//		for(int i = startSegmentNo+1; i<= (startSegmentNo + segmentCount); i++)
//		{
//			GameObject go = (GameObject) GameObject.Instantiate(pomGO);
//			go.transform.SetParent(pomGO.transform.parent);
//			go.name = "Seg"+i.ToString();
//			go.GetComponent<HingeJoint2D>().connectedBody = pomRB;
//			go.transform.position  = pomGO.transform.position + offset;
//			go.GetComponent<DistanceJoint2D>().enabled = true;
//			go.GetComponent<DistanceJoint2D>().connectedBody = pomRB;
//
//			if( i< 8 )  go.GetComponent<Rigidbody2D>().mass =   100 * ((8-i)/8f);//   0.5f*i;
//			else if( i< segmentCount*.9f)  go.GetComponent<Rigidbody2D>().mass =   10;//   0.5f*i;
//		 
//			pomRB = go.GetComponent<Rigidbody2D>();
//			pomGO = go;
//
//		}
//
//		for(int i = 4; i<6;i++)
//		{
//			GameObject go = (GameObject) GameObject.Find( "Seg"+i.ToString());
//				go.transform.GetComponent<LimitRotation>().enabled = true;
//				//go.transform.GetComponent<SegmentDistanceScale>().enabled = false;
//			 
//		}
//		StartSegment.GetComponent<Rigidbody2D>().mass =   10000;
//		StartSegment.transform.GetComponent<LimitRotation>().enabled = true;
//		StartSegment.transform.GetComponent<SegmentDistanceScale>().enabled = false;
//		//StartSegment.transform.localScale = new Vector3(1.3f,1,1);
//		 
//
//		EndSegment.GetComponent<HingeJoint2D>().connectedBody = pomRB;
 
		GameObject[]  goC =   GameObject.FindGameObjectsWithTag("T2");
		for(int i =0 ; i<goC.Length; i++)
		{
			if(goC[i].GetComponent<SpriteRenderer> () !=null)
			{
				spritesCompressor.Add(goC[i].GetComponent<SpriteRenderer> ());
			}
		}
		SetVisible(false);
		//Deactivate();
		//transform.position  = Vector3.zero;
		destinationPos = transform.position;

		// StartCoroutine ("ShowWaterHose");
	}

 
	public void Deactivate()
	{
        if (transform.parent.gameObject.activeSelf)
        if (transform.gameObject.activeSelf)
        {
            transform.gameObject.SetActive(true);
            Debug.Log(transform.name);
            Debug.Log(transform.gameObject.activeSelf);
            StartCoroutine(DeactivateWaterHose());
        }
	}

	 
	public void Activate()
	{
		StopCoroutine ("DeactivateWaterHose");
		bEnableDrag = true;
		transform.parent.gameObject.SetActive(true);
		SetVisible(true);
		transform.GetComponent<CircleCollider2D>().enabled = true;
	}

	IEnumerator DeactivateWaterHose()
	{
		transform.GetComponent<CircleCollider2D>().enabled = false;
		if(anim== null) anim = transform.GetComponent<Animator>();
		else anim.SetBool("clean",false);
		 
		//StartCoroutine("HideWater");
		bEnableDrag = false;
		TutorialBusCleaning.bPause = true;
		 CancelInvoke("TestClean" );
		SetVisible(false);
		 transform.position  = new Vector3(3.5f, .5f,0);
		 destinationPos = transform.position;
        //yield return new WaitForSeconds(5.1f);
        yield return null;
		 
		transform.gameObject.SetActive(false);
	}

	 

	public void SetVisible(bool visible)
	{
		StopCoroutine("DeactivateWaterHose");

		foreach(SpriteRenderer spr in spritesCompressor)
		{
			spr.enabled = visible;
		}
	}

 

	IEnumerator ShowWaterHose()
	{
		//transform.position  = Vector3.zero;
		destinationPos = transform.position;

		yield return null;
		//yield return new WaitForSeconds(2);
		 

		SetVisible(true);
	}
 


	void Update ()
	{
		if(bDrag)
		{
			difVelocity = Vector3.Lerp( transform.position,  destinationPos,  .8f * Time.deltaTime) - transform.position;
				
			float mag = Vector3.Magnitude(difVelocity)/ Time.deltaTime;
			if(mag>0.1f)
			{
				if(  mag> maxSpeed)  difVelocity = difVelocity.normalized*maxSpeed*Time.deltaTime;
				else if(  mag<minSpeed)  difVelocity = difVelocity.normalized*minSpeed*Time.deltaTime;
				transform.position += difVelocity;  
			}
			//
			//splash1.localScale = new Vector3(1- (transform.position.x+6)/32f,1,1);
			//splash2.position = copyPos.position;
		}		
	}

 
 
 

	
	void TestClean () {
		 
		if( bDrag  )
		{
			// if (EventSystem.current.IsPointerOverGameObject()) 
			{          
				 
				pointerData.position =   Camera.main.WorldToScreenPoint(testPos.position );    //    Input.mousePosition;
				
				List<RaycastResult> objectsHit = new List<RaycastResult> ();
				EventSystem.current.RaycastAll(pointerData, objectsHit);
				
				for(int x = 0;x<objectsHit.Count;x++)
				{
					if(objectsHit[x].gameObject.name.StartsWith("Dirt"))
					{
						 
						StartCoroutine(CleanDirt(objectsHit[x].gameObject.name));
						break;
					}
				}
			}
		}
	}

 

	//---------------------------------------------------------------------------------------------------------------------------------
	public void OnBeginDrag(PointerEventData eventData)
	{
 
	}
	
	 

 

	void OnMouseDown()
	{
 
		if(!bEnableDrag) return;
		StopCoroutine("HideWater");
		InvokeRepeating("WashWithWater",.02f,.02f);

		bDrag = true;
		TutorialBusCleaning.bPause = true;
		 
		diffPos = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition)   ;
		diffPos = new Vector3(diffPos.x,diffPos.y,0);
		anim.SetBool("clean",true);
		CarWashSoundManager.Instance.Play_Sound(CarWashSoundManager.Instance.WaterHose);
		InvokeRepeating("TestClean",.3f,.3f);
	}
	
	
	
	void OnMouseDrag()
	{
 
		if( bEnableDrag &&  bDrag )
		{
			x = Input.mousePosition.x;
			y = Input.mousePosition.y;

			destinationPos =     Camera.main.ScreenToWorldPoint(new Vector3(x ,y,10.0f)) + diffPos  ;
            if( destinationPos.y> 1.9f) destinationPos  = new Vector3(destinationPos.x,1.9f,destinationPos.z);
//			if( destinationPos.y < -1f) destinationPos  = new Vector3(destinationPos.x,-1f,destinationPos.z);

			if( destinationPos.x> 8f) destinationPos  = new Vector3(8f,destinationPos.y, destinationPos.z);
			if( destinationPos.x < -2.5f) destinationPos  = new Vector3(-2.5f,destinationPos.y,destinationPos.z);
		}

		float rotZ =-12* (  transform.position.y) + 90;
		transform.rotation = Quaternion.Euler(new Vector3(0,0,rotZ));

	}
	
	void OnMouseUp()
	{

		if(  bEnableDrag &&  bDrag   )
		{
			x = Input.mousePosition.x;
			y = Input.mousePosition.y;
			
			destinationPos =     Camera.main.ScreenToWorldPoint(new Vector3(x ,y,10.0f)) + diffPos  ;
			if( destinationPos.y> 2f) destinationPos  = new Vector3(destinationPos.x,2f,destinationPos.z);
			if( destinationPos.y < -1f) destinationPos  = new Vector3(destinationPos.x,-1f,destinationPos.z);

			if( destinationPos.x> 8f) destinationPos  = new Vector3(8f,destinationPos.y, destinationPos.z);
			if( destinationPos.x < -2.5f) destinationPos  = new Vector3(-2.5f,destinationPos.y,destinationPos.z);
			TutorialBusCleaning.bPause = false;
			TutorialBusCleaning.timeWaitToShowHelp = TutorialBusCleaning.PeriodToShowHelp;
		}
		
		bDrag = false;


		CancelInvoke ( "WashWithWater" );
		 CancelInvoke("TestClean" );
		if(washedPercent <=100)
		{
			StopCoroutine("HideWater");
			StartCoroutine("HideWater");
		}
		             
		anim.SetBool("clean",false);
		CarWashSoundManager.Instance.Stop_Sound(CarWashSoundManager.Instance.WaterHose);
	}


	void WashWithWater()
	{

		if(washedPercent <100)
		{
			float a = Water.color.a;
			if(  a + 0.1f<1) Water.color = new Color(1,1,1,a + 0.1f);
			else Water.color = Color.white;
		}

		if(washedPercent <=100)
		{
			washedPercent +=1.4f;
			Water.color = new Color(1,1,1,1);

			WaterTR.anchoredPosition =  new Vector2(0,350 - .8f*washedPercent);
			WaterTR.sizeDelta = new Vector2(600,180+washedPercent*3.2f);
		}
		else
		{
			StopCoroutine("HideWater");
			StartCoroutine("HideWater");
			CancelInvoke("WashWithWater");
			washedPercent = 102;

		}
	}


		 
	IEnumerator HideWater()
	{
		int i = Mathf.FloorToInt( (1 - Water.color.a) * 20 ); 
		//Debug.Log(i + "  a " + Water.color.a);
		while( i<=20)
		{
			i++;
			yield return new WaitForSeconds(0.05f);
			Water.color = new Color(1,1,1,1-i/20f);
		}
	}


	IEnumerator CleanDirt(string _name)
	{
		yield return new WaitForSeconds(0.05f);
		Image Dirt = (Image) GameObject.Find("CAR/Mask/Dirt/"+_name).GetComponent<Image>();
		float status = 0;
		
		switch(_name)
		{
		case "Dirt1": status = (dirt1Status++); break;
		case "Dirt2": status = (dirt2Status++); break;
		case "Dirt3": status = (dirt3Status++); break;
		case "Dirt4": status = (dirt4Status++); break;
		case "Dirt5": status = (dirt5Status++); break;
		}
		
		//Debug.Log(_name + "  "+status);
		
		if(status == 3)
		{
			status = (status * 5 - 5)/15f;
			for(int i =0;i<3;i++)
			{
				yield return new WaitForSeconds(0.05f);
				if(Dirt != null)  Dirt.color = new Color(1,1,1,1 - status  -i/10f);
			}
			
			GameObject.Destroy(Dirt.gameObject);
			//Debug.Log("ADD STAR  " + _name);
			CarWashLevelManager.Instance.AddStarDirt();
			LeftToBeCleaned = LeftToBeCleaned.Replace((_name+","), "");
		}
		else
		{
			status = (status * 5 - 5)/15f;
			for(int i =0;i<3;i++)
			{
				yield return new WaitForSeconds(0.05f);
				if(Dirt != null) Dirt.color = new Color(1,1,1,1 - status  -i/10f);
			}
			
		}
	}

	 
}
