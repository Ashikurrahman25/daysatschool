using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using AdvancedMobilePaint;

/// <summary>
/// <para>Scene: Gameplay</para>
/// <para>Object: PaintSetUpManager</para>
/// <para>Skripta za podesavanje AMP-a. Sadrzi funkcije za setovanje radne teksture, brush-eva kao i funkcije koje se pozivaju klikom na UI dugmeta u Gameplay sceni</para>
/// </summary>
public class PaintSetUp : MonoBehaviour {

    #region Configuration
    public bool drawingIsVisible;
    [Header("Drawings")]
    public Texture2D[] drawings;
    [Header("Bitmaps")]
    public Sprite[] bmp1;
    public Sprite[] bmp2;
    public Sprite[] bmp3;
    public Sprite[] bmp4;
    public Sprite[] bmp5;
    public Sprite[] bmp6;
    public Sprite[] bmp7;
    public Sprite[] bmp8;
    public Sprite[] bmp9;
    public Sprite[] bmp10;
    #endregion

    [Header("PaintEngine and brush slider")]
    public AdvancedMobilePaint.AdvancedMobilePaint paintEngine;
    public Slider brushSize;
//  public Texture2D mask;
//  public Sprite[] bitmaps;
    public GameObject loading;
    public PaintUndoManager undoManager;


    int currentDrawing;
	Texture2D currentBrush;
	Sprite[][] bmps;
	Color currentColor;
	GameObject currentShade;
	DrawMode currentDrawMode = DrawMode.Stamp;
    FilterMode tempFilterMode;

	int bmpsIndex;
	int bmpIndex;

	bool maskForBrush;


//    //Treba za painting
//    public static bool unlockAll = false;
//    public static bool unlockShade;
//    public static int shadeToUnlock;
//    public static int stickerToUnlock;
//    public static int colorToUnlock;
//    public static int[][] shadesLockIndices = new int[12][];
//    public static int[] stickersLockIndices = new int[65];


    IEnumerator Start()
	{
//        for (int i = 0; i < 12; i++)
//        {
//            shadesLockIndices[i] = new int[] { 0, 1, 1, 0, 1, 1, 0, 1, 1, 0 };      // Pocetno lock stanje nijansi (duzina niza je broj nijansi
//
//        }
//
//        for (int i = 0; i < stickersLockIndices.Length; i++)
//        {
//            if (i % 3 == 0)
//                stickersLockIndices[i] = 1;
//            else
//                stickersLockIndices[i] = 0;
//        }

        yield return new WaitForSeconds(2f);
		bmps = new Sprite[10][];
		bmps[0] = bmp1;
		bmps[1] = bmp2;
		bmps[2] = bmp3;
		bmps[3] = bmp4;
		bmps[4] = bmp5;
		bmps[5] = bmp6;
		bmps[6] = bmp7;
		bmps[7] = bmp8;
		bmps[8] = bmp9;
		bmps[9] = bmp10;

		bmpsIndex = 0;
		bmpIndex = (int)brushSize.value;
//		currentBrush = PaintUtils.ConvertSpriteToTexture2DForBmp(bitmaps[0]);
		currentBrush = PaintUtils.ConvertSpriteToTexture2DForBmp(bmps[bmpsIndex][4-bmpIndex]);
		currentColor = new Color32(255,197,197,255);
		currentShade = GameObject.Find("Canvas/PaintMenu/SubMenus/ColorsAndBrushes/AnimationHolder/Shades").transform.GetChild(0).gameObject;
        currentDrawing = 0;

        if (drawingIsVisible)
            tempFilterMode = FilterMode.Point;
        else
            tempFilterMode = FilterMode.Bilinear;

//		SetUpQuadPaint();
		SetUpFloodFillBrush();
		SetDrawing();
//		Invoke("EnableDrawing", 1.2f);
	}

