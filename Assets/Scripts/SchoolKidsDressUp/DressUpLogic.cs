
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Security.Cryptography;
using System.Text;
using UnityEngine.SceneManagement;

///<summary>
///<para>Scene:All/NameOfScene/NameOfScene1,NameOfScene2,NameOfScene3...</para>
///<para>Object:N/A</para>
///<para>Description: Sample Description </para>
///</summary>

public class DressUpLogic : MonoBehaviour {

	// TEMP
	Color deselectColor = new Color(1,1,1,0.5f);
	int nmbclick = 0;                   

	// TEMP
   

	int currentCategory = 0; // hair -1; dress -2, Shirt-3,Pants-4,HeadWear-5,Bag-6,Shoes-7
	[System.Serializable]
	public struct CharacterStruct
	{
		public Image dress;
		public Image pants;
		public Image shirt;
		public Image PamPamLeft;
		public Image PamPamRight;
		public Image frontHair;
		public Image backHair;
		public Image sockLeft;
		public Image sockRight;
		public Image shoeLeft;
		public Image shoeRight;
	}



    public DressUpDataBoy boyData;
    public DressUpDataGirl girlData;

//	[HideInInspector]
	public Animator categoryDressAnimator, categoryPantsAnimator, categoryShirtAnimator, categoryHeadWearAnimator, categoryBagAnimator, categoryHairAnimator, categoryShoesAnimator;
//	[HideInInspector]
    public GameObject categoryDressButton, categoryPantsButton, categoryShirtButton, categoryHeadWearButton, categoryBagButton, categoryHairButton, categoryShoesButton;
	public CharacterStruct Character;




    private GameObject canvas;
	// Use this for initialization
	void Start () 
    {
        if (boyData != null)
        {
            boyData = (DressUpDataBoy)Instantiate(boyData);
            boyData.Initialize();
        }
        if (girlData)
        {
            girlData = (DressUpDataGirl)Instantiate(girlData);
            girlData.Initialize();
        }
        
        canvas = GameObject.Find("Canvas");
        HideAllCategories();
        if (SceneManager.GetActiveScene().name == "DressUpGirl")
        {
            ShowDresses();
            LoadData();
        }
        else
        {
            LoadData();
            ShowShirts();
        }
        LoadChoices();

	}

