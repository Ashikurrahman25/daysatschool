using UnityEngine;
using System.Collections;

public class AnimEventsFoodMaker : MonoBehaviour {

	bool soundPlayed;
	bool visible = false;



	void TurnOffGameLoadingHolder()
	{
		transform.parent.gameObject.SetActive(false);
	}

	void DisablePopup()
	{
		gameObject.SetActive(false);
	}


	

//	void PlayYawning()
//	{
//		SoundManager.Instance.Play_PetYawning();
//	}
//
//	void PlaySnoring()
//	{
//		if(!SoundManager.Instance.petSnoring.isPlaying)
//			SoundManager.Instance.Play_PetSnoring();
//	}
//
//	void PlayLickScreen()
//	{
//		SoundManager.Instance.Play_LickScreen();
//		Invoke("WaterDrops",0.1f);
//	}
	
	void WaterDrops()
	{
		Camera.main.GetComponent<Animator>().Play("TestWaterCamera",0,0);
	}

//	void PlayGlassShatter()
//	{
//		SoundManager.Instance.Play_GlassShatter();
//		VariablesManager.Instance.GlassBreak();
//	}

	void EnableBall()
	{
		transform.Find("Ball_LP").gameObject.SetActive(true);
	}

	void DisableBall()
	{
		transform.Find("Ball_LP").gameObject.SetActive(false);
		Invoke("DisableGlass",2f);
	}

//	void DisableGlass()
//	{
//		VariablesManager.Instance.TurnOffGlass();
//	}

	void PlayMixer()
	{
        SoundManagerPotionMaker.PlaySound("Mixer");
    }

	void FoodExitsMixer()
	{
        SoundManagerPotionMaker.PlaySound("FoodOutOfMixer");

    }

    //	void Bite()
    //	{
    //		SoundManager.Instance.Play_Bite();
    //	}
    //
    //	void PlayLiftingWeights()
    //	{
    //		SoundManager.Instance.Play_LiftingWeights();
    //	}

    //	void PlayTickle()
    //	{
    //		if(Gestures.Instance.allowPlayTickleSound)
    //			SoundManager.Instance.Play_PetTickle();
    //	}

    //	void PlayMessageShow()
    //	{
    //		SoundManager.Instance.Play_HungryMessageShow();
    //	}

    //	void PlayLegHop()
    //	{
    //		SoundManager.Instance.Play_LegHop();
    //	}

    //	void PlayPrepareForJump()
    //	{
    //		SoundManagerMiniGames.Instance.Play_PrepareForJump();
    //	}

    //	void PlayBlades()
    //	{
    //		if(!SoundManagerMiniGames.Instance.blades.isPlaying && !soundPlayed && visible)
    //		{
    //			SoundManagerMiniGames.Instance.Play_Blades();
    //			soundPlayed = true;
    //		}
    //	}

    //	void PlayRockFallDown()
    //	{
    //		if(visible)
    //			SoundManagerMiniGames.Instance.Play_RockFallDown();
    //	}

    //	void PlayOpenWoodenDoor()
    //	{
    //		if(visible)
    //			SoundManagerMiniGames.Instance.Play_WoodenDoorOpen();
    //	}

   
//	{
//		if(!VariablesManager.Instance.parrotNoSounds)
//			SoundManager.Instance.Play_ParrotIdle();
//	}
//
//	void ParrotIdleGame()
//	{
//		if(!VariablesManager.Instance.parrotNoSounds && VariablesManager.Instance.parrotSoundInGame)
//			SoundManager.Instance.Play_ParrotIdle();
//	}
//
//	void ParrotClick()
//	{
//		if(!VariablesManager.Instance.parrotNoSounds)
//			SoundManager.Instance.Play_ParrotClick();
//	}

//	void JumpingAnimal_Splash()
//	{
//		Transform animal = GameObject.Find("PetHolder").transform.Find("AnimtionHolder/PetSideBody");
//
//		if(Mathf.Abs(animal.position.x - transform.parent.position.x) <= 0.5f && transform.parent.GetComponent<JumpingAnimalPlatform>().activateEvent)
//		{
//			Camera.main.GetComponent<JumpThePenguinScript>().PetSplash();
//			transform.parent.gameObject.SetActive(false);
//		}
//	}

	void PlayLogoSound()
	{
		GetComponent<AudioSource>().Play();
	}

	void EnableGestureBlocker()
	{
		Camera.main.transform.Find("GestureBlocker").gameObject.SetActive(true);
	}

	void DisableGestureBlocker()
	{
		Camera.main.transform.Find("GestureBlocker").gameObject.SetActive(false);
	}
}
