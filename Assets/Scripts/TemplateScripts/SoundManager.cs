using UnityEngine;
using System.Collections;
using System;

/**
  * Scene:All
  * Object:SoundManager
  * Description: Skripta zaduzena za zvuke u apliakciji, njihovo pustanje, gasenje itd...
  **/
public class SoundManager : MonoBehaviour {

	public static int musicOn = 1;
	public static int soundOn = 1;
	public AudioSource buttonClick;
	public AudioSource menuMusic;
	public AudioSource gameplayMusic;
	



	public static SoundManager instance;

    private void Awake()
    {
		if (instance == null) instance = this;
		else Destroy(gameObject);
    }

    void Start () 
	{
		DontDestroyOnLoad(this.gameObject);

		if(PlayerPrefs.HasKey("SoundOn"))
		{
			musicOn = PlayerPrefs.GetInt("MusicOn");
			soundOn = PlayerPrefs.GetInt("SoundOn");
		}

        if (musicOn == 2)
            gameplayMusic.Stop();

        Screen.sleepTimeout = SleepTimeout.NeverSleep; 
	}

	public void Play_ButtonClick()
	{
		if(buttonClick!=null&&buttonClick.clip != null && soundOn != 2)
			buttonClick.Play();
	}

	public void Play_MenuMusic()
	{
		if(menuMusic.clip != null && musicOn != 2)
			menuMusic.Play();
	}

	public void Stop_MenuMusic()
	{
		if(menuMusic.clip != null && musicOn != 2)
			menuMusic.Stop();
	}

	public void Play_GameplayMusic()
	{
		if(gameplayMusic.clip != null && musicOn != 2)
		{
			gameplayMusic.Play();
		}
	}

	public void Stop_GameplayMusic()
	{
		if(gameplayMusic.clip != null && musicOn != 2)
		{
            gameplayMusic.Stop();
		}
	}

	/// <summary>
	/// Corutine-a koja za odredjeni AudioSource, kroz prosledjeno vreme, utisava AudioSource do 0, gasi taj AudioSource, a zatim vraca pocetni Volume na pocetan kako bi AudioSource mogao opet da se koristi
	/// </summary>
	/// <param name="sound">AudioSource koji treba smanjiti/param>
	/// <param name="time">Vreme za koje treba smanjiti Volume/param>
	IEnumerator FadeOut(AudioSource sound, float time)
	{
		float originalVolume = sound.volume;
		while(sound.volume != 0)
		{
			sound.volume = Mathf.MoveTowards(sound.volume, 0, time);
			yield return null;
		}
		sound.Stop();
		sound.volume = originalVolume;
	}

	/// <summary>
	/// F-ja koja Mute-uje sve zvuke koja su deca SoundManager-a
	/// </summary>
	public void MuteAllSounds()
	{
		foreach (Transform t in transform)
		{
			t.GetComponent<AudioSource>().mute = true;
		}
	}

	/// <summary>
	/// F-ja koja Unmute-uje sve zvuke koja su deca SoundManager-a
	/// </summary>
	public void UnmuteAllSounds()
	{
		foreach (Transform t in transform)
		{
			t.GetComponent<AudioSource>().mute = false;
		}
	}

    public bool ToggleSound()
    {
        bool returnvalue;
        if(PlayerPrefs.GetInt("SoundOn")!=2)
        {
            PlayerPrefs.SetInt("SoundOn", 2);
            PlayerPrefs.SetInt("MusicOn", 2);
            MuteAllSounds();
            Stop_GameplayMusic();
            soundOn = 2;
            musicOn = 2;
           
            returnvalue = false;
        }
        else
        {
            soundOn = 1;
            musicOn = 1;
            PlayerPrefs.SetInt("SoundOn", 1);
            PlayerPrefs.SetInt("MusicOn", 1);
            UnmuteAllSounds();
            Play_GameplayMusic();
            returnvalue = true;
        }
        PlayerPrefs.Save();
        return returnvalue;
    }
}
