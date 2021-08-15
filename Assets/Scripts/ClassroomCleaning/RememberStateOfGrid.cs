using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

///<summary>
///<para>Scene:All/NameOfScene/NameOfScene1,NameOfScene2,NameOfScene3...</para>
///<para>Object:N/A</para>
///<para>Description: Sample Description </para>
///</summary>
public class RememberStateOfGrid : MonoBehaviour {

	public Color[] colorsToChose;
	public Sprite[] shapeToChose;
	public Sprite emptySprite;
	public Transform Grid;
	public Transform[] ThumbGrids;
	string[] allCells;
	string[] tmp = new string[2];
	string[] colors = new string[120];
	string[] shapes = new string[120];
	[Serializable]
	public struct Cell
	{
		public string cellColor;
		public string cellSprite;
		
		public Cell(string myColor, string mySprite)
		{
			this.cellColor = myColor;
			this.cellSprite = mySprite;
		}
	}

	void Awake()
	{
//		if(Application.loadedLevelName.Equals("MainScene"))
//		{
//			SetThumbs();
//		}
	}

	public Cell[] listOfCells = new Cell[120];
	
	public void SaveGrid()
	{
		string temp = "";
		for(int i=0;i<Grid.childCount;i++)
		{
			if(!String.IsNullOrEmpty(Grid.GetChild(i).GetComponent<StateOfObject>().colorString))
			{
				listOfCells[i].cellColor = Grid.GetChild(i).GetComponent<StateOfObject>().colorString;
				listOfCells[i].cellSprite = Grid.GetChild(i).GetComponent<StateOfObject>().shapeString;
			}
			else
			{
				listOfCells[i].cellColor = "0";
				listOfCells[i].cellSprite = "0";
			}
			temp += listOfCells[i].cellColor+"!"+listOfCells[i].cellSprite;
			if(i!=Grid.childCount-1)
				temp +="#";
		}
//		Debug.Log(temp);
		GlobalVariables.mgDrawShaper_SavedProgres = temp;
		/*

		if(!PlayerPrefs.HasKey("Gallery1"))
		{
			PlayerPrefs.SetString("Gallery1",temp);
		}
		else if(!PlayerPrefs.HasKey("Gallery2"))
		{
			PlayerPrefs.SetString("Gallery2",temp);
		}
		else if(!PlayerPrefs.HasKey("Gallery3"))
		{
			PlayerPrefs.SetString("Gallery3",temp);
		}
		else if(!PlayerPrefs.HasKey("Gallery4"))
		{
			PlayerPrefs.SetString("Gallery4",temp);
		}
		else if(!PlayerPrefs.HasKey("Gallery5"))
		{
			PlayerPrefs.SetString("Gallery5",temp);
		}
		else
		{
			PlayerPrefs.SetString("Gallery5",temp);
		}
		PlayerPrefs.Save();

		Invoke ("LoadMainScene",1f);

	*/
	}

//	void LoadMainScene()
//	{
//		Application.LoadLevel ("MainScene");
//	}


	 

