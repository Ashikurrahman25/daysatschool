using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// <para>Scene: Gameplay</para>
/// <para>Object: Dugmeta za odabir stikera u SubMenus</para>
/// <para>Description: Skripta za poziv funkcije za instanciranje stikera</para>
/// </summary>
public class StickerClicked : MonoBehaviour {


	public void StickerClick()
	{
		GameObject.Find("Canvas/PaintMenu/Paint/PaintPanel/StickersHolder").GetComponent<StickersController>().InstantiateSticker(GetComponent<Image>().sprite);
        GameObject.Find("Canvas").GetComponent<MenuManager>().PlayButtonClickSound();
    }
}




