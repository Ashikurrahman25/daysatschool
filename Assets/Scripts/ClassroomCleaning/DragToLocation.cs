using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DragToLocation : MonoBehaviour , IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public static int ToClean= 0;

	float SnapDistance = 4f;

    public float EndLocationScale { get; private set; }
    public Vector3 StartScale { get; private set; }

    public bool bDestroyWithParent;
    public bool bDispseAtLoaction;
    private bool bMoveBackAfterAnimation;
    Vector3 StartPosition ;
	public bool bDrag = false;
    private Vector3 diffPos;
    private bool bDisposed;
    public Transform[] AlternativeLocations ;
	 

	public bool bPlayAnimation = false;
	public bool bLocation2 = false;

	public static int ToClean_L2= 0;
    private GameObject topLimit;

	Transform ParentOld;

	void Awake()
	{
        topLimit=GameObject.Find("TopLimit");

	}

	// Use this for initialization
	void Start () {
		if( !(Gameplay.roomNo ==6 && bPlayAnimation) &&  !bLocation2) ToClean++;
		else if (Gameplay.roomNo ==6  && bLocation2) ToClean_L2++;

		if(AlternativeLocations !=null)
		{
			
		}
		StartPosition = transform.localPosition;
		if(Gameplay.roomNo <5)
		{
			target = GameObject.Find("TrashCanTarget").transform.position;
			trash_can_lead =GameObject.Find("TrashCan").transform.GetComponent<Animator>();
		}
		else if(Gameplay.roomNo ==5)
		{
			EndLocationParent = GameObject.Find("Drawer").transform ;
			target = EndLocationParent.Find("DrawerTarget").transform.position;
			SnapDistance = 2;

			StartScale = transform.localScale;
			bDestroyWithParent = true;
			bDispseAtLoaction = false;
		}
		else if(Gameplay.roomNo ==6 && !bPlayAnimation  && !bLocation2)
		{
			EndLocationParent = GameObject.Find("WashingMachineTarget").transform ;
			target = EndLocationParent.position;
			SnapDistance = 2;
			EndLocationScale = 0.8f;
			StartScale = transform.localScale;
			bDestroyWithParent = true;
			bDispseAtLoaction = false;
		}
		else if(Gameplay.roomNo ==6 && bPlayAnimation)
		{
			EndLocationParent = GameObject.Find("DetergentTarget").transform ;
			target = EndLocationParent.position;
			SnapDistance = 1;
			EndLocationScale = 1;
			StartScale = transform.localScale;
			bDestroyWithParent = true;
			bDispseAtLoaction = false;
			bMoveBackAfterAnimation = true;
		}
		else if(Gameplay.roomNo ==6 &&  bLocation2)
		{

			EndLocationParent = GameObject.Find("CommodeTarget").transform ;
			target = EndLocationParent.position;
			SnapDistance = 2;
			EndLocationScale = .5f;
			StartScale = transform.localScale;
			bDestroyWithParent = true;
			bDispseAtLoaction = false;
		}
		ParentOld = transform.parent;
	}

	void TestDistance()
	{
        if (Vector2.Distance(transform.position, target) < 2f)
        {

            if (bDispseAtLoaction) StartCoroutine("DisposeJunk");
            else if (!bMoveBackAfterAnimation) StartCoroutine("SnapToParent");
            else if (bMoveBackAfterAnimation) StartCoroutine("SnapToLoaction");

            bDrag = false;
            bDisposed = true;


        }
			Debug.Log("DISPOSE");
			Debug.Log(Vector2.Distance(transform.position, target));
	}

	public void OnBeginDrag (PointerEventData eventData)
	{
		if(!bEnableDrag) return;

		if(!bDrag && !bDisposed )
		{
			bDrag = true;
			diffPos =transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition)   ;
			diffPos = new Vector3(diffPos.x,diffPos.y,0);
			InvokeRepeating("TestDistance",0, .2f);
            TutorialCleanClassRoom.Instance.HidePointer();
            TutorialCleanClassRoom.bPause = true;
			transform.SetParent(Gameplay.Instance.DragItemParent);//--
		}
	}

	void Update()
	{
		if( bDrag  && bEnableDrag)  
		{
            Vector3 posToSet = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (topLimit != null)
            {
                Debug.Log("TOP LIMIT POS " + topLimit.transform.position);
                Debug.Log("MOUSE POS " + Input.mousePosition);
                if (posToSet.y > topLimit.transform.position.y)
                    posToSet.y = topLimit.transform.position.y;
                else
                    y = Input.mousePosition.y;
            }
            posToSet.z = 100;

			transform.position =     Vector3.Lerp (transform.position, posToSet  ,10* Time.deltaTime)  ;
			//if( transform.position.y > 2.5f) transform.position  = new Vector3(transform.position .x,2.5f,transform.position.z);
			//else if( transform.position.y < -3.5f) transform.position  = new Vector3(transform.position .x,-3.5f,transform.position.z);

		}
	}


	public void OnEndDrag (PointerEventData eventData)
	{
		if(bDrag  &&  !bDisposed)
		{
			if(bMoveBackAfterAnimation) transform.SetParent(ParentOld);//--
			transform.localScale = Vector3.one;
			bDrag = false;

			CancelInvoke("TestDistance");
			StartCoroutine("MoveBack" );
		}
	}
 

	public void OnDrag (PointerEventData eventData)
	{
		Debug.Log("Dragging");
	}




	IEnumerator DisposeJunk()
	{
		bEnableDrag = false;
		CancelInvoke("TestDistance");
		trash_can_lead.SetTrigger("OpenLead");
		Debug.Log("SHOULD OPEN LEAD");
		float pomX = target.x -transform.position.x;
		float pomY = target.y - transform.position.y;
		Vector3 pos;
		float startX   = transform.position.x;
		float startY   = transform.position.y;
		float speedY =  pomY/pomX;
		float dX = pomX;

		SoundManagerCleaningClassroom.Instance.Play_Sound(SoundManagerCleaningClassroom.Instance.Trash);

		float dist = Vector2.Distance(transform.position,target);
		while  (dist  >0.3f && dist <20 )
		{
			yield return new WaitForFixedUpdate();
			//Debug.Log (dist) ;
			pos = transform.position;
			dX  = (pos.x - startX);

			pos.x +=pomX*Time.deltaTime*2;
			pos.y  =   startY  +  dX*  speedY  +      5*   (target.x -transform.position.x)/pomX *dX /pomX  ;

			transform.position = pos;
			transform.Rotate( new Vector3(0,0, 360*Time.deltaTime ));
			dist = Vector2.Distance(transform.position,target);
		}
		bEnableDrag = true;
		yield return new WaitForFixedUpdate();
		ToClean--;

		Gameplay.Instance.ChangeProgressBar();
		if(ToClean == 0)  Gameplay.Instance.CollectingItemsFinished();
//        TutorialCleanClassRoom.bPause = true;
//        TutorialCleanClassRoom.timeLeftToShowHelp = 10000;
//        TutorialCleanClassRoom.ShowHelpPeriod = 10000;
		GameObject.Destroy(gameObject);

	}

	IEnumerator SnapToParent()
	{
//		Debug.Log( ToClean + " ,  " + ToClean_L2);
		bEnableDrag = false;
		transform.GetComponent<Collider2D>().enabled = false;
		CancelInvoke("TestDistance");
		float timeMove = 0;
		bool animationStarted = false;
		SoundManagerCleaningClassroom.Instance.StopAndPlay_Sound(SoundManagerCleaningClassroom.Instance.ElementCompleted);

		while  (timeMove  <1 )
		{
			yield return new WaitForFixedUpdate();
			transform.position = Vector3.Lerp (transform.position, target , timeMove)  ;
			transform.localScale  = Vector3.Lerp (transform.localScale,  EndLocationScale * StartScale,  timeMove );
			timeMove += Time.fixedDeltaTime*2;
			if(bPlayAnimation &&   timeMove > 0.5f && !animationStarted)
			{
				animationStarted = true;
				transform.GetComponent<Animator>().SetTrigger("tStart");
			}
		}

		transform.parent = EndLocationParent; 

		yield return new WaitForFixedUpdate();
		transform.GetComponent<Collider2D>().enabled = false;

		if( !bLocation2 ) ToClean--;
		else ToClean_L2--;
		Gameplay.Instance.ChangeProgressBar();

        TutorialCleanClassRoom.bPause = true;
        TutorialCleanClassRoom.timeLeftToShowHelp = 10000;
        TutorialCleanClassRoom.ShowHelpPeriod = 10000;


		bEnableDrag = true;
		if(ToClean == 0  && !Gameplay.Instance.Room6WashingMachineFull)  { Gameplay.Instance.CollectingItemsFinished();  }
		if( Gameplay.roomNo ==6 && ToClean_L2 == 0 && !Gameplay.Instance.Room6CommodeClosed ) { Gameplay.Instance.CollectingItemsFinished();}
		this.enabled = false;


	}

	IEnumerator SnapToLoaction()
	{
		bEnableDrag = false;
		transform.GetComponent<Collider2D>().enabled = false;
		CancelInvoke("TestDistance");
		float timeMove = 0;
		bool animationStarted = false;
		while  (timeMove  <1 )
		{
			yield return new WaitForFixedUpdate();
			transform.position = Vector3.Lerp (transform.position, target , timeMove)  ;
			transform.localScale  = Vector3.Lerp (transform.localScale,  EndLocationScale * StartScale,  timeMove );
			timeMove += Time.fixedDeltaTime*2;
			if(bPlayAnimation &&   timeMove > 0.9f && !animationStarted)
			{
				animationStarted = true;
				transform.GetComponent<Animator>().SetTrigger("tStart");
			}
		}
 
		yield return new WaitForFixedUpdate();
		transform.GetComponent<Collider2D>().enabled = false;
 
        TutorialCleanClassRoom.bPause = true;
        TutorialCleanClassRoom.timeLeftToShowHelp = 10000;
        TutorialCleanClassRoom.ShowHelpPeriod = 10000;
		bEnableDrag = true;

		 
	}



	public void AnimationFinished()
	{
		Gameplay.Instance.DetergentFinished();
		transform.SetParent(ParentOld);
		if(transform.name == "Detergent") ParentOld. Find("Detergent2"). GetComponent<RectTransform>().SetAsLastSibling();
		StartCoroutine("MoveBackAndDisable" );
	}

	IEnumerator MoveBackAndDisable(  )
	{
		StartCoroutine("MoveBack" );
		yield return new WaitForSeconds(1.2f);
		transform.GetComponent<Collider2D>().enabled = false;
		this.enabled = false;
	}

	IEnumerator MoveBack(  )
	{
		bEnableDrag = false;
		yield return new WaitForEndOfFrame( );
		if(!bMovingBack)
		{
			bMovingBack = true;
			 

			yield return new WaitForEndOfFrame( );
			float pom = 0;
			while(pom<1 )
			{ 
				 
				pom+=Time.deltaTime;
				transform.localPosition = Vector3.Lerp(transform.localPosition, StartPosition,pom);
				yield return new WaitForEndOfFrame( );
			}

			transform.localPosition = StartPosition;
			 
			bMovingBack = false;
		}

		if(transform.name != "Detergent")	transform.SetParent(ParentOld);//--


		bEnableDrag = true;
        TutorialCleanClassRoom.bPause = false;	 
	}

	 
	bool appFoucs = true;

    public Transform EndLocationParent { get; private set; }

    private Vector3 target;
    public Animator trash_can_lead;
    public bool bEnableDrag;
    private float y;
    private bool bMovingBack;

    void OnApplicationFocus( bool hasFocus )
	{
		if(  !appFoucs && hasFocus )
		{
			if(    bDrag )
			{
				bDrag = false;
				
				CancelInvoke("TestDistance");
				StartCoroutine("MoveBack" );
			}
		}
		appFoucs = hasFocus;
		
	}

 

}
