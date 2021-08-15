using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/**
  * Scene:All
  * Object:SoundManagerEyeExamination
  * Description: Skripta zaduzena za zvuke u apliakciji, njihovo pustanje, gasenje itd...
  **/
public class SoundManagerEyeExamination : MonoBehaviour {

	public static int musicOn = 1;
	public static int soundOn = 1;

	// FX
    public AudioSource smallParticle;
    public AudioSource bigParticle;
    public AudioSource miniGameWrong;
    public AudioSource miniGameCorrect;
    public AudioSource badgeEarned;
    [Header ("Item sounds")]
    public AudioSource calibrateItemSound;



//	public Sprite musicOffImageHolderMainScene;
//	public Sprite musicOnImageHolderMainScene;

	static SoundManagerEyeExamination instance;

	public static SoundManagerEyeExamination Instance
	{
		get
		{
			if(instance == null)
			{
				instance = GameObject.FindObjectOfType(typeof(SoundManagerEyeExamination)) as SoundManagerEyeExamination;
			}

			return instance;
		}
	}



	void Start () 
	{
		DontDestroyOnLoad(this.gameObject);

		if(PlayerPrefs.HasKey("SoundOn"))
		{
			musicOn = PlayerPrefs.GetInt("MusicOn");
			soundOn = PlayerPrefs.GetInt("SoundOn");
		}
		else
		{
			PlayerPrefs.SetInt("SoundOn", 1);
			PlayerPrefs.SetInt("MusicOn", 1);
			musicOn = 1;
			soundOn = 1;
		}

		Screen.sleepTimeout = SleepTimeout.NeverSleep; 
	}

	public static void ToggleSound()
	{
		if (soundOn == 0)
		{
			soundOn = 1;
			PlayerPrefs.SetInt("SoundOn", 1);

			// Unmute all sounds just in case
			foreach (Transform t in Instance.transform)
			{
				t.GetComponent<AudioSource>().mute = false;
			}

			// Play menu music
			PlaySound("Music");

			//GameObject.Find("Canvas").GetComponent<MenuManager>().soundButton.transform.GetChild(1).GetComponent<Image>().enabled = false;
//			GameObject.Find("Canvas").GetComponent<MenuManager>().soundButton.GetComponent<Image>().sprite = SoundManagerEyeExamination.Instance.musicOnImageHolder;
		}
		else if (soundOn == 1)
		{
			soundOn = 0;
			PlayerPrefs.SetInt("SoundOn", 0);

			// Mute all sounds just in case
			foreach (Transform t in Instance.transform)
			{
				t.GetComponent<AudioSource>().mute = true;
			}

			// GameObject.Find("Canvas").GetComponent<MenuManager>().soundButton.transform.GetChild(1).GetComponent<Image>().enabled = true;
//			GameObject.Find("Canvas").GetComponent<MenuManager>().soundButton.GetComponent<Image>().sprite = SoundManagerEyeExamination.Instance.musicOffImageHolder;
		}
	}

	public static void PlaySound(string soundName)
	{
		if (soundOn != 2)
		{
			switch(soundName)
			{
               
              
                case "SmallParticle":
                    Instance.smallParticle.Play();
                    break;
                case "BigParticle":
                    Instance.bigParticle.Play();
                    break;
                case "MiniGameWrong":
                    Instance.miniGameWrong.Play();
                    break;
                case "MiniGameCorrect":
                    Instance.miniGameCorrect.Play();
                    break;
               
                case "BadgeEarned":
                    Instance.badgeEarned.Play();
                    break;
               
                case "CalibrateItemSound":
                    Instance.calibrateItemSound.Play();
                    break;
              
                default:
                    Debug.Log("Ne postoji zvuk: " + soundName);
                    break;
            }
        }
    }

    public static void StopSound(string soundName)
    {
		if (soundOn != 2)
		{
            switch(soundName)
            {
               
                case "SmallParticle":
                    Instance.smallParticle.Stop();
                    break;
                case "BigParticle":
                    Instance.bigParticle.Stop();
                    break;
                case "MiniGameWrong":
                    Instance.miniGameWrong.Stop();
                    break;
                case "MiniGameCorrect":
                    Instance.miniGameCorrect.Stop();
                    break;
               
                case "BadgeEarned":
                    Instance.badgeEarned.Stop();
                    break;
               
                case "CalibrateItemSound":
                    Instance.calibrateItemSound.Stop();
                    break;
               
                default:
                    Debug.Log("Ne postoji zvuk: " + soundName);
                    break;
            }
        }
    }

    public static bool IsSoundPlaying(string soundName)
	{
		if (soundOn != 2)
		{
            switch(soundName)
            {
  
                case "SmallParticle":
                    if(Instance.smallParticle.isPlaying)
                        return true;
                    break;
                case "BigParticle":
                    if(Instance.bigParticle.isPlaying)
                        return true;
                    break;
                case "MiniGameWrong":
                    if(Instance.miniGameWrong.isPlaying)
                        return true;
                    break;
                case "MiniGameCorrect":
                    if(Instance.miniGameCorrect.isPlaying)
                        return true;
                    break;
                case "BadgeEarned":
                    if(Instance.badgeEarned.isPlaying)
                        return true;
                    break;
                case "CalibrateItemSound":
                    if(Instance.calibrateItemSound.isPlaying)
                        return true;
                    break;
                default:
                    Debug.Log("Ne postoji zvuk: " + soundName);
                    break;
            }
		}

		return false;
	}

	/// <summary>
	/// Corutine-a koja za odredjeni AudioSource, kroz prosledjeno vreme, utisava AudioSource do 0, gasi taj AudioSource, a zatim vraca pocetni Volume na pocetan kako bi AudioSource mogao opet da se koristi
	/// </summary>
	/// <param name="sound">AudioSource koji treba smanjiti/param>
	/// <param name="time">Vreme za koje treba smanjiti Volume/param>
	IEnumerator FadeOut(AudioSource sound, float time)
	{
		float originalVolume = 1f;

		while(sound.volume != 0)
		{
			sound.volume = Mathf.MoveTowards(sound.volume, 0, time);
			yield return null;
		}

		sound.Stop();
		sound.volume = originalVolume;
	}
	
}
