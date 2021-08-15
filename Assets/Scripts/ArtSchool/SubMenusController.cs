using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// <para>Scene: Gameplay</para>
/// <para>Object: SubMenus</para>
/// <para>Description: Skripta za hendlovanje pod-menija (odabir stikera, undo, brisanje, odabir boja i brush-eva)</para>
/// </summary>
public class SubMenusController : MonoBehaviour {

	public AdvancedMobilePaint.AdvancedMobilePaint paintEngine;
	public Transform stickersHolder;
	public CanvasGroup brushControlls;
	public CanvasGroup brushBitmaps;
	public Animator colorsAndBrushes;
	public Animator stickers;
	public Transform stickerButtonsHolder;

	[Header ("Color palette")]
	public Transform shadesHolder;
	public PaletteAsset[] palettes;

	[Header("Check marks")]
	public Transform colorsHolder;
	public Transform brushesHolder;

	[Header("Button Icons")]
	public Image[] butonIcons;


	public static bool StickerMode;
	bool ShadesShown;

	void Start()
	{
		SetPalette(0);
		LockStickers();
		StickerMode = false;
		ShadesShown = true;
		brushControlls.alpha = 0;
		brushBitmaps.alpha = 0;
		MarkCurrentDrawMode(1);
	}

	public void StickersButton()
	{
		colorsAndBrushes.Play("HideColorsAndBrushes");
		stickers.Play("ShowStickers");
		StickerMode = true;
		paintEngine.drawEnabled = false;
		SetStickersDragEnabled(true);
		if(stickersHolder.childCount >= 1)
			StickersController.SelectSticker(stickersHolder.GetChild(stickersHolder.childCount-1).gameObject);

		MarkCurrentDrawMode (0);
	}

	public void FloodFillButton()
	{
		brushControlls.alpha = 0;
		brushBitmaps.alpha = 0;
		brushControlls.interactable = false;
		brushBitmaps.interactable = false;

		if(StickerMode)
		{
			stickers.Play("HideStickers");
			colorsAndBrushes.Play("ShowColorsAndBrushes");
			StickerMode = false;
//			paintEngine.drawEnabled = true;
			SetStickersDragEnabled(false);
			StickersController.SelectSticker(null);
		}

		MarkCurrentDrawMode(1);
	}

	public void BrushButton()
	{
		brushControlls.alpha = 1;
		brushBitmaps.alpha = 1;
		brushControlls.interactable = true;
		brushBitmaps.interactable = true;

		if(StickerMode)
		{
			stickers.Play("HideStickers");
			colorsAndBrushes.Play("ShowColorsAndBrushes");
			StickerMode = false;
			ShadesShown = true;
//			paintEngine.drawEnabled = true;
			SetStickersDragEnabled(false);
			StickersController.SelectSticker(null);
		}

		MarkCurrentDrawMode (2);
	}

	public void DeleteButton()
	{
        GameObject.Find("Canvas").transform.Find("PaintMenu/UI/AnimationHolder/Delete").GetComponent<Button>().interactable = false;
		GameObject.Find("PaintSetUpManager").GetComponent<PaintSetUp>().SetUpQuadPaint();
		GameObject.Find("Canvas/PaintMenu/Paint/PaintPanel").GetComponent<AdvancedMobilePaint.PaintUndoManager>().ClearSteps();

		for(int i = 0; i<stickersHolder.childCount; i++)
		{
			Destroy (stickersHolder.GetChild(i).gameObject);
		}
		StickersController.SelectSticker(null);
		if(StickerMode)
			paintEngine.drawEnabled = false;

        GameObject.Find("Canvas").transform.Find("PaintMenu/UI/AnimationHolder/Delete").GetComponent<Button>().interactable = true;
    }

    public void ShadesIn()
	{
		if(ShadesShown)
		{
			colorsAndBrushes.Play("ShadesIn");
			ShadesShown = false;
		}
	}

	public void ShadesOut()
	{
		if(!ShadesShown)
		{
			colorsAndBrushes.Play("ShadesOut");
			ShadesShown = true;
		}
	}