    public void LoadData()
    {
        if(SceneManager.GetActiveScene().name=="DressUpGirl")
        {
            int i = 0;
            foreach(Transform t in categoryDressAnimator.transform.Find("Image/ScrollView/CategoryHolder"))
            {
                t.GetComponent<Image>().sprite = girlData.dresses[i].icon;
                if (girlData.dresses[i].locked)
                    t.Find("Lock").gameObject.SetActive(true);
                int index=i;
                GameObject lockHolder = t.gameObject;
                t.GetComponent<Button>().onClick.AddListener(delegate
                    {
                        if(girlData.dresses[index].locked)
                        {
                            canvas.GetComponent<MenuManager>().ShowPopUpMenu(this.canvas.transform.Find("PopUps/PopUpDialogWatchVideo").gameObject);
                            canvas.transform.Find("PopUps/PopUpDialogWatchVideo/AnimationHolder/Body/ButtonsHolder/ButtonYes").GetComponent<Button>().onClick.RemoveAllListeners();
                            canvas.transform.Find("PopUps/PopUpDialogWatchVideo/AnimationHolder/Body/ButtonsHolder/ButtonYes").GetComponent<Button>().onClick.AddListener(delegate {
                                if (AdsManager.videoReady)
                                {
                                    AdsManager.videoRewarded.RemoveAllListeners();
                                    AdsManager.videoRewarded.AddListener(delegate {
                                        string ppstring = PlayerPrefs.GetString("GirlDresses");
                                        if (!ppstring.Contains(index.ToString() + ","))
                                        {
                                            ppstring += index.ToString() + ",";
                                        }
                                        PlayerPrefs.SetString("GirlDresses", ppstring);
                                        PlayerPrefs.Save();
                                        girlData.dresses[index].locked = false;
                                        lockHolder.transform.GetChild(0).gameObject.SetActive(false);
                                    });
                                    AdsManager.Instance.ShowVideoReward();
                                }
                                else
                                {
                                    canvas.GetComponent<MenuManager>().ShowPopUpMenu(canvas.transform.Find("PopUps/PopUpMessage").gameObject);
                                }
                            });
                        }
                        else
                        {
                            ChangeDress(girlData.dresses[index].sprite);
                            string ppToSet="Girl";
                            if(DressUpManager.firstCharacterFinished)
                                ppToSet+="2Dress";
                            else
                                ppToSet+="1Dress";
                            PlayerPrefs.SetInt(ppToSet,index);
                            PlayerPrefs.Save();
                        }
                    });
                i++;
            }

            i = 0;
            foreach(Transform t in categoryHairAnimator.transform.Find("Image/ScrollView/CategoryHolder"))
            {
                t.GetComponent<Image>().sprite = girlData.hats[i].icon;
                if (girlData.hats[i].locked)
                    t.Find("Lock").gameObject.SetActive(true);
                int index = i;
                GameObject lockHolder = t.gameObject;
                t.GetComponent<Button>().onClick.AddListener(delegate
                    {
                        if(girlData.hats[index].locked)
                        {
                            canvas.GetComponent<MenuManager>().ShowPopUpMenu(this.canvas.transform.Find("PopUps/PopUpDialogWatchVideo").gameObject);
                            canvas.transform.Find("PopUps/PopUpDialogWatchVideo/AnimationHolder/Body/ButtonsHolder/ButtonYes").GetComponent<Button>().onClick.RemoveAllListeners();
                            canvas.transform.Find("PopUps/PopUpDialogWatchVideo/AnimationHolder/Body/ButtonsHolder/ButtonYes").GetComponent<Button>().onClick.AddListener(delegate {
                                if(AdsManager.videoReady)
                                {
                                    AdsManager.videoRewarded.RemoveAllListeners();
                                    AdsManager.videoRewarded.AddListener(delegate {
                                        string ppstring=PlayerPrefs.GetString("GirlHatsUnlocked");
                                        if(!ppstring.Contains(index.ToString()+","))
                                        {
                                            ppstring+=index.ToString()+",";
                                        }
                                        PlayerPrefs.SetString("GirlHatsUnlocked",ppstring);
                                        PlayerPrefs.Save();
                                        girlData.hats[index].locked=false;
                                        lockHolder.transform.GetChild(0).gameObject.SetActive(false);
                                    });
                                    AdsManager.Instance.ShowVideoReward();
                                }
                                else
                                {
                                    canvas.GetComponent<MenuManager>().ShowPopUpMenu(canvas.transform.Find("PopUps/PopUpMessage").gameObject);
                                }
                            });
                        }
                        else
                        {
                            ChangeFrontHair(girlData.hats[index].sprite);
                            string ppToSet="Girl";
                            if(DressUpManager.firstCharacterFinished)
                                ppToSet+="2Hat";
                            else
                                ppToSet+="1Hat";
                            PlayerPrefs.SetInt(ppToSet,index);
                            PlayerPrefs.Save();
                        }
                    });
                i++;
            }

            i = 0;
            foreach(Transform t in categoryBagAnimator.transform.Find("Image/ScrollView/CategoryHolder"))
            {
                t.GetComponent<Image>().sprite = girlData.bracelets[i].icon;
                if (girlData.bracelets[i].locked)
                    t.Find("Lock").gameObject.SetActive(true);
                int index = i;
                GameObject lockHolder = t.gameObject;

                t.GetComponent<Button>().onClick.AddListener(delegate
                    {
                        if(girlData.bracelets[index].locked)
                        {
                            canvas.GetComponent<MenuManager>().ShowPopUpMenu(this.canvas.transform.Find("PopUps/PopUpDialogWatchVideo").gameObject);
                            canvas.transform.Find("PopUps/PopUpDialogWatchVideo/AnimationHolder/Body/ButtonsHolder/ButtonYes").GetComponent<Button>().onClick.RemoveAllListeners();
                            canvas.transform.Find("PopUps/PopUpDialogWatchVideo/AnimationHolder/Body/ButtonsHolder/ButtonYes").GetComponent<Button>().onClick.AddListener(delegate {
                                if(AdsManager.videoReady)
                                {
                                    AdsManager.videoRewarded.RemoveAllListeners();
                                    AdsManager.videoRewarded.AddListener(delegate {
                                        string ppstring=PlayerPrefs.GetString("GirlBraceletsUnlocked");
                                        if(!ppstring.Contains(index.ToString()+","))
                                        {
                                            ppstring+=index.ToString()+",";
                                        }
                                        PlayerPrefs.SetString("GirlBraceletsUnlocked",ppstring);
                                        PlayerPrefs.Save();
                                        girlData.bracelets[index].locked=false;
                                        lockHolder.transform.GetChild(0).gameObject.SetActive(false);
                                    });
                                    AdsManager.Instance.ShowVideoReward();
                                }
                                else
                                {
                                    canvas.GetComponent<MenuManager>().ShowPopUpMenu(canvas.transform.Find("PopUps/PopUpMessage").gameObject);
                                }
                            });
                        }
                        else
                        {
                            ChangePamPamRight(girlData.bracelets[index].sprite);
                            string ppToSet="Girl";
                            if(DressUpManager.firstCharacterFinished)
                                ppToSet+="2Bracelet";
                            else
                                ppToSet+="1Bracelet";
                            PlayerPrefs.SetInt(ppToSet,index);
                            PlayerPrefs.Save();
                        }
                    });
                i++;
            }

            i = 0;
            foreach(Transform t in categoryShoesAnimator.transform.Find("Image/ScrollView/CategoryHolder"))
            {
                t.GetComponent<Image>().sprite = girlData.shoes[i].icon;
                if (girlData.shoes[i].locked)
                    t.Find("Lock").gameObject.SetActive(true);
                int index = i;
                GameObject lockHolder = t.gameObject;
                t.GetComponent<Button>().onClick.AddListener(delegate
                    {
                        if(girlData.shoes[index].locked)
                        {
                            canvas.GetComponent<MenuManager>().ShowPopUpMenu(this.canvas.transform.Find("PopUps/PopUpDialogWatchVideo").gameObject);
                            canvas.transform.Find("PopUps/PopUpDialogWatchVideo/AnimationHolder/Body/ButtonsHolder/ButtonYes").GetComponent<Button>().onClick.RemoveAllListeners();
                            canvas.transform.Find("PopUps/PopUpDialogWatchVideo/AnimationHolder/Body/ButtonsHolder/ButtonYes").GetComponent<Button>().onClick.AddListener(delegate {
                                if(AdsManager.videoReady)
                                {
                                    AdsManager.videoRewarded.RemoveAllListeners();
                                    AdsManager.videoRewarded.AddListener(delegate {
                                        string ppstring=PlayerPrefs.GetString("GirlShoesUnlocked");
                                        if(!ppstring.Contains(index.ToString()+","))
                                        {
                                            ppstring+=index.ToString()+",";
                                        }
                                        PlayerPrefs.SetString("GirlShoesUnlocked",ppstring);
                                        PlayerPrefs.Save();
                                        girlData.shoes[index].locked=false;
                                        lockHolder.transform.GetChild(0).gameObject.SetActive(false);
                                    });
                                    AdsManager.Instance.ShowVideoReward();
                                }
                                else
                                {
                                    canvas.GetComponent<MenuManager>().ShowPopUpMenu(canvas.transform.Find("PopUps/PopUpMessage").gameObject);
                                }
                            });
                            Debug.Log("Item zakljucan cipele devojcica");
                        }
                        else
                        {
                            ChangeRightShoe(girlData.shoes[index].sprite);
                            string ppToSet="Girl";
                            if(DressUpManager.firstCharacterFinished)
                                ppToSet+="2Shoes";
                            else
                                ppToSet+="1Shoes";
                            PlayerPrefs.SetInt(ppToSet,index);
                            PlayerPrefs.Save();
                        }
                    });
                i++;
            }
        }
        else //decak je
        {
            int i = 0;
            foreach(Transform t in categoryPantsAnimator.transform.Find("Image/ScrollView/CategoryHolder"))
            {
                t.GetComponent<Image>().sprite = boyData.pants[i].icon;
                if (boyData.pants[i].locked)
                    t.Find("Lock").gameObject.SetActive(true);
                int index=i;
                GameObject lockHolder = t.gameObject;
                t.GetComponent<Button>().onClick.AddListener(delegate
                    {
                        if(boyData.pants[index].locked)
                        {
                           
                            canvas.GetComponent<MenuManager>().ShowPopUpMenu(this.canvas.transform.Find("PopUps/PopUpDialogWatchVideo").gameObject);
                            canvas.transform.Find("PopUps/PopUpDialogWatchVideo/AnimationHolder/Body/ButtonsHolder/ButtonYes").GetComponent<Button>().onClick.RemoveAllListeners();
                            canvas.transform.Find("PopUps/PopUpDialogWatchVideo/AnimationHolder/Body/ButtonsHolder/ButtonYes").GetComponent<Button>().onClick.AddListener(delegate {
                                if(AdsManager.videoReady)
                                {
                                    AdsManager.videoRewarded.RemoveAllListeners();
                                    AdsManager.videoRewarded.AddListener(delegate {
                                        string ppstring=PlayerPrefs.GetString("BoyPantsUnlocked");
                                        if(!ppstring.Contains(index.ToString()+","))
                                        {
                                            ppstring+=index.ToString()+",";
                                        }
                                        PlayerPrefs.SetString("BoyPantsUnlocked",ppstring);
                                        PlayerPrefs.Save();
                                        boyData.pants[index].locked=false;
                                        lockHolder.transform.GetChild(0).gameObject.SetActive(false);
                                    });
                                    AdsManager.Instance.ShowVideoReward();
                                }
                                else
                                {
                                    canvas.GetComponent<MenuManager>().ShowPopUpMenu(canvas.transform.Find("PopUps/PopUpMessage").gameObject);
                                }
                            });
                        }
                        else
                        {
                            ChangePants(boyData.pants[index].sprite);
                            string ppToSet="Boy";
                            if(DressUpManager.firstCharacterFinished)
                                ppToSet+="2Pants";
                            else
                                ppToSet+="1Pants";
                            PlayerPrefs.SetInt(ppToSet,index);
                            PlayerPrefs.Save();
                        }
                    });
                i++;
            }

            i = 0;
            foreach(Transform t in categoryHairAnimator.transform.Find("Image/ScrollView/CategoryHolder"))
            {
                t.GetComponent<Image>().sprite = boyData.hats[i].icon;
                if (boyData.hats[i].locked)
                    t.Find("Lock").gameObject.SetActive(true);
                GameObject lockHolder = t.gameObject;
                int index = i;
                t.GetComponent<Button>().onClick.AddListener(delegate
                    {
                        if(boyData.hats[index].locked)
                        {
                            canvas.GetComponent<MenuManager>().ShowPopUpMenu(this.canvas.transform.Find("PopUps/PopUpDialogWatchVideo").gameObject);
                            canvas.transform.Find("PopUps/PopUpDialogWatchVideo/AnimationHolder/Body/ButtonsHolder/ButtonYes").GetComponent<Button>().onClick.RemoveAllListeners();
                            canvas.transform.Find("PopUps/PopUpDialogWatchVideo/AnimationHolder/Body/ButtonsHolder/ButtonYes").GetComponent<Button>().onClick.AddListener(delegate {
                                if(AdsManager.videoReady)
                                {
                                    AdsManager.videoRewarded.RemoveAllListeners();
                                    AdsManager.videoRewarded.AddListener(delegate {
                                        string ppstring=PlayerPrefs.GetString("BoyHatsUnlocked");
                                        if(!ppstring.Contains(index.ToString()+","))
                                        {
                                            ppstring+=index.ToString()+",";
                                        }
                                        PlayerPrefs.SetString("BoyHatsUnlocked",ppstring);
                                        PlayerPrefs.Save();
                                        boyData.hats[index].locked=false;
                                        lockHolder.transform.GetChild(0).gameObject.SetActive(false);
                                    });
                                    AdsManager.Instance.ShowVideoReward();
                                }
                                else
                                {
                                    canvas.GetComponent<MenuManager>().ShowPopUpMenu(canvas.transform.Find("PopUps/PopUpMessage").gameObject);
                                }
                            });
                        }
                        else
                        {
                            ChangeFrontHair(boyData.hats[index].sprite);
                            string ppToSet="Boy";
                            if(DressUpManager.firstCharacterFinished)
                                ppToSet+="2Hat";
                            else
                                ppToSet+="1Hat";
                            PlayerPrefs.SetInt(ppToSet,index);
                            PlayerPrefs.Save();
                        }
                    });
                i++;
            }

            i = 0;
            foreach(Transform t in categoryShirtAnimator.transform.Find("Image/ScrollView/CategoryHolder"))
            {
                t.GetComponent<Image>().sprite = boyData.hoodies[i].icon;
                if (boyData.hoodies[i].locked)
                    t.Find("Lock").gameObject.SetActive(true);
                int index = i;
                GameObject lockHolder = t.gameObject;
                t.GetComponent<Button>().onClick.AddListener(delegate
                    {
                        if(boyData.hoodies[index].locked)
                        {
                            canvas.GetComponent<MenuManager>().ShowPopUpMenu(this.canvas.transform.Find("PopUps/PopUpDialogWatchVideo").gameObject);
                            canvas.transform.Find("PopUps/PopUpDialogWatchVideo/AnimationHolder/Body/ButtonsHolder/ButtonYes").GetComponent<Button>().onClick.RemoveAllListeners();
                            canvas.transform.Find("PopUps/PopUpDialogWatchVideo/AnimationHolder/Body/ButtonsHolder/ButtonYes").GetComponent<Button>().onClick.AddListener(delegate {
                                if(AdsManager.videoReady)
                                {
                                    AdsManager.videoRewarded.RemoveAllListeners();
                                    AdsManager.videoRewarded.AddListener(delegate {
                                        string ppstring=PlayerPrefs.GetString("BoyShirtsUnlocked");
                                        if(!ppstring.Contains(index.ToString()+","))
                                        {
                                            ppstring+=index.ToString()+",";
                                        }
                                        PlayerPrefs.SetString("BoyShirtsUnlocked",ppstring);
                                        PlayerPrefs.Save();
                                        boyData.hoodies[index].locked=false;
                                        lockHolder.transform.GetChild(0).gameObject.SetActive(false);
                                    });
                                    AdsManager.Instance.ShowVideoReward();
                                }
                                else
                                {
                                    canvas.GetComponent<MenuManager>().ShowPopUpMenu(canvas.transform.Find("PopUps/PopUpMessage").gameObject);
                                }
                            });
                        }
                        else
                        {
                            ChangeShirt(boyData.hoodies[index].sprite);
                            string ppToSet="Boy";
                            if(DressUpManager.firstCharacterFinished)
                                ppToSet+="2Hoodie";
                            else
                                ppToSet+="1Hoodie";
                            PlayerPrefs.SetInt(ppToSet,index);
                            PlayerPrefs.Save();
                        }
                    });
                i++;
            }

            i = 0;
            foreach(Transform t in categoryShoesAnimator.transform.Find("Image/ScrollView/CategoryHolder"))
            {
                t.GetComponent<Image>().sprite = boyData.shoes[i].icon;
                if (boyData.shoes[i].locked)
                    t.Find("Lock").gameObject.SetActive(true);
                int index = i;
                GameObject lockHolder = t.gameObject;
                t.GetComponent<Button>().onClick.AddListener(delegate
                    {
                        if(boyData.shoes[index].locked)
                        {
                            canvas.GetComponent<MenuManager>().ShowPopUpMenu(this.canvas.transform.Find("PopUps/PopUpDialogWatchVideo").gameObject);
                            canvas.transform.Find("PopUps/PopUpDialogWatchVideo/AnimationHolder/Body/ButtonsHolder/ButtonYes").GetComponent<Button>().onClick.RemoveAllListeners();
                            canvas.transform.Find("PopUps/PopUpDialogWatchVideo/AnimationHolder/Body/ButtonsHolder/ButtonYes").GetComponent<Button>().onClick.AddListener(delegate {
                                if(AdsManager.videoReady)
                                {
                                    AdsManager.videoRewarded.RemoveAllListeners();
                                    AdsManager.videoRewarded.AddListener(delegate {
                                        string ppstring=PlayerPrefs.GetString("BoyShoesUnlocked");
                                        if(!ppstring.Contains(index.ToString()+","))
                                        {
                                            ppstring+=index.ToString()+",";
                                        }
                                        PlayerPrefs.SetString("BoyShoesUnlocked",ppstring);
                                        PlayerPrefs.Save();
                                        boyData.shoes[index].locked=false;
                                        lockHolder.transform.GetChild(0).gameObject.SetActive(false);
                                    });
                                    AdsManager.Instance.ShowVideoReward();
                                }
                                else
                                {
                                    canvas.GetComponent<MenuManager>().ShowPopUpMenu(canvas.transform.Find("PopUps/PopUpMessage").gameObject);
                                }
                            });
                            Debug.Log("Item zakljucan cipele devojcica");
                        }
                        else
                        {
                            ChangeRightShoe(boyData.shoes[index].sprite);
                            string ppToSet="Boy";
                            if(DressUpManager.firstCharacterFinished)
                                ppToSet+="2Shoes";
                            else
                                ppToSet+="1Shoes";
                            PlayerPrefs.SetInt(ppToSet,index);
                            PlayerPrefs.Save();
                        }
                    });
                i++;
            }
        }
    }

