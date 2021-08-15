using UnityEngine;
using System.Collections;
using System.Collections.Generic;

 
public class SoundManagerConnectLines : MonoBehaviour {

	public static int musicOn = 1;
	public static int soundOn = 1;

	//CONNECT LINES
	public AudioSource flowBreakLine;
	public AudioSource flowCompleteLine;

 
	static SoundManagerConnectLines instance;

	public static SoundManagerConnectLines Instance
	{
		get
		{
			if(instance == null)
			{
				instance = GameObject.FindObjectOfType(typeof(SoundManagerConnectLines)) as SoundManagerConnectLines;
			}

			return instance;
		}
	}

	void Start () 
	{

		DontDestroyOnLoad(this.gameObject);

		if(PlayerPrefs.HasKey("SoundOn"))
		{
			soundOn = PlayerPrefs.GetInt("SoundOn",1);
			if(SoundManagerConnectLines.soundOn == 2) MuteAllSounds();
			else UnmuteAllSounds();
		}
		else
		{
			SetSound(true);
		}

		musicOn = PlayerPrefs.GetInt("MusicOn",1);


	


//		if(musicOn == 1) Play_Music();
//		else  Stop_Music();
//		 

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

	
 
	 


 



	IEnumerator PlayClip(AudioSource Clip, float time)
	{
		yield return new WaitForSeconds(time);
		Clip.Play();
	}

 
	
	/// <summary>
	/// F-ja koja Mute-uje sve zvuke koja su deca SoundManagerConnectLines-a
	/// </summary>
	public void MuteAllSounds()
	{
		foreach (Transform t in transform)
		{
			t.GetComponent<AudioSource>().mute = true;
		}
	}

	/// <summary>
	/// F-ja koja Unmute-uje sve zvuke koja su deca SoundManagerConnectLines-a
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
		if(!sound.isPlaying  && soundOn != 2) 
			sound.Play();
	}
	
	public void	Stop_Sound(AudioSource sound)
	{
		
		if(sound.isPlaying)
			sound.Stop();
	}

	public void	StopAndPlay_Sound(AudioSource sound)
	{
		if(sound.isPlaying) sound.Stop();
		if(soundOn != 2)  	sound.Play();
	}


	
	 
	public IEnumerator FadeOutAndIn(AudioSource sound, float time)
	{
		float originalVolume = sound.volume;
 
		float timePom = 0;
		while(timePom<0.3f)
		{
			timePom+=Time.deltaTime;
			sound.volume =  sound.volume* (0.3f -  timePom)/.3f;
			yield return new WaitForEndOfFrame();
		}

		timePom = 0;
		yield return new WaitForSeconds(time-.6f);
		while(timePom<0.3f)
		{
			timePom+=Time.deltaTime;
			sound.volume =  sound.volume* ( timePom)/.3f;
			yield return new WaitForEndOfFrame();
		}
		sound.volume = originalVolume;
	}

	public void FadeOutAndStop(AudioSource sound, float time)
	{
		StartCoroutine( CFadeOutAndStop(sound,time));
	}

	public IEnumerator CFadeOutAndStop(AudioSource sound, float time)
	{
		if(time <=0 ) sound.Stop();
		else // if(sound.isPlaying)
		{
			
			float originalVolume = sound.volume;

			float timePom = 0;
			while(timePom<time)
			{
				timePom+=Time.deltaTime;
				sound.volume =  sound.volume* (time -  timePom)/time;
				yield return new WaitForEndOfFrame();
			}
			Debug.Log("STOP");
			sound.Stop();
			yield return new WaitForEndOfFrame();
			sound.volume = originalVolume;

		}
		sound.Stop();
		yield return new WaitForEndOfFrame();
	}



	

	


	public void	StopAndPlay_Sound(AudioSource sound, float time)
	{
		if(  soundOn == 1) StartCoroutine(StopAndPlay_SoundDly(sound,time));
	}

	IEnumerator StopAndPlay_SoundDly(AudioSource sound, float time)
	{
		yield return new WaitForSeconds(time);
		if(sound.isPlaying)
			sound.Stop();
		yield return null; 
		if( soundOn == 1) 
			sound.Play();
	}

	public void	 Play_Sound(AudioSource sound, float time)
	{
		if(  soundOn == 1) StartCoroutine(Play_SoundDly(sound,time));
	}

	IEnumerator Play_SoundDly(AudioSource sound, float time)
	{
		yield return new WaitForSeconds(time);
		if(!sound.isPlaying  && soundOn == 1) 
			sound.Play();
	}

 
}
