using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

///<summary>
///<para>Scene:All/NameOfScene/NameOfScene1,NameOfScene2,NameOfScene3...</para>
///<para>Object:N/A</para>
///<para>Description: Sample Description </para>
///</summary>

public class AnimEventsPotionGame : MonoBehaviour {

	public double thermometerValue = 35.5;
	public Text thermometerText;

	public void LoadScene()
	{
		Application.LoadLevel("Loading");
	}

	public void PlayButtonClickSound(bool playButtonSound)
	{
        //PAVLE ButtonCLick SM
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
        SoundManagerPotionMaker.PlaySound(soundName);
    }

	public void SetOrderInLayer()
	{
		transform.parent.GetComponent<Canvas>().overrideSorting = true;
		transform.parent.GetComponent<Canvas>().sortingOrder = 2;
	}
}