    public void LoadChoices()
    {
        if(SceneManager.GetActiveScene().name=="DressUpGirl")
        {
            if(DressUpManager.firstCharacterFinished)
            {
                Character.dress.sprite = girlData.dresses[PlayerPrefs.GetInt("Girl2Dress")].sprite;
                Character.PamPamRight.sprite = girlData.bracelets[PlayerPrefs.GetInt("Girl2Bracelet")].sprite;
                Character.frontHair.sprite = girlData.hats[PlayerPrefs.GetInt("Girl2Hat")].sprite;
                Character.shoeRight.sprite = girlData.shoes[PlayerPrefs.GetInt("Girl2Shoes")].sprite;
            }
            else
            {
                Character.dress.sprite = girlData.dresses[PlayerPrefs.GetInt("Girl1Dress")].sprite;
                Character.PamPamRight.sprite = girlData.bracelets[PlayerPrefs.GetInt("Girl1Bracelet")].sprite;
                Character.frontHair.sprite = girlData.hats[PlayerPrefs.GetInt("Girl1Hat")].sprite;
                Character.shoeRight.sprite = girlData.shoes[PlayerPrefs.GetInt("Girl1Shoes")].sprite;
            }
        }
        else
        {
            if(DressUpManager.firstCharacterFinished)
            {
                Character.pants.sprite = boyData.pants[PlayerPrefs.GetInt("Boy2Pants")].sprite;
                Character.shirt.sprite = boyData.hoodies[PlayerPrefs.GetInt("Boy2Hoodie")].sprite;
                Character.shoeRight.sprite = boyData.shoes[PlayerPrefs.GetInt("Boy2Shoes")].sprite;
                Character.frontHair.sprite = boyData.hats[PlayerPrefs.GetInt("Boy2Hat")].sprite;
            }
            else
            {
                Character.pants.sprite = boyData.pants[PlayerPrefs.GetInt("Boy1Pants")].sprite;
                Character.shirt.sprite = boyData.hoodies[PlayerPrefs.GetInt("Boy1Hoodie")].sprite;
                Character.shoeRight.sprite = boyData.shoes[PlayerPrefs.GetInt("Boy1Shoes")].sprite;
                Character.frontHair.sprite = boyData.hats[PlayerPrefs.GetInt("Boy1Hat")].sprite;
            }
        }
    }

