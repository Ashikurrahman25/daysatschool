using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;



[CreateAssetMenu(menuName="ClassroomDecorationData",fileName="ClassroomDecorationDataSO")]
public class DecorationData : ScriptableObject {

    public List<DecorationItem> walls;
    public List<DecorationItem> benches;
    public List<DecorationItem> chairs;
    public List<DecorationItem> boards;
    public List<DecorationItem> shelves;


    public void Initialize()
    {
        MatchCollection indeksi;
        string indexes = PlayerPrefs.GetString("WallsUnlocked");
        if(indexes!="")
        {
            indeksi=Regex.Matches(indexes,@"[0-9]+");
            foreach (Match m in indeksi)
            {
                walls[int.Parse(m.Value)].locked = false;
            }
        }
        indexes = PlayerPrefs.GetString("BenchesUnlocked");
        if(indexes!="")
        {
            indeksi=Regex.Matches(indexes,@"[0-9]+");
            foreach (Match m in indeksi)
            {
                benches[int.Parse(m.Value)].locked = false;
            }
        }
        indexes = PlayerPrefs.GetString("ChairsUnlocked");
        if(indexes!="")
        {
            indeksi=Regex.Matches(indexes,@"[0-9]+");
            foreach (Match m in indeksi)
            {
                chairs[int.Parse(m.Value)].locked = false;
            }
        }
        indexes = PlayerPrefs.GetString("BoardsUnlocked");
        if(indexes!="")
        {
            indeksi=Regex.Matches(indexes,@"[0-9]+");
            foreach (Match m in indeksi)
            {
                boards[int.Parse(m.Value)].locked = false;
            }
        }
        indexes = PlayerPrefs.GetString("ShelvesUnlocked");
        if(indexes!="")
        {
            indeksi=Regex.Matches(indexes,@"[0-9]+");
            foreach (Match m in indeksi)
            {
                shelves[int.Parse(m.Value)].locked = false;
            }
        }
    }
}



[System.Serializable]
public class DecorationItem
{
    public bool locked = false;
    public Sprite icon;
    public Sprite content;

    [Header("Ako ne menjas sprite vec palis neki holder")]
    //Ako ne menjam sprite nego samo palim holder
    public int index;
}