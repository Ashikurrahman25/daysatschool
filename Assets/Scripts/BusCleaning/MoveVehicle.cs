using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoveVehicle : MonoBehaviour {

	public Transform NextLevelPlatform;
	public GameObject CAR;


	public Image SalonBG;
	public Image SalonFG;

	public Image GarargeBG;
	public Image GarargeBGTop;
	public Image GarargeBarMask;
	public Image GarargeDoor;

	public GameObject[] VEHICLES;
	int SelectedVehicle;
	GameObject newVehicle;
	Animator newVehicleAnim;
	public RectTransform IndicatorArrow;

//	public ScrollBackground TestDriveBG;
 
	public static bool bMoveDown = false;
	public static bool bTestDrive = false;

	float[] VehicleScale = new float[] {  1f,	 	1.2f,		0.85f, 		1.1f, 		1.2f, 
														1.35f,	1.2f,		1.4f, 		1.1f, 	1.1f,
														1.0f,		1.1f,	    1.15f,		1,			1.2f,
														.88f,		.95f,			1.55f,	1.2f,		1.1f,
														1.1f,		.95f };

	int[] VehicleWheelsCount = new int[] {		2, 2, 2, 2, 2, 
																2, 2, 2, 3, 2, 
																3, 3, 2, 2, 2,
																0, 2, 2, 2, 3,
																0, 0 };
	public static int wheelsCount = 2;

	Vector3 startPos;
	Vector3 carPos;
	Vector3 carDownPos;   
	Vector3 carUpPos;
	Vector3 exitGaragePos;
	Vector3  drivePos;

	Vector3 platformStartPos;    
	Vector3 platformDownPos;   
	Vector3 platformUpPos;   
	// Use this for initialization

	GameObject Platform;

 
	public GameObject[] buttons;

	float timeA = -.5f ;
	float timeDown = 0 ;
	float timeTestDrive = 0 ;
	
	
	
	public static float MoveDownTime = 3;
	bool bShowHide = false;
	bool bSpeedUpTime = false;
	bool bSlowDown = false;


	void Awake() 
	{
		bMoveDown = false;
		bTestDrive = false;

//		GarargeDoor.enabled = false;
//		GarargeBarMask.enabled = false;
//		GarargeBG.enabled = false;
//		GarargeBGTop.enabled = false;
		//SalonBG.enabled = true;
		//SalonFG.enabled = true;


//		SelectedVehicle =  ItemsSlider.ActiveItemNo-1;
		wheelsCount = VehicleWheelsCount[SelectedVehicle];
		if( SelectedVehicle>=0 && SelectedVehicle < VEHICLES.Length) 
		{
			newVehicle = (GameObject) GameObject.Instantiate(VEHICLES[SelectedVehicle]);
			//carPos = CAR.transform.position;
			carPos = CAR.transform.GetComponent<RectTransform>().anchoredPosition3D;
            //startPos = CAR.transform.position; /*- new Vector3(13,0,0);*/
            startPos = CAR.transform.parent.GetComponent<RectTransform>().anchoredPosition3D; 
            newVehicle.transform.position = carPos;
			newVehicle.name = "CAR";
			newVehicle.transform.SetParent(CAR.transform.parent,false);
            //newVehicle.transform.localScale = Vector3.one*VehicleScale[SelectedVehicle];

            //carPos = CAR.transform.parent.position;
            carPos = CAR.transform.parent.GetComponent<RectTransform>().anchoredPosition3D;


            startPos = newVehicle.transform.parent.GetComponent<RectTransform>().anchoredPosition3D - new Vector3(1300,0,0);
			exitGaragePos = newVehicle.transform.parent.position + new Vector3(1700,0,0);
			GameObject.DestroyImmediate(CAR);
			//newVehicle.transform.SetSiblingIndex(2);

			newVehicleAnim = newVehicle.GetComponent<Animator>();

			 //Gameplay.Instance.StartAndStopLeaves();
		}

//		platformStartPos =    NextLevelPlatform.position;

		platformDownPos = platformStartPos - new Vector3(0,8,0);
		platformUpPos = platformStartPos + new Vector3(0,8,0);

        carDownPos = carPos - new Vector3(0, 8, 0);
        carUpPos = carPos + new Vector3(0, 8, 0);
        drivePos = carPos - new Vector3(1, 0.5f, 0);
    }


	void SetGameTools()
	{
		//podesavanje alata koji se ne koriste
		//		Debug.Log("SET TOOLS "  + SelectedVehicle);
		CarWashLevelManager.Instance.RemovedToolsList = "";

		//PRVI DEO GARAZE
		int removedButtonsCount = 0;
		int remove = 0;
//		if(    SelectedVehicle == 15 || SelectedVehicle == 20 ||SelectedVehicle == 21) //tenk,avion i NLO neamju tockove
//		{
//			CarWashLevelManager.Instance.RemovedToolsList = "05";
//			GameObject.Destroy(buttons[4]);
//			removedButtonsCount++;
//		}
		while(removedButtonsCount<2)
		{
			remove =  Random.Range(1,5);
			if(remove != 3)
			{
			   	if(!CarWashLevelManager.Instance.RemovedToolsList.Contains(remove.ToString().PadLeft(2,'0')))
				{

					if( (remove == 4 || remove == 5) && removedButtonsCount <=1)
					{
						CarWashLevelManager.Instance.RemovedToolsList+="04,05,";
						GameObject.Destroy(buttons[3]);
						GameObject.Destroy(buttons[4]);
						removedButtonsCount += 2;
					}
					else if( remove != 4 && remove != 5)
					{
						CarWashLevelManager.Instance.RemovedToolsList+=remove.ToString().PadLeft(2,'0')+",";
						GameObject.Destroy(buttons[remove-1]);
						removedButtonsCount++;
					}
				}
			}
		}

 		//DRUGI DEO GARAZE
//		if(    SelectedVehicle == 15 || SelectedVehicle == 20 ||SelectedVehicle == 21) //tenk,avion i NLO neamju tockove
//		{
//			CarWashLevelManager.Instance.RemovedToolsList +=  "10,11,";
//			GameObject.Destroy(buttons[10]);
//			GameObject.Destroy(buttons[9]);
//			 
//		}
//		else
		{
//			if(Random.Range(0,2) == 1)
//			{
//				CarWashLevelManager.Instance.RemovedToolsList+="08,09,";
//				GameObject.Destroy(buttons[7]);
//				GameObject.Destroy(buttons[8]);
//			}
//			else
//			{
//				CarWashLevelManager.Instance.RemovedToolsList +=  "10,11";
//				GameObject.Destroy(buttons[10]);
//				GameObject.Destroy(buttons[9]);
//			}
		}



		//UKLANJANJE NEPOTREBNIH SPRAJTOVA

		if(CarWashLevelManager.Instance.RemovedToolsList.Contains( "01")) //lisce
			newVehicle.transform.Find("Leaves").gameObject.SetActive(false);
		
		if(CarWashLevelManager.Instance.RemovedToolsList.Contains( "02")) //grancice
			newVehicle.transform.Find("Branches").gameObject.SetActive(false);
		
		if(CarWashLevelManager.Instance.RemovedToolsList.Contains( "03")) //prljavstina
			newVehicle.transform.Find("Mask/Dirt").gameObject.SetActive(false);
		
		if(CarWashLevelManager.Instance.RemovedToolsList.Contains( "04")) //stains
			newVehicle.transform.Find("Mask/Stains").gameObject.SetActive(false);
		
//		if(CarWashLevelManager.Instance.RemovedToolsList.Contains( "05")) //prljavi tockovi
//		{
//			if(GameObject.Find("Mask/Wheel/WheelDirt01")!=null) GameObject.Find("Mask/Wheel/WheelDirt01").gameObject.SetActive(false);
//			if(GameObject.Find("Mask/Wheel/WheelDirt02")!=null)  GameObject.Find("Mask/Wheel/WheelDirt02").gameObject.SetActive(false);
//			if(GameObject.Find("Mask/Wheel/WheelDirt03")!=null)  GameObject.Find("Mask/Wheel/WheelDirt03").gameObject.SetActive(false);
//		}

		if(CarWashLevelManager.Instance.RemovedToolsList.Contains( "05")) //sapunica
			newVehicle.transform.Find("Bubbles").gameObject.SetActive(false);

		if(CarWashLevelManager.Instance.RemovedToolsList.Contains( "07")) //vosak i sjaj
		{
			newVehicle.transform.Find("Mask/Wax").gameObject.SetActive(false);
			newVehicle.transform.Find("Sparkles").gameObject.SetActive(false);
		}
//
//		if(CarWashLevelManager.Instance.RemovedToolsList.Contains( "10")) //pumpanje guma
//		{
//			if(GameObject.Find("Wheel1/AnimationHolder/FlatTire")!=null) 
//			{
//				GameObject.Find("Wheel1/AnimationHolder/FlatTire").gameObject.SetActive(false);
//				GameObject.Find("Wheel1/AnimationHolder/Mask/Wheel").GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
//			}
//			if(GameObject.Find("Wheel2/AnimationHolder/FlatTire")!=null)
//			{
//				GameObject.Find("Wheel2/AnimationHolder/FlatTire").gameObject.SetActive(false);
//				GameObject.Find("Wheel2/AnimationHolder/Mask/Wheel").GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
//			}
//			if(GameObject.Find("Wheel3/AnimationHolder/FlatTire")!=null) 
//			{
//				GameObject.Find("Wheel3/AnimationHolder/FlatTire").gameObject.SetActive(false);
//				GameObject.Find("Wheel3/AnimationHolder/Mask/Wheel").GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
//			}

//
//		}
//
// 		if(CarWashLevelManager.Instance.RemovedToolsList.Contains( "09")) //gorivo
//			newVehicle.transform.Find("Mask/FuelTank").gameObject.SetActive(false);
	}







	IEnumerator Start()
	{
		SetGameTools();
 
		GameObject.Find ("psExhaustSmoke").GetComponent<ParticleSystem>().enableEmission = false;
//		if(ItemsSlider.ActiveItemNo == 22) GameObject.Find ("psExhaustSmoke2").GetComponent<ParticleSystem>().enableEmission = false;

		yield return new WaitForSeconds(.15f);
		CarWashLevelManager.Instance.StartAndStopLeaves();
	}


	void Update ()
	{
		if(bMoveDown) //SPUSTANJE PLATFORME 
		{

			if(timeDown == 0)
			{
				//ANIMACIJA ZA SCENU
				bShowHide = true;
				TutorialBusCleaning.bPause = true;
			}
			timeDown+=Time.deltaTime ;

			if(timeDown   < MoveDownTime)
			{
				NextLevelPlatform.position = Vector3.Lerp(platformStartPos, platformDownPos, timeDown *.33f );
				//newVehicle.transform.parent.position =  Vector3.Lerp(carPos, carDownPos, timeDown *.33f );

				if( bShowHide && timeDown  > (MoveDownTime - 1f))
				{
					 
//					LevelTransition.Instance.HideAndShowScene( );
					bShowHide = false;
				}
			}
			 
			else if(   timeDown  > (MoveDownTime ) && timeDown  < MoveDownTime*2+1.1f)
			{
				if(!GarargeBG.enabled )
				{
					GarargeBG.enabled = true;
					GarargeDoor.enabled = true;
					GarargeBGTop.enabled = true;
					GarargeBarMask.enabled = true;

					SalonBG.enabled = false;
					SalonFG.enabled = false;


				}

				NextLevelPlatform.position = Vector3.Lerp(platformUpPos, platformStartPos, (timeDown - MoveDownTime-1) *.33f);
				//newVehicle.transform.parent.position =  Vector3.Lerp(carUpPos, carPos, (timeDown - MoveDownTime-1) *.33f);
			 
			//	 if(	newVehicle.transform.parent.position.y > 7.5f) Gameplay.Instance.StartAndStopLeaves();

				if(NextLevelPlatform.position.y*1000 <800)  
				{
					GarargeBarMask.enabled = false;
					NextLevelPlatform.Find("bar").GetComponent<Image>().enabled = false;
				}
			}
			else if(timeDown > MoveDownTime*2+1.1f ) 
			{ bMoveDown = false;TutorialBusCleaning.bPause = false;}


		}
//		else if(bTestDrive) //TEST VOZNJA 
//		{
//			Tutorial.bPause = true;
//			if(!bSpeedUpTime)
//			{
//
//				if(timeTestDrive >=0.5f && timeTestDrive < 1f &&  !bShowHide)
//				{
//					if(SelectedVehicle == 21 ||  SelectedVehicle == 20 ) newVehicleAnim.SetBool("bFlyOut", true);
//					else newVehicleAnim.SetBool("bDrive", true);
//					bShowHide = true;
//				}
//				else if(timeTestDrive >0.5f && timeTestDrive <3.5f && bShowHide)
//				{
//					newVehicle.transform.parent.position = Vector3.Lerp(carPos, exitGaragePos ,(timeTestDrive-0.5f)*.3f );
//				}
//				else if(timeTestDrive >3.5f  && bShowHide)
//				{
//					bShowHide = false;
////					LevelTransition.Instance.HideAndShowScene( );
//
//
//					//unistavanje sparkles
//
//					if(GameObject.Find("VehicleHolder/CAR/Sparkles")!=null) GameObject.Find("VehicleHolder/CAR/Sparkles").SetActive(false);
// 
//				}
//				else if(timeTestDrive > 4.8f )
//				{
//					CarWashSoundManager.Instance.Play_CarDrive();
//					if(SelectedVehicle == 21 ||  SelectedVehicle == 20 ) 
//					{
//						newVehicleAnim.SetBool("bFlyOut", false);
//						newVehicleAnim.SetBool("bTest", true);
//					}
//					else
//					{
//						newVehicleAnim.SetBool("bDriveOut", false);
//						newVehicleAnim.SetBool("bDrive", true);
//					}
//					newVehicle.transform.parent.localScale  = newVehicle.transform.parent.localScale *  0.8f;
//					newVehicle.transform.parent.position = drivePos;
//					TestDriveBG.gameObject.SetActive(true);
//
//					GameObject.Destroy( SalonBG.gameObject.gameObject);
//					GameObject.Destroy(SalonFG.gameObject);
//					
//	                GameObject.Destroy (GarargeBG.gameObject);
//					GameObject.Destroy(GarargeBGTop.gameObject);
//					GameObject.Destroy(GarargeBarMask.gameObject);
//					GameObject.Destroy(GarargeDoor.gameObject);
//					bSpeedUpTime = true;
//					IndicatorArrow.localRotation =      Quaternion.Euler( new Vector3(0,0, 60 ) ) ;
//
//					// Gameplay.Instance.StartAndStopLeaves();
//				}
//
//				timeTestDrive += Time.unscaledDeltaTime;
//			}
//			else
//			{
////				if(FakeLoading.bPauseGame ) return;
//
//				timeTestDrive += Time.unscaledDeltaTime;
//				//if(timeTestDrive > 4.5f && Time.timeScale<3 && !bSlowDown) Time.timeScale  = Time.timeScale*1.01f;
//
//				//else 
//				if(timeTestDrive >drivingTime && !bSlowDown) bSlowDown = true;
//				else if(bSlowDown) 
//				{
//					Time.timeScale = Time.timeScale*.99f;
//
//					if(Time.timeScale<=1)
//					{
//						CancelInvoke();
//						CarWashSoundManager.Instance.CarDrive.pitch = 1;
//
//						Time.timeScale = 1;
//						bMoveDown = false;
//						bTestDrive = false;
//						CarWashSoundManager.Instance.Stop_CarDrive();
////						LevelTransition.Instance.HideScene("SelectCar");
//					}
//					IndicatorArrow.localRotation =  Quaternion.Lerp(  IndicatorArrow.localRotation,       Quaternion.Euler( new Vector3(0,0, 130- 70*Time.timeScale)) , Time.deltaTime*5) ;
//				}
//			}
//
//
//		}
 
		else  if(timeA <4) //ULAZAK NA SCENU
		{
			if(timeA >=0 && timeA<=0.8f )
			{
				newVehicleAnim.SetBool("bDrive", true);
			}

			timeA += Time.deltaTime;
		 
			newVehicle.transform.parent.GetComponent<RectTransform>().anchoredPosition3D = Vector3.Lerp(startPos,carPos,timeA*.3f );
			if(timeA>3.1f && SelectedVehicle !=20 && SelectedVehicle !=21) newVehicleAnim.SetBool("bDrive", false);
			else if(timeA>3.9f && ( SelectedVehicle ==20 || SelectedVehicle ==21)) newVehicleAnim.SetBool("bDrive", false);
		}

		
	}

	public void btnGasPedalPressed()
	{
		CancelInvoke();
		InvokeRepeating("Accelerate",0,0.1f);
	}

	public void btnGasPedalReleased()
	{
		CancelInvoke();
		InvokeRepeating("Decelerate",0,0.1f);
	}

	float drivingTime =  30;

	void Accelerate()
	{
		if(bSlowDown) return;
		 
		Time.timeScale  = Time.timeScale*1.015f;
	 
		if(Time.timeScale>3)
		{
			Time.timeScale = 3;

		}
		//IndicatorArrow.localRotation = Quaternion.Euler( new Vector3(0,0, 45- 40*Time.timeScale));3
		IndicatorArrow.localRotation =  Quaternion.Lerp(  IndicatorArrow.localRotation,       Quaternion.Euler( new Vector3(0,0, 130- 70*Time.timeScale)) , Time.deltaTime*5) ;
	}

	void Decelerate()
	{

//		Debug.Log(Time.timeScale);
		Time.timeScale  = Time.timeScale*.99f;
		if(Time.timeScale<1)
		{
			Time.timeScale = 1;

		}
		IndicatorArrow.localRotation =  Quaternion.Lerp(  IndicatorArrow.localRotation,       Quaternion.Euler( new Vector3(0,0, 130- 70*Time.timeScale) ), Time.deltaTime*5);
	}
}