	public void ChangeDress(Sprite newDress)
	{
		Character.dress.sprite = newDress;

		Character.dress.gameObject.GetComponent<Animator>().Play("ChangeItem");
		if(!Character.dress.enabled)
		{
			Character.pants.enabled = false;
			Character.shirt.enabled = false;
			Character.dress.enabled = true;
		}
        SoundManagerPackingAndDressUp.Instance.Play_ChangeClothes();
    }

    public void ChangePants(Sprite newPants)
    {
        Character.pants.gameObject.GetComponent<Animator>().Play("ChangeItem");
        Character.pants.sprite = newPants;
        if(!Character.pants.enabled)
        {
            Character.pants.enabled = true;
            Character.shirt.enabled = true;
            Character.dress.enabled = false;
        }
        SoundManagerPackingAndDressUp.Instance.Play_ChangeClothes();
    }

    public void ChangeShirt(Sprite newShirt)
    {
        Character.shirt.gameObject.GetComponent<Animator>().Play("ChangeItem");
        Character.shirt.sprite = newShirt;
        if(!Character.shirt.enabled)
        {
            Character.pants.enabled = true;
            Character.shirt.enabled = true;
            Character.dress.enabled = false;
        }
        SoundManagerPackingAndDressUp.Instance.Play_ChangeClothes();
    }

