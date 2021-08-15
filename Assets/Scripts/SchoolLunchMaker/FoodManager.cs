using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public partial class FoodManager : MonoBehaviour {

	static FoodManager instance;
	public static FoodManager Instance
	{
		get
		{
			if(instance == null)
				instance = GameObject.FindObjectOfType(typeof(FoodManager)) as FoodManager;

			return instance;
		}
	}

    public FoodData foodData;

//	Transform foodSlot1;
//	Transform foodSlot2;
//	Transform foodSlot3;
//	Transform foodSlot4;
//	Transform foodSlot5;
	[HideInInspector] public GameObject arrowLeft;
	[HideInInspector] public GameObject arrowRight;
//	Transform kitchenHolder;

	int currentRecipeIndex = 0;
	Transform recipe1;
	Transform recipe2;


	public List<Ingredient> ingredients;
	

    private Transform canvas;
    private List<Sprite> inMixer;
    private List<Recipe> validRecipes;

    private int recipesFinished;
	void Awake ()
	{
        inMixer = new List<Sprite>();

		recipe1 = transform.Find("AnimationHolder/MakeYourOwnFood/Recipe1");
		recipe2 = transform.Find("AnimationHolder/MakeYourOwnFood/Recipe2");
	}

	void Start ()
	{
        canvas = GameObject.Find("Canvas").transform;
        Transform ingredientsHolder = canvas.transform.Find("FoodShop/AnimationHolder/MakeYourOwnFood/ShelfHolder/Ingredients");

        validRecipes = new List<Recipe>(foodData.recipes);

        int i = 0;
        foreach(Ingredient ingredient in foodData.ingredients)
        {
            ingredientsHolder.GetChild(i).GetComponent<Image>().sprite = ingredient.image;
            ingredientsHolder.GetChild(i).GetChild(0).GetComponent<Image>().sprite = ingredient.image;
            Button btn = ingredientsHolder.GetChild(i).GetComponent<Button>();
            ingredientsHolder.GetChild(i).GetComponent<Button>().onClick.AddListener(delegate
                {
                    btn.interactable=false;
                    AddIngredient(ingredient.image);
                    btn.transform.GetChild(0).GetComponent<Image>().enabled=true;
                    StartCoroutine(moveIngredientToMixer(btn.transform.GetChild(0),btn.transform.position ,transform.Find("AnimationHolder/MakeYourOwnFood/MixerBottom/BowlFront").position));
                });
            i++;
        }
        setRecipe(recipe1,0);
        setRecipe(recipe2,1);
	}

	void CheckForTutorialMakeYourOwnFood()
	{
        //PAVLE tutorial za make food
//		if(VariablesManager.Instance.tutorialFinished == 1)
//		{
//			VariablesManager.Instance.currentLocationForEscapeButton = VariablesManager.location.MakeFoodTutorial;
//			transform.Find("AnimationHolder/PopUpTutorial").gameObject.SetActive(true);
//			transform.Find("AnimationHolder/PopUpTutorial").GetComponent<Animator>().Play("Open");
//			transform.Find("AnimationHolder/PopUpTutorial/AnimationHolder/Body/ContentHolder").GetComponent<Animator>().Play("TutorialMakeYourOnFood");
//			VariablesManager.Instance.tutorialFinished = 2;
//			PlayerPrefs.SetInt("TutFinished",2);
//			PlayerPrefs.Save();
//			//UPDATE!!!!!
//			if(GlobalVariables.nativeAdsAllowed && !GlobalVariables.removeAdsBought)
//				transform.Find("AnimationHolder/AllCategories/NativeAdsHolder").GetComponent<FacebookNativeAd>().CancelLoading();
//		}
//		else
//		{
//			StartCoroutine(SetSortingOrderOnMixerFront());
//			//UPDATE!!!!!
//			if(GlobalVariables.nativeAdsAllowed && !GlobalVariables.removeAdsBought)
//			{
//				transform.Find("AnimationHolder/AllCategories/NativeAdsHolder").GetComponent<FacebookNativeAd>().CancelLoading();
//				transform.Find("AnimationHolder/MakeYourOwnFood/NativeAdsHolder").GetComponent<FacebookNativeAd>().LoadAd();
//			}
//		}
	}

	public void CloseTutorialMakeYourOwnFood()
	{
		transform.Find("AnimationHolder/PopUpTutorial").GetComponent<Animator>().Play("MainMenuClosed");
		Invoke("SetSortingOrderOnMixerAfterTutorial",0.75f);
		Invoke("ShowNativeAdAfterTutorial",0.35f);
	}

	void SetSortingOrderOnMixerAfterTutorial()
	{
		transform.Find("AnimationHolder/PopUpTutorial").gameObject.SetActive(false);
		StartCoroutine(SetSortingOrderOnMixerFront());
	}

	public void OpenMakeYourOwnFoodInstant()
	{
		transform.Find("AnimationHolder/MakeYourOwnFood").gameObject.SetActive(true);
		transform.Find("AnimationHolder/AllCategories").GetComponent<RectTransform>().anchoredPosition = new Vector2(-1200,0);
		transform.Find("AnimationHolder/MakeYourOwnFood").GetComponent<RectTransform>().anchoredPosition = new Vector2(0,0);
		transform.Find("AnimationHolder/MakeYourOwnFood").GetComponent<Animator>().Play("InstantShow");
		transform.Find("AnimationHolder").gameObject.SetActive(true);
		ResetMixer();
		CheckForTutorialMakeYourOwnFood();
	}
	
	public void ResetMixer()
	{
		transform.Find("AnimationHolder/MakeYourOwnFood/ArrowLeft").gameObject.SetActive(true);
		transform.Find("AnimationHolder/MakeYourOwnFood/ArrowRight").gameObject.SetActive(true);
		transform.Find("AnimationHolder/MakeYourOwnFood/ArrowLeft").GetComponent<Button>().interactable = true;
		transform.Find("AnimationHolder/MakeYourOwnFood/ArrowRight").GetComponent<Button>().interactable = true;
		
		Transform ingredientsHolder = transform.Find("AnimationHolder/MakeYourOwnFood/ShelfHolder/Ingredients");
        for(int i=0;i<ingredientsHolder.childCount;i++)
		{
            if(recipesFinished<4)
                ingredientsHolder.Find("Item"+i.ToString()).GetComponent<Button>().interactable = true;
//            ingredientsHolder.Find("Item"+i.ToString()).GetComponent<Button>().interactable = true;
//            ingredientsHolder.Find("Item"+i.ToString()).GetComponent<Button>().interactable = true;
//            ingredientsHolder.Find("Item"+i.ToString()).GetComponent<Button>().interactable = true;
//            ingredientsHolder.Find("Item"+i.ToString()).GetComponent<Button>().interactable = true;
//			ingredientsHolder.Find("Item"+i.ToString()).GetComponent<Button>().interactable = true;
		}
		transform.Find("AnimationHolder/MakeYourOwnFood/MixerBottom").GetComponent<Animator>().Play("MixerNormalState");

		currentRecipeIndex = 0;
		if(transform.Find("AnimationHolder/MakeYourOwnFood/ButtonResetMixer").gameObject.activeSelf)
		{
			transform.Find("AnimationHolder/MakeYourOwnFood/ButtonResetMixer").gameObject.SetActive(false);
			transform.Find("AnimationHolder/MakeYourOwnFood/ButtonResetMixer").GetComponent<Image>().enabled = true;
		}
        inMixer.Clear();
        validRecipes = new List<Recipe>(foodData.recipes);
        recipe1.gameObject.SetActive(true);
        recipe2.gameObject.SetActive(true);
        setRecipe(recipe1, 0);
        setRecipe(recipe2, 1);
	}

	public void SetSortingOrderOnMixerFrontAfterGetCoins()
	{
		StartCoroutine(SetSortingOrderOnMixerFront());
	}

	IEnumerator SetSortingOrderOnMixerFront()
	{
		yield return new WaitForSeconds(0.1f);
		transform.Find("AnimationHolder/MakeYourOwnFood/MixerBottom/BowlFront").GetComponent<Canvas>().overrideSorting = true;
		//transform.Find("AnimationHolder/MakeYourOwnFood/BowlFront").GetComponent<Canvas>().renderOrder = value;
	}

	void setRecipe(Transform recipe, int index)
	{
		recipe.Find("ItemResult").GetComponent<Image>().sprite = validRecipes[index].image;
		recipe.Find("Ingredients/Item1").GetComponent<Image>().sprite = validRecipes[index].ingredients[0].image;
		recipe.Find("Ingredients/Item2").GetComponent<Image>().sprite = validRecipes[index].ingredients[1].image;
		recipe.Find("Ingredients/Item3").GetComponent<Image>().sprite = validRecipes[index].ingredients[2].image;

        if(inMixer.Contains(validRecipes[index].ingredients[0].image))
		{
			if(!recipe.Find("Ingredients/Item1/CheckMark").GetComponent<Image>().enabled)
			{
				recipe.Find("Ingredients/Item1/CheckMark").GetComponent<Image>().enabled = true;
//				recipe.Find("Ingredients/Item1/CheckMark").GetComponent<Animator>().Play("RecipeCheckMarkShow");
			}
		}
		else
		{
			recipe.Find("Ingredients/Item1/CheckMark").GetComponent<Image>().enabled = false;
		}
        if(inMixer.Contains(validRecipes[index].ingredients[1].image))
		{
			if(!recipe.Find("Ingredients/Item2/CheckMark").GetComponent<Image>().enabled)
			{
				recipe.Find("Ingredients/Item2/CheckMark").GetComponent<Image>().enabled = true;
//				recipe.Find("Ingredients/Item2/CheckMark").GetComponent<Animator>().Play("RecipeCheckMarkShow");
			}
		}
		else
		{
			recipe.Find("Ingredients/Item2/CheckMark").GetComponent<Image>().enabled = false;
		}
        if(inMixer.Contains(validRecipes[index].ingredients[2].image))
		{
			if(!recipe.Find("Ingredients/Item3/CheckMark").GetComponent<Image>().enabled)
			{
				recipe.Find("Ingredients/Item3/CheckMark").GetComponent<Image>().enabled = true;
//				recipe.Find("Ingredients/Item3/CheckMark").GetComponent<Animator>().Play("RecipeCheckMarkShow");
			}
		}
		else
		{
			recipe.Find("Ingredients/Item3/CheckMark").GetComponent<Image>().enabled = false;
		}

		if(validRecipes[index].ingredients.Count == 4)
		{
			float yPos = 364;
			for(int i=0;i<recipe.Find("Ingredients").childCount;i++)
			{
				recipe.Find("Ingredients").GetChild(i).GetComponent<RectTransform>().anchoredPosition = new Vector2(recipe.Find("Ingredients").GetChild(i).GetComponent<RectTransform>().anchoredPosition.x,yPos);
				yPos -= 91;
			}
			recipe.Find("Ingredients/Item4").GetComponent<Image>().sprite = validRecipes[index].ingredients[3].image;
            if(inMixer.Contains(validRecipes[index].ingredients[3].image))
			{
				if(!recipe.Find("Ingredients/Item4/CheckMark").GetComponent<Image>().enabled)
				{
					recipe.Find("Ingredients/Item4/CheckMark").GetComponent<Image>().enabled = true;
//					recipe.Find("Ingredients/Item4/CheckMark").GetComponent<Animator>().Play("RecipeCheckMarkShow");
				}
			}
			else
			{
				recipe.Find("Ingredients/Item4/CheckMark").GetComponent<Image>().enabled = false;
			}

			recipe.Find("Ingredients/Plus3").gameObject.SetActive(true);
			recipe.Find("Ingredients/Item4").gameObject.SetActive(true);
		}
		else
		{
			float yPos = 220;
			for(int i=0;i<recipe.Find("Ingredients").childCount;i++)
			{
				recipe.Find("Ingredients").GetChild(i).GetComponent<RectTransform>().anchoredPosition = new Vector2(recipe.Find("Ingredients").GetChild(i).GetComponent<RectTransform>().anchoredPosition.x,yPos);
				yPos -= 82f;
			}
			recipe.Find("Ingredients/Plus3").gameObject.SetActive(false);
			recipe.Find("Ingredients/Item4").gameObject.SetActive(false);
		}
	}

	void EnableArrowButtonsAgain()
	{
		transform.Find("AnimationHolder/MakeYourOwnFood/ArrowLeft").GetComponent<Button>().interactable = true;
		transform.Find("AnimationHolder/MakeYourOwnFood/ArrowRight").GetComponent<Button>().interactable = true;
	}

	public void NextRecipes()
	{
		recipe1.GetComponent<Animator>().Play("Recipe1MoveRightToLeft");
		recipe2.GetComponent<Animator>().Play("Recipe2MoveRightToLeft");
		transform.Find("AnimationHolder/MakeYourOwnFood/ArrowLeft").GetComponent<Button>().interactable = false;
		transform.Find("AnimationHolder/MakeYourOwnFood/ArrowRight").GetComponent<Button>().interactable = false;
		Invoke("NextRecipesSwitch",0.1f);
		Invoke("EnableArrowButtonsAgain",0.5f);
	}

	void NextRecipesSwitch()
	{
		currentRecipeIndex += 2;
		currentRecipeIndex = currentRecipeIndex % validRecipes.Count;
		setRecipe(recipe1,currentRecipeIndex);
		int tempRecipeIndex = currentRecipeIndex + 1;
		tempRecipeIndex = tempRecipeIndex % validRecipes.Count;
		setRecipe(recipe2,tempRecipeIndex);
//		SoundManager.Instance.Play_ButtonClick4();
	}

	public void PreviousRecipes()
	{
		recipe1.GetComponent<Animator>().Play("Recipe1MoveLeftToRight");
		recipe2.GetComponent<Animator>().Play("Recipe2MoveLeftToRight");
		transform.Find("AnimationHolder/MakeYourOwnFood/ArrowLeft").GetComponent<Button>().interactable = false;
		transform.Find("AnimationHolder/MakeYourOwnFood/ArrowRight").GetComponent<Button>().interactable = false;
		Invoke("PreviousRecipesSwitch",0.1f);
		Invoke("EnableArrowButtonsAgain",0.5f);
	}

	public void PreviousRecipesSwitch()
	{
		currentRecipeIndex -= 2;
		if(currentRecipeIndex < 0) currentRecipeIndex = validRecipes.Count + currentRecipeIndex;
		setRecipe(recipe1,currentRecipeIndex);
		int tempRecipeIndex = currentRecipeIndex + 1;
		tempRecipeIndex = tempRecipeIndex % validRecipes.Count;
		setRecipe(recipe2,tempRecipeIndex);
//		SoundManager.Instance.Play_ButtonClick4();
	}

	public void DisableButton(Button button)
	{
		button.interactable = false;
		Transform item = button.transform.Find("MovableItem");
		item.GetComponent<Image>().enabled = true;
		StartCoroutine(moveIngredientToMixer(item,button.transform.position ,transform.Find("AnimationHolder/MakeYourOwnFood/MixerBottom/BowlFront").position));
	}

	IEnumerator moveIngredientToMixer(Transform ingredient,Vector3 startPos, Vector3 targetPosition)
	{
        ingredient.localScale = Vector3.one;
        ingredient.position = startPos;
		float t=4;
		while(ingredient.position.y > targetPosition.y + 0.1f)
		{
			ingredient.position = Vector3.Lerp(ingredient.position,targetPosition, t*Time.deltaTime);
			yield return null;
			t+=0.2f;//=Time.deltaTime;
			ingredient.localScale -= Vector3.one*0.0175f;
		}
        ingredient.localScale = Vector3.zero;
	}

	public void AddIngredient(Sprite index)
	{
		currentRecipeIndex = 0;
		bool hasMatch = false;
		int counter = 0;
        inMixer.Add(index);

        for (int i=validRecipes.Count-1;i>=0;i--)
        {
            hasMatch = false;
            foreach (Ingredient ing in validRecipes[i].ingredients)
                if (ing.image == index)
                {
                    hasMatch = true;
                    break;
                }
            if (!hasMatch)
                validRecipes.RemoveAt(i);
        }

        for (int i = validRecipes.Count - 1; i >= 0; i--)
        {
            if (validRecipes[i].ingredients.Count == inMixer.Count)
            {
                bool recipeFinished = true;
                foreach (Ingredient ingr in validRecipes[i].ingredients)
                {
                    if (!inMixer.Contains(ingr.image))
                    {
                        recipeFinished = false;
                        break;
                    }
                }
                if(recipeFinished)
                {
                    RecipeFinished(validRecipes[i]);
                }
            }
            else
                continue;
        }

		if(!transform.Find("AnimationHolder/MakeYourOwnFood/ButtonResetMixer").gameObject.activeSelf)
			transform.Find("AnimationHolder/MakeYourOwnFood/ButtonResetMixer").gameObject.SetActive(true);

		if(validRecipes.Count == 0)
		{
            canvas.transform.Find("FoodShop/AnimationHolder/MakeYourOwnFood/ButtonResetMixer/HandHolder").gameObject.SetActive(true);
			recipe1.gameObject.SetActive(false);
			recipe2.gameObject.SetActive(false);
			transform.Find("AnimationHolder/MakeYourOwnFood/ArrowLeft").gameObject.SetActive(false);
			transform.Find("AnimationHolder/MakeYourOwnFood/ArrowRight").gameObject.SetActive(false);
		}
		else if(validRecipes.Count == 1)
		{
            canvas.transform.Find("FoodShop/AnimationHolder/MakeYourOwnFood/ButtonResetMixer/HandHolder").gameObject.SetActive(false);
            setRecipe(recipe1,currentRecipeIndex);
			recipe2.gameObject.SetActive(false);
			transform.Find("AnimationHolder/MakeYourOwnFood/ArrowLeft").gameObject.SetActive(false);
			transform.Find("AnimationHolder/MakeYourOwnFood/ArrowRight").gameObject.SetActive(false);
		}
		else
		{
			if(validRecipes.Count == 2||validRecipes.Count>2)
			{
                canvas.transform.Find("FoodShop/AnimationHolder/MakeYourOwnFood/ButtonResetMixer/HandHolder").gameObject.SetActive(false);
                transform.Find("AnimationHolder/MakeYourOwnFood/ArrowLeft").gameObject.SetActive(false);
				transform.Find("AnimationHolder/MakeYourOwnFood/ArrowRight").gameObject.SetActive(false);
			}
			setRecipe(recipe1,currentRecipeIndex);
			setRecipe(recipe2,currentRecipeIndex+1);
		}
	}

	void RecipeFinished(Recipe targetRecipe)
	{
		if(transform.Find("AnimationHolder/MakeYourOwnFood/ButtonResetMixer").gameObject.activeSelf)
		{
			transform.Find("AnimationHolder/MakeYourOwnFood/ButtonResetMixer").GetComponent<Image>().enabled = false;
		}
		Food tempFood;
		

		Transform ingredientsHolder = transform.Find("AnimationHolder/MakeYourOwnFood/ShelfHolder/Ingredients");
        for(int i=0;i<ingredientsHolder.childCount;i++)
		{
			ingredientsHolder.Find("Item"+i.ToString()).GetComponent<Button>().interactable = false;
		}
		transform.Find("AnimationHolder/MakeYourOwnFood/MixerBottom").GetComponent<Animator>().Play("MixerTopRotateDown");
		transform.Find("AnimationHolder/MakeYourOwnFood/MixerBottom/RecipeResult").GetComponent<Image>().sprite = targetRecipe.image;
		StartCoroutine("WaitForMixerToFinish");
        recipesFinished++;
        PlayerPrefs.SetInt("Recipe" + recipesFinished.ToString(), foodData.recipes.IndexOf(targetRecipe));
        PlayerPrefs.Save();
        if(recipesFinished==4)
        {
            FoodMakingFinished();
        }
	}

	IEnumerator WaitForMixerToFinish()
	{
		yield return new WaitForSeconds(4.5f);
		transform.Find("AnimationHolder/MakeYourOwnFood/MixerBottom").GetComponent<Animator>().SetTrigger("Finished");
		yield return new WaitForSeconds(4.7f);
		ResetMixer();
	}

	public void EnableDisableFoodArrowsInteraction(bool condition)
	{
		arrowLeft.GetComponent<Button>().interactable = condition;
		arrowRight.GetComponent<Button>().interactable = condition;
	}
	
	void ReturnMakeYourOwnFood()
	{
//		if(ShopLocation != 3 && ShopLocation != 5)
		{
			transform.Find("AnimationHolder/MakeYourOwnFood").GetComponent<Animator>().Play("InstantBack");
			transform.Find("AnimationHolder/MakeYourOwnFood").gameObject.SetActive(false);
			//SoundManager.Instance.Play_ButtonClick4();
		}
	}

    public void FoodMakingFinished()
    {
        Debug.Log("Food MAking finished");

        for (int i = 1; i <=4; i++)
        {
            canvas.transform.Find("Kitchen/Table/Tray/Plate" + i.ToString() + "/Food").GetComponent<Image>().sprite = foodData.recipes[PlayerPrefs.GetInt("Recipe" + i)].image;
        }

        Timer.Schedule(this, 5f, delegate
            {
                
                canvas.transform.Find("FoodShop/AnimationHolder/MakeYourOwnFood/ParticleHolder/SuccessParticle").GetComponent<ParticleSystem>().Play();
                canvas.transform.Find("TopUI/ButtonNext").gameObject.SetActive(true);
            });


       

        canvas.transform.Find("TopUI/ButtonNext").GetComponent<Button>().onClick.RemoveAllListeners();

        canvas.transform.Find("TopUI/ButtonNext").GetComponent<Button>().onClick.AddListener(delegate
            {
                canvas.GetComponent<MenuManager>().PlayTransitionNoLoadScene();
                Timer.Schedule(this, 2f, delegate
                    {
                        canvas.transform.Find("MagicPotionGame").gameObject.SetActive(true);
                        this.gameObject.SetActive(false);
                    });
            });

        Transform ingredientsHolder = transform.Find("AnimationHolder/MakeYourOwnFood/ShelfHolder/Ingredients");
        for (int i = 0; i < ingredientsHolder.childCount; i++)
        {
            ingredientsHolder.Find("Item" + i.ToString()).GetComponent<Button>().interactable = false;
        }
    }
}
