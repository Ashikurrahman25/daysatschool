using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MenuDecorations : MonoBehaviour {

//	Sprite[] ItemiSprites ;
//	List<Transform> itemButtons = new List<Transform>();
//	MenuItems mi = new MenuItems();

//
    public DecorationData data;

	bool bLeftMenuDecorations = false;
	public Animator AnimLeftMenuDecorations;
	public RectTransform scrollContent;
	public ScrollRect scrollRect;
	public ScrollRect scrollRectMainMenu;
	public Transform btnPref;
	Transform HidenButtonsHolder;
	int activeMenu = -1;
//
	string scrollMenu = "";
//
	public string listaGlavnihMenija = "";
//
	public static string[] LevelDecorations = new string [] { "", "", "", "", "", "" } ;
	public static string LevelDecorationsStart = "";
//
	float speedMainMenu;
	float speedSubMenu;
//
	ScrollRect scrollContentMainMenu;
    Coroutine changeWallC;
//	
    private GameObject canvas;

    void Awake()
    {
        data = (DecorationData)Instantiate(data);
        data.Initialize();

        LoadChosenDecorations();
    }
//
//	// Use this for initialization
	void Start () {
        canvas = GameObject.Find("Canvas");
//		LevelDecorationsStart = LevelDecorations[(Gameplay.roomNo-1)];
//
//		ItemiSprites =(Sprite[]) Resources.LoadAll<Sprite>( "MenuItems/Ikone");
//
//
//		HidenButtonsHolder = AnimLeftMenuDecorations.transform.Find("HideButtons");
//		//LISTA MENIJA PO NIVOIMA - sluzi da bi se proverilo koja je dekoracija upotrebljena
//		if( Gameplay.roomNo == 1) { listaGlavnihMenija = "01;02;03;04;05;06;"; SetSubMenuItems(1);}
//		else if(Gameplay.roomNo == 2) { listaGlavnihMenija = "07;08;09;10;11;"; SetSubMenuItems(7);}
//		else if(Gameplay.roomNo == 3) { listaGlavnihMenija = "12;13;06;14;29;30;"; SetSubMenuItems(12);}
//		else if(Gameplay.roomNo == 4) { listaGlavnihMenija = "01;16;17;18;19;31;"; SetSubMenuItems(01);}
//		else if(Gameplay.roomNo == 5) { listaGlavnihMenija = "20;03;21;22;23;"; SetSubMenuItems(20);}
//		else if(Gameplay.roomNo == 6) { listaGlavnihMenija = "25;27;28;32;33"; SetSubMenuItems(25);}
//
//		UklanjanjePostavljenihItema();
//		if(GameData.MiniGamePlayed[(Gameplay.roomNo-1)]  && listaGlavnihMenija == "" )
//		{
//			Gameplay.Instance.ShowChildGoToNextLevel();
//		}
////		Debug.Log(listaGlavnihMenija + " ---  :" + LevelDecorationsStart);
//
//
		scrollContentMainMenu = transform.Find("AnimationHolder/BG/ScrollButtons").GetComponent<ScrollRect>();
//
//		gameObject.SetActive(false);
//	}
//	
// 
//	void UklanjanjePostavljenihItema()
//	{
//		if( LevelDecorations[(Gameplay.roomNo-1)]  != "")
//		{
//			string[] decorations = LevelDecorations[(Gameplay.roomNo-1)].Split( new char[] {';'},System.StringSplitOptions.RemoveEmptyEntries);
//
//			foreach(string item in decorations)
//			{
//				 
//				listaGlavnihMenija = listaGlavnihMenija.Replace(item.Substring(1,2) + ";", "");
//			}
//		}
	}
//
//
//
//	//********************************************************************

	public void ShowDecorationsMenu()
	{
		bLeftMenuDecorations =true;
		AnimLeftMenuDecorations.SetBool("Show", bLeftMenuDecorations);
        OpenWallsCategory();
		SoundManagerCleaningClassroom.Instance.StopAndPlay_Sound(SoundManagerCleaningClassroom.Instance.ShowMenu);
	 
		BlockClicks.Instance.SetBlockAll(true);
		BlockClicks.Instance.SetBlockAllDelay(.5f,false);
	}
//
	public void HideDecorationsMenuAndTestEnd()
	{
		bLeftMenuDecorations =false;
		AnimLeftMenuDecorations.SetBool("Show", bLeftMenuDecorations);

		SoundManagerCleaningClassroom.Instance.StopAndPlay_Sound(SoundManagerCleaningClassroom.Instance.MenuHide);

		BlockClicks.Instance.SetBlockAll(true);
		BlockClicks.Instance.SetBlockAllDelay(.5f,false);

		if(GameData.MiniGamePlayed[(Gameplay.roomNo-1)]  && listaGlavnihMenija == "" )
		{
			Gameplay.Instance.ShowChildGoToNextLevel();
		}
	}
//
	public void ShowHideDecorationsMenu()
	{
		bLeftMenuDecorations = !bLeftMenuDecorations;
		AnimLeftMenuDecorations.SetBool("Show", bLeftMenuDecorations);

		if(bLeftMenuDecorations)
			SoundManagerCleaningClassroom.Instance.StopAndPlay_Sound(SoundManagerCleaningClassroom.Instance.ShowMenu);
		else
			SoundManagerCleaningClassroom.Instance.StopAndPlay_Sound(SoundManagerCleaningClassroom.Instance.MenuHide);
 
		BlockClicks.Instance.SetBlockAll(true);
		BlockClicks.Instance.SetBlockAllDelay(.5f,false);

		if(!bLeftMenuDecorations && GameData.MiniGamePlayed[(Gameplay.roomNo-1)]  && listaGlavnihMenija == "" )
		{
			Gameplay.Instance.ShowChildGoToNextLevel();
		}
	}

    public void OpenWallsCategory()
    {
        Transform holder = transform.Find("AnimationHolder/SubMenu/BG/ScrollButtons/Holder");
        holder.transform.parent.GetComponent<ScrollRect>().normalizedPosition = new Vector2(1, 1);
        int count = holder.childCount-1;
        while(count>0)
        {
            Destroy(holder.GetChild(count--).gameObject);
        }
        GameObject refItem = holder.GetChild(0).gameObject;
        GameObject newItem;

     
        refItem.GetComponent<Button>().onClick.RemoveAllListeners();

      
        refItem.GetComponent<Image>().sprite = data.walls[0].icon;
        if (data.walls[0].locked)
            refItem.transform.GetChild(0).gameObject.SetActive(true);
        else
            refItem.transform.GetChild(0).gameObject.SetActive(false);

        refItem.GetComponent<Button>().onClick.AddListener(delegate
            {
                if(data.walls[0].locked)
                {
                    this.canvas.GetComponent<MenuManager>().ShowPopUpMenu(this.canvas.transform.Find("PopUps/PopUpDialogWatchVideo").gameObject);
                    this.canvas.transform.Find("PopUps/PopUpDialogWatchVideo/AnimationHolder/Body/ButtonsHolder/ButtonYes").GetComponent<Button>().onClick.RemoveAllListeners();
                    this.canvas.transform.Find("PopUps/PopUpDialogWatchVideo/AnimationHolder/Body/ButtonsHolder/ButtonYes").GetComponent<Button>().onClick.AddListener(delegate {
                        if(AdsManager.videoReady)
                        {
                            AdsManager.videoRewarded.RemoveAllListeners();
                            AdsManager.videoRewarded.AddListener(delegate {
                                string ppstring=PlayerPrefs.GetString("WallsUnlocked");
                                if(!ppstring.Contains("0,"))
                                {
                                    ppstring+="0,";
                                }
                                PlayerPrefs.SetString("WallsUnlocked",ppstring);
                                PlayerPrefs.Save();
                                data.walls[0].locked=false;
                                refItem.transform.GetChild(0).gameObject.SetActive(false);
                            });
                            AdsManager.Instance.ShowVideoReward();
                        }
                        else
                        {
                            this.canvas.GetComponent<MenuManager>().ShowPopUpMenu(canvas.transform.Find("PopUps/PopUpMessage").gameObject);
                        }
                    });
                }
                else
                {
                    if(changeWallC!=null)
                        StopCoroutine(changeWallC);
                    changeWallC=StartCoroutine(changeWall(data.walls[0].content));
                    PlayerPrefs.SetInt("Wall",0);
                    PlayerPrefs.Save();
                }
            });

        for(int i=1;i<data.walls.Count;i++)
        {
            newItem = GameObject.Instantiate(refItem);
            newItem.transform.SetParent(refItem.transform.parent, false);
            newItem.GetComponent<Image>().sprite = data.walls[i].icon;
            if (data.walls[i].locked)
                newItem.transform.GetChild(0).gameObject.SetActive(true);
            else
                newItem.transform.GetChild(0).gameObject.SetActive(false);
            newItem.GetComponent<Button>().onClick.RemoveAllListeners();
            int index = i;
            GameObject lockHolder = newItem;
            newItem.GetComponent<Button>().onClick.AddListener(delegate
                {
                    if(data.walls[index].locked)
                    {
                        this.canvas.GetComponent<MenuManager>().ShowPopUpMenu(this.canvas.transform.Find("PopUps/PopUpDialogWatchVideo").gameObject);
                        this.canvas.transform.Find("PopUps/PopUpDialogWatchVideo/AnimationHolder/Body/ButtonsHolder/ButtonYes").GetComponent<Button>().onClick.RemoveAllListeners();
                        this.canvas.transform.Find("PopUps/PopUpDialogWatchVideo/AnimationHolder/Body/ButtonsHolder/ButtonYes").GetComponent<Button>().onClick.AddListener(delegate {
                            if(AdsManager.videoReady)
                            {
                                AdsManager.videoRewarded.RemoveAllListeners();
                                AdsManager.videoRewarded.AddListener(delegate {
                                    string ppstring=PlayerPrefs.GetString("WallsUnlocked");
                                    if(!ppstring.Contains(index.ToString()+","))
                                    {
                                        ppstring+=index.ToString()+",";
                                    }
                                    PlayerPrefs.SetString("WallsUnlocked",ppstring);
                                    PlayerPrefs.Save();
                                    data.walls[index].locked=false;
                                    lockHolder.transform.GetChild(0).gameObject.SetActive(false);
                                });
                                AdsManager.Instance.ShowVideoReward();
                            }
                            else
                            {
                                this.canvas.GetComponent<MenuManager>().ShowPopUpMenu(this.canvas.transform.Find("PopUps/PopUpMessage").gameObject);
                            }
                        });
                    }
                    else
                    {
                        if(changeWallC!=null)
                            StopCoroutine(changeWallC);
                        changeWallC=StartCoroutine(changeWall(data.walls[index].content));
                        PlayerPrefs.SetInt("Wall",index);
                        PlayerPrefs.Save();
                    }
                });
        }
    }

    IEnumerator changeWall(Sprite newBGSprite)
    {
        GameObject canvas = GameObject.Find("CanvasBG");
        GameObject oldBG = canvas.transform.Find("SceneGraphics/Decorations/P1/Decoration1").gameObject;
        GameObject newBG = canvas.transform.Find("SceneGraphics/Decorations/P1/Decoration2").gameObject;
        GameObject particles = canvas.transform.Find("SceneGraphics/Decorations/P1/Particles").gameObject;
        newBG.GetComponent<Image>().sprite=newBGSprite;
        newBG.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
        float a = 0f;
        while(a<1f)
        {
            yield return new WaitForSeconds(0.01f);
            a += 0.02f;
            newBG.GetComponent<Image>().color = new Color(1f, 1f, 1f, a);
        }
        newBG.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        oldBG.GetComponent<Image>().sprite = newBG.GetComponent<Image>().sprite;
        oldBG.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        newBG.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
    }

    public void OpenBenchesCategory()
    {

        Transform holder = transform.Find("AnimationHolder/SubMenu/BG/ScrollButtons/Holder");
        holder.parent.GetComponent<ScrollRect>().normalizedPosition = new Vector2(1, 1);
        int count = holder.childCount-1;
        while(count>0)
        {
            Destroy(holder.GetChild(count--).gameObject);
        }
        GameObject refItem = holder.GetChild(0).gameObject;
        refItem.GetComponent<Button>().onClick.RemoveAllListeners();

        GameObject newItem;

        GameObject canvasBG = GameObject.Find("CanvasBG");


        refItem.GetComponent<Image>().sprite = data.benches[0].icon;
        if (data.benches[0].locked)
            refItem.transform.GetChild(0).gameObject.SetActive(true);
        else
            refItem.transform.GetChild(0).gameObject.SetActive(false);

        refItem.GetComponent<Button>().onClick.AddListener(delegate
            {
                if(data.benches[0].locked)
                {
                    this.canvas.GetComponent<MenuManager>().ShowPopUpMenu(this.canvas.transform.Find("PopUps/PopUpDialogWatchVideo").gameObject);
                    this.canvas.transform.Find("PopUps/PopUpDialogWatchVideo/AnimationHolder/Body/ButtonsHolder/ButtonYes").GetComponent<Button>().onClick.RemoveAllListeners();
                    this.canvas.transform.Find("PopUps/PopUpDialogWatchVideo/AnimationHolder/Body/ButtonsHolder/ButtonYes").GetComponent<Button>().onClick.AddListener(delegate {
                        if(AdsManager.videoReady)
                        {
                            AdsManager.videoRewarded.RemoveAllListeners();
                            AdsManager.videoRewarded.AddListener(delegate {
                                string ppstring=PlayerPrefs.GetString("BenchesUnlocked");
                                if(!ppstring.Contains("0,"))
                                {
                                    ppstring+="0,";
                                }
                                PlayerPrefs.SetString("BenchesUnlocked",ppstring);
                                PlayerPrefs.Save();
                                data.benches[0].locked=false;
                                refItem.transform.GetChild(0).gameObject.SetActive(false);
                            });
                            AdsManager.Instance.ShowVideoReward();
                        }
                        else
                        {
                            this.canvas.GetComponent<MenuManager>().ShowPopUpMenu(this.canvas.transform.Find("PopUps/PopUpMessage").gameObject);
                        }
                    });
                }
                else
                {
                    foreach(Transform t in canvasBG.transform.Find("SceneGraphics/SchoolBenches/AnimationHolder"))
                    {
                        t.gameObject.SetActive(false);
                    }
                    canvasBG.transform.Find("SceneGraphics/SchoolBenches/AnimationHolder/Set"+data.benches[0].index).gameObject.SetActive(true);
                    PlayerPrefs.SetInt("Benches",data.benches[0].index);
                    PlayerPrefs.Save();
                }
            });

        for(int i=1;i<data.benches.Count;i++)
        {
            newItem = GameObject.Instantiate(refItem);
            newItem.transform.SetParent(refItem.transform.parent, false);
            newItem.GetComponent<Image>().sprite = data.benches[i].icon;
            if (data.benches[i].locked)
                newItem.transform.GetChild(0).gameObject.SetActive(true);
            else
                newItem.transform.GetChild(0).gameObject.SetActive(false);
            newItem.GetComponent<Button>().onClick.RemoveAllListeners();
            GameObject lockHolder = newItem;
            int index = i;
            newItem.GetComponent<Button>().onClick.AddListener(delegate
                {
                    if(data.benches[index].locked)
                    {
                        this.canvas.GetComponent<MenuManager>().ShowPopUpMenu(this.canvas.transform.Find("PopUps/PopUpDialogWatchVideo").gameObject);
                        this.canvas.transform.Find("PopUps/PopUpDialogWatchVideo/AnimationHolder/Body/ButtonsHolder/ButtonYes").GetComponent<Button>().onClick.RemoveAllListeners();
                        this.canvas.transform.Find("PopUps/PopUpDialogWatchVideo/AnimationHolder/Body/ButtonsHolder/ButtonYes").GetComponent<Button>().onClick.AddListener(delegate {
                            if(AdsManager.videoReady)
                            {
                                AdsManager.videoRewarded.RemoveAllListeners();
                                AdsManager.videoRewarded.AddListener(delegate {
                                    string ppstring=PlayerPrefs.GetString("BenchesUnlocked");
                                    if(!ppstring.Contains(index.ToString()+","))
                                    {
                                        ppstring+=index.ToString()+",";
                                    }
                                    PlayerPrefs.SetString("BenchesUnlocked",ppstring);
                                    PlayerPrefs.Save();
                                    data.benches[index].locked=false;
                                    lockHolder.transform.GetChild(0).gameObject.SetActive(false);
                                });
                                AdsManager.Instance.ShowVideoReward();
                            }
                            else
                            {
                                this.canvas.GetComponent<MenuManager>().ShowPopUpMenu(this.canvas.transform.Find("PopUps/PopUpMessage").gameObject);
                            }
                        });
                    }
                    else
                    {
                        foreach(Transform t in canvasBG.transform.Find("SceneGraphics/SchoolBenches/AnimationHolder"))
                        {
                            t.gameObject.SetActive(false);
                        }
                        canvasBG.transform.Find("SceneGraphics/SchoolBenches/AnimationHolder/Set"+data.benches[index].index).gameObject.SetActive(true);
                        PlayerPrefs.SetInt("Benches",data.benches[index].index);
                        PlayerPrefs.Save();
                    }
                });
        }
    }
    public void OpenShelvesCategory()
    {
        Transform holder = transform.Find("AnimationHolder/SubMenu/BG/ScrollButtons/Holder");
        holder.parent.GetComponent<ScrollRect>().normalizedPosition = new Vector2(1,1);
        int count = holder.childCount-1;
        while(count>0)
        {
            Destroy(holder.GetChild(count--).gameObject);
        }
        GameObject refItem = holder.GetChild(0).gameObject;
        GameObject newItem;
        GameObject canvas = GameObject.Find("CanvasBG");
        refItem.GetComponent<Button>().onClick.RemoveAllListeners();

        refItem.GetComponent<Image>().sprite = data.shelves[0].icon;
        if (data.shelves[0].locked)
            refItem.transform.GetChild(0).gameObject.SetActive(true);
        else
            refItem.transform.GetChild(0).gameObject.SetActive(false);

        refItem.GetComponent<Button>().onClick.AddListener(delegate
            {
                if(data.shelves[0].locked)
                {
                    this.canvas.GetComponent<MenuManager>().ShowPopUpMenu(this.canvas.transform.Find("PopUps/PopUpDialogWatchVideo").gameObject);
                    this.canvas.transform.Find("PopUps/PopUpDialogWatchVideo/AnimationHolder/Body/ButtonsHolder/ButtonYes").GetComponent<Button>().onClick.RemoveAllListeners();
                    this.canvas.transform.Find("PopUps/PopUpDialogWatchVideo/AnimationHolder/Body/ButtonsHolder/ButtonYes").GetComponent<Button>().onClick.AddListener(delegate {
                        if(AdsManager.videoReady)
                        {
                            AdsManager.videoRewarded.RemoveAllListeners();
                            AdsManager.videoRewarded.AddListener(delegate {
                                string ppstring=PlayerPrefs.GetString("ShelvesUnlocked");
                                if(!ppstring.Contains("0,"))
                                {
                                    ppstring+="0,";
                                }
                                PlayerPrefs.SetString("ShelvesUnlocked",ppstring);
                                PlayerPrefs.Save();
                                data.shelves[0].locked=false;
                                refItem.transform.GetChild(0).gameObject.SetActive(false);
                            });
                            AdsManager.Instance.ShowVideoReward();
                        }
                        else
                        {
                            this.canvas.GetComponent<MenuManager>().ShowPopUpMenu(this.canvas.transform.Find("PopUps/PopUpMessage").gameObject);
                        }
                    });
                }
                else
                {
                    canvas.transform.Find("SceneGraphics/Decorations/P4/Decoration1").GetComponent<Image>().sprite=data.shelves[0].content;
                    PlayerPrefs.SetInt("Shelves",0);
                    PlayerPrefs.Save();   
                }
            });

        for(int i=1;i<data.shelves.Count;i++)
        {
            newItem = GameObject.Instantiate(refItem);
            newItem.transform.SetParent(refItem.transform.parent, false);
            newItem.GetComponent<Image>().sprite = data.shelves[i].icon;
            if (data.shelves[i].locked)
                newItem.transform.GetChild(0).gameObject.SetActive(true);
            else
                newItem.transform.GetChild(0).gameObject.SetActive(false);
            newItem.GetComponent<Button>().onClick.RemoveAllListeners();
            int index = i;
            GameObject lockHolder = newItem;
            newItem.GetComponent<Button>().onClick.AddListener(delegate
                {
                    if(data.shelves[index].locked)
                    {
                        this.canvas.GetComponent<MenuManager>().ShowPopUpMenu(this.canvas.transform.Find("PopUps/PopUpDialogWatchVideo").gameObject);
                        this.canvas.transform.Find("PopUps/PopUpDialogWatchVideo/AnimationHolder/Body/ButtonsHolder/ButtonYes").GetComponent<Button>().onClick.RemoveAllListeners();
                        this.canvas.transform.Find("PopUps/PopUpDialogWatchVideo/AnimationHolder/Body/ButtonsHolder/ButtonYes").GetComponent<Button>().onClick.AddListener(delegate {
                            if(AdsManager.videoReady)
                            {
                                AdsManager.videoRewarded.RemoveAllListeners();
                                AdsManager.videoRewarded.AddListener(delegate {
                                    string ppstring=PlayerPrefs.GetString("ShelvesUnlocked");
                                    if(!ppstring.Contains(index.ToString()+","))
                                    {
                                        ppstring+=index.ToString()+",";
                                    }
                                    PlayerPrefs.SetString("ShelvesUnlocked",ppstring);
                                    PlayerPrefs.Save();
                                    data.shelves[index].locked=false;
                                    lockHolder.transform.GetChild(0).gameObject.SetActive(false);
                                });
                                AdsManager.Instance.ShowVideoReward();
                            }
                            else
                            {
                                this.canvas.GetComponent<MenuManager>().ShowPopUpMenu(this.canvas.transform.Find("PopUps/PopUpMessage").gameObject);
                            }
                        });
                    }
                    else
                    {
                        canvas.transform.Find("SceneGraphics/Decorations/P4/Decoration1").GetComponent<Image>().sprite=data.shelves[index].content;
                        PlayerPrefs.SetInt("Shelves",index);
                        PlayerPrefs.Save();
                    }
                });
        }
    }

    public void OpenBoardsCategory()
    {
        Transform holder = transform.Find("AnimationHolder/SubMenu/BG/ScrollButtons/Holder");
        holder.parent.GetComponent<ScrollRect>().normalizedPosition = new Vector2(1,1);
        int count = holder.childCount-1;
        while(count>0)
        {
            Destroy(holder.GetChild(count--).gameObject);
        }
        GameObject refItem = holder.GetChild(0).gameObject;
        GameObject newItem;
        GameObject canvas = GameObject.Find("CanvasBG");
        refItem.GetComponent<Button>().onClick.RemoveAllListeners();

        refItem.GetComponent<Image>().sprite = data.boards[0].icon;
        if (data.boards[0].locked)
            refItem.transform.GetChild(0).gameObject.SetActive(true);
        else
            refItem.transform.GetChild(0).gameObject.SetActive(false);

        refItem.GetComponent<Button>().onClick.AddListener(delegate
            {
                if(data.boards[0].locked)
                {
                    this.canvas.GetComponent<MenuManager>().ShowPopUpMenu(this.canvas.transform.Find("PopUps/PopUpDialogWatchVideo").gameObject);
                    this.canvas.transform.Find("PopUps/PopUpDialogWatchVideo/AnimationHolder/Body/ButtonsHolder/ButtonYes").GetComponent<Button>().onClick.RemoveAllListeners();
                    this.canvas.transform.Find("PopUps/PopUpDialogWatchVideo/AnimationHolder/Body/ButtonsHolder/ButtonYes").GetComponent<Button>().onClick.AddListener(delegate {
                        if(AdsManager.videoReady)
                        {
                            AdsManager.videoRewarded.RemoveAllListeners();
                            AdsManager.videoRewarded.AddListener(delegate {
                                string ppstring=PlayerPrefs.GetString("BoardsUnlocked");
                                if(!ppstring.Contains("0,"))
                                {
                                    ppstring+="0,";
                                }
                                PlayerPrefs.SetString("BoardsUnlocked",ppstring);
                                PlayerPrefs.Save();
                                data.boards[0].locked=false;
                                refItem.transform.GetChild(0).gameObject.SetActive(false);
                            });
                            AdsManager.Instance.ShowVideoReward();
                        }
                        else
                        {
                            this.canvas.GetComponent<MenuManager>().ShowPopUpMenu(this.canvas.transform.Find("PopUps/PopUpMessage").gameObject);
                        }
                    });
                }
                else
                {
                    canvas.transform.Find("SceneGraphics/Decorations/P5/Decoration1").GetComponent<Image>().sprite=data.boards[0].content;
                    PlayerPrefs.SetInt("Board",0);
                    PlayerPrefs.Save();
                }
            });

        for(int i=1;i<data.boards.Count;i++)
        {
            newItem = GameObject.Instantiate(refItem);
            newItem.transform.SetParent(refItem.transform.parent, false);
            newItem.GetComponent<Image>().sprite = data.boards[i].icon;
            if (data.boards[i].locked)
                newItem.transform.GetChild(0).gameObject.SetActive(true);
            else
                newItem.transform.GetChild(0).gameObject.SetActive(false);
            newItem.GetComponent<Button>().onClick.RemoveAllListeners();
            int index = i;
            GameObject lockHolder = newItem;
            newItem.GetComponent<Button>().onClick.AddListener(delegate
                {
                    if(data.boards[index].locked)
                    {
                        this.canvas.GetComponent<MenuManager>().ShowPopUpMenu(this.canvas.transform.Find("PopUps/PopUpDialogWatchVideo").gameObject);
                        this.canvas.transform.Find("PopUps/PopUpDialogWatchVideo/AnimationHolder/Body/ButtonsHolder/ButtonYes").GetComponent<Button>().onClick.RemoveAllListeners();
                        this.canvas.transform.Find("PopUps/PopUpDialogWatchVideo/AnimationHolder/Body/ButtonsHolder/ButtonYes").GetComponent<Button>().onClick.AddListener(delegate {
                            if(AdsManager.videoReady)
                            {
                                AdsManager.videoRewarded.RemoveAllListeners();
                                AdsManager.videoRewarded.AddListener(delegate {
                                    string ppstring=PlayerPrefs.GetString("BoardsUnlocked");
                                    if(!ppstring.Contains(index.ToString()+","))
                                    {
                                        ppstring+=index.ToString()+",";
                                    }
                                    PlayerPrefs.SetString("BoardsUnlocked",ppstring);
                                    PlayerPrefs.Save();
                                    data.boards[index].locked=false;
                                    lockHolder.transform.GetChild(0).gameObject.SetActive(false);
                                });
                                AdsManager.Instance.ShowVideoReward();
                            }
                            else
                            {
                                this.canvas.GetComponent<MenuManager>().ShowPopUpMenu(this.canvas.transform.Find("PopUps/PopUpMessage").gameObject);
                            }
                        });
                    }
                    else
                    {
                        canvas.transform.Find("SceneGraphics/Decorations/P5/Decoration1").GetComponent<Image>().sprite=data.boards[index].content;
                        PlayerPrefs.SetInt("Board",index);
                        PlayerPrefs.Save();
                    }
                });
        }
    }

    public void OpenChairsCategory()
    {
        Transform holder = transform.Find("AnimationHolder/SubMenu/BG/ScrollButtons/Holder");
        holder.parent.GetComponent<ScrollRect>().normalizedPosition = new Vector2(1,1);
        int count = holder.childCount-1;
        while(count>0)
        {
            Destroy(holder.GetChild(count--).gameObject);
        }
        GameObject refItem = holder.GetChild(0).gameObject;
        GameObject newItem;

        GameObject canvasBG = GameObject.Find("CanvasBG");

        refItem.GetComponent<Button>().onClick.RemoveAllListeners();

        refItem.GetComponent<Image>().sprite = data.chairs[0].icon;
        if (data.chairs[0].locked)
            refItem.transform.GetChild(0).gameObject.SetActive(true);
        else
            refItem.transform.GetChild(0).gameObject.SetActive(false);

        refItem.GetComponent<Button>().onClick.AddListener(delegate
            {
                if(data.chairs[0].locked)
                {
                    this.canvas.GetComponent<MenuManager>().ShowPopUpMenu(this.canvas.transform.Find("PopUps/PopUpDialogWatchVideo").gameObject);
                    this.canvas.transform.Find("PopUps/PopUpDialogWatchVideo/AnimationHolder/Body/ButtonsHolder/ButtonYes").GetComponent<Button>().onClick.RemoveAllListeners();
                    this.canvas.transform.Find("PopUps/PopUpDialogWatchVideo/AnimationHolder/Body/ButtonsHolder/ButtonYes").GetComponent<Button>().onClick.AddListener(delegate {
                        if(AdsManager.videoReady)
                        {
                            AdsManager.videoRewarded.RemoveAllListeners();
                            AdsManager.videoRewarded.AddListener(delegate {
                                string ppstring=PlayerPrefs.GetString("ChairsUnlocked");
                                if(!ppstring.Contains("0,"))
                                {
                                    ppstring+="0,";
                                }
                                PlayerPrefs.SetString("ChairsUnlocked",ppstring);
                                PlayerPrefs.Save();
                                data.chairs[0].locked=false;
                                refItem.transform.GetChild(0).gameObject.SetActive(false);
                            });
                            AdsManager.Instance.ShowVideoReward();
                        }
                        else
                        {
                            this.canvas.GetComponent<MenuManager>().ShowPopUpMenu(this.canvas.transform.Find("PopUps/PopUpMessage").gameObject);
                        }
                    });
                }
                else
                {
                    foreach(Transform t in canvasBG.transform.Find("SceneGraphics/Chairs/AnimationHolder"))
                    {
                        t.gameObject.SetActive(false);
                    }
                    canvasBG.transform.Find("SceneGraphics/Chairs/AnimationHolder/Set"+data.chairs[0].index).gameObject.SetActive(true);
                    PlayerPrefs.SetInt("Chairs",data.chairs[0].index);
                    PlayerPrefs.Save();   
                }
            });

        for(int i=1;i<data.chairs.Count;i++)
        {
            newItem = GameObject.Instantiate(refItem);
            newItem.transform.SetParent(refItem.transform.parent, false);
            newItem.GetComponent<Image>().sprite = data.chairs[i].icon;
            if (data.chairs[i].locked)
                newItem.transform.GetChild(0).gameObject.SetActive(true);
            else
                newItem.transform.GetChild(0).gameObject.SetActive(false);

            newItem.GetComponent<Button>().onClick.RemoveAllListeners();
            int index = i;
            GameObject lockHolder = newItem;
            newItem.GetComponent<Button>().onClick.AddListener(delegate
                {
                    if(data.chairs[index].locked)
                    {
                        this.canvas.GetComponent<MenuManager>().ShowPopUpMenu(this.canvas.transform.Find("PopUps/PopUpDialogWatchVideo").gameObject);
                        this.canvas.transform.Find("PopUps/PopUpDialogWatchVideo/AnimationHolder/Body/ButtonsHolder/ButtonYes").GetComponent<Button>().onClick.RemoveAllListeners();
                        this.canvas.transform.Find("PopUps/PopUpDialogWatchVideo/AnimationHolder/Body/ButtonsHolder/ButtonYes").GetComponent<Button>().onClick.AddListener(delegate {
                            if(AdsManager.videoReady)
                            {
                                AdsManager.videoRewarded.RemoveAllListeners();
                                AdsManager.videoRewarded.AddListener(delegate {
                                    string ppstring=PlayerPrefs.GetString("ChairsUnlocked");
                                    if(!ppstring.Contains(index.ToString()+","))
                                    {
                                        ppstring+=index.ToString()+",";
                                    }
                                    PlayerPrefs.SetString("ChairsUnlocked",ppstring);
                                    PlayerPrefs.Save();
                                    data.chairs[0].locked=false;
                                    lockHolder.transform.GetChild(0).gameObject.SetActive(false);
                                });
                                AdsManager.Instance.ShowVideoReward();
                            }
                            else
                            {
                                this.canvas.GetComponent<MenuManager>().ShowPopUpMenu(canvas.transform.Find("PopUps/PopUpMessage").gameObject);
                            }
                        });
                    }
                    else
                    {
                        foreach(Transform t in canvasBG.transform.Find("SceneGraphics/Chairs/AnimationHolder"))
                        {
                            t.gameObject.SetActive(false);
                        }
                        canvasBG.transform.Find("SceneGraphics/Chairs/AnimationHolder/Set"+data.chairs[index].index).gameObject.SetActive(true);
                        PlayerPrefs.SetInt("Chairs",data.chairs[index].index);
                        PlayerPrefs.Save();   
                    }
                });
        }
    }

    public void LoadChosenDecorations()
    {
        GameObject canvasBG = GameObject.Find("CanvasBG");
        if(PlayerPrefs.HasKey("Wall"))
        {
            StartCoroutine(changeWall(data.walls[PlayerPrefs.GetInt("Wall")].content));
        }

        if(PlayerPrefs.HasKey("Board"))
        {
            canvasBG.transform.Find("SceneGraphics/Decorations/P5/Decoration1").GetComponent<Image>().sprite = data.boards[PlayerPrefs.GetInt("Board")].content;
        }

        if(PlayerPrefs.HasKey("Chairs"))
        {
            foreach(Transform t in canvasBG.transform.Find("SceneGraphics/Chairs/AnimationHolder"))
            {
                t.gameObject.SetActive(false);
            }
            canvasBG.transform.Find("SceneGraphics/Chairs/AnimationHolder/Set"+PlayerPrefs.GetInt("Chairs")).gameObject.SetActive(true);
        }

        if(PlayerPrefs.HasKey("Benches"))
        {
            foreach(Transform t in canvasBG.transform.Find("SceneGraphics/SchoolBenches/AnimationHolder"))
            {
                t.gameObject.SetActive(false);
            }
            canvasBG.transform.Find("SceneGraphics/SchoolBenches/AnimationHolder/Set"+PlayerPrefs.GetInt("Benches")).gameObject.SetActive(true);
        }

        if(PlayerPrefs.HasKey("Shelves"))
        {
            canvasBG.transform.Find("SceneGraphics/Decorations/P4/Decoration1").GetComponent<Image>().sprite=data.shelves[PlayerPrefs.GetInt("Shelves")].content;
        }
    }

//
//	public void MenuButtonClicked(int subMenuNo)
//	{
//		//activeMenu= subMenuNo;
//		SetSubMenuItems( subMenuNo);
//		SoundManagerCleaningClassroom.Instance.StopAndPlay_Sound(SoundManagerCleaningClassroom.Instance.ButtonClick2);
//	}
//
//
//	//klik na stavku submenija 
//	public void ButtonItemClicked(string item)
//	{
////		Debug.Log(item);
//		Image Decoration1;
//		Image Decoration2 ;
//		StopAllCoroutines();
//	 
//		MenuItemData it =MenuItems.mitd[item];
//		Sprite sp;
//		switch(activeMenu)
//		{
//		case 1: //meni - zamena tapeta
//		case 14: //meni -  zamena tapeta
//			Decoration1 = GameObject.Find("CanvasBG/SceneGraphics/Decorations/P"+activeMenu.ToString()+"/Decoration1").GetComponent<Image>();
//			Decoration2 = GameObject.Find("CanvasBG/SceneGraphics/Decorations/P"+activeMenu.ToString()+"/Decoration2").GetComponent<Image>();
//			 sp = GetSpriteByAtalasAndName(it.Atlas,it.Name);
//			if(sp != null) 
//			{
//				StartCoroutine( DecorationCrossFadeAndSwap(Decoration1,Decoration2,sp ));
//				GameObject.Find("CanvasBG/SceneGraphics/Decorations/P"+activeMenu.ToString()+"/Particles").GetComponent<ParticleSystem>().Play();
//				
//			}
//			break;
//
//		case 2: //meni - klupe
//                GameObject benches=GameObject.Find("CanvasBG/SceneGraphics").transform.Find("SchoolBenches/AnimationHolder").gameObject;
//
//
//                break;
//		case 3: //meni - luster
//                break;
//		case 4: //meni - ormar
//                break;
//		case 5://meni - prozor
//                break;
//		case 6: //meni - lampa iznad kreveta
//                break;
//		
//		case 7: //meni - bazen
//                break;
//		case 9: //meni - cvece
//                break;
//		case 11://meni - kucni ljubimac
//                break;
//
//		case 12: //meni - kauc
//                break;
//		case 13: //meni - tv
//                break;
//		case 15: //meni - lampe
//                break;
//
//		case 16: //meni - prozor 2
//                break;
//		case 17: //meni - viseci elementi
//                break;
//		case 18: //meni - kuhinja
//                break;
//		case 19: //meni - sporet
//                break;
//		case 20: //meni - ogledalo
//                break;
//		case 21: //meni - kada
//                break;
//		case 22: //meni - lavabo i fioke
//                break;
//		case 23: //meni - wc solja
//                break;
//
//		case 24: //meni - lampa veseraj
//                break;
//		case 27: //meni - komoda
//                break;
//		case 28: //men - ogledalo
//                break;
//		case 29: //meni - tepih
//                break;
//		case 30: //meni - stocic
//                break;
//		case 31: //meni - frizider
//                break;
//		case 32: //meni - korpa za ves
//                break;
//		case 33: //meni - plafonjera za vesernicu
//
//			Decoration1 = GameObject.Find("CanvasBG/SceneGraphics/Decorations/P"+activeMenu.ToString()+"/Decoration1").GetComponent<Image>();
//			Decoration1.rectTransform.sizeDelta = it.ImgeSize;
//			 sp = GetSpriteByAtalasAndName(it.Atlas,it.Name);
//			if(sp != null) 
//			{
//				StartCoroutine( DecorationFadeIn(Decoration1,sp ));
//				GameObject.Find("CanvasBG/SceneGraphics/Decorations/P"+activeMenu.ToString()+"/Particles").GetComponent<ParticleSystem>().Play(); 
//			}
//			break;
//
////		case 20: //meni - lampe pored ogledala
////			Decoration1 = GameObject.Find("CanvasBG/SceneGraphics/Decorations/P"+activeMenu.ToString()+"/Decoration1").GetComponent<Image>();
////			Decoration1.rectTransform.sizeDelta = it.ImgeSize;
////
////			Decoration2 = GameObject.Find("CanvasBG/SceneGraphics/Decorations/P"+activeMenu.ToString()+"/Decoration2").GetComponent<Image>();
////			Decoration2.rectTransform.sizeDelta = it.ImgeSize;
////
////			sp = GetSpriteByAtalasAndName(it.Atlas,it.Name);
////			if(sp != null) 
////			{
////				StartCoroutine( DecorationFadeIn(Decoration1,sp ));
////				StartCoroutine( DecorationFadeIn(Decoration2,sp ));
////				GameObject.Find("CanvasBG/SceneGraphics/Decorations/P"+activeMenu.ToString()+"/Particles").GetComponent<ParticleSystem>().Play();
////				GameObject.Find("CanvasBG/SceneGraphics/Decorations/P"+activeMenu.ToString()+"/Particles2").GetComponent<ParticleSystem>().Play();
////			}
////			break;
//
//		case 25: //meni - masina za ves
//			Decoration1 = GameObject.Find("CanvasBG/SceneGraphics/Decorations/P"+activeMenu.ToString()+"/Decoration1").GetComponent<Image>();
//			Decoration1.rectTransform.sizeDelta = it.ImgeSize;
//			sp = GetSpriteByAtalasAndName(it.Atlas,it.Name);
//			if(sp != null) 
//			{
//				StartCoroutine( DecorationFadeIn(Decoration1,sp ));
//				GameObject wm = GameObject.Find("CanvasBG/SceneGraphics/WashingMachine") ;
//				GameObject.Find("CanvasBG/SceneGraphics/Decorations/P"+activeMenu.ToString()+"/Particles").GetComponent<ParticleSystem>().Play();
//				if(wm != null) GameObject.Destroy(wm);
//			}
//			break;
//
//		case 8: //meni - igracke
//
//			Decoration1 = GameObject.Find("CanvasBG/SceneGraphics/Decorations/P"+activeMenu.ToString()+"/Decoration1").GetComponent<Image>();
//			Decoration2 = GameObject.Find("CanvasBG/SceneGraphics/Decorations/P"+activeMenu.ToString()+"/Decoration2").GetComponent<Image>();
//			Decoration2.rectTransform.sizeDelta = it.ImgeSize;
//			sp = GetSpriteByAtalasAndName(it.Atlas,it.Name);
//			if(it.Name.StartsWith("P1"))
//			{
//				Decoration2.transform.position =   GameObject.Find("CanvasBG/SceneGraphics/Decorations/P"+activeMenu.ToString()+"/P1").transform.position;
//				GameObject.Find("CanvasBG/SceneGraphics/Decorations/P"+activeMenu.ToString()+"/Particles").GetComponent<ParticleSystem>().Play();
//			}
//			else
//			{
//				Decoration2.transform.position =   GameObject.Find("CanvasBG/SceneGraphics/Decorations/P"+activeMenu.ToString()+"/P2").transform.position;
//				GameObject.Find("CanvasBG/SceneGraphics/Decorations/P"+activeMenu.ToString()+"/Particles2").GetComponent<ParticleSystem>().Play();
//			}
//			StartCoroutine( DecorationCrossFadeAndSwap(Decoration1,Decoration2,sp , true));
//			break;
//
//		case 10://meni - drvo
//			Decoration1 = GameObject.Find("CanvasBG/SceneGraphics/Decorations/P"+activeMenu.ToString()+"/Decoration1").GetComponent<Image>();
//			Decoration2 = GameObject.Find("CanvasBG/SceneGraphics/Decorations/P"+activeMenu.ToString()+"/Decoration1b").GetComponent<Image>();
//			Decoration2.rectTransform.sizeDelta = it.ImgeSize;
//			sp = GetSpriteByAtalasAndName(it.Atlas,it.Name);
//			if(sp != null) 
//			{
//				StartCoroutine( DecorationCrossFadeAndSwap(Decoration1,Decoration2,sp ));
//				GameObject.Find("CanvasBG/SceneGraphics/Decorations/P"+activeMenu.ToString()+"/Particles").GetComponent<ParticleSystem>().Play();
//				
//			}
//			
//			Decoration1 = GameObject.Find("CanvasBG/SceneGraphics/Decorations/P"+activeMenu.ToString()+"/Decoration2").GetComponent<Image>();
//			Decoration2 = GameObject.Find("CanvasBG/SceneGraphics/Decorations/P"+activeMenu.ToString()+"/Decoration2b").GetComponent<Image>();
//			Decoration2.rectTransform.sizeDelta = it.ImgeSize;
//			if(sp != null) 
//			{
//				StartCoroutine( DecorationCrossFadeAndSwap(Decoration1,Decoration2,sp ));
//				GameObject.Find("CanvasBG/SceneGraphics/Decorations/P"+activeMenu.ToString()+"/Particles2").GetComponent<ParticleSystem>().Play();
//				
//			}
//			break;
//		}
//
//		listaGlavnihMenija = listaGlavnihMenija .Replace(activeMenu.ToString().PadLeft(2,'0') + ";","");
//		if(listaGlavnihMenija == "")
//		{
//			
//			HideDecorationsMenuAndTestEnd();
//		}
//
//		SaveDecoration( item);
//
//		SoundManagerCleaningClassroom.Instance.StopAndPlay_Sound(SoundManagerCleaningClassroom.Instance.ButtonClick2);
//		SoundManagerCleaningClassroom.Instance.StopAndPlay_Sound(SoundManagerCleaningClassroom.Instance.ElementCompleted);
//	}
//
//
//	IEnumerator DecorationFadeIn(Image img,   Sprite sp)
//	{
//		BlockClicks.Instance.SetBlockAll(true);
//		BlockClicks.Instance.SetBlockAllDelay(.5f,false);
//
//		img.sprite = sp;
//		img.color = new Color(1,1,1,0);
//		yield return null;
//		for(float i =0 ; i<=1;i+=0.1f)
//		{
//			img.color = new Color(1,1,1,i);
//			yield return new WaitForEndOfFrame();
//		}
//		img.color = new Color(1,1,1,1);
//	
//	}
//
//
//	IEnumerator DecorationCrossFadeAndSwap(Image img1, Image img2,  Sprite sp, bool swapPositions = false)
//	{
//		BlockClicks.Instance.SetBlockAll(true);
//		BlockClicks.Instance.SetBlockAllDelay(.5f,false);
//
//		img2.sprite = sp;
//		img2.color = new Color(1,1,1,0);
//		yield return null;
//		for(float i =0 ; i<=1;i+=0.1f)
//		{
//			img2.color = new Color(1,1,1,i);
//			yield return new WaitForEndOfFrame();
//		}
//		img1.color = new Color(1,1,1,1);
//		img1.sprite = sp;
//		img2.color = new Color(1,1,1,0);
//		img2.sprite = null;
//		img1.rectTransform.sizeDelta = img2.rectTransform.sizeDelta; 
//
//		if(swapPositions)
//		{
//			Vector3 pom = img1.transform.position;
//			img1.transform.position  = img2.transform.position;
//			img2.transform.position = pom;
//
//		}
//	}
//
//	 
//	//popunjavanje ikonica za submenu
//	void SetSubMenuItems(int subMenuNo)
//	{
//		activeMenu = subMenuNo;
//		Dictionary <string,MenuItemData> m = mi.ReturmMenu(subMenuNo);
//		string tmp =  "M" + subMenuNo.ToString().PadLeft(2,'0') + "_";
//		//Debug.Log(tmp);
//		int btnMenuCount = 0;
//		float scrollContentSize = 110;
//		float btnWidth = 120;
//		for(int i = 1; i<=m.Count;i++)
//		{
//			MenuItemData d = m[ tmp + i.ToString().PadLeft(2,'0')];
//			if(!d.Locked)  btnMenuCount++;
//		}
//
//		scrollContentSize = btnWidth * btnMenuCount;
//		//Debug.Log("scrollContentSize:  "+ scrollContentSize);
//		scrollContent.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(  RectTransform.Axis.Vertical, scrollContentSize);
//
//		//POPUNA ITEMA
//		if(m.Count<itemButtons.Count)
//		{
//			for(int i = m.Count; i<itemButtons.Count;i++)
//			{
//				itemButtons[i].SetParent(HidenButtonsHolder);
//				itemButtons[i].GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
//			}
//		}
//
//
//		 
//		for(int i = 1; i<=m.Count;i++)
//		{
//			MenuItemData d = m[ tmp + i.ToString().PadLeft(2,'0')];
//			if(d.Locked) continue;
//
//			if(i<=itemButtons.Count)
//			{
//				bool bNadjeno = false;
//			 
//				foreach( Sprite sp in  ItemiSprites)
//				{
//					if(bNadjeno) break;
//					if(sp.name == d.ButtonImgName)
//					{
//						Transform btn =itemButtons[i-1];
//						btn.SetParent(scrollContent);
//						//btn.Find("Text").GetComponent<Text>().text = d.ButtonImgName;//  "IT"+(i+1).ToString().PadLeft(2,'0');
//						//btn.Find("Lock").GetComponent<Image>().enabled = d.Locked;
//						//btn.Find("Image").GetComponent<Image>().sprite = sp;
//						btn.GetComponent<Image>().sprite = sp;
//						btn.GetComponent<RectTransform>().localScale = 1.3f*Vector3.one;
//						btn.name =   tmp + i.ToString().PadLeft(2,'0');  
//
//						btn.GetComponent<Button>().onClick.RemoveAllListeners();
//						btn.GetComponent<Button>().onClick.AddListener(() =>ButtonItemClicked(btn.name));
//						bNadjeno = true;
//						btn.GetComponent<Button>().enabled = !d.Locked;
//					}
//				}
//				//if(!bNadjeno) Debug.Log ("GRESKA: "+i);
//
//			}
//			else
//			{
//
//				bool bNadjeno = false;
//				foreach( Sprite sp in  ItemiSprites)
//				{
//
//					if(bNadjeno) break;
//
//					if(sp.name.Trim() == d.ButtonImgName.Trim())
//					{
//						//						Debug.Log("* " + sp.name + "  "+d.ButtonImgName);
//						Transform btn = (Transform) GameObject.Instantiate(btnPref);
//						btn.SetParent(scrollContent);
//						//btn.Find("Text").GetComponent<Text>().text = d.ButtonImgName;//   "IT"+(i+1).ToString().PadLeft(2,'0');
//						//						btn.Find("Lock").GetComponent<Image>().enabled = d.Locked;
//						//						btn.Find("Image").GetComponent<Image>().sprite = sp;
//						btn.GetComponent<Image>().sprite = sp;
//						btn.GetComponent<RectTransform>().localScale = 1.3f*Vector3.one;
//						btn.name =   tmp + i.ToString().PadLeft(2,'0');   //btn.name =  "btn"+(i+1).ToString().PadLeft(2,'0');
//						btn.GetComponent<Button>().onClick.RemoveAllListeners();
//						btn.GetComponent<Button>().onClick.AddListener(() =>ButtonItemClicked(btn.name));
//						bNadjeno = true;
//						btn.GetComponent<Button>().enabled = !d.Locked;
//						itemButtons.Add(btn);
//					}
//				}
//				//if(!bNadjeno) Debug.Log ("GRESKA: "+i);
//			}
//		}
//
//
//		scrollRect.verticalNormalizedPosition = 1;
//		scrollRect.verticalNormalizedPosition = 1;
//
//	}
//
//
//
//	//****************************
//
//
//
//	//6 -720
//	public void ButtonPointerDown(string scrollMenu)
//	{
//		this.scrollMenu = scrollMenu;
//		SoundManagerCleaningClassroom.Instance.StopAndPlay_Sound(SoundManagerCleaningClassroom.Instance.ButtonClick2);
//
//		//float speedMainMenu;
//		//float speedSubMenu;
//		
//		//RectTransform scrollContentMainMenu;
//
//		if(scrollContentMainMenu.transform.childCount > 3 ) speedMainMenu = 3f/(float)(scrollContentMainMenu.transform.childCount-3);
//		else  speedMainMenu =1;
//		if(scrollContent.transform.childCount > 3 ) speedSubMenu =  3f/(float)(scrollContent.transform.childCount-3);
//		else  speedSubMenu =1;
//		//Debug.Log(scrollContentMainMenu.rect.height + ", speed    "+  speedSubMenu);
//	}
////
//	public void ButtonPointerUp( )
//	{
//		scrollMenu = "";
//		 
//	}
//
//	void Update () {
//		if(scrollMenu != "")
//		{
//			if(scrollMenu == "SubMenuDown") SubMenuScrollDown();
//			else if(scrollMenu == "SubMenuUp") SubMenuScrollUp();
//			else if(scrollMenu == "MainMenuDown") MainMenuScrollDown();
//			else if(scrollMenu == "MainMenuUp") MainMenuScrollUp();
//		}
//	}
//
//	void MainMenuScrollUp()
//	{
//		scrollRectMainMenu.verticalNormalizedPosition += speedMainMenu*Time.deltaTime;
//		if(scrollRectMainMenu.verticalNormalizedPosition >1) scrollRectMainMenu.verticalNormalizedPosition = 1;
//	}
//
//	void MainMenuScrollDown()
//	{
//		scrollRectMainMenu.verticalNormalizedPosition -= speedMainMenu*Time.deltaTime;
//		if(scrollRectMainMenu.verticalNormalizedPosition <0) scrollRectMainMenu.verticalNormalizedPosition = 0;
//	}
//
//	void SubMenuScrollUp()
//	{
//		scrollRect.verticalNormalizedPosition += speedSubMenu*Time.deltaTime;
//		if(scrollRect.verticalNormalizedPosition >1) scrollRect.verticalNormalizedPosition = 1;
//	}
//
//	void SubMenuScrollDown()
//	{
//		scrollRect.verticalNormalizedPosition -= speedSubMenu*Time.deltaTime;
//		if(scrollRect.verticalNormalizedPosition <0) scrollRect.verticalNormalizedPosition = 0;
//	}
//
//	 
//	Sprite GetSpriteByAtalasAndName(string atlas, string spriteName)
//	{
//		foreach( Sprite spr in Resources.LoadAll<Sprite>(atlas))
//		{
//			if( spr.name == spriteName) return spr;
//		}
//		return null;
//	}
//
//
//	void SaveDecoration( string item)
//	{
//		 
//		string pom =  LevelDecorations[(Gameplay.roomNo-1)];
//		if( !pom.Contains( item.Substring(0,4)))
//		{
//			LevelDecorations[(Gameplay.roomNo-1)] += item+";";
//			 
//		}
//		else
//		{
//			int index = pom.IndexOf(item.Substring(0,4));
//			LevelDecorations[(Gameplay.roomNo-1)] = pom .Replace( pom.Substring(index,7),item+";");
//		}
////		Debug.Log(LevelDecorations[(Gameplay.roomNo-1)]);
//	}
//
//	public void  RestoreDecorations()
//	{
////		Debug.Log( "Restore" + LevelDecorations[(Gameplay.roomNo-1)]);
//		if( LevelDecorations[(Gameplay.roomNo-1)]  != "")
//		{
//			string[] decorations = LevelDecorations[(Gameplay.roomNo-1)].Split( new char[] {';'},System.StringSplitOptions.RemoveEmptyEntries);
//		
//			foreach(string item in decorations)
//			{
//				//Debug.Log(item);
//				Image Decoration1;
//				 
//				MenuItemData it =MenuItems.mitd[item];
//				Sprite sp;
//				int pos = int.Parse(item.Substring(1,2));
//			 
//				Decoration1 = GameObject.Find("CanvasBG/SceneGraphics/Decorations/P"+pos.ToString()+"/Decoration1").GetComponent<Image>();
//				Decoration1.rectTransform.sizeDelta = it.ImgeSize;
//
//				sp = GetSpriteByAtalasAndName(it.Atlas,it.Name);
//				if(sp != null) 
//				{
//					Decoration1.sprite = sp;
//					Decoration1.color = Color.white;
//				}
//
//				if(pos == 10)
//				{
//					Decoration1 = GameObject.Find("CanvasBG/SceneGraphics/Decorations/P"+pos.ToString()+"/Decoration2").GetComponent<Image>();
//					Decoration1.rectTransform.sizeDelta = it.ImgeSize;
//
//					sp = GetSpriteByAtalasAndName(it.Atlas,it.Name);
//					if(sp != null) 
//					{
//						Decoration1.sprite = sp;
//						Decoration1.color = Color.white;
//					}
//				}
//
//
//			}
//		}
//	}
//
//	public static void ResetMenuDecorations()
//	{
//		LevelDecorations = new string [] { "", "", "", "", "", "" } ;
//	}
}

 