	/// <summary>
	/// Postavlja odgovarajucu paletu poja i zakljucava nijanse u zavisnosti od odabrane palete.
	/// </summary>
	/// <param name="index">Index palete boja.</param>
	public void SetPalette(int index)
	{
		for (int i = 0; i<shadesHolder.childCount; i++)
		{
			shadesHolder.GetChild(i).GetComponent<Image>().color = palettes[index].color[i];

            //PAVLE artclass ovde bilo zakljucano
//            if(!GlobalVariables.unlockAll)
//			{
//                if(GlobalVariables.shadesLockIndices[index][i] == 1)
//					shadesHolder.GetChild(i).GetChild(1).gameObject.SetActive(true);
//				else
					shadesHolder.GetChild(i).GetChild(1).gameObject.SetActive(false);
//			}
		}
//        GlobalVariables.colorToUnlock = index;
	}

	void SetStickersDragEnabled(bool b)
	{
		for(int i = 0; i<stickersHolder.childCount; i++)
		{
			stickersHolder.GetChild(i).GetComponent<Sticker>().enabled = b;
		}
	}

	/// <summary>
	/// Funkcija za paljenje i gasenje Check ikonice za nijanse.
	/// </summary>
	/// <param name="index">Index nijanse koja treba da bude cekirana.</param>
	public void MarkShade(int index)
	{
		for(int i = 0; i< shadesHolder.childCount; i++)
		{
			shadesHolder.GetChild(i).GetChild(0).gameObject.SetActive(false);
		}
		shadesHolder.GetChild(index).GetChild(0).gameObject.SetActive(true);
	}

	public void MarkColor(int index)
	{
		for(int i = 0; i< colorsHolder.childCount; i++)
		{
			colorsHolder.GetChild(i).GetChild(0).gameObject.SetActive(false);
		}
		colorsHolder.GetChild(index).GetChild(0).gameObject.SetActive(true);
	}

	public void MarkBrushe(int index)
	{
		for(int i = 0; i< brushesHolder.childCount; i++)
		{
			brushesHolder.GetChild(i).GetChild(0).gameObject.SetActive(false);
		}
		brushesHolder.GetChild(index).GetChild(0).gameObject.SetActive(true);
	}

	/// <summary>
	/// Obelezava koji mod je tenutno aktivan.
	/// </summary>
	/// <param name="index">0 za stikere, 1 za FloodFill, 2 za brush mod.</param>
	public void MarkCurrentDrawMode(int index)
	{
		for(int i = 0 ; i<butonIcons.Length; i++)
		{
			butonIcons[i].color = Color.white;
		}

		butonIcons[index].color = new Color32(203,255,0,255);
	}

    //PAVLE bilo zakljucano vidi sta ces 
	public void LockStickers()
	{
		for(int i=0;i<stickerButtonsHolder.childCount;i++)
		{

            //if (!GlobalVariables.unlockAll)
            //{
            //    if (GlobalVariables.stickersLockIndices[i] == 1)
            //        stickerButtonsHolder.GetChild(i).GetChild(0).gameObject.SetActive(true);
            //    else
                    stickerButtonsHolder.GetChild(i).GetChild(0).gameObject.SetActive(false);
			//}
		}
	}

	public void UnlockCurrentSticker()
	{
//        Debug.Log("Sticker to unlcok: " + GlobalVariables.stickerToUnlock);
//        stickerButtonsHolder.GetChild(GlobalVariables.stickerToUnlock).GetChild(0).gameObject.SetActive(false);
//        stickerButtonsHolder.GetChild(GlobalVariables.stickerToUnlock).GetComponent<Button>().onClick.Invoke();
	}

	public void UnlockCurrentShade()
	{
//        shadesHolder.GetChild(GlobalVariables.shadeToUnlock).GetChild(1).gameObject.SetActive(false);
//        shadesHolder.GetChild(GlobalVariables.shadeToUnlock).GetComponent<Button>().onClick.Invoke();
	}

	public void ColorsButtonLeft()
	{
        ScrollRect target = transform.Find("ColorsAndBrushes/AnimationHolder/Colors/ScrollRect").GetComponent<ScrollRect>();

        target.normalizedPosition =new Vector2(target.normalizedPosition.x-0.1f,target.normalizedPosition.y);
	}

	public void ColorsButtonRight()
	{
        ScrollRect target = transform.Find("ColorsAndBrushes/AnimationHolder/Colors/ScrollRect").GetComponent<ScrollRect>();

        target.normalizedPosition =new Vector2(target.normalizedPosition.x+0.1f,target.normalizedPosition.y);
//		Vector3 target = colorsHolder.transform.localPosition - new Vector3 (75,0,0);
//		if(target.x < -200f)
//			target = new Vector3(-200f,0,0);
	}
}





