    public void ChangeSockLeft(Sprite newSockLeft)
    {
        Character.sockLeft.gameObject.GetComponent<Animator>().Play("ChangeItem");
        Character.sockLeft.sprite = newSockLeft;
        SoundManagerPackingAndDressUp.Instance.Play_ChangeClothes();
    }

    public void ChangeSockRight(Sprite newSockRight)
    {
        Character.sockRight.gameObject.GetComponent<Animator>().Play("ChangeItem");
        Character.sockRight.sprite = newSockRight;
        SoundManagerPackingAndDressUp.Instance.Play_ChangeClothes();
    }

    public void ChangePamPamLeft(Sprite newPamPamLeft)
    {
        Character.PamPamLeft.gameObject.GetComponent<Animator>().Play("ChangeItem");
        Character.PamPamLeft.sprite = newPamPamLeft;
        SoundManagerPackingAndDressUp.Instance.Play_ChangeClothes();
    }

    public void ChangePamPamRight(Sprite newPamPamRight)
    {
        Character.PamPamRight.gameObject.GetComponent<Animator>().Play("ChangeItem");
        Character.PamPamRight.sprite = newPamPamRight;
        SoundManagerPackingAndDressUp.Instance.Play_ChangeClothes();
    }

    public void ChangeFrontHair(Sprite newFrontHair)
    {
        Character.frontHair.gameObject.GetComponent<Animator>().Play("ChangeItem");
        Character.frontHair.sprite = newFrontHair;
        SoundManagerPackingAndDressUp.Instance.Play_ChangeClothes();
    }

