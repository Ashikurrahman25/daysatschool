using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="FoodData",menuName="FoodData")]
public class FoodData : ScriptableObject 
{
    public List<Ingredient> ingredients;
    public List<Recipe> recipes;
}
[System.Serializable]
public struct Food
{
    public Sprite image;
    public string name;
}
[System.Serializable]
public struct Ingredient
{
    public Sprite image;
}
[System.Serializable]
public struct Recipe
{
    public List<Ingredient> ingredients;
    public Sprite image;
}

