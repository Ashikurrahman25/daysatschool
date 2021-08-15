using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerPackingAndDressUp : MonoBehaviour {

    public static int musicOn = 1;
    public static int soundOn = 1;
    public static bool forceTurnOff = false;

    // FX
    public AudioSource levelFinished;
    public AudioSource badgeEarned;
    public AudioSource changeClothes;
    public AudioSource itemInPlace;

    static SoundManagerPackingAndDressUp instance;

    public static SoundManagerPackingAndDressUp Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(SoundManagerPackingAndDressUp)) as SoundManagerPackingAndDressUp;
            }

            return instance;
        }
    }

    void Start()
    {
        //if(this.gameObject != null)
        //    DontDestroyOnLoad(this.gameObject);

        if (PlayerPrefs.HasKey("SoundOn"))
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


    public void Play_LevelFinished()
    {
        if(levelFinished.clip != null && soundOn !=2)
            levelFinished.Play();
    }

    public void Play_BadgeEarned()
    {
        if(badgeEarned.clip != null && soundOn != 2)
            badgeEarned.Play();
    }

    public void Play_ChangeClothes()
    {
        if(changeClothes.clip != null && soundOn != 2)
            changeClothes.Play();
    }

    public void Play_ItemInPlace()
    {
        if(itemInPlace.clip != null && soundOn != 2)
            itemInPlace.Play();
    }

}
