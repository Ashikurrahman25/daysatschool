using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuMoreOptions : MonoBehaviour {

	public bool bRightMenu = false;
	public Animator AnimRightMenu;
	public Image SoundOn;
	public Image SoundOff;


	public Image OpenMenuIcon;
	public Image CloseMenuIcon;

	void Start () {
		StartCoroutine("SetMenuButtons");
	}

	IEnumerator SetMenuButtons()
	{
		yield return new WaitForSeconds(.5f);
//		AnimRightMenu.SetBool("LargeMenu",(Gameplay.Instance.GamePhase ==3));
	}
	
	public void btnRightMenuClicked()
	{
		AnimRightMenu.SetBool("LargeMenu",(Gameplay.Instance.GamePhase ==3));
		bRightMenu = !bRightMenu;
//		if(bRightMenu) AnimRightMenu.SetTrigger("tOpen");
//		else  AnimRightMenu.SetTrigger("tClose");
		AnimRightMenu.SetBool("bOpen", bRightMenu);

		BlockClicks.Instance.SetBlockAll(true);
		BlockClicks.Instance.SetBlockAllDelay(.5f,false);
	}

	public void CloseRightMenu()
	{
		if(bRightMenu)
		{
			AnimRightMenu.SetBool("LargeMenu",(Gameplay.Instance.GamePhase ==3));
			bRightMenu = false;
			OpenMenuIcon.enabled = true;
			CloseMenuIcon.enabled = false;
			AnimRightMenu.SetBool("bOpen", false);
			AnimRightMenu.Play("Default");
			BlockClicks.Instance.SetBlockAll(true);
			BlockClicks.Instance.SetBlockAllDelay(.5f,false);
		}
	}


	public void btnHomeClicked()
	{
		MenuDecorations.LevelDecorations[(Gameplay.roomNo-1)] = MenuDecorations.LevelDecorationsStart;

		StopAllCoroutines();
		ItemsSlider.ActiveItemNo = Gameplay.roomNo;
//		LevelTransition.Instance.HideSceneAndLoadNext("HomeScene");
		BlockClicks.Instance.SetBlockAll(true);
		BlockClicks.Instance.SetBlockAllDelay(1f,false);

		GameData.IncrementButtonHomeClickedCount();

	}


	public void btnSoundClicked()
	{
		if(SoundManagerCleaningClassroom.soundOn == 1)
		{
			//SoundOn.enabled = true;
			SoundOff.enabled = false;
			SoundManagerCleaningClassroom.soundOn = 0;
			SoundManagerCleaningClassroom.Instance.MuteAllSounds();

		}
		else
		{
			//SoundOn.enabled = false;
			SoundOff.enabled = true;
			SoundManagerCleaningClassroom.soundOn = 1;
			SoundManagerCleaningClassroom.Instance.UnmuteAllSounds();

		}

		//SoundManager.Instance.Play_ButtonClick();
		if(SoundManagerCleaningClassroom.musicOn == 1)
		{
			SoundManagerCleaningClassroom.musicOn = 0;
			//GameObject.Find("MusicOnOff").GetComponent<Image>().enabled = true;
		}
		else
		{
			SoundManagerCleaningClassroom.musicOn = 1;
			//GameObject.Find("MusicOnOff").GetComponent<Image>().enabled = false;
		}

		PlayerPrefs.SetInt("SoundOn",SoundManagerCleaningClassroom.soundOn);
		PlayerPrefs.SetInt("MusicOn",SoundManagerCleaningClassroom.musicOn);
		PlayerPrefs.Save();


		BlockClicks.Instance.SetBlockAll(true);
		BlockClicks.Instance.SetBlockAllDelay(.3f,false);

	}


	public void OpenMoreOptionsMenuAnimationFinished()
	{
		OpenMenuIcon.enabled = false;
		CloseMenuIcon.enabled = true;
 
		SoundManagerCleaningClassroom.Instance.StopAndPlay_Sound(SoundManagerCleaningClassroom.Instance.ShowMenu);
	}

	public void CloseMoreOptionsMenuAnimationFinished()
	{
		OpenMenuIcon.enabled = true;
		CloseMenuIcon.enabled = false;

		SoundManagerCleaningClassroom.Instance.StopAndPlay_Sound(SoundManagerCleaningClassroom.Instance.MenuHide);
	}

	public void btnFinishDecorating()
	{
		Camera.main.SendMessage("FinishLevel");
	}

}
