using UnityEngine;
using System.Collections;

/**
  * Scene:All
  * Object:SoundManager
  * Description: Skripta zaduzena za zvuke u apliakciji, njihovo pustanje, gasenje itd...
  **/
public class PaintingSoundManager : MonoBehaviour {

	public static int musicOn = 1;
	public static int soundOn = 1;
	public static bool forceTurnOff = false;

	public AudioSource buttonClick;
//	public AudioSource menuMusic;
//	public AudioSource gameplayMusic;
	public AudioSource takePhoto;
	



	static PaintingSoundManager instance;

	public static PaintingSoundManager Instance
	{
		get
		{
			if(instance == null)
			{
				instance = GameObject.FindObjectOfType(typeof(PaintingSoundManager)) as PaintingSoundManager;
			}

			return instance;
		}
	}

	void Start () 
	{
//		DontDestroyOnLoad(this.gameObject);

		if(PlayerPrefs.HasKey("SoundOn"))
		{
			musicOn = PlayerPrefs.GetInt("MusicOn");
			soundOn = PlayerPrefs.GetInt("SoundOn");
		}

		Screen.sleepTimeout = SleepTimeout.NeverSleep; 
	}

	public void Play_ButtonClick()
	{
		if(buttonClick.clip != null && soundOn == 1)
			buttonClick.Play();
	}

//	public void Play_MenuMusic()
//	{
//		if(menuMusic.clip != null && musicOn == 1)
//			menuMusic.Play();
//	}
//
//	public void Stop_MenuMusic()
//	{
//		if(menuMusic.clip != null && musicOn == 1)
//			menuMusic.Stop();
//	}
//
//	public void Play_GameplayMusic()
//	{
//		if(gameplayMusic.clip != null && musicOn == 1)
//		{
//			gameplayMusic.Play();
//		}
//	}
//
//	public void Stop_GameplayMusic()
//	{
//		if(gameplayMusic.clip != null && musicOn == 1)
//		{
//			gameplayMusic.Stop();
//		}
//	}

	public void Play_TakePhotoSound()
	{
		if (takePhoto.clip != null && soundOn == 1)
			takePhoto.Play ();

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
	
}
