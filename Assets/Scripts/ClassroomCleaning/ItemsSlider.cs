using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemsSlider : MonoBehaviour,  IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
	CanvasGroup BlockAll;
	private const float inchToCm = 2.54f;
	private EventSystem eventSystem = null;
	private float dragThresholdCM =  0.5f; //vrednost u cm
 
	public GameObject[] ItemPanels;
	 
	public GameObject NativaAdsPanel;

	public static int ActiveItemNo = 1;
	public Sprite[] ItemSprites;
	//public ShopManager shopManager;
 
	bool bEnableDrag = true;
	bool bDrag = false;
	bool bInertia = false;
	 
	float x;
	float y;
	float speedX = 0;
	float speedLimit = 2;
	float prevX=0;
	Vector3 diffPos = new Vector3(0,0,0);
	Vector3 startPos = new Vector3(0,0,0);

	Vector3 dist1;
	Vector3 dist3;

	float nextItemTresholdX = 1;

	Vector3 ActiveItemPosition;
	float itemDistanceX; 

	public Button Next;
	public Button Prev;
	bool bNativeAdRemoved = false;
	public bool b_Enabled = false;


	float inactiveX;

	void Awake()
	{
		ActiveItemNo = ReturnActiveItemIndex(ActiveItemNo);

		inactiveX = 0.3f+((Screen.width/(float)Screen.height  - 1.7777f)*.18f);
	 
	}

	void OnEnable () {
		 
		BlockAll = GameObject.Find("BlockAll").GetComponent<CanvasGroup>();

		if (eventSystem == null) 	eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();

		SetDragThreshold();
		StartCoroutine("Init");
	}

	IEnumerator Init()
	{

		yield return new WaitForSeconds(.2f);
		
		ActiveItemPosition =   transform.position;
		itemDistanceX = ItemPanels[2].transform.position.x - ItemPanels[1].transform.position.x;

		dist1 = new Vector3(   itemDistanceX  , 0, 0);
		dist3 = new Vector3(   itemDistanceX*3  , 0, 0);
		startPos   = new Vector3(0, ItemPanels[1].transform.position.y,ItemPanels[1].transform.position.z);

		//if( Shop.RemoveAds == 2 ) 
			UkloniNativeAds();
		SetRooms();


	}

	public void SetRooms()
	{
		if( !ItemPanels[0].activeSelf) ItemPanels[0].SetActive(true);
		if( !ItemPanels[1].activeSelf) ItemPanels[1].SetActive(true);
		if( !ItemPanels[2].activeSelf) ItemPanels[2].SetActive(true);

		if(ActiveItemNo == 1)
		{
			if(!bNativeAdRemoved) ItemPanels[2].SetActive(false);
			else ItemPanels[2].SetActive(true);
			
			ItemPanels[0].SetActive(false);
		}

		//prethodni panel
		if(ActiveItemNo>2)
		{ 
			bool bActive = ItemPanels[0].activeSelf;
			ItemPanels[0].SetActive(true);
			if(bNativeAdRemoved && ActiveItemNo==3) ItemPanels[0].transform.Find("Room").GetComponent<Image>().sprite = ItemSprites[0];
			else
				ItemPanels[0].transform.Find("Room").GetComponent<Image>().sprite = ItemSprites[ActiveItemNo-2];
			if(GameData.ItemsDataList[ActiveItemNo-3].unlocked)
			{
				ItemPanels[0].transform.Find("Text"). GetComponent<Text>().text = "";
			}
			else
			{
				ItemPanels[0].transform.Find("Text"). GetComponent<Text>().text =  GameData.TotalStars.ToString()+"/"+ GameData.ItemsDataList[ActiveItemNo-3].stars.ToString();            
			}
			ItemPanels[0].transform.Find("Lock"). GetComponent<Image>().enabled = !GameData.ItemsDataList[ActiveItemNo-3].unlocked;
			ItemPanels[0].transform.Find("TextBG"). GetComponent<Image>().enabled = !GameData.ItemsDataList[ActiveItemNo-3].unlocked;
			ItemPanels[0].transform.Find("ImageSelected"). GetComponent<Image>().enabled =  GameData.CheckedRooms[ActiveItemNo-3];
			ItemPanels[0].SetActive(bActive);
		}
		else
		{
			bool bActive = ItemPanels[0].activeSelf;
			ItemPanels[0].SetActive(true);
			if(ActiveItemNo == 2) ItemPanels[0].transform.Find("Room").GetComponent<Image>().sprite = ItemSprites[0];
			 
			ItemPanels[0].transform.Find("Text"). GetComponent<Text>().text = "";
			Prev.interactable = false;
			ItemPanels[0].SetActive(bActive);
		}



//		Debug.Log("OTKLJUCANO  "+ ActiveItemNo+"  "+ GameData.ItemsDataList[ActiveItemNo-1].unlocked);
	

		//trenutni panel

		if(ActiveItemNo >1)
		{
			bool bActive = ItemPanels[1].activeSelf;
			ItemPanels[1].SetActive(true);

			ItemPanels[1].transform.Find("Room").GetComponent<Image>().sprite = ItemSprites[ActiveItemNo-1];
			if(GameData.ItemsDataList[ActiveItemNo-2].unlocked)
			{
				ItemPanels[1].transform.Find("Text"). GetComponent<Text>().text = "";
				//Tutorial.bPause = false;
				//Tutorial.timeWaitToShowHelp = Tutorial.PeriodToShowHelp;
			}
			else
			{
				ItemPanels[1].transform.Find("Text"). GetComponent<Text>().text = GameData.TotalStars.ToString()+"/"+ GameData.ItemsDataList[ActiveItemNo-2].stars.ToString();
				//Tutorial.bPause = true;
				//Tutorial.timeWaitToShowHelp = Tutorial.PeriodToShowHelp;
			}

			ItemPanels[1].transform.Find("Lock"). GetComponent<Image>().enabled = !GameData.ItemsDataList[ActiveItemNo-2].unlocked;
			ItemPanels[1].transform.Find("TextBG"). GetComponent<Image>().enabled = !GameData.ItemsDataList[ActiveItemNo-2].unlocked;
			ItemPanels[1].transform.Find("ImageSelected"). GetComponent<Image>().enabled =  GameData.CheckedRooms[ActiveItemNo-2];
			ItemPanels[1].SetActive(bActive);
		}

		else
		{
			bool bActive = ItemPanels[1].activeSelf;
			ItemPanels[1].SetActive(true);

			ItemPanels[1].transform.Find("Room").GetComponent<Image>().sprite = ItemSprites[0];
			if(GameData.ItemsDataList[0].unlocked)
			{
				ItemPanels[1].transform.Find("Text"). GetComponent<Text>().text = "";
				//Tutorial.bPause = false;
				//Tutorial.timeWaitToShowHelp = Tutorial.PeriodToShowHelp;
			}
			else
			{
				ItemPanels[1].transform.Find("Text"). GetComponent<Text>().text = GameData.TotalStars.ToString()+"/"+ GameData.ItemsDataList[0].stars.ToString();
				//Tutorial.bPause = true;
				//Tutorial.timeWaitToShowHelp = Tutorial.PeriodToShowHelp;
			}
			ItemPanels[1].transform.Find("Lock"). GetComponent<Image>().enabled = !GameData.ItemsDataList[0].unlocked;
			ItemPanels[1].transform.Find("TextBG"). GetComponent<Image>().enabled = !GameData.ItemsDataList[0].unlocked;
			ItemPanels[1].transform.Find("ImageSelected"). GetComponent<Image>().enabled =  GameData.CheckedRooms[0];
			ItemPanels[1].SetActive(bActive);
		}



		//sledeci panel
		if(ActiveItemNo< ItemSprites.Length)
		{
			bool bActive = ItemPanels[2].activeSelf;
			ItemPanels[2].SetActive(true);

			if(ActiveItemNo == 1) ItemPanels[2].transform.Find("Room").GetComponent<Image>().sprite = ItemSprites[2];
			else ItemPanels[2].transform.Find("Room").GetComponent<Image>().sprite = ItemSprites[ActiveItemNo];

			if(GameData.ItemsDataList[ActiveItemNo-1].unlocked)
			{
				ItemPanels[2].transform.Find("Text"). GetComponent<Text>().text = "";
				 
			}
			else
			{
				ItemPanels[2].transform.Find("Text"). GetComponent<Text>().text =GameData.TotalStars.ToString()+"/"+ GameData.ItemsDataList[ActiveItemNo-1].stars.ToString();// 
				 
			}
			ItemPanels[2].transform.Find("Lock"). GetComponent<Image>().enabled = !GameData.ItemsDataList[ActiveItemNo-1].unlocked;
			ItemPanels[2].transform.Find("TextBG"). GetComponent<Image>().enabled = !GameData.ItemsDataList[ActiveItemNo-1].unlocked;
			ItemPanels[2].transform.Find("ImageSelected"). GetComponent<Image>().enabled =  GameData.CheckedRooms[ActiveItemNo-1];
			ItemPanels[2].SetActive(bActive);
		}
		else
		{
			Next.interactable = false;
		}

	}

	 
	private void SetDragThreshold()
	{
		if (eventSystem != null)
		{
			eventSystem.pixelDragThreshold = (int)( dragThresholdCM * Screen.dpi / inchToCm);

		}
	}

 
	void FixedUpdate () {
		if(!b_Enabled) return;
		if(bInertia)
		{
			speedX *=.80f;
			if(speedX <0.05f && speedX > -0.05f)
			{
				speedX = 0;
				bInertia = false;
				//MoveBack
			}


			ItemPanels[1].transform.position  += new Vector3(  speedX   , 0, 0);
			ItemPanels[0].transform.position   = ItemPanels[1].transform.position  - dist1;
			ItemPanels[2].transform.position   = ItemPanels[1].transform.position  + dist1;



			if(ItemPanels[1].transform.position.x < -nextItemTresholdX || ItemPanels[1].transform.position.x >nextItemTresholdX)
			{
				bInertia = false;
				ChangeRoom();
			}
			else if (!bInertia )  StartCoroutine ("SnapToPosition");

		}
	}


	void LateUpdate()
	{
		if(!bNativeAdRemoved)
		{
			if(ActiveItemNo == 1 )  NativaAdsPanel.transform.position =   ItemPanels[2].transform.position;
			else	if(ActiveItemNo == 2 )  NativaAdsPanel.transform.position =   ItemPanels[1].transform.position;
			else if(ActiveItemNo == 3 )  NativaAdsPanel.transform.position =   ItemPanels[0].transform.position;
			else 	NativaAdsPanel.transform.position = new Vector3(100,100,0);
		}
	}

	void ChangeRoom()
	{
	 	if(ItemPanels[1].transform.position.x < -nextItemTresholdX ) //pomeranje u levo
		{
			if(ActiveItemNo < ItemSprites.Length ) 
			{
				ChangeNext();
			}
			else  StartCoroutine ("SnapToPosition");
		}
		else
		{
			if(ActiveItemNo > 1 ) 
			{
				ChangePrevious();
			}
			else  StartCoroutine ("SnapToPosition");

		}
		SwitchPlace(); 

	
	}
 
	void ChangeNext()
	{
		if( !ItemPanels[0].activeSelf) ItemPanels[0].SetActive(true);
		if( !ItemPanels[1].activeSelf) ItemPanels[1].SetActive(true);
		if( !ItemPanels[2].activeSelf) ItemPanels[2].SetActive(true);

		if(ActiveItemNo < ItemSprites.Length ) 
		{

			ActiveItemNo++;
			if(bNativeAdRemoved  && ActiveItemNo == 2) ActiveItemNo++;
			Prev.interactable = true;
			//Prev.GetComponent<Animator>().SetTrigger("Normal");
			Prev.GetComponent<Animator>().Play("Normal");

			//Debug.Log("Active Item"+ActiveItemNo);

			if(ActiveItemNo== ItemSprites.Length )
			{
				ItemPanels[0].SetActive(false);
				Next.interactable = false;
			}
			else
			if(ActiveItemNo>=2 && ActiveItemNo< ItemSprites.Length )
			{

				ItemPanels[0].transform.Find("Room").GetComponent<Image>().sprite = ItemSprites[ActiveItemNo];

				if(GameData.ItemsDataList[ActiveItemNo-1].unlocked)
				{
					ItemPanels[0].transform.Find("Text"). GetComponent<Text>().text = "";
				}
				else
				{
					ItemPanels[0].transform.Find("Text"). GetComponent<Text>().text = GameData.TotalStars.ToString()+"/"+ GameData.ItemsDataList[ActiveItemNo-1].stars.ToString();// 
					 
				}
				ItemPanels[0].transform.Find("Lock"). GetComponent<Image>().enabled = !GameData.ItemsDataList[ActiveItemNo-1].unlocked;
				ItemPanels[0].transform.Find("TextBG"). GetComponent<Image>().enabled = !GameData.ItemsDataList[ActiveItemNo-1].unlocked;
				ItemPanels[0].transform.Find("ImageSelected"). GetComponent<Image>().enabled =  GameData.CheckedRooms[ActiveItemNo-1];

				if(ActiveItemNo == 2)
				{
					ItemPanels[2].SetActive(false);
					ItemPanels[0].SetActive(false);
				}
			}
		 

			GameObject  ItemPanelTmp = ItemPanels[0];
			
			ItemPanels[0] = ItemPanels[1];
			ItemPanels[1] = ItemPanels[2];
			ItemPanels[2] = ItemPanelTmp;
			
			
			StartCoroutine ("SnapToPosition");

 /*
			if(GameData.ItemsDataList[ActiveItemNo-2].unlocked)
			{
				//Tutorial.bPause = false;
				//Tutorial.timeWaitToShowHelp = Tutorial.PeriodToShowHelp;
			}
			else
			{
				//Tutorial.bPause = true;
				//Tutorial.timeWaitToShowHelp = Tutorial.PeriodToShowHelp;
			}
			*/
		} 


	}

 

	//*******************
	void ChangePrevious()
	{
		if( !ItemPanels[0].activeSelf) ItemPanels[0].SetActive(true);
		if( !ItemPanels[1].activeSelf) ItemPanels[1].SetActive(true);
		if( !ItemPanels[2].activeSelf) ItemPanels[2].SetActive(true);

 		if(ActiveItemNo > 1 ) 
		{
			//SoundManager.Instance.Play_PopUpHide(0f);
			ActiveItemNo--;
			if(bNativeAdRemoved && ActiveItemNo == 2) ActiveItemNo--;

			//Debug.Log("Active Item"+ActiveItemNo);

			Next.interactable = true;
			//Next.GetComponent<Animator>().SetTrigger("Normal");
			Next.GetComponent<Animator>().Play("Normal");
			//if(ItemPanels[2].transform.FindChild("Item").childCount>0) 
			//	GameObject.DestroyImmediate( ItemPanels[2].transform.FindChild("Item").GetChild(0).gameObject);

			if(ActiveItemNo>3 )
			{
				ItemPanels[2].transform.Find("Room").GetComponent<Image>().sprite = ItemSprites[ActiveItemNo-2];

				if(GameData.ItemsDataList[ActiveItemNo-3].unlocked)
				{
					ItemPanels[2].transform.Find("Text"). GetComponent<Text>().text = "";
				}
				else
				{
					ItemPanels[2].transform.Find("Text"). GetComponent<Text>().text =GameData.TotalStars.ToString()+"/"+ GameData.ItemsDataList[ActiveItemNo-3].stars.ToString();// 
				}
				ItemPanels[2].transform.Find("Lock"). GetComponent<Image>().enabled = !GameData.ItemsDataList[ActiveItemNo-3].unlocked;
				ItemPanels[2].transform.Find("TextBG"). GetComponent<Image>().enabled = !GameData.ItemsDataList[ActiveItemNo-3].unlocked;
				ItemPanels[2].transform.Find("ImageSelected"). GetComponent<Image>().enabled =  GameData.CheckedRooms[ActiveItemNo-3];

			}
			else if(ActiveItemNo==3 )
			{
				if(!bNativeAdRemoved)  ItemPanels[2].SetActive(false);
				else
				{
					ItemPanels[2].transform.Find("Room").GetComponent<Image>().sprite = ItemSprites[0];
					
					if(GameData.ItemsDataList[ActiveItemNo-3].unlocked)
					{
						ItemPanels[2].transform.Find("Text"). GetComponent<Text>().text = "";
					}
					else
					{
						ItemPanels[2].transform.Find("Text"). GetComponent<Text>().text =GameData.TotalStars.ToString()+"/"+ GameData.ItemsDataList[0].stars.ToString();// 
					}
					ItemPanels[2].transform.Find("Lock"). GetComponent<Image>().enabled = !GameData.ItemsDataList[0].unlocked;
					ItemPanels[2].transform.Find("TextBG"). GetComponent<Image>().enabled = !GameData.ItemsDataList[0].unlocked;
					ItemPanels[2].transform.Find("ImageSelected"). GetComponent<Image>().enabled =  GameData.CheckedRooms[0];

				}
			}
			else if(ActiveItemNo==2 )
			{
				ItemPanels[2].transform.Find("Room").GetComponent<Image>().sprite = ItemSprites[0];
				
				if(GameData.ItemsDataList[0].unlocked)
				{
					ItemPanels[2].transform.Find("Text"). GetComponent<Text>().text = "";
				}
				else
				{
					ItemPanels[2].transform.Find("Text"). GetComponent<Text>().text =GameData.TotalStars.ToString()+"/"+ GameData.ItemsDataList[0].stars.ToString();// 
				}
				ItemPanels[2].transform.Find("Lock"). GetComponent<Image>().enabled = !GameData.ItemsDataList[0].unlocked;
				ItemPanels[2].transform.Find("TextBG"). GetComponent<Image>().enabled = !GameData.ItemsDataList[0].unlocked;
				ItemPanels[2].transform.Find("ImageSelected"). GetComponent<Image>().enabled =  GameData.CheckedRooms[0];
			 
				ItemPanels[0].SetActive(false);
			}
			else if(ActiveItemNo==1 )
			{
				ItemPanels[2].SetActive(false);
				if(!bNativeAdRemoved) ItemPanels[1].SetActive(false);
				Prev.interactable = false;
			}
 

			GameObject  ItemPanelTmp = ItemPanels[2];

			ItemPanels[2] = ItemPanels[1];
			ItemPanels[1] = ItemPanels[0];
			ItemPanels[0] = ItemPanelTmp;

			 
			StartCoroutine ("SnapToPosition");
			//GameData.IncrementScrollCarCount();
			/*
			if(GameData.ItemsDataList[ActiveItemNo-1].unlocked)
			{
				//Tutorial.bPause = false;
				//Tutorial.timeWaitToShowHelp = Tutorial.PeriodToShowHelp;
			}
			else
			{
				//Tutorial.bPause = true;
				//Tutorial.timeWaitToShowHelp = Tutorial.PeriodToShowHelp;
			}
			*/
		} 
	}

	IEnumerator SnapToPosition()
	{
		bEnableDrag = false;
		//speedX
		float i =0;
		while(   i<1.1f)
		{
			i+=0.06f;
			yield return new WaitForFixedUpdate();

			ItemPanels[1].transform.position   =   Vector3.Lerp( ItemPanels[1].transform.position, startPos  , i);
			ItemPanels[0].transform.position   = ItemPanels[1].transform.position  - dist1;
			ItemPanels[2].transform.position   = ItemPanels[1].transform.position  + dist1;
		}
		bEnableDrag = true;
	}



	void SwitchPlace()
	{
		if(ItemPanels[0].transform.position.x < itemDistanceX) ItemPanels[0].transform.position  += dist3;
		if(ItemPanels[1].transform.position.x < itemDistanceX) ItemPanels[1].transform.position  += dist3;
		if(ItemPanels[2].transform.position.x < itemDistanceX) ItemPanels[2].transform.position  += dist3;

		if(ItemPanels[0].transform.position.x > itemDistanceX) ItemPanels[0].transform.position  -= dist3;
		if(ItemPanels[1].transform.position.x > itemDistanceX) ItemPanels[1].transform.position  -= dist3;
		if(ItemPanels[2].transform.position.x > itemDistanceX) ItemPanels[2].transform.position  -= dist3;
		
	}


	public void OnBeginDrag(PointerEventData eventData)
	{
		if(!b_Enabled) return;
		if(MenuManager.activeMenu != "") return;
		if(!bEnableDrag) return;

		bDrag = true;
	 
		diffPos = ItemPanels[1].transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition)   ;
		diffPos = new Vector3(diffPos.x,diffPos.y,0);
		prevX = ItemPanels[1].transform.position.x;
			
	}
	
	public void OnDrag(PointerEventData eventData)
	{
		if(!b_Enabled) return;
		if(MenuManager.activeMenu != "") return;
		if( bEnableDrag &&  bDrag )
		{
			 
			prevX = ItemPanels[1].transform.position.x;
			x = Input.mousePosition.x;
			y = Input.mousePosition.y;
		 
			ItemPanels[1].transform.position = new Vector3(  (Camera.main.ScreenToWorldPoint(new Vector3(x ,y,100.0f)) + diffPos).x   ,  ActiveItemPosition.y,  ActiveItemPosition.z);
			 

			ItemPanels[0].transform.position   = ItemPanels[1].transform.position  - dist1;
			ItemPanels[2].transform.position   = ItemPanels[1].transform.position  + dist1;

			if( (ActiveItemNo ==1 && ItemPanels[1].transform.position.x> 1) ||  (ActiveItemNo== ItemSprites.Length &&  ItemPanels[1].transform.position.x < -1 ))
			{
				bDrag = false;
				bInertia = true;
			}

			else if( ActiveItemNo> 1 && ItemPanels[1].transform.position.x> 1.8f)   
			{
				ChangePrevious();
				bDrag = false;
				bInertia = false;
			}
			else if( ActiveItemNo < ItemSprites.Length &&  ItemPanels[1].transform.position.x < -1.8f ) 
			{
				ChangeNext();
				bDrag = false;
				bInertia = false;
			}
		}

	 
	}
	
	public void OnEndDrag(PointerEventData eventData)
	{
		 
		if(  bEnableDrag &&  bDrag   )
		{
			x = Input.mousePosition.x;
			y = Input.mousePosition.y;
 	 
			ItemPanels[1].transform.position = new Vector3(  (Camera.main.ScreenToWorldPoint(new Vector3(x ,y,100.0f)) + diffPos).x   ,  ActiveItemPosition.y,  ActiveItemPosition.z);
			
			ItemPanels[0].transform.position   = ItemPanels[1].transform.position  - dist1;
			ItemPanels[2].transform.position   = ItemPanels[1].transform.position  + dist1;
 	 
			speedX =  ItemPanels[1].transform.position.x  - prevX;
			if(speedX <-speedLimit) speedX = -speedLimit;
			else if(speedX > speedLimit) speedX = speedLimit;
		 
			bDrag = false;
			bInertia = true;
 
		}
	}


 




	int  ReturnActiveItemIndex (int _ActiveItemNo)
	{
		if(_ActiveItemNo == 1) return 1;
		if(_ActiveItemNo>1) return _ActiveItemNo+1;
		else return 2;
	}

	int ReturnRoomIndex (int _ActiveItemNo)
	{
		if(_ActiveItemNo == 1) return 1;
		if(_ActiveItemNo>2) return _ActiveItemNo-1;
		else return 0;
	}


	public void OnPointerClick( PointerEventData eventData)
	{
		if(!b_Enabled) return;
		if( ActiveItemNo == 2){  return;}
		if(MenuManager.activeMenu != "") return;
		if(BlockAll.blocksRaycasts) return;

		Vector2 mpos = Camera.main.ScreenToViewportPoint(Input.mousePosition);

	 	
		if(mpos.y<0.15f || mpos.y>0.65f) return;
		if(mpos.x<inactiveX|| mpos.x>(1-inactiveX)) return;



		if(!eventData.dragging) 
		{
			if(GameData.ItemsDataList[ ReturnRoomIndex( ActiveItemNo )-1 ].unlocked)
			{
				Gameplay.roomNo = ReturnRoomIndex( ActiveItemNo  );
				//	SoundManager.Instance.Play_ButtonClick();
//				Debug.Log("Room" + ActiveItemNo);
				//eventData.rawPointerPress.transform.parent.FindChild("ImageSelected").GetComponent<Animator>().SetTrigger("tShow");
				b_Enabled = false;
				HomeScene.Instance.CarouselSelected();//umesto animacije zvezdice
				BlockAll.blocksRaycasts = true;

				GameData.IncrementTapOnRoomCount();
			}
			else
			{
//				if( GameData.TotalStars  >=  GameData.VehicleDataList[ActiveItemNo-1].stars  )
//				{
//					GameData.UnlockVehicle( ActiveItemNo-1);
//					ItemPanels[1].transform.FindChild("Lock"). GetComponent<Image>().enabled = !GameData.VehicleDataList[ActiveItemNo-1].unlocked;
//					Shop.Instance.AnimiranjeDodavanjaZvezdica( -GameData.VehicleDataList[ActiveItemNo-1].stars,null, ""); 
//					BlockAll.blocksRaycasts = true;
//					StartCoroutine(SetBlockAll(1f,false));
//				}
//				else
//				{
//					Debug.Log("NEMA DOVOLJNO ZVEZDICA");
					//shopManager.ShowPopUpShop();
				 	//SoundManager.Instance.Play_Error();
				//	BlockAll.blocksRaycasts = true;
				//	StartCoroutine(SetBlockAll(1f,false));
//				}
			}
		}
	}













	public void btnNext(  )
	{
		if(!b_Enabled) return;
		ChangeNext();
		//SoundManager.Instance.Play_PopUpHide(0f);	
		BlockAll.blocksRaycasts = true;
		StartCoroutine(SetBlockAll(.4f,false));
		//Tutorial.timeWaitToShowHelp = Tutorial.PeriodToShowHelp;
		//GameObject.Find("Tutorial").SendMessage("HidePointer");
		 
	}
	
	public void btnPrevious(  )
	{
		if(!b_Enabled) return;
		ChangePrevious();
		//SoundManager.Instance.Play_PopUpHide(0f);
		BlockAll.blocksRaycasts = true;
		StartCoroutine(SetBlockAll(.4f,false));
		//Tutorial.timeWaitToShowHelp = Tutorial.PeriodToShowHelp;
		//GameObject.Find("Tutorial").SendMessage("HidePointer");
		 
	}

	IEnumerator SetBlockAll(float time, bool blockRays)
	{
		if(BlockAll == null) BlockAll = GameObject.Find("BlockAll").GetComponent<CanvasGroup>();
		yield return new WaitForSeconds(time);
		BlockAll.blocksRaycasts = blockRays;
		
	}


	public void UkloniNativeAds()
	{
		if(!bNativeAdRemoved)
		{
			bNativeAdRemoved = true;
			GameObject.Destroy(  NativaAdsPanel. gameObject);
//			Sprite[] tmpItemSprites= new Sprite[ItemSprites.Length-1];
//			tmpItemSprites[0] = ItemSprites[0];
//			
//			for(int i = (ItemSprites.Length-2); i>0; i--)
//			{
//				tmpItemSprites[i] =  ItemSprites[i+1];
//			}

			//reset carousel position
			//ContentHolder.position = startPos;
			if(ActiveItemNo ==2) ActiveItemNo --;
			 
		}
	}

}