    public void ChangeBackHair(Sprite newBackHair)
    {
        Character.backHair.sprite = newBackHair;
        SoundManagerPackingAndDressUp.Instance.Play_ChangeClothes();
    }

    public void ChangeLeftShoe(Sprite newShoeLeft)
    {
        Character.shoeLeft.gameObject.GetComponent<Animator>().Play("ChangeItem");
        Character.shoeLeft.sprite = newShoeLeft;
        SoundManagerPackingAndDressUp.Instance.Play_ChangeClothes();
    }

    public void ChangeRightShoe(Sprite newShoeRight)
    {
        Character.shoeRight.gameObject.GetComponent<Animator>().Play("ChangeItem");
        Character.shoeRight.sprite = newShoeRight;
        SoundManagerPackingAndDressUp.Instance.Play_ChangeClothes();
    }

	public void ShowDresses()
	{
		if(currentCategory>0 && currentCategory!=2)
		{
			HideCurrentCategory();
		}
		currentCategory = 2;
//        categoryDressButton.color = Color.white; 
        categoryDressButton.SetActive(true); 
		categoryDressAnimator.Play("Arive");
	}

	public void ShowPants()
	{
		if(currentCategory>0 && currentCategory!=4)
		{
			HideCurrentCategory();
		}
		currentCategory = 4;
//        categoryPantsButton.color = Color.white; 
        categoryPantsButton.SetActive(true);
		categoryPantsAnimator.Play("Arive");
	}

