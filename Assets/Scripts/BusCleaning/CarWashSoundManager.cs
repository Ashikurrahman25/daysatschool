using UnityEngine;
using System.Collections;

public class CarWashSoundManager : MonoBehaviour {

	public static int musicOn = 1;
	public static int soundOn = 1;
	public static bool forceTurnOff = false;


	public AudioSource Correct;
	public AudioSource Error;
	public AudioSource Error2;

	public AudioSource Fuel;
	public AudioSource TirePump;
	public AudioSource Sponge;
	public AudioSource WaterHose;
	public AudioSource Compressor;
	public AudioSource Shears;
	public AudioSource Leaves;
	public AudioSource Air;
	public AudioSource RotatingBrush;

	public AudioSource Star;

	static CarWashSoundManager instance;

	public static CarWashSoundManager Instance
	{
		get
		{
			if(instance == null)
			{
				instance = FindObjectOfType(typeof(CarWashSoundManager)) as CarWashSoundManager;
			}

			return instance;
		}
	}

	void Start () 
	{
		if(PlayerPrefs.HasKey("SoundOn"))
		{
			soundOn = PlayerPrefs.GetInt("SoundOn",1);
			if(CarWashSoundManager.soundOn == 2) MuteAllSounds();
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
 
	public void Play_Error()
	{
		if(Error.clip != null && soundOn != 2)
			Error.Play();
	}

	public void Play_TaskCompleted()
	{
		if(Correct.clip != null&& soundOn != 2)
			Correct.Play();
	}

	IEnumerator PlayClip(AudioSource Clip, float time)
	{
		yield return new WaitForSeconds(time);
		Clip.Play();
	}

	/// <summary>
	/// Corutine-a koja za odredjeni AudioSource, kroz prosledjeno vreme, utisava AudioSource do 0, gasi taj AudioSource, a zatim vraca pocetni Volume na pocetan kako bi AudioSource mogao opet da se koristi
	/// </summary>
	/// <param name="sound">AudioSource koji treba smanjiti/param>
	/// <param name="time">Vreme za koje treba smanjiti Volume/param>
	IEnumerator FadeOut(AudioSource sound, float time)
	{
		float originalVolume = sound.volume;

		while(sound.volume > 0.05f)
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

	public void	Play_Sound(AudioSource sound)
	{
        if(sound !=null&&!sound.isPlaying  && soundOn == 1) 
			sound.Play();
	}
	
	public void	Stop_Sound(AudioSource sound)
	{
        if(sound!=null&&sound.isPlaying)
			sound.Stop();
	}
}