	/// <summary>
	/// Podesava PaintEngine u zavisnosti od GlobalVariables.gameMode.
	/// </summary>
	void SetDrawing()
	{
        paintEngine.filterMode = tempFilterMode;
        SetUpQuadPaint();
//        paintEngine.SetMandalaMask(GlobalVariables.selectedTexture, FilterMode.Bilinear);
        paintEngine.SetMandalaMask(drawings[currentDrawing], tempFilterMode);
        paintEngine.useMaskLayerOnly = true;
        paintEngine.transform.GetChild(0).gameObject.SetActive(drawingIsVisible);

//      switch(GlobalVariables.gameMode)
//      {
//      case "Magic":
//          paintEngine.filterMode = FilterMode.Bilinear;
//          SetUpQuadPaint();
//          paintEngine.SetMandalaMask(GlobalVariables.selectedTexture, FilterMode.Bilinear);
//          paintEngine.useMaskLayerOnly = true;
//          paintEngine.transform.GetChild(0).gameObject.SetActive(false);
//          break;
//      case "Mandala":
//          paintEngine.filterMode = FilterMode.Point;
//          SetUpQuadPaint();
//          paintEngine.SetMandalaMask(GlobalVariables.selectedTexture, FilterMode.Point);
//          paintEngine.useMaskLayerOnly = true;
//          paintEngine.transform.GetChild(0).gameObject.SetActive(true);
//          break;
//      case "Blank":
//          paintEngine.filterMode = FilterMode.Bilinear;
//          SetUpQuadPaint();
//          paintEngine.useMaskLayerOnly = false;
//          break;
//      default:
//          Debug.Log("Invalid string for GameMode!");
//          break;
//      }
    }

    /// <summary>
    /// Postavlja radnu teksturu AMP-a.
    /// </summary>
    public void SetUpQuadPaint()
    {
        paintEngine.SetMandalaTexture(GenerateColoredTex(1024,1024, new Color32(245,236,227,255)),paintEngine.filterMode); //FIXME Boja teksture ce biti malo tamnija zbog Shader-a
	}

	/// <summary>
	/// Postavlja FloodFill brush.
	/// </summary>
	public void SetUpFloodFillBrush()
	{
		paintEngine.SetFloodFIllBrush(currentColor,true);
		paintEngine.canDrawOnBlack=false;
	}

	/// <summary>
	/// Postavlja bitmap brush.
	/// </summary>
	public void SetUpBitmapBrush()
	{
//		if(GlobalVariables.gameMode == "Blank")
//			maskForBrush = false;
//		else
			maskForBrush = true;
		
		paintEngine.SetBitmapBrush(currentBrush,BrushProperties.Default,false,false,currentColor,maskForBrush,true,null);
		paintEngine.useLockArea = false;
//		paintEngine.drawMode = DrawMode.CustomBrush;
		paintEngine.drawMode = currentDrawMode;
	}

	/// <summary>
	/// Poziva metod za promenu palete boja i postavlja boju sa odabrane palete u trenurnoj nijansi. Poziva se klikom na neku od osnovnih boja.
	/// </summary>
	/// <param name="index">Index.</param>
	public void ChangePalette(int index)
	{
		GameObject.Find("Canvas/PaintMenu/SubMenus").GetComponent<SubMenusController>().SetPalette(index);
		ChangeColor(currentShade);
	}

	/// <summary>
	/// Menja boju u Paint-u, poziva se klikom na dugme za odredjenu nijansu.
	/// </summary>
	/// <param name="go">Go.</param>
	public void ChangeColor(GameObject go)
	{
		if(go.transform.GetChild(1).gameObject.activeInHierarchy)		//Da li je zeljena boja zakljucana?
		{
			currentColor = go.transform.parent.GetChild(0).GetComponent<Image>().color;
			paintEngine.paintColor = currentColor;
			currentShade = go.transform.parent.GetChild(0).gameObject;
			GameObject.Find("Canvas/PaintMenu/SubMenus").GetComponent<SubMenusController>().MarkShade(0);
		}
		else
		{
			currentColor = go.GetComponent<Image>().color;
			paintEngine.paintColor = currentColor;
			currentShade = go;
		}
	}

	/// <summary>
	/// Menja sliku bitmap brush-a. Zove se klikom na brush iz sub-menija.
	/// </summary>
	/// <param name="index">Index.</param>
	public void ChangeBitmap(int index)
	{
		bmpsIndex = index;
		UpdateBrushSize();

	}

	/// <summary>
	/// Poziva se prilikom promene vrednosti slajdera za BrushSize.
	/// </summary>
	public void UpdateBmpIndex()
	{
		bmpIndex = (int)brushSize.value;
	}