	public void SetGrid( )
	{
		if(GlobalVariables.mgDrawShaper_SavedProgres == "") return;



 
		//EraseAll();


		string temp = GlobalVariables.mgDrawShaper_SavedProgres;

//		Debug.Log(temp);
		allCells = new string[120];
		allCells = temp.Split('#');
		for(int i=0;i<120;i++)
		{
			tmp = allCells[i].Split('!');
			colors[i]=tmp[0];
			shapes[i]=tmp[1];
			//Debug.Log("Index "+i);
			if(!colors[i].Equals("0"))
			{
				Grid.GetChild(i).GetComponent<Image>().color = colorsToChose[int.Parse(colors[i])-1];
				Grid.GetChild(i).GetComponent<Image>().sprite = shapeToChose[int.Parse(shapes[i])-1];
				//MARKO

				Grid.GetChild(i).GetComponent<StateOfObject> ().chosenColor = colorsToChose[int.Parse(colors[i])-1];
				Grid.GetChild(i).GetComponent<StateOfObject> ().chosenShape = shapeToChose[int.Parse(shapes[i])-1];

				Grid.GetChild(i).GetComponent<StateOfObject> ().colorString = colors[i];
				Grid.GetChild(i).GetComponent<StateOfObject> ().shapeString = shapes[i];
			}

		}

		/*
		switch (number)
		{
		case 1:
			if(PlayerPrefs.HasKey("Gallery1"))
			{
				string temp = PlayerPrefs.GetString("Gallery1");
				Debug.Log(temp);
				allCells = new string[120];
				allCells = temp.Split('#');
				for(int i=0;i<120;i++)
				{
					tmp = allCells[i].Split('!');
					colors[i]=tmp[0];
					shapes[i]=tmp[1];
					Debug.Log("Index "+i);
					if(!colors[i].Equals("0"))
					{
						Grid.GetChild(i).GetComponent<Image>().color = colorsToChose[int.Parse(colors[i])-1];
						Grid.GetChild(i).GetComponent<Image>().sprite = shapeToChose[int.Parse(shapes[i])-1];
					}

				}
			}
			break;
		case 2:
			if(PlayerPrefs.HasKey("Gallery2"))
			{
				string temp = PlayerPrefs.GetString("Gallery2");
				Debug.Log(temp);
				allCells = new string[120];
				allCells = temp.Split('#');
				for(int i=0;i<120;i++)
				{
					tmp = allCells[i].Split('!');
					colors[i]=tmp[0];
					shapes[i]=tmp[1];
					Debug.Log("Index "+i);
					if(!colors[i].Equals("0"))
					{
						Grid.GetChild(i).GetComponent<Image>().color = colorsToChose[int.Parse(colors[i])-1];
						Grid.GetChild(i).GetComponent<Image>().sprite = shapeToChose[int.Parse(shapes[i])-1];
					}
					
				}
			}
			break;
		case 3:
			if(PlayerPrefs.HasKey("Gallery3"))
			{
				string temp = PlayerPrefs.GetString("Gallery3");
				Debug.Log(temp);
				allCells = new string[120];
				allCells = temp.Split('#');
				for(int i=0;i<120;i++)
				{
					tmp = allCells[i].Split('!');
					colors[i]=tmp[0];
					shapes[i]=tmp[1];
					Debug.Log("Index "+i);
					if(!colors[i].Equals("0"))
					{
						Grid.GetChild(i).GetComponent<Image>().color = colorsToChose[int.Parse(colors[i])-1];
						Grid.GetChild(i).GetComponent<Image>().sprite = shapeToChose[int.Parse(shapes[i])-1];
					}
					
				}
			}
			break;
		case 4:
			if(PlayerPrefs.HasKey("Gallery4"))
			{
				string temp = PlayerPrefs.GetString("Gallery4");
				Debug.Log(temp);
				allCells = new string[120];
				allCells = temp.Split('#');
				for(int i=0;i<120;i++)
				{
					tmp = allCells[i].Split('!');
					colors[i]=tmp[0];
					shapes[i]=tmp[1];
					Debug.Log("Index "+i);
					if(!colors[i].Equals("0"))
					{
						Grid.GetChild(i).GetComponent<Image>().color = colorsToChose[int.Parse(colors[i])-1];
						Grid.GetChild(i).GetComponent<Image>().sprite = shapeToChose[int.Parse(shapes[i])-1];
					}
					
				}
			}
			break;
		case 5:
			if(PlayerPrefs.HasKey("Gallery5"))
			{
				string temp = PlayerPrefs.GetString("Gallery5");
				Debug.Log(temp);
				allCells = new string[120];
				allCells = temp.Split('#');
				for(int i=0;i<120;i++)
				{
					tmp = allCells[i].Split('!');
					colors[i]=tmp[0];
					shapes[i]=tmp[1];
					Debug.Log("Index "+i);
					if(!colors[i].Equals("0"))
					{
						Grid.GetChild(i).GetComponent<Image>().color = colorsToChose[int.Parse(colors[i])-1];
						Grid.GetChild(i).GetComponent<Image>().sprite = shapeToChose[int.Parse(shapes[i])-1];
					}
					
				}
			}
			break;
		}
		*/
	}

	public void EraseAll()
	{
		for(int i=0;i<120;i++)
		{
			Grid.GetChild(i).GetComponent<Image>().color = Color.white;
			Grid.GetChild(i).GetComponent<Image>().sprite = emptySprite;
		}
	}

