using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;
 
public class WaterGun : MonoBehaviour ,  IBeginDragHandler //, IDragHandler, IEndDragHandler 
{
	public Transform Prefab1;
//	public Transform StartSegment;
//	public Transform EndSegment;

	int startSegmentNo = 3;
	int segmentCount = 30;//60;
	Vector3 offset = new Vector3(-0.08f,0,0);
	public string LeftToBeCleaned = "";

	public List <SpriteRenderer> spritesCompressor = new List<SpriteRenderer>() ; 



	//-------------------------------------------------
	bool bEnableDrag = true;
	bool bDrag = false;
	
	
	Animator anim ;
	float x;
	float y;
	
	
	Vector3 diffPos = new Vector3(0,0,0);
	Transform splash1;
	Transform splash2;
	Transform copyPos;
	
	bool bStain1 = false;
	bool bStain2 = false;
	bool bStain3 = false;
	bool bStain4 = false;
	bool bStain5 = false;
	
	Transform Stain1;
	Transform Stain2;
	Transform Stain3;
	Transform Stain4;
	Transform Stain5;
	
	
	 float distance = .5f;


	Vector3 destinationPos;
	private Vector3 difVelocity = Vector3.zero;
	float maxSpeed = 20f;
	float minSpeed = 5f;
	 
 

	void Start () 
	{
		LeftToBeCleaned  = "";
		if( GameObject.Find("CAR/Mask/Stains/stain1") ==  null)  
		{ 
			transform.parent.gameObject.SetActive(false); 
			return; 
		}

		LeftToBeCleaned = "stain1,stain2,stain3,stain4,stain5,";
		anim = transform.GetComponent<Animator>();
		destinationPos = transform.position;

	 	//splash1 = transform.Find("splash").transform;
	 	 splash2 = transform.Find("splash2").transform;
	 	//copyPos = transform.Find("splash/copyPos").transform;

		{
			Stain1 = GameObject.Find("CAR/Mask/Stains/stain1").transform;
			Stain2 = GameObject.Find("CAR/Mask/Stains/stain2").transform;
			Stain3 = GameObject.Find("CAR/Mask/Stains/stain3").transform;
			Stain4 = GameObject.Find("CAR/Mask/Stains/stain4").transform;
			Stain5 = GameObject.Find("CAR/Mask/Stains/stain5").transform;
 
		}

	

//		Rigidbody2D pomRB = StartSegment.GetComponent<Rigidbody2D>();
//		GameObject pomGO = StartSegment.gameObject;
//		for(int i = startSegmentNo+1; i<= (startSegmentNo + segmentCount); i++)
//		{
//			GameObject go = (GameObject) GameObject.Instantiate(pomGO);
//			go.transform.SetParent(pomGO.transform.parent);
//			go.name = "Seg"+i.ToString();
//			go.GetComponent<HingeJoint2D>().connectedBody = pomRB;
//			go.transform.position  = pomGO.transform.position + offset;
//			go.GetComponent<DistanceJoint2D>().connectedBody = pomRB;
//			if( i< segmentCount*.1f)  go.GetComponent<Rigidbody2D>().mass =   80;//   0.5f*i;
//			else if( i< segmentCount*.9f)  go.GetComponent<Rigidbody2D>().mass =   20;//   0.5f*i;
//			else
//				go.GetComponent<Rigidbody2D>().mass = 1f*i;
//			 
//			if(i-(startSegmentNo + segmentCount - 5) >0 )
//			{
//				go.GetComponent<HingeJoint2D>().useLimits = false;
//				go.transform.localRotation = Quaternion.Euler(new Vector3(0,0,-75/5* (i-(startSegmentNo + segmentCount - 5) )) );
//				JointAngleLimits2D lm = new JointAngleLimits2D();
//				lm.min = -3;
//				lm.max = 3;
//				go.GetComponent<HingeJoint2D>().limits =lm;
//				go.GetComponent<HingeJoint2D>().useLimits = true;
//				 
//			}
//
//			pomRB = go.GetComponent<Rigidbody2D>();
//			pomGO = go;
//
//
//
//		}
// 
//
//		//EndSegment.name = "crevo" + (startSegmentNo + segmentCount +1).ToString();
//		EndSegment.GetComponent<HingeJoint2D>().connectedBody = pomRB;


		GameObject[]  goC =   GameObject.FindGameObjectsWithTag("T1");
		for(int i =0 ; i<goC.Length; i++)
		{
			if(goC[i].GetComponent<SpriteRenderer> () !=null)
			{
				spritesCompressor.Add(goC[i].GetComponent<SpriteRenderer> ());
				 
			}
		}
		SetVisible(false);

		transform.position  = Vector3.zero;
		destinationPos = transform.position;

		//StartCoroutine ("ShowWaterHose");
	}

	public void Deactivate()
	{
		if(transform.parent.gameObject.activeSelf)
        if(transform.gameObject.activeSelf)
			StartCoroutine ("DeactivateWaterHose");
	}

 

	public void Activate()
	{
		StopCoroutine ("DeactivateWaterHose");
		bEnableDrag = true;
		transform.parent.gameObject.SetActive(true);
		SetVisible(true);
	}