	/// <summary>
	/// Azurira velicinu brush-a. Poziva se prilikom OnPointerUp eventa slajdera za BrushSize.
	/// </summary>
	public void UpdateBrushSize()
	{
		currentBrush = PaintUtils.ConvertSpriteToTexture2DForBmp(bmps[bmpsIndex][4-bmpIndex]);
		if(bmpsIndex > 5)
		{
			currentDrawMode = DrawMode.CustomBrush;
			SetUpBitmapBrush();
		}
		else
		{
			currentDrawMode = DrawMode.Stamp;
			SetUpBitmapBrush();			
		}
	}

	/// <summary>
	/// Pomocna funkcija za generisanje teksture u zadatoj boji (ukoliko je c.alpha == 0, onda je tekstura transparentna).
	/// </summary>
	/// <returns>Teksturu.</returns>
	/// <param name="width">Sirina.</param>
	/// <param name="height">Visina.</param>
	/// <param name="c">Boja.</param>
	Texture2D GenerateColoredTex(int width, int height, Color c)
	{
		Texture2D tex = new Texture2D(width,height,TextureFormat.RGBA32,false);
		Color[] pix = tex.GetPixels();
		for(int i = 0; i<pix.Length;i++)
		{
			pix[i] = c;
		}
		tex.SetPixels(pix);
		tex.Apply();
		tex.filterMode = FilterMode.Point;
		return tex;
	}

	/// <summary>
	/// Postavlja drawEnabled na "true". Poziva se u startu preko Invoke() metoda kako bi se izbeglo iscrtavanje dok traje loading screen.
	/// </summary>
	void EnableDrawing()
	{
		paintEngine.drawEnabled = true;
	}

    public void NextDrawing()
    {
        if (currentDrawing < drawings.Length-1)
            currentDrawing++;
        else
            currentDrawing = 0;

        GameObject.Find("Canvas").transform.Find("PaintMenu/UI/AnimationHolder/NextDrawing").GetComponent<Button>().interactable = false;
        

        StartCoroutine("NextDrawingCoroutine");
    }

    IEnumerator NextDrawingCoroutine()
    {
        loading.SetActive (true);
        yield return new WaitForSeconds (0.02f);
        GameObject.Find ("Canvas/PaintMenu/SubMenus").GetComponent<SubMenusController> ().DeleteButton ();
        SetDrawing();
        yield return new WaitForEndOfFrame ();
        loading.SetActive (false);
        GameObject.Find("Canvas").transform.Find("PaintMenu/UI/AnimationHolder/NextDrawing").GetComponent<Button>().interactable = true;
        GameObject.Find("Canvas").transform.Find("PaintMenu/Paint/PaintPanel").localScale = Vector3.one;
        GameObject.Find("Canvas").transform.Find("PaintMenu/Paint/PaintPanel").GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
    }

    #region EndGameMethods
    public void PaintingFinished()
    {
//        if (PlayerPrefs.HasKey("ArtSchoolFinished"))
//        {
//            if (PlayerPrefs.GetString("ArtSchoolFinished") == "true")
//                GameObject.Find("Canvas").GetComponent<MenuManager>().LoadSceneAndShowTransitionScreen("MainScene");
//            else
//                MiniGameManager.Instance.MiniGameFinished();
//        }
//        else
//        {
//            PlayerPrefs.SetString("ArtSchoolFinished","true");
//            PlayerPrefs.Save();
//            MiniGameManager.Instance.MiniGameFinished();
//        }
    }
    #endregion

    // used for ArtSchool mini game (from Mandala)
    public void StartUndoCoroutine ()
    {
        if (!loading.activeInHierarchy)
            StartCoroutine ("UndoCoroutine");
    }

    IEnumerator UndoCoroutine ()
    {
        loading.SetActive (true);
        yield return new WaitForSeconds (0.02f);
        undoManager.UndoLastStep ();
        while (undoManager.doingWork)
            yield return new WaitForEndOfFrame ();
        yield return new WaitForSeconds (1f);
        loading.SetActive (false);
    }

    public void PopUpDialogYesButton ()
    {
        if (GameObject.Find ("Canvas/PopUps/PopUpDialog/AnimationHolder/Body/HeaderHolder/TextHeader").GetComponent<Text> ().text == "Go to Home Screen?") 
        {
            PaintingFinished();
        } 
        else 
        { 
            GameObject.Find ("Canvas/PaintMenu/SubMenus").GetComponent<SubMenusController> ().DeleteButton ();
            GameObject.Find ("Canvas").GetComponent<MenuManager>().ClosePopUpMenu (GameObject.Find ("Canvas/PopUps/PopUpDialog"));
        }
    }
}




















