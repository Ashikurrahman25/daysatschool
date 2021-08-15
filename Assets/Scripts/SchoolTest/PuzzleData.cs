using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="PuzzleData",menuName="Create Puzzle Data")]
public class PuzzleData : ScriptableObject
{
    public List<Puzzle> puzzles;
}


[System.Serializable]
public class Puzzle
{
    public Sprite fullImage;
    public List<Sprite> parts;
}
