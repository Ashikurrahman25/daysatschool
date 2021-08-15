using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

///<summary>
///<para>Scene:All/NameOfScene/NameOfScene1,NameOfScene2,NameOfScene3...</para>
///<para>Object:N/A</para>
///<para>Description: Sample Description </para>
///</summary>

public class AnimEventsEyeExamination : MonoBehaviour {

	public double thermometerValue = 35.5;
	public Text thermometerText;
	public Image objectImage;
	public Sprite transparentSprite;

	public void LoadScene()
	{
		Application.LoadLevel("Loading");
	}

	public void PlayButtonClickSound(bool playButtonSound)
	{
        //PAVLE SoundManager BCLICK
//		if (playButtonSound)
//			SoundManager.PlaySound("ButtonClick");
	}

	public void AddValueToThemometer()
	{
		thermometerValue += 0.2;

		thermometerText.text = Math.Round((thermometerValue + 0.2), 1).ToString();
	}

	public void PlaySound(string soundName)
	{
        SoundManagerEyeExamination.PlaySound(soundName);
    }

	public void SetOrderInLayer()
	{
		transform.parent.GetComponent<Canvas>().overrideSorting = true;
		transform.parent.GetComponent<Canvas>().sortingOrder = 2;
	}

	public void OpenPopup(int orderInDisabledObjects)
	{
		LevelManager.levelManager.menuManager.ShowPopUpMenu(LevelManager.levelManager.menuManager.disabledObjects[orderInDisabledObjects]);
	}

    public void ClosePopup(int orderInDisabledObjects)
    {
        LevelManager.levelManager.menuManager.ClosePopUpMenu(LevelManager.levelManager.menuManager.disabledObjects[orderInDisabledObjects]);
    }

	public void SetImageToBeTransparent()
	{
		objectImage.sprite = transparentSprite;
	}

	public void PlayBurgijaSound()
	{
        SoundManagerEyeExamination.PlaySound("BlanketSound");
    }

	public void StopBurgijaSound()
	{
        SoundManagerEyeExamination.StopSound("BlanketSound");
    }

	public void PlayVadjenjeZuba()
	{
        SoundManagerEyeExamination.PlaySound("ThermometerSound");
    }

    public void PlayBadgeSound()
    {
        SoundManagerEyeExamination.PlaySound("BadgeEarned");
    }
}
