using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterClothes : MonoBehaviour {

    public DressUpDataGirl girlData;
    public DressUpDataBoy boyData;

    public bool firstCharacter;
    public bool girl;

    [Header("Image-i za odecu")]
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


    public void Start()
    {
        LoadClothes();
    }


    public void LoadClothes()
    {
        if(firstCharacter)
        {
            if(girl&&girlData!=null)
            {
                if(PlayerPrefs.HasKey("Girl1Dress"))
                    dress.sprite = girlData.dresses[PlayerPrefs.GetInt("Girl1Dress")].sprite;
                if(PlayerPrefs.HasKey("Girl1Dress"))
                    PamPamRight.sprite = girlData.bracelets[PlayerPrefs.GetInt("Girl1Bracelet")].sprite;
                if(PlayerPrefs.HasKey("Girl1Dress"))
                    frontHair.sprite = girlData.hats[PlayerPrefs.GetInt("Girl1Hat")].sprite;
                if(PlayerPrefs.HasKey("Girl1Dress"))
                    shoeRight.sprite = girlData.shoes[PlayerPrefs.GetInt("Girl1Shoes")].sprite;
            }
            else if(boyData!=null)
            {
                if(PlayerPrefs.HasKey("Boy1Pants"))
                    pants.sprite = boyData.pants[PlayerPrefs.GetInt("Boy1Pants")].sprite;
                if(PlayerPrefs.HasKey("Boy1Hoodie"))
                    shirt.sprite = boyData.hoodies[PlayerPrefs.GetInt("Boy1Hoodie")].sprite;
                if(PlayerPrefs.HasKey("Boy1Shoes"))
                    shoeRight.sprite = boyData.shoes[PlayerPrefs.GetInt("Boy1Shoes")].sprite;
                if(PlayerPrefs.HasKey("Boy1Hat"))
                    frontHair.sprite = boyData.hats[PlayerPrefs.GetInt("Boy1Hat")].sprite;
            }
        }
        else
        {
            if(girl&&girlData!=null)
            {
                if(PlayerPrefs.HasKey("Girl2Dress"))
                    dress.sprite = girlData.dresses[PlayerPrefs.GetInt("Girl2Dress")].sprite;
                if(PlayerPrefs.HasKey("Girl2Bracelet"))
                    PamPamRight.sprite = girlData.bracelets[PlayerPrefs.GetInt("Girl2Bracelet")].sprite;
                if(PlayerPrefs.HasKey("Girl2Hat"))
                    frontHair.sprite = girlData.hats[PlayerPrefs.GetInt("Girl2Hat")].sprite;
                if(PlayerPrefs.HasKey("Girl2Shoes"))
                    shoeRight.sprite = girlData.shoes[PlayerPrefs.GetInt("Girl2Shoes")].sprite;
            }
            else if(boyData!=null)
            {
                if(PlayerPrefs.HasKey("Boy2Pants"))
                    pants.sprite = boyData.pants[PlayerPrefs.GetInt("Boy2Pants")].sprite;
                if(PlayerPrefs.HasKey("Boy2Hoodie"))
                    shirt.sprite = boyData.hoodies[PlayerPrefs.GetInt("Boy2Hoodie")].sprite;
                if(PlayerPrefs.HasKey("Boy2Shoes"))
                    shoeRight.sprite = boyData.shoes[PlayerPrefs.GetInt("Boy2Shoes")].sprite;
                if(PlayerPrefs.HasKey("Boy2Hat"))
                    frontHair.sprite = boyData.hats[PlayerPrefs.GetInt("Boy2Hat")].sprite;
            }
        }
    }
}
