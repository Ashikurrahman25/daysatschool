using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

[CreateAssetMenu(fileName="GirlDressUpData",menuName="DressUpDataGirl")]
public class DressUpDataGirl : ScriptableObject {

    public List<DressUpItem> hats;
    public List<DressUpItem> dresses;
    public List<DressUpItem> bracelets;
    public List<DressUpItem> shoes;


    public void Initialize()
    {
        MatchCollection indeksi;
        string indexes = PlayerPrefs.GetString("GirlHatsUnlocked");
        if (indexes != "")
        {
            indeksi = Regex.Matches(indexes, @"[0-9]+");
            foreach (Match m in indeksi)
            {
                hats[int.Parse(m.Value)].locked = false;
            }
        }

        indexes = PlayerPrefs.GetString("GirlDresses");
        if (indexes != "")
        {
            indeksi = Regex.Matches(indexes, @"[0-9]+");
            foreach (Match m in indeksi)
            {
                dresses[int.Parse(m.Value)].locked = false;
            }
        }

        indexes = PlayerPrefs.GetString("GirlBraceletsUnlocked");
        if (indexes != "")
        {
            indeksi = Regex.Matches(indexes, @"[0-9]+");
            foreach (Match m in indeksi)
            {
                bracelets[int.Parse(m.Value)].locked = false;
            }
        }

        indexes = PlayerPrefs.GetString("GirlShoesUnlocked");
        if (indexes != "")
        {
            indeksi = Regex.Matches(indexes, @"[0-9]+");
            foreach (Match m in indeksi)
            {
                shoes[int.Parse(m.Value)].locked = false;
            }
        }

    }
}
