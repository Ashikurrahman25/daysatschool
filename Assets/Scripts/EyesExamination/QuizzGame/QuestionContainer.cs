using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestionsData", menuName = "Webelinx/QuestionData")]
public class QuestionContainer : ScriptableObject {
   
    public SpriteArray[] questions;
}

[System.Serializable]
public class SpriteArray
{
    public Sprite [] sprites;
}
