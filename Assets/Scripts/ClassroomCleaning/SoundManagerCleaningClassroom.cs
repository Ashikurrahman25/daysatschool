using UnityEngine;
using System.Collections;

/**
  * Scene:All
  * Object:SoundManager
  * Description: Skripta zaduzena za zvuke u apliakciji, njihovo pustanje, gasenje itd...
  **/
public class SoundManagerCleaningClassroom : MonoBehaviour {

	public static int musicOn = 1;
	public static int soundOn = 1;
	public static bool forceTurnOff = false;

	public AudioSource CleaningFinished;

	public AudioSource Trash;
	public AudioSource ShowMenu;
	public AudioSource MenuHide;
	public AudioSource ElementCompleted;
	public AudioSource Sponge;
	public AudioSource RollerBrush;
	public AudioSource Duster;
	public AudioSource Mop;
	public AudioSource FixFlor;
	public AudioSource Draw;
	public AudioSource Eraser;
	public AudioSource GlassSpray;


	public AudioSource Coins;
	public AudioSource Star;

	static SoundManagerCleaningClassroom instance;

	public static SoundManagerCleaningClassroom Instance
	{
		get
		{
			if(instance == null)
			{
				instance = GameObject.FindObjectOfType(typeof(SoundManagerCleaningClassroom)) as SoundManagerCleaningClassroom;
			}

			return instance;
		}
	}

	void Start () 
	{
		//DontDestroyOnLoad(this.gameObject);

		if(PlayerPrefs.HasKey("SoundOn"))
		{
			soundOn = PlayerPrefs.GetInt("SoundOn",1);
			if(SoundManagerCleaningClassroom.soundOn == 2) MuteAllSounds();
			else UnmuteAllSounds();
		}
		else
		{
			SetSound(true);
		}

		musicOn = PlayerPrefs.GetInt("MusicOn",1);

		Screen.sleepTimeout = SleepTimeout.NeverSleep; 
	}

	public void SetSound(bool bEnabled)
	{
		if(bEnabled)
		{
			PlayerPrefs.SetInt("SoundOn", 1);
			UnmuteAllSounds();
		}
		else
		{
			PlayerPrefs.SetInt("SoundOn", 0);
			MuteAllSounds();
		}

		soundOn = PlayerPrefs.GetInt("SoundOn");
	}

	public void Play_TaskCompleted()
	{
		if(ElementCompleted.clip != null&& soundOn == 1)
			ElementCompleted.Play();
	}

 


	IEnumerator PlayClip(AudioSource Clip, float time)
	{
		yield return new WaitForSeconds(time);
		Clip.Play();
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

	public void	Play_Sound(AudioSource sound)
	{
        if(sound!=null&&!sound.isPlaying  && soundOn == 1) 
			sound.Play();
	}

	public void	StopAndPlay_Sound(AudioSource sound)
	{
        if(sound!=null&&sound.isPlaying)
			sound.Stop();

        if(sound!=null && soundOn == 1) 
			sound.Play();
	}
	
	public void	Stop_Sound(AudioSource sound)
	{
		
        if(sound!=null&&sound.isPlaying)
			sound.Stop();
	}
	
	 
	
}