	public void ShowShirts()
	{
		if(currentCategory>0 && currentCategory!=3)
		{
			HideCurrentCategory();
		}
		currentCategory = 3;
//        categoryShirtButton.color = Color.white; 
        categoryShirtButton.SetActive(true); 
		categoryShirtAnimator.Play("Arive");
	}

	public void ShowHeadWears()
	{
		if(currentCategory>0 && currentCategory!=5)
		{
			HideCurrentCategory();
		}
		currentCategory = 5;
//        categoryHeadWearButton.color = Color.white; 
        categoryHeadWearButton.SetActive(true); 
		categoryHeadWearAnimator.Play("Arive");
	}

	public void ShowBags()
	{
		if(currentCategory>0 && currentCategory!=6)
		{
			HideCurrentCategory();
		}
		currentCategory = 6;
//        categoryBagButton.color = Color.white; 
        categoryBagButton.SetActive(true); 
		categoryBagAnimator.Play("Arive");
	}

	public void ShowHairs()
	{
		if(currentCategory>0 && currentCategory!=1)
		{
			HideCurrentCategory();
		}
		currentCategory = 1;
//        categoryHairButton.color = Color.white; 
        categoryHairButton.SetActive(true); 
		categoryHairAnimator.Play("Arive");
	}

	public void ShowShoes()
	{
		if(currentCategory>0 && currentCategory!=7)
		{
			HideCurrentCategory();
		}
		currentCategory = 7;
//        categoryShoesButton.color = Color.white; 
        categoryShoesButton.SetActive(true);
		categoryShoesAnimator.Play("Arive");
	}
	// hair -1; dress -2, Shirt-3,Pants-4,HeadWear-5,Bag-6,Shoes-7
	