	void EraseThumbGrid(Transform GO)
	{
		for(int i=0;i<120;i++)
		{
			GO.GetChild(i).GetComponent<Image>().color = Color.white;
			GO.GetChild(i).GetComponent<Image>().sprite = emptySprite;
		}
	}

	/*

	public void SetThumbs()
	{
		if(PlayerPrefs.HasKey("Gallery1"))
		{
			string temp = PlayerPrefs.GetString("Gallery1");
			Debug.Log(temp);
			allCells = new string[120];
			allCells = temp.Split('#');
			for(int i=0;i<120;i++)
			{
				tmp = allCells[i].Split('!');
				colors[i]=tmp[0];
				shapes[i]=tmp[1];
				Debug.Log("Index "+i);
				if(!colors[i].Equals("0"))
				{
					ThumbGrids[0].GetChild(i).GetComponent<Image>().color = colorsToChose[int.Parse(colors[i])-1];
					ThumbGrids[0].GetChild(i).GetComponent<Image>().sprite = shapeToChose[int.Parse(shapes[i])-1];
				}
				
			}
		}
		if(PlayerPrefs.HasKey("Gallery2"))
		{
			string temp = PlayerPrefs.GetString("Gallery2");
			Debug.Log(temp);
			allCells = new string[120];
			allCells = temp.Split('#');
			for(int i=0;i<120;i++)
			{
				tmp = allCells[i].Split('!');
				colors[i]=tmp[0];
				shapes[i]=tmp[1];
				Debug.Log("Index "+i);
				if(!colors[i].Equals("0"))
				{
					ThumbGrids[1].GetChild(i).GetComponent<Image>().color = colorsToChose[int.Parse(colors[i])-1];
					ThumbGrids[1].GetChild(i).GetComponent<Image>().sprite = shapeToChose[int.Parse(shapes[i])-1];
				}
				
			}
		}
		if(PlayerPrefs.HasKey("Gallery3"))
		{
			string temp = PlayerPrefs.GetString("Gallery3");
			Debug.Log(temp);
			allCells = new string[120];
			allCells = temp.Split('#');
			for(int i=0;i<120;i++)
			{
				tmp = allCells[i].Split('!');
				colors[i]=tmp[0];
				shapes[i]=tmp[1];
				Debug.Log("Index "+i);
				if(!colors[i].Equals("0"))
				{
					ThumbGrids[2].GetChild(i).GetComponent<Image>().color = colorsToChose[int.Parse(colors[i])-1];
					ThumbGrids[2].GetChild(i).GetComponent<Image>().sprite = shapeToChose[int.Parse(shapes[i])-1];
				}
				
			}
		}
		if(PlayerPrefs.HasKey("Gallery4"))
		{
			string temp = PlayerPrefs.GetString("Gallery4");
			Debug.Log(temp);
			allCells = new string[120];
			allCells = temp.Split('#');
			for(int i=0;i<120;i++)
			{
				tmp = allCells[i].Split('!');
				colors[i]=tmp[0];
				shapes[i]=tmp[1];
				Debug.Log("Index "+i);
				if(!colors[i].Equals("0"))
				{
					ThumbGrids[3].GetChild(i).GetComponent<Image>().color = colorsToChose[int.Parse(colors[i])-1];
					ThumbGrids[3].GetChild(i).GetComponent<Image>().sprite = shapeToChose[int.Parse(shapes[i])-1];
				}
				
			}
		}
		if(PlayerPrefs.HasKey("Gallery5"))
		{
			string temp = PlayerPrefs.GetString("Gallery5");
			Debug.Log(temp);
			allCells = new string[120];
			allCells = temp.Split('#');
			for(int i=0;i<120;i++)
			{
				tmp = allCells[i].Split('!');
				colors[i]=tmp[0];
				shapes[i]=tmp[1];
				Debug.Log("Index "+i);
				if(!colors[i].Equals("0"))
				{
					ThumbGrids[4].GetChild(i).GetComponent<Image>().color = colorsToChose[int.Parse(colors[i])-1];
					ThumbGrids[4].GetChild(i).GetComponent<Image>().sprite = shapeToChose[int.Parse(shapes[i])-1];
				}
				
			}
		}

	}
*/
}
