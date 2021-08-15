using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

[CreateAssetMenu(fileName="BoyDressUpData",menuName="DressUpDataBoy")]
public class DressUpDataBoy : ScriptableObject 
{
    public List<DressUpItem> hats;
    public List<DressUpItem> hoodies;
    public List<DressUpItem> pants;
    public List<DressUpItem> shoes;

    public void Initialize()
    {
        MatchCollection indeksi;
        string indexes = PlayerPrefs.GetString("BoyHatsUnlocked");
        if(indexes!="")
        {
            indeksi=Regex.Matches(indexes,@"[0-9]+");
            foreach (Match m in indeksi)
            {
                hats[int.Parse(m.Value)].locked = false;
            }
        }

        indexes = PlayerPrefs.GetString("BoyShirtsUnlocked");
        if(indexes!="")
        {
            indeksi=Regex.Matches(indexes,@"[0-9]+");
            foreach (Match m in indeksi)
            {
                hoodies[int.Parse(m.Value)].locked = false;
            }
        }

        indexes = PlayerPrefs.GetString("BoyPantsUnlocked");
        if(indexes!="")
        {
            indeksi=Regex.Matches(indexes,@"[0-9]+");
            foreach (Match m in indeksi)
            {
                pants[int.Parse(m.Value)].locked = false;
            }
        }

        indexes = PlayerPrefs.GetString("BoyShoesUnlocked");
        if(indexes!="")
        {
            indeksi=Regex.Matches(indexes,@"[0-9]+");
            foreach (Match m in indeksi)
            {
                shoes[int.Parse(m.Value)].locked = false;
            }
        }

    }
}

[System.Serializable]
public class DressUpItem
{
    public bool locked;
    public Sprite icon;
    public Sprite sprite;
}