	IEnumerator DeactivateWaterHose()
	{
		if(anim== null) anim = transform.GetComponent<Animator>();
		else anim.SetBool("clean",false);

		bEnableDrag = false;
		TutorialBusCleaning.bPause = true;

		SetVisible(false);
		transform.position  = Vector3.zero;
		destinationPos = transform.position;
        //yield return new WaitForSeconds(1);
        yield return null;
		 
		transform.gameObject.SetActive(false);
	}

	 

	public void SetVisible(bool visible)
	{
		foreach(SpriteRenderer spr in spritesCompressor)
		{
			spr.enabled = visible;
		}
	}

 

	IEnumerator ShowWaterHose()
	{
		transform.position  = Vector3.zero;
		destinationPos = transform.position;
		yield return new WaitForSeconds(3);
		 

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














	//----------------------------------------------------------------------------------------------------------------------------------

 
	void TestClean () 
	{
		if( bDrag  )
		{
			if( !bStain1 &&  Vector2.Distance( Stain1.position, splash2.position) < distance) {  bStain1 = true; StartCoroutine("HideStain",Stain1);    return;}
			if( !bStain2 &&  Vector2.Distance( Stain2.position, splash2.position) < distance) {  bStain2 = true; StartCoroutine("HideStain",Stain2);    return;}
			if( !bStain3 &&  Vector2.Distance( Stain3.position, splash2.position) < distance) {  bStain3 = true; StartCoroutine("HideStain",Stain3);   return;}
			if( !bStain4 &&  Vector2.Distance( Stain4.position, splash2.position) < distance) {  bStain4 = true; StartCoroutine("HideStain",Stain4);    return;}
			if( !bStain5 &&  Vector2.Distance( Stain5.position, splash2.position) < distance) {  bStain5 = true; StartCoroutine("HideStain",Stain5);    return;}
		}
	}
	
	IEnumerator HideStain(Transform stain)
	{
		LeftToBeCleaned = LeftToBeCleaned.Replace((stain.name+","), "");
		Image imgStain =  stain.GetComponent<Image>();
		
		imgStain.color = Color.white;
		
		for(int i =0;i<=10;i++)
		{
			yield return new WaitForSeconds(0.05f);
			imgStain.color = new Color(1,1,1,1-i/10f);
		}

		CarWashLevelManager.Instance.AddStarStains();
	}
	 
	//---------------------------------------------------------------------------------------------------------------------------------
	public void OnBeginDrag(PointerEventData eventData)
	{
 
	}
	
 
	//***********************************************
 


 

	void OnMouseDown()
	{
 
		if(!bEnableDrag) return;
		
		InvokeRepeating("TestClean",.5f,.5f);
		bDrag = true;
		 
		TutorialBusCleaning.bPause = true;

		diffPos = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition)   ;
		diffPos = new Vector3(diffPos.x,diffPos.y,0);
		anim.SetBool("clean",true);
		CarWashSoundManager.Instance.Play_Sound(CarWashSoundManager.Instance.Compressor);
	}
	
	
	
	void OnMouseDrag()
	{
 
		if( bEnableDrag &&  bDrag )
		{
			x = Input.mousePosition.x;
			y = Input.mousePosition.y;

			destinationPos =     Camera.main.ScreenToWorldPoint(new Vector3(x ,y,10.0f)) + diffPos  ;
			if( destinationPos.y> 2.5f) destinationPos  = new Vector3(destinationPos.x,2.5f,destinationPos.z);
//			if( destinationPos.y < -2f) destinationPos  = new Vector3(destinationPos.x,-2f,destinationPos.z);

			if( destinationPos.x> 8f) destinationPos  = new Vector3(8f,destinationPos.y, destinationPos.z);
			if( destinationPos.x < -2.5f) destinationPos  = new Vector3(-2.5f,destinationPos.y,destinationPos.z);
		}

	}
	
	void OnMouseUp()
    {

		if(  bEnableDrag &&  bDrag   )
		{
			x = Input.mousePosition.x;
			y = Input.mousePosition.y;
			
			destinationPos =     Camera.main.ScreenToWorldPoint(new Vector3(x ,y,10.0f)) + diffPos  ;
			if( destinationPos.y> 1.5f) destinationPos  = new Vector3(destinationPos.x,1.5f,destinationPos.z);
			if( destinationPos.y < -3f) destinationPos  = new Vector3(destinationPos.x,-3f,destinationPos.z);

			if( destinationPos.x> 8f) destinationPos  = new Vector3(8f,destinationPos.y, destinationPos.z);
			if( destinationPos.x < -2.5f) destinationPos  = new Vector3(-2.5f,destinationPos.y,destinationPos.z);
			TutorialBusCleaning.timeWaitToShowHelp = TutorialBusCleaning.PeriodToShowHelp;
			TutorialBusCleaning.bPause = false;
		}
		
		bDrag = false;



		CancelInvoke ( "TestClean" );
		anim.SetBool("clean",false);
		CarWashSoundManager.Instance.Stop_Sound(CarWashSoundManager.Instance.Compressor);
	}

 
 

}
