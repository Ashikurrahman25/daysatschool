using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class CleaningTool : MonoBehaviour  , IBeginDragHandler, IDragHandler, IEndDragHandler
{
	 
	Animator LeftMenu;
 
	public ToolType toolType;
	Vector3 StartPosition ;
	bool bIskoriscen = false;
	[HideInInspector()]
	public  bool bDrag = false;
	 
	public static int OneToolEnabledNo = 1;
	 
	float x;
	float y;
	Vector3 diffPos = new Vector3(0,0,0);
	float testDistance = .7f;//1;// .25f;

	public int ToolNo = 0;
	 
	public static int activeToolNo = 0;
	public static bool bCleaning = false;
 
	Vector3 offPos = Vector3.zero;
	Transform trPom;

	Transform TestPoint;

	public ToolBehavior toolBehavior; 
	ParticleSystem psFinishCleaningAll;

	PointerEventData pointerEventData;
	public Quaternion StartRotation;

	public Animator AnimationChild; 

    public List<GameObject> Indicators;

    Vector3 startScale;

	public void Start()
	{
		 
		OneToolEnabledNo = 1;//na pocetku je dozvoljena upotreba samo jednog alata (prvog)
		bCleaning = false;
		bDrag = false;
		activeToolNo = 0;
 
		StartPosition  = transform.localPosition;
		//StartParent = transform.parent;
		//ActiveToolParent = GameObject.Find("ActiveTool").transform;
        AnimationChild=transform.GetChild(0).GetChild(0).GetComponent<Animator>();

 
		TestPoint = transform.Find("TestPoint");
		if(toolType == ToolType.lawn_mower)  testDistance = 0.5f;
		if(toolType == ToolType.crumbs_cleaner)  testDistance = 0.5f;

		LeftMenu = transform.parent.GetComponent<Animator>();

	}
	 
	void Update()
	{ 
		if(   bDrag )
		{
			x = Input.mousePosition.x;
			y = Input.mousePosition.y;

			transform.position =     Vector3.Lerp (transform.position, Camera.main.ScreenToWorldPoint(new Vector3(x ,y,100.0f))  - offPos ,10* Time.deltaTime);
			if( transform.position.y > 2.5f) transform.position  = new Vector3(transform.position .x,2.5f,transform.position.z);
			//else if( transform.position.y < -3.5f) transform.position  = new Vector3(transform.position .x,-3.5f,transform.position.z);
 
		}

		if(  Input.GetKeyDown(KeyCode.Escape) )
		{
			bDrag = false;
			CancelInvoke("TestClean");
			StartCoroutine("MoveBack" );
		}
	}

	
	public static GameObject  itemBeingDragged;
	
	#region IBeginDragHandler implementation
	
	public void OnBeginDrag (PointerEventData eventData)
	{
		if(bMovingBack) return;
		pointerEventData = eventData;
		bCleaning = false;
	 
		if(OneToolEnabledNo >-1 && ToolNo != OneToolEnabledNo)
		{
			bDrag = false;
			return;
		}
        startScale = transform.localScale;
 
		if(  !bIskoriscen   && !bDrag  )
		{
			transform.localScale = 1.7f*Vector3.one;
			AnimationChild.transform.parent.rotation = Quaternion.Euler(0,0,0);
			activeToolNo = ToolNo;
		//	SoundManager.Instance.Play_ToolClick();
			bDrag = true;
			diffPos =transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition)   ;
			diffPos = new Vector3(diffPos.x,diffPos.y,0);

			//transform.parent = ActiveToolParent;


			InvokeRepeating("TestClean",0, .1f);
			LeftMenu.SetBool("Show",false);
			SoundManagerCleaningClassroom.Instance.StopAndPlay_Sound(SoundManagerCleaningClassroom.Instance.MenuHide);

			if( toolType == ToolType.watering_can  ) 
			{	
				AnimationChild.SetTrigger("tWateringCan");
			}
		}

        TutorialCleanClassRoom.Instance.HidePointer();
        TutorialCleanClassRoom.bPause = true;
        foreach(GameObject go in Indicators)
        {
            if (go != null)
                go.SetActive(true);
        }
		 
	}

	void TestClean()
	{
		if(bCleaning) return;
		if(toolBehavior == ToolBehavior.AnimateOnlyWhenMovingOverObject && !pointerEventData.IsPointerMoving()) return;
		 
		//.2f radius
		Collider2D[] hitColliders = Physics2D.OverlapCircleAll(TestPoint.position, testDistance  , 1 << LayerMask.NameToLayer("Tool"+ToolNo.ToString()+"Interact")); //layermask to filter the varius colliders
		if(hitColliders.Length > 0  )
		{
			trPom = null;
			for (int i =0 ; i<hitColliders.Length; i++)    
			{
				//hitColliders[i].transform.SendMessage("FadeOut_CleaningTool",SendMessageOptions.DontRequireReceiver);
				//RoomScene.ToolActionsLeft[ToolNo-1] --;
				trPom = hitColliders[i].transform;
				//if(RoomScene.ToolActionsLeft[ToolNo-1] <= 0) bIskoriscen = true;
				bCleaning = true;
				break;
			}
			AnimationChild.ResetTrigger("tStop");
			 if( toolType == ToolType.sponge  )
			{
				AnimationChild.SetTrigger("tUpDownClean");  
				SoundManagerCleaningClassroom.Instance.StopAndPlay_Sound(SoundManagerCleaningClassroom.Instance.Sponge);
			}
			else if( toolType == ToolType.brush ) 
			{
				AnimationChild.SetTrigger("tLeftRightClean");
				SoundManagerCleaningClassroom.Instance.StopAndPlay_Sound(SoundManagerCleaningClassroom.Instance.Sponge);
			}
			else if( toolType == ToolType.duster  ) 
			{
				AnimationChild.SetTrigger("tDusterClean");
				SoundManagerCleaningClassroom.Instance.StopAndPlay_Sound(SoundManagerCleaningClassroom.Instance.Duster);
			}
 			else if( toolType == ToolType.broom  ) 
			{
				AnimationChild.SetTrigger("tBroomClean");
				SoundManagerCleaningClassroom.Instance.StopAndPlay_Sound(SoundManagerCleaningClassroom.Instance.Sponge);
			}
			else if( toolType == ToolType.mop  ) 
			{
				AnimationChild.SetTrigger("tMopClean");
				SoundManagerCleaningClassroom.Instance.StopAndPlay_Sound(SoundManagerCleaningClassroom.Instance.Mop);
			}
			else if( toolType == ToolType.roller_brush  ) 
			{
				AnimationChild.SetTrigger("tRollerBrush");
				SoundManagerCleaningClassroom.Instance.StopAndPlay_Sound(SoundManagerCleaningClassroom.Instance.RollerBrush);
			}
			else if( toolType == ToolType.fix_floor  ) 
			{
				AnimationChild.SetTrigger("tFixFloor");
				SoundManagerCleaningClassroom.Instance.StopAndPlay_Sound(SoundManagerCleaningClassroom.Instance.FixFlor);
			}
			else if( toolType == ToolType.garden_shears ) 
			{
				AnimationChild.SetTrigger("tGardenShears");
			}
			else if( toolType == ToolType.rake ) 
			{
				AnimationChild.SetTrigger("tRake");
				if(trPom.parent.Find("ParticlesFinishCleaningAll"))
				{
					psFinishCleaningAll = trPom.parent.Find("ParticlesFinishCleaningAll").GetComponent<ParticleSystem>();
				}
			}
			else if( toolType == ToolType.lawn_mower ) 
			{
				AnimationChild.SetTrigger("tLawnMower");
				if(trPom.parent.Find("ParticlesFinishCleaningAll"))
				{
					psFinishCleaningAll = trPom.parent.Find("ParticlesFinishCleaningAll").GetComponent<ParticleSystem>();
				}
			}
			else if( toolType == ToolType.glass_spray ) 
			{
				AnimationChild.SetTrigger("tGlassSpary");
				SoundManagerCleaningClassroom.Instance.StopAndPlay_Sound(SoundManagerCleaningClassroom.Instance.GlassSpray);
			}
			else if( toolType == ToolType.cloth ) 
			{
				AnimationChild.SetTrigger("tCloth");
			}
			else if( toolType == ToolType.crumbs_cleaner ) 
			{
				AnimationChild.SetTrigger("tCrumbsCleaner");
			}
			 
		}
		 
	}

	public void CleaningAnimationFinised()
	{
		bCleaning = false;
		//if(toolBehavior == ToolBehavior.AnimateOnlyWhenMovingOverObject && !pointerEventData.IsPointerMoving()) return;
		if(trPom!=null) 
		{

			Collider2D[] hitColliders = Physics2D.OverlapCircleAll(TestPoint.position, testDistance *4 , 1 << LayerMask.NameToLayer("Tool"+ToolNo.ToString()+"Interact")); //layermask to filter the varius colliders
			if(hitColliders.Length > 0 )
			{
				for (int i =0 ; i<hitColliders.Length; i++)    
				{
					if(trPom == hitColliders[i].transform)
					{
						trPom.SendMessage("Cleaned",ToolNo);
 
						break;
					}
				}
			}
		}

		if( toolType == ToolType.sponge  )
		{ 
			AnimationChild.SetTrigger("tStop");
			SoundManagerCleaningClassroom.Instance.Stop_Sound(SoundManagerCleaningClassroom.Instance.Sponge);
		}
		else if( toolType == ToolType.brush ) 
		{
			AnimationChild.SetTrigger("tStop");
			SoundManagerCleaningClassroom.Instance.Stop_Sound(SoundManagerCleaningClassroom.Instance.Sponge);
		}
		else if( toolType == ToolType.duster  ) 
		{
			AnimationChild.SetTrigger("tStop");
			SoundManagerCleaningClassroom.Instance.Stop_Sound(SoundManagerCleaningClassroom.Instance.Duster);
		}
		else if( toolType == ToolType.broom  ) 
		{
			SoundManagerCleaningClassroom.Instance.Stop_Sound(SoundManagerCleaningClassroom.Instance.Sponge);
		}
		else if( toolType == ToolType.mop  ) 
		{
			SoundManagerCleaningClassroom.Instance.Stop_Sound(SoundManagerCleaningClassroom.Instance.Mop);
		}
		else if( toolType == ToolType.roller_brush  ) 
		{
			SoundManagerCleaningClassroom.Instance.Stop_Sound(SoundManagerCleaningClassroom.Instance.RollerBrush);
		}
		else if( toolType == ToolType.fix_floor  ) 
		{
			SoundManagerCleaningClassroom.Instance.Stop_Sound(SoundManagerCleaningClassroom.Instance.FixFlor);
		}
		else if( toolType == ToolType.garden_shears ) 
		{
			AnimationChild.SetTrigger("tStop");
		}
		else if( toolType == ToolType.rake ) 
		{
		}
		else if( toolType == ToolType.lawn_mower ) 
		{
			AnimationChild.SetTrigger("tStop");
			//SoundManager.Instance.Stop_Sound(SoundManager.Instance.LawnMower);
		}
		else if( toolType == ToolType.glass_spray ) 
		{
			SoundManagerCleaningClassroom.Instance.Stop_Sound(SoundManagerCleaningClassroom.Instance.GlassSpray);
		}
		else if( toolType == ToolType.cloth ) 
		{
		}
		else if( toolType == ToolType.crumbs_cleaner ) 
		{
			AnimationChild.SetTrigger("tStop");
		}
		
	}
	
	public void WateringCanAnimEnd()
	{
		//if(bMovingBack) 
		//AnimationChild.transform.parent.rotation  = StartRotation;
	
	}
 

	#endregion
	
	#region IDragHandler implementation
	
	public void OnDrag (PointerEventData eventData)
	{
		 
	}
	
	#endregion
	
	#region IEndDragHandler implementation
	
	public void OnEndDrag (PointerEventData eventData)
	{
		if(  !bIskoriscen &&  bDrag /*&& activeToolNo == ToolNo*/  )
		{

			 
			bDrag = false;

			CancelInvoke("TestClean");
			StartCoroutine("MoveBack" );
		}

        foreach(GameObject ob in Indicators)
        {
            if (ob != null)
                ob.SetActive(false);
        }
	}
		#endregion

	 
	bool bMovingBack = false;
	IEnumerator MoveBack(  )
	{
		if(!bMovingBack)
		{
			bMovingBack = true;
			//transform.localScale =  Vector3.one;
			if( toolType == ToolType.watering_can  )
			{

				//AnimationChild.GetCurrentAnimatorClipInfo
				if(AnimationChild.GetCurrentAnimatorStateInfo(0).IsName("WateringCanStartMoving"))
				{
					if(AnimationChild.GetCurrentAnimatorStateInfo(0).normalizedTime<.8f)
					{
						AnimationChild.ResetTrigger("tWateringCan");
					 	AnimationChild.Play("WateringCanEnd",0,0.9f);
					}
					else
					{
						AnimationChild.SetTrigger("tStop");
					}

				}
				else
					AnimationChild.SetTrigger("tStop");

				//AnimationChild.transform.parent.rotation = StartRotation;

				//AnimationChild.ResetTrigger("tWateringCan");
				//AnimationChild.Play("WateringCanEnd",0,0.9f);

			}
			else if( toolType == ToolType.sponge  )
			{ 
				AnimationChild.SetTrigger("tStop");
				SoundManagerCleaningClassroom.Instance.Stop_Sound(SoundManagerCleaningClassroom.Instance.Sponge);
			}
			else if( toolType == ToolType.brush ) 
			{
				AnimationChild.SetTrigger("tStop");
				SoundManagerCleaningClassroom.Instance.Stop_Sound(SoundManagerCleaningClassroom.Instance.Sponge);
			}
			else if( toolType == ToolType.duster  ) 
			{
				AnimationChild.SetTrigger("tStop");
				SoundManagerCleaningClassroom.Instance.Stop_Sound(SoundManagerCleaningClassroom.Instance.Duster);
			}
			else if( toolType == ToolType.broom  ) 
			{
				SoundManagerCleaningClassroom.Instance.Stop_Sound(SoundManagerCleaningClassroom.Instance.Sponge);
			}
			else if( toolType == ToolType.mop  ) 
			{
				SoundManagerCleaningClassroom.Instance.Stop_Sound(SoundManagerCleaningClassroom.Instance.Mop);
			}
			else if( toolType == ToolType.roller_brush  ) 
			{
				SoundManagerCleaningClassroom.Instance.Stop_Sound(SoundManagerCleaningClassroom.Instance.RollerBrush);
			}
			else if( toolType == ToolType.fix_floor  ) 
			{
				SoundManagerCleaningClassroom.Instance.Stop_Sound(SoundManagerCleaningClassroom.Instance.FixFlor);
			}
			else if( toolType == ToolType.garden_shears ) 
			{
				AnimationChild.SetTrigger("tStop");
			}
			else if( toolType == ToolType.rake ) 
			{
			}
			else if( toolType == ToolType.lawn_mower ) 
			{
				AnimationChild.SetTrigger("tStop");
				transform.Find("AnimationHolder/LawnMowerAnim/SmokeParticles1").GetComponent<ParticleSystem>().Stop();
	
			}
			else if( toolType == ToolType.glass_spray ) 
			{
				transform.Find("AnimationHolder/GlassSprayAnim/Particles").GetComponent<ParticleSystem>().Stop();
				SoundManagerCleaningClassroom.Instance.Stop_Sound(SoundManagerCleaningClassroom.Instance.GlassSpray);
			}
			else if( toolType == ToolType.cloth ) 
			{
			}
			else if( toolType == ToolType.crumbs_cleaner ) 
			{
				AnimationChild.SetTrigger("tStop");
			}






			LeftMenu.SetBool("Show",true);
			SoundManagerCleaningClassroom.Instance.StopAndPlay_Sound(SoundManagerCleaningClassroom.Instance.ShowMenu);
			yield return new WaitForEndOfFrame( );

		 

			//transform.parent = StartParent;

			//transform.parent.GetComponent<Mask>().enabled =false;
			//transform.parent.GetComponent<Image>().enabled =false;
			
			yield return new WaitForEndOfFrame( );
			float pom = 0;
			while(pom<1 )
			{ 
				pom+=Time.deltaTime;
				transform.localPosition = Vector3.Lerp(transform.localPosition, StartPosition,pom);
				if( toolType != ToolType.watering_can  ) 
				{
					AnimationChild.transform.parent.rotation = Quaternion.Lerp(AnimationChild.transform.parent.rotation, StartRotation,pom*2);

				}
				//if(transform.localScale.x >  1) transform.localScale =  (1.7f - 2*pom)*Vector3.one;
				//else transform.localScale =  Vector3.one;
				yield return new WaitForEndOfFrame( );
			}
		 
			transform.localPosition = StartPosition;
            transform.localScale = startScale;
		//	transform.parent.GetComponent<Mask>().enabled =true;
		//	transform.parent.GetComponent<Image>().enabled =true;
			activeToolNo = 0;
			bMovingBack = false;
			bCleaning = false;
			if(bIskoriscen) 
			{
//				Image[] imgs = transform.GetComponentsInChildren<Image>();
//				for(int i = 0; i< imgs.Length;i++)
//				{
//					imgs[i].color= new Color(0,0,0,0.5f);
//				}
				transform.Find("Finished").GetComponent<Image>().enabled = true;
				SoundManagerCleaningClassroom.Instance.StopAndPlay_Sound(SoundManagerCleaningClassroom.Instance.CleaningFinished);
			}
		}
        TutorialCleanClassRoom.bPause = false;	 
	}

	public void StartMoveBack()
	{
		CancelInvoke("TestClean");
		StartCoroutine("MoveBack" );
	
	}

	public void AllCleaned()
	{
		bIskoriscen = true; 
		StartCoroutine("MoveBack" );
		 
		bDrag = false;
		if(psFinishCleaningAll != null) psFinishCleaningAll.Play();
		CancelInvoke("TestClean");
	 
	}

 
	bool appFoucs = true;
	void OnApplicationFocus( bool hasFocus )
	{
		if(  !appFoucs && hasFocus )
		{
			if(  !bIskoriscen &&  bDrag )
			{
				bDrag = false;
				 
				CancelInvoke("TestClean");
				StartCoroutine("MoveBack" );
			}
		}
		appFoucs = hasFocus;
		
	}



	 
}

public enum ToolBehavior
{
	AnimateWhenHoveringOverObject,
	AnimateWhenDroppedOnObject,
	AnimateOnlyWhenMovingOverObject

}


