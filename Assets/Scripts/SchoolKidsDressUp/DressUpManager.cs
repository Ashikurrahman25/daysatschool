using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DressUpManager : MonoBehaviour {

    public static bool firstCharacterFinished = false;


    Transform canvas;
    public void Start()
    {
        firstCharacterFinished = false;
        canvas = GameObject.Find("Canvas").transform;
        AdsManager.Instance.IsVideoRewardAvailable();
     
    }

    public void DressUpFinished()
    {

        if(!firstCharacterFinished)
        {
            firstCharacterFinished = true;
            canvas.GetComponent<MenuManager>().PlayTransitionNoLoadScene();
            Timer.Schedule(this, 1.5f,delegate {
                if(SceneManager.GetActiveScene().name=="DressUpBoy")
                {
                    canvas.transform.Find("Gameplay/AnimationHolder/CharacterBoy").GetComponent<DressUpLogic>().HideAllCategories();
                    canvas.transform.Find("Gameplay/AnimationHolder/CharacterBoy").gameObject.SetActive(false);
                    canvas.transform.Find("Gameplay/AnimationHolder/CharacterBoy").GetComponent<DressUpLogic>().ShowShirts();
                    canvas.transform.Find("Gameplay/AnimationHolder/CharacterBoy2").gameObject.SetActive(true);
                    Timer.Schedule(this, 1f,delegate {
                        canvas.transform.Find("Gameplay/AnimationHolder/CharacterBoy2").GetComponent<DressUpLogic>().ShowShirts();
                    });
                    canvas.transform.Find("Gameplay/AnimationHolder/CharacterBoy2").GetComponent<DressUpLogic>().LoadChoices();
                }
                else
                {
                    canvas.transform.Find("Gameplay/AnimationHolder/CharacterGirl").GetComponent<DressUpLogic>().HideAllCategories();

                    canvas.transform.Find("Gameplay/AnimationHolder/CharacterGirl").gameObject.SetActive(false);
                    canvas.transform.Find("Gameplay/AnimationHolder/CharacterGirl").GetComponent<DressUpLogic>().ShowDresses();

                    canvas.transform.Find("Gameplay/AnimationHolder/CharacterGirl2").gameObject.SetActive(true);
                    Timer.Schedule(this, 1f,delegate {
                        canvas.transform.Find("Gameplay/AnimationHolder/CharacterGirl2").GetComponent<DressUpLogic>().ShowDresses();
                    });
                    canvas.transform.Find("Gameplay/AnimationHolder/CharacterGirl2").GetComponent<DressUpLogic>().LoadChoices();
                }
            });
        }
        else
        {
            if (SceneManager.GetActiveScene().name == "DressUpBoy")
            {
                //                firstCharacterFinished = false;
                canvas.GetComponent<MenuManager>().LoadSceneWithTransition("DressUpGirl");
            }
            else
            {
                MenuManager.justStarted = true;
                canvas.GetComponent<MenuManager>().LoadSceneWithNextInterstitial("MapScene");
            }
        }
    }
}