    public void HideAllCategories()
    {
        if(categoryHairButton!=null)
            categoryHairButton.SetActive(false);
        if(categoryHairAnimator!=null)
            categoryHairAnimator.Play("Depart");
        if(categoryDressButton!=null)
            categoryDressButton.SetActive(false);
        if(categoryDressAnimator!=null)
            categoryDressAnimator.Play("Depart");
        if(categoryShirtButton!=null)
            categoryShirtButton.SetActive(false);
        if(categoryShirtAnimator!=null)
            categoryShirtAnimator.Play("Depart");
        if(categoryPantsButton!=null)
            categoryPantsButton.SetActive(false);
        if(categoryPantsAnimator!=null)
            categoryPantsAnimator.Play("Depart");
        if(categoryHeadWearButton!=null)
            categoryHeadWearButton.SetActive(false);
        if(categoryHeadWearAnimator!=null)
            categoryHeadWearAnimator.Play("Depart");
        if(categoryBagButton!=null)
            categoryBagButton.SetActive(false);
        if(categoryBagAnimator!=null)
            categoryBagAnimator.Play("Depart");
        if(categoryShoesButton!=null)
            categoryShoesButton.SetActive(false);
        if(categoryShoesAnimator!=null)
        categoryShoesAnimator.Play("Depart");
    }

	void HideCurrentCategory()
	{
		switch(currentCategory)
		{
		case 1:
//                categoryHairButton.color = deselectColor;
                categoryHairButton.SetActive(false);
			categoryHairAnimator.Play("Depart");
			break;
		case 2:
//                categoryDressButton.color = deselectColor;
                categoryDressButton.SetActive(false);
			categoryDressAnimator.Play("Depart");
			break;
		case 3:
//                categoryShirtButton.color = deselectColor;
                categoryShirtButton.SetActive(false);
			categoryShirtAnimator.Play("Depart");
			break;
		case 4:
//                categoryPantsButton.color = deselectColor;
                categoryPantsButton.SetActive(false);
			categoryPantsAnimator.Play("Depart");
			break;
		case 5:
//                categoryHeadWearButton.color = deselectColor;
                categoryHeadWearButton.SetActive(false);
			categoryHeadWearAnimator.Play("Depart");
			break;
		case 6:
//                categoryBagButton.color = deselectColor;
                categoryBagButton.SetActive(false);
			categoryBagAnimator.Play("Depart");
			break;
		case 7:
//                categoryShoesButton.color = deselectColor;
                categoryShoesButton.SetActive(false);
			categoryShoesAnimator.Play("Depart");
			break;
		}
	}

}
