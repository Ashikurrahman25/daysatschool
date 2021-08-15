using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="FindTheDifferenceData",menuName="Create Find The Difference Data")]
public class FindTheDifferenceData : ScriptableObject 
{
    public List<FindTheDifferenceLevel> levels;    
}

[System.Serializable]
public class FindTheDifferenceLevel
{
    public Sprite picture1;
    public Sprite picture2;
    public List<DifferenceObject> differences;

    public FindTheDifferenceLevel()
    {
        differences = new List<DifferenceObject>();
    }
}

[System.Serializable]
public class DifferenceObject
{
    public DifferencePicture pictureId;
    public Vector3 anchoredPosition3D;
    public Vector2 sizeDelta;
    public Vector2 anchorMax,anchorMin;
}
[System.Serializable]
public enum DifferencePicture
{
    LeftPicture,
    RightPicture
}

