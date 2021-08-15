using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using UnityEngine.UI;

public class MiniGameConnectLines : MonoBehaviour {

	public MenuManager menuManager;
	public GameObject LevelButtonsHolder;
	public GameObject PopUpSelectLevel;
	public GameObject MatrixHolder;

	public GameObject matrixElement;
	public GameObject nodeElement;
	public GameObject connectLine;
	public GameObject[] nodePrefabs;
	public GameObject[] endNodePrefabs;

	GameObject[,] playMatrix;
	string[,] drawnLines;
	GameObject[,] lines;
	static int matrixSize;
	float defaultPosX;
	float defaultPosY;
	float sizeIncrement;
	float sizeScale = .52f;
	float elementScale;

	int currentLevel;
	int maxLevel;
	XElement levelSettings;
	Dictionary<string, Color32> colors;
	Dictionary<string, GameObject> nodeColors;
	Dictionary<string, GameObject> endNodeColors;

	List<string> nodes;
	List<List<string>> lineCoords;
	List<bool> isLineCompleted;
	List<List<int>> positionOfSplit;
	string currentDrawingLine;

	int lastDrawnX;
	int lastDrawnY;

	bool bPauseGame = false;

	int lastUnlockedLevel = 0;

	public Animator animStewardes1;
	public Animator animStewardes2;
	Animator animStewardes;

	public GameObject buttonNext;

	public void Awake()
	{
        
		
		lastUnlockedLevel = PlayerPrefs.GetInt("ConnectLinesUnlockedLevel",0);
		Debug.Log("UL " + lastUnlockedLevel);
		//lastUnlockedLevel = 10;
		SetLevelButtons();

		//GameData.Init();
		MatrixHolder.SetActive(false);
		buttonNext.SetActive(false);
        GameObject canvas = GameObject.Find("Canvas");
	}
 
	IEnumerator Start ()
	{
		bPauseGame = true;

//		if(Random.Range(0,4) > 2)
//		{
			animStewardes = animStewardes1;
			animStewardes1.transform.parent.gameObject.SetActive(true);
//			animStewardes2.transform.parent.gameObject.SetActive(false);
//		}
//		else
//		{
//			animStewardes = animStewardes2;
//			animStewardes2.transform.parent.gameObject.SetActive(true);
//			animStewardes1.transform.parent.gameObject.SetActive(false);
//		}

		animStewardes.Play("CharacterIdle_Pointing");

//		LevelTransition.Instance.ShowScene();
		float timeW = 0;
		while(timeW < 1f)
		{
            /*if( !GlobalVariables.bPauseGame) */
            timeW += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}

//		BlockClicks.Instance.SetBlockAll(false); 

		animStewardes.transform.parent.GetComponent<Animator>().Play("ShowStewardess");


		timeW = 0;
		while(timeW < 5f)
		{
            /*if( !GlobalVariables.bPauseGame) */
            timeW += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
	 
		animStewardes.transform.parent.GetComponent<Animator>().Play("HideStewardess");

		timeW = 0;
		while(timeW <1f)
		{
            /*if( !GlobalVariables.bPauseGame)*/
            timeW += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}

		animStewardes.transform.parent.gameObject.SetActive(false);
		//BlockClicks.Instance.SetBlockAll(false); 

		ShowStartPopup();
	}

	void GenerateLevel()
	{
		MatrixHolder.SetActive(true);


//		if(currentLevel == 0 && GameData.bTutorialMGConnectLines == 0)
//		{
//			Tutorial1.Tutorial.Instance.ShowTutorial(0);
//		}


		//currentLevel = 0;
		lastDrawnX = -1;
		lastDrawnY = -1;


		TextAsset ta = (TextAsset) Resources.Load("XML/Level Settings/ConnectLinesLevelSettings");
		levelSettings = XElement.Parse(ta.ToString());

		maxLevel = levelSettings.Elements ().Count ();

		colors = new Dictionary<string, Color32> ();
		colors ["blue"] = new Color32 (41, 187, 255, 255);
		colors ["yellow"] = new Color32 (255, 255, 0, 255);
		colors ["green"] = new Color32 (115, 243, 66, 255);
		colors ["red"] = new Color32 (255, 64, 103, 255);
		colors ["orange"] = new Color32 (255, 192, 57, 255);
		colors ["white"] = new Color32 (255, 255, 255, 255);
		colors ["purple"] = new Color32 (131, 58, 255, 255);

		nodeColors = new Dictionary<string, GameObject> ();
		nodeColors ["blue"] = nodePrefabs [3];
		nodeColors ["green"] = nodePrefabs [0];
		nodeColors ["red"] = nodePrefabs [4];
		nodeColors ["orange"] = nodePrefabs [2];
		nodeColors ["purple"] = nodePrefabs [1];


		endNodeColors = new Dictionary<string, GameObject> ();
		endNodeColors ["blue"] = endNodePrefabs [3];
		endNodeColors ["green"] = endNodePrefabs [0];
		endNodeColors ["red"] = endNodePrefabs [4];
		endNodeColors ["orange"] = endNodePrefabs [2];
		endNodeColors ["purple"] = endNodePrefabs [1];



		currentDrawingLine = "";

		CreatePlayField ();



		bPauseGame = false;

	}

	public void CreatePlayField ()
	{
		Debug.Log("NEW LEVEL " + currentLevel.ToString ());
		if(lastUnlockedLevel <currentLevel)
		{
			lastUnlockedLevel = currentLevel;
			PlayerPrefs.SetInt("ConnectLinesUnlockedLevel",lastUnlockedLevel);
			PlayerPrefs.Save();
		}
 
		matrixSize = int.Parse(levelSettings.Element ("level" + currentLevel.ToString ()).Element ("matrixSize").Value);

		playMatrix = new GameObject[matrixSize, matrixSize]; //references to the matrix fields game objects
		drawnLines = new string[matrixSize, matrixSize]; //for every element in the matrix holds the name of the line it's a part of, and the weight (starting node has a weight of 1)
		lines = new GameObject[matrixSize, matrixSize];

		nodes = new List<string> (); //this holds the colors of the lines
		lineCoords = new List<List<string>> (); //this list contains the x, y coordinates (in the matrix) of every element of every line
		isLineCompleted = new List<bool> ();
		positionOfSplit = new List<List<int>> ();

		sizeIncrement = 0.75f * 5 / matrixSize;
		elementScale = 0.6f * 5 / matrixSize;

		defaultPosX = -1.5f - 0.75f * (matrixSize - 5) / (2 * matrixSize);
		defaultPosY = 1.4f + 0.7f * (matrixSize - 5) / (2 * matrixSize);
		Vector3 defaultPosition = new Vector3 (defaultPosX, defaultPosY, -0.1f);

		for (int i = 0; i < matrixSize; i++)
		{
			for (int j = 0; j < matrixSize; j++)
			{	
				Vector3 position = new Vector3 (defaultPosition.x + j * sizeIncrement, defaultPosition.y - i * sizeIncrement, defaultPosition.z);
				playMatrix[i, j] = Instantiate (matrixElement, position, Quaternion.identity) as GameObject;
				playMatrix[i, j].transform.localScale = new Vector3(elementScale, elementScale, elementScale);
				playMatrix[i, j].transform.SetParent (MatrixHolder.transform, false);
				playMatrix[i, j].name = "MatrixElement_" + i + "_" + j;

				CreateLine (i, j);
			}
		}
		FillPlayField ();
	}

	public void DestroyPlayField ()
	{
		for (int i = 0; i < matrixSize; i++)
			for (int j = 0; j < matrixSize; j++)
				Destroy (playMatrix [i, j]);
	}

	public void FillPlayField ()
	{
		IEnumerable<XElement> currentLevelSettings = levelSettings.Element ("level" + currentLevel.ToString ()).Elements ();

		foreach (XElement node in currentLevelSettings)
		{
			string nodeName = node.Name.ToString ();

			if (nodeName != "matrixSize")
			{
				string[] nodePosString = node.Value.Split (new string[] { "," }, System.StringSplitOptions.None);
				int[] nodePos = new int[nodePosString.Length];
				float nodeScale = 0.3f * 5 / matrixSize;

				for (int i = 0; i < nodePos.Length; i++)
					nodePos[i] = int.Parse (nodePosString[i]);

				GameObject nodeObject; 

				if ( !nodes.Contains (nodeName) )  
				{
//					Debug.Log("NodeName  " + nodeName);
					nodeObject = Instantiate (nodeColors[nodeName], new Vector3 (0f, 0f, -0.3f), Quaternion.identity) as GameObject;
				}
				else 
				{
	//				Debug.Log("NodeName2  " + nodeName);
					nodeObject = Instantiate (endNodeColors[nodeName], new Vector3 (0f, 0f, -0.3f), Quaternion.identity) as GameObject;
				}

				nodeObject.transform.SetParent (playMatrix[nodePos[0], nodePos[1]].transform, false);
				nodeObject.name = nodeName + "_Node";

				if (!nodes.Contains (nodeName))
				{
//					Debug.Log("NodeName  " + nodeName);
					nodes.Add (nodeName);
					lineCoords.Add (new List<string> {});
					isLineCompleted.Add (false);
					positionOfSplit.Add (new List<int> {});
				}
			}
		}
	}

	public void CreateLine (int i, int j)
	{
		lines [i, j] = Instantiate (connectLine, new Vector3 (0f, 0f, -0.2f), Quaternion.identity) as GameObject;
		lines [i, j].transform.SetParent (playMatrix[i, j].transform, false);
		lines [i, j].GetComponent<LineRenderer> ().SetVertexCount (2);
		//lines [i, j].GetComponent<LineRenderer> ().SetWidth (sizeScale* sizeIncrement * 4 / 3, sizeScale* sizeIncrement * 4 / 3);
		lines [i, j].GetComponent<LineRenderer> ().startWidth =  sizeScale* sizeIncrement * 4 / 3;
		lines [i, j].GetComponent<LineRenderer> ().endWidth = 	sizeScale* sizeIncrement * 4 / 3 ;
		lines [i, j].GetComponent<LineRenderer> ().SetPosition (0, new Vector3 (0, 0, 0));
		lines [i, j].GetComponent<LineRenderer> ().SetPosition (1, new Vector3 (0, 0, 0));
		lines [i, j].name = "ConnectLine";
	}

	//This function deletes every node in the currently selected line after the currently selected node
	//Currently selected line is the one which includes the matrix field we're currently holding our finger over, even if we're currently drawing another line
	//This allows this function to either delete nodes in the current line if we go backwards, or delete the nodes in the line which we intersect
	public void RestartLine (int x, int y)
	{
		string lineInfo = drawnLines [x, y];

		if (lineInfo != null)
		{
			string[] lineData = lineInfo.Split (new string[] { "," }, System.StringSplitOptions.None);

			int index = nodes.IndexOf (lineData [0]);
			int lineLength;

			/*if (currentDrawingLine == lineData [0] || currentDrawingLine != lineData[0] && !isLineCompleted[index])
			{*/
			//This is the check for if we're deleting the current or another line
			if (currentDrawingLine == lineData[0])
			{
				lineLength = int.Parse (lineData[1]);
			}
			else
			{
				lineLength = int.Parse (lineData[1]) - 1;
                SoundManagerConnectLines.Instance.StopAndPlay_Sound(SoundManagerConnectLines.Instance.flowBreakLine);
            }

			lineCoords [index] = lineCoords [index].GetRange (0, lineLength);
			isLineCompleted [index] = CheckIfLineCompleted (index);

			for (int i = 0; i < matrixSize; i++)
				for (int j = 0; j < matrixSize; j++)
				{
					if (drawnLines [i, j] != null && drawnLines [i, j].Split (new string[] { "," }, System.StringSplitOptions.None)[0] == nodes[index])
					{	
						if (lineLength - 1 < int.Parse (drawnLines [i, j].Split (new string[] { "," }, System.StringSplitOptions.None)[1]))
							lines [i, j].GetComponent<LineRenderer> ().SetPosition (1, new Vector3 (0, 0, 0));

						if (lineLength < int.Parse (drawnLines [i, j].Split (new string[] { "," }, System.StringSplitOptions.None)[1]))
							drawnLines[i, j] = null;
					}
				}
			/*}
			else
			{
				positionOfSplit[index].Add (lineLength);
				SoundManager.Instance.PlaySound (SoundManager.Instance.flowBreakLine);

				//if (positionOfSplit[index].Count == 1)
					for (int i = 0; i < matrixSize; i++)
						for (int j = 0; j < matrixSize; j++)
						{
							if (drawnLines [i, j] != null && drawnLines [i, j].Split (new string[] { "," }, System.StringSplitOptions.None)[0] == nodes[index])
							{	
								if (lineLength - 1 == int.Parse (drawnLines [i, j].Split (new string[] { "," }, System.StringSplitOptions.None)[1]) || lineLength == int.Parse (drawnLines [i, j].Split (new string[] { "," }, System.StringSplitOptions.None)[1]))
									lines [i, j].GetComponent<LineRenderer> ().SetPosition (1, new Vector3 (0, 0, 0));

								if (lineLength == int.Parse (drawnLines [i, j].Split (new string[] { "," }, System.StringSplitOptions.None)[1]))
									drawnLines[i, j] = null;
							}
						}

				if (positionOfSplit[index].Count > 1)
					RemoveLineParts (lineData [0]);
			}*/
		}
		//If we click on a different ending node than the one we started at, the whole line is deleted and redrawn from the currently clicked node
		else
		{
			int index = nodes.IndexOf (currentDrawingLine);
			lineCoords [index].Clear ();
			lineCoords [index].Add (x + "," + y);
			drawnLines [x, y] = nodes[index] + "," + lineCoords [index].Count;
			isLineCompleted [index] = false;

			for (int i = 0; i < matrixSize; i++)
				for (int j = 0; j < matrixSize; j++)
					if (drawnLines [i, j] != null && drawnLines [i, j].Split (new string[] { "," }, System.StringSplitOptions.None)[0] == currentDrawingLine && !(i == x && j == y))
					{
						drawnLines[i, j] = null;
						lines [i, j].GetComponent<LineRenderer> ().SetPosition (1, new Vector3 (0, 0, 0));
					}
		}
	}

	//The line is scheduled for restart if we click on an element of a current line or if we click on an element of another line that is adjacent to the last drawn element in the current line
	public bool ShouldRestartLine (int x, int y, int index)
	{
		bool shouldRestart = false;

		for (int i = 0; i < lineCoords[index].Count; i++)
		{
			string[] coords = lineCoords[index][i].Split (new string[] { "," }, System.StringSplitOptions.None);
			string elementColor = drawnLines[x, y].Split(new string[] { "," }, System.StringSplitOptions.None)[0];
			if (int.Parse (coords[0]) == x && int.Parse (coords[1]) == y || elementColor != currentDrawingLine && (lastDrawnX == x && (lastDrawnY == y - 1 || lastDrawnY == y + 1) || lastDrawnY == y && (lastDrawnX == x - 1 || lastDrawnX == x + 1)) && !CheckIfLineCompleted (nodes.IndexOf (currentDrawingLine)))
			{
				shouldRestart = true;
				lastDrawnX = x;
				lastDrawnY = y;
			}
		}

		return shouldRestart;
	}

	public void DrawLine(int i, int j, int index)
	{
		string prevElement = lineCoords[index].LastOrDefault<string>();

		if (prevElement != null)
		{
			string[] prevCoordsString = prevElement.Split (new string[] { "," }, System.StringSplitOptions.None);
			int[] prevCoords = new int[2];
			prevCoords[0] = int.Parse (prevCoordsString[0]);
			prevCoords[1] = int.Parse (prevCoordsString[1]);

			lines[prevCoords[0], prevCoords[1]].GetComponent<LineRenderer> ().SetColors (colors[nodes[index]], colors[nodes[index]]);
			lines[prevCoords[0], prevCoords[1]].GetComponent<LineRenderer> ().SetPosition (1, new Vector3 (1.489f * (j - prevCoords[1]), -1.489f * (i - prevCoords[0]), 0));
		}

		lineCoords [index].Add (i + "," + j);
		drawnLines [i, j] = nodes[index] + "," + lineCoords [index].Count;
		isLineCompleted [index] = CheckIfLineCompleted (index);

		if (positionOfSplit [index].Contains (lineCoords [index].Count))
			positionOfSplit [index].Remove (lineCoords [index].Count);

		lastDrawnX = i;
		lastDrawnY = j;


//		if(currentLevel == 0 && GameData.bTutorialMGConnectLines == 0 && isLineCompleted [index])
//		{
//			Debug.Log("STOP TUT");
//			Tutorial1.Tutorial.Instance.StopTutorial();
//			GameData.SetTutorialOver(7);
//		}

	}

	//The line is completed if it goes through both of the ending nodes of its colors
	public bool CheckIfLineCompleted (int index)
	{
		int numberOfNodes = 0;
		for (int i = 0; i < lineCoords[index].Count; i++)
		{
			string[] coords = lineCoords[index][i].Split (new string[] { "," }, System.StringSplitOptions.None);
			if (playMatrix[int.Parse(coords[0]), int.Parse(coords[1])].transform.childCount == 3)
				numberOfNodes++;
		}

		if (numberOfNodes == 2)
			return true;
		else
			return false;
	}

	/*public void RemoveLineParts (string color)
	{
		int index = nodes.IndexOf (color);
		
		for (int i = positionOfSplit[index].Min (); i <= positionOfSplit[index].Max (); i++)
		{
			string[] coords = lineCoords[index][i - 1].Split (new string[] { "," }, System.StringSplitOptions.None);
			string[] lineInfo = (drawnLines[int.Parse (coords[0]), int.Parse (coords[1])] != null) ? drawnLines[int.Parse (coords[0]), int.Parse (coords[1])].Split (new string[] { "," }, System.StringSplitOptions.None) : null;

			lineCoords[index][i - 1] += ",forRemoval";
			if (lineInfo != null && lineInfo[0] == color)
			{
				drawnLines[int.Parse (coords[0]), int.Parse (coords[1])] = "forRemoval";
			}
		}

		string print = "";
		for (int i = 0; i < matrixSize; i++)
		{
			for (int j = 0; j < matrixSize; j++)
				if (drawnLines[i,j] == null)
					print += "null ";
				else
					print += drawnLines[i, j] + " ";

			print += "\n";
		}
		Debug.Log (print);

		for (int i = 0; i < lineCoords[index].Count; i++)
		{
			if (lineCoords[index][i].Contains("forRemoval"))
			{
				Debug.Log (lineCoords[index][i]);
				string[] coords = lineCoords[index][i].Split (new string[] { "," }, System.StringSplitOptions.None);

				if (drawnLines[int.Parse (coords[0]), int.Parse (coords[1])] != null && drawnLines[int.Parse (coords[0]), int.Parse (coords[1])].Split (new string[] { "," }, System.StringSplitOptions.None)[0] == color)
					lines[int.Parse(coords[0]), int.Parse (coords[1])].GetComponent<LineRenderer> ().SetPosition (1, new Vector3 (0, 0, 0));
			}
		}
		
		lineCoords [index].RemoveAll (match => match.Contains("forRemoval"));

		for (int i = 0; i < matrixSize; i++)
		{
			for (int j = 0; j < matrixSize; j++)
			{
				if (drawnLines[i, j] == "forRemoval")
					drawnLines[i, j] = null;
			}
		}
		
		for (int i = 0; i < lineCoords[index].Count; i++)
		{
			string[] coords = lineCoords[index][i].Split (new string[] { "," }, System.StringSplitOptions.None);
			string[] lineInfo = drawnLines[int.Parse (coords[0]), int.Parse (coords[1])].Split (new string[] { "," }, System.StringSplitOptions.None);
			
			drawnLines[int.Parse (coords[0]), int.Parse (coords[1])] = lineInfo[0] + "," + (i + 1).ToString ();
		}

		positionOfSplit [index].Remove (positionOfSplit [index].Min ());
	}*/

	/*public void ReverseLine (string color)
	{
		int index = nodes.IndexOf (color);
		int numOfElements = lineCoords [index].Count;

		for (int i = 0; i < positionOfSplit[index].Count; i++)
			lineCoords [index] [positionOfSplit [index] [i] - 1] = "forRemoval";

		lineCoords [index].RemoveAll (match => match == "forRemoval");

		int numOfElements = lineCoords [index].Count;

		for (int i = 0; i < lineCoords[index].Count; i++)
		{
			string[] coords = lineCoords[index][i].Split (new string[] { "," }, System.StringSplitOptions.None);
			string[] lineInfo = drawnLines[int.Parse (coords[0]), int.Parse (coords[1])].Split (new string[] { "," }, System.StringSplitOptions.None);

			drawnLines[int.Parse (coords[0]), int.Parse (coords[1])] = lineInfo[0] + "," + (i + 1).ToString ();
		}

		for (int i = 0; i < Mathf.CeilToInt((float)numOfElements / 2.0f); i++)
		{
			string pom;

			pom = lineCoords[index][i];
			lineCoords[index][i] = lineCoords[index][numOfElements - i - 1];
			lineCoords[index][numOfElements - i - 1] = pom;

			string[] coords = lineCoords[index][i].Split (new string[] { "," }, System.StringSplitOptions.None);
			string[] nextCoords = lineCoords[index][numOfElements - i - 1].Split (new string[] { "," }, System.StringSplitOptions.None);

			pom = drawnLines[int.Parse (coords [0]), int.Parse (coords [1])];
			drawnLines[int.Parse (coords [0]), int.Parse (coords [1])] = drawnLines[int.Parse (nextCoords [0]), int.Parse (nextCoords [1])];
			drawnLines[int.Parse (nextCoords [0]), int.Parse (nextCoords [1])] = pom;
		}

		for (int i = 0; i < numOfElements - 1; i++)
		{
			string[] coords = lineCoords[index][i].Split (new string[] { "," }, System.StringSplitOptions.None);
			string[] nextCoords = lineCoords[index][i + 1].Split (new string[] { "," }, System.StringSplitOptions.None);
			float posX = (int.Parse (nextCoords[1]) - int.Parse (coords[1])) * 1.489f;
			float posY = (int.Parse (nextCoords[0]) - int.Parse (coords[0])) * -1.489f;

			for (int j = 0; j < positionOfSplit[index].Count; j++)
				if (i != positionOfSplit[index][j] || i != positionOfSplit[index][j] - 1)
				{
					lines[int.Parse (coords[0]), int.Parse (coords[1])].GetComponent<LineRenderer> ().SetColors (colors[color], colors[color]);
					lines[int.Parse (coords[0]), int.Parse (coords[1])].GetComponent<LineRenderer> ().SetPosition (1, new Vector3 (posX, posY, 0));
				}
		}

		for (int j = 0; j < positionOfSplit[nodes.IndexOf (currentDrawingLine)].Count; j++)
			positionOfSplit [nodes.IndexOf (currentDrawingLine)].RemoveAt (0);

		string print = "";
		for (int i = 0; i < matrixSize; i++)
		{
			for (int j = 0; j < matrixSize; j++)
				if (drawnLines[i,j] == null)
					print += "null ";
				else
					print += drawnLines[i, j] + " ";

			print += "\n";
		}
		Debug.Log (print);
	}*/

	public void ClickOnElement (GameObject clickedOn)
	{
        Debug.Log("KLIKO NA " + clickedOn.name);
		if (clickedOn != null && clickedOn.transform.name.Contains ("MatrixElement"))
		{
			string[] coordinates = clickedOn.transform.name.Split(new string[] { "_" }, System.StringSplitOptions.None);
			int x = int.Parse (coordinates[1]);
			int y = int.Parse (coordinates[2]);

			//behavior for when an element is first clicked after lifting the finger from the screen and the element already is part of a line
			if (drawnLines[x, y] != null && currentDrawingLine == "")
			{
				lastDrawnX = x;
				lastDrawnY = y;

				currentDrawingLine = drawnLines[x, y].Split (new string[] { "," }, System.StringSplitOptions.None) [0];
				int currentElementIndex = int.Parse (drawnLines[x, y].Split (new string[] { "," }, System.StringSplitOptions.None) [1]);

				/*if (positionOfSplit[nodes.IndexOf (currentDrawingLine)].Count > 0 && positionOfSplit[nodes.IndexOf (currentDrawingLine)].Max() < currentElementIndex)
				{
					ReverseLine (currentDrawingLine);
					RestartLine (x, y);
				}*/
			}
			//if an ending node is clicked which isn't already a part of a drwan line
			else if (drawnLines[x, y] == null && playMatrix[x, y].transform.childCount == 3 && currentDrawingLine == "")
			{
				currentDrawingLine = playMatrix[x, y].transform.GetChild (2).name.Split (new string[] { "_" }, System.StringSplitOptions.None) [0];
				RestartLine (x, y);
			}
			//when the finger is already on the screen
			if (currentDrawingLine != "" && (playMatrix[x, y].transform.childCount == 2 || playMatrix[x, y].transform.childCount == 3 && currentDrawingLine == playMatrix[x, y].transform.GetChild (2).name.Split (new string[] { "_" }, System.StringSplitOptions.None) [0]))
			{
				int index = nodes.IndexOf (currentDrawingLine);

				if (drawnLines[x, y] != null && ShouldRestartLine (x, y, index))
					RestartLine (x, y);

				string prevElement = lineCoords[index].LastOrDefault<string> ();
				string[] prevCoordsString;
				int[] prevCoords = new int[2];

				if (prevElement != null)
				{
					prevCoordsString = prevElement.Split (new string[] { "," }, System.StringSplitOptions.None);
					prevCoords[0] = int.Parse (prevCoordsString[0]);
					prevCoords[1] = int.Parse (prevCoordsString[1]);
				}
				else
				{
					prevCoords[0] = -2;
					prevCoords[1] = -2;
				}

				bool shouldDraw = (prevCoords[0] == -2 && prevCoords[1] == -2) || ((prevCoords[0] == x - 1 || prevCoords[0] == x + 1) && prevCoords[1] == y || prevCoords[0] == x && (prevCoords[1] == y + 1 || prevCoords[1] == y - 1));
				if (drawnLines[x, y] == null && shouldDraw && !isLineCompleted[index])
					DrawLine (x, y, index);
			}
		}
	}

	//This function is called after lifting the finger from the screen
	public void FinishDrawing()
	{
		if (currentDrawingLine != "" && isLineCompleted[nodes.IndexOf (currentDrawingLine)])
            SoundManagerConnectLines.Instance.StopAndPlay_Sound(SoundManagerConnectLines.Instance.flowCompleteLine);

        currentDrawingLine = "";

		int missingFields = 0;
		for (int i = 0; i < matrixSize; i++)
			for (int j = 0; j < matrixSize; j++)
				if (drawnLines[i, j] == null)
					missingFields++;

		int lineBreaks = 0;
		for (int i = 0; i < positionOfSplit.Count; i++)
			if (positionOfSplit [i].Count > 0)
				lineBreaks++;

		if (missingFields == 0 && lineBreaks == 0)
		{
            if(((currentLevel + 1) % maxLevel)!=0)
			    DestroyPlayField ();
			currentLevel = (currentLevel + 1) % maxLevel;
			//VRATI				buttonsScript.playerScore++;
			//VRATI				StartCoroutine (buttonsScript.CollectTime ());

//			Debug.Log(currentLevel + " ****   " + maxLevel );

			if(currentLevel>0)
			{
				CreatePlayField ();
			}
			else 
			{
				Debug.Log("KRAJ");
				//buttonNext.SetActive(true);
				//buttonNext.transform.parent.GetComponent<Animator>().Play("Change");
				bPauseGame = true;
//				menuManager.ShowPopUpMenu( GameObject.Find("Canvas/PopUps").transform.FindChild("WinMenu").gameObject);
                GameObject.Find("Canvas/TopUI").transform.Find("ButtonNext").gameObject.SetActive(true);
                GameObject.Find("Canvas/SuccessParticle").GetComponent<ParticleSystem>().Play();
				StopAllCoroutines();
				//StartCoroutine("CEndGame");
			}

		}
	}


	//--------------------------------------
	public GameObject clickedOn = null;

	public GameObject RaycastFunct(Vector3 v)
	{
		Ray rej=Camera.main.ScreenPointToRay(v+Vector3.forward*10);
		RaycastHit hit;
		if(Physics.Raycast(rej,out hit,500))
		{
			return hit.transform.gameObject;
		}
		return null;

	}

	public void AnimateButton(GameObject btn,int Onoff)
	{
		if (Onoff == 1) 
		{
			//scaleSave = btn.transform.localScale;
			btn.transform.localScale *= 0.9f;				
		} 
		else 
		{	
			btn.transform.localScale /= 0.9f;//= scaleSave;
		}
	}


	void Update () {
//		if (canPlay)
//			UpdateTime ();

		if(bPauseGame) 
            return;

		if (Input.GetMouseButton (0))
		{
			clickedOn = RaycastFunct(Input.mousePosition);
			if (clickedOn != null && clickedOn.transform.name.Contains("MatrixElement"))
				 ClickOnElement(clickedOn);
		}

//		if(Input.GetMouseButtonDown(0))
//		{
//			clickedOn = RaycastFunct(Input.mousePosition);
//			if (clickedOn != null && !clickedOn.transform.name.Contains("MatrixElement"))
//			{
//				AnimateButton(clickedOn, 1);
//
//				if (clickedOn.tag == "Button")
//					SoundManager.Instance.PlaySound(SoundManager.Instance.buttonClick);
//			}
//		}

		if (Input.GetMouseButtonUp (0)) 
		{
			if (clickedOn == null || (clickedOn != null && clickedOn.tag != "Button"))
				 FinishDrawing ();

//			if (clickedOn != null && !clickedOn.transform.name.Contains("MatrixElement"))
//				AnimateButton (clickedOn, 2);

			GameObject rez = RaycastFunct(Input.mousePosition);

			 
		}
	}


	//---------------------------------------------------------------


	void SetLevelButtons()
	{
		for(int i =0; i <20; i++)
		{
			if(i>lastUnlockedLevel)
			{
				LevelButtonsHolder.transform.GetChild(i).GetComponent<Button>().interactable = false;


				LevelButtonsHolder.transform.GetChild(i).GetChild(1).gameObject.SetActive(true);
			}
            else
            {
                int index = i;
                LevelButtonsHolder.transform.GetChild(i).GetComponent<Button>().onClick.AddListener(delegate
                    {
                        SelectLevelButtonClick(index+1);
                    });
            }
		}
		 
	}

	public void SelectLevelButtonClick(int levelNo)
	{
		if( SoundManager.instance != null ) SoundManager.instance.Play_ButtonClick();
//		Debug.Log(levelNo);
		if(levelNo>(lastUnlockedLevel+1))
		{
        }
        else
		{
			menuManager.ClosePopUpMenu(PopUpSelectLevel);
			currentLevel = levelNo-1;
			Invoke("GenerateLevel", .5f);
		}

	}

	void ShowStartPopup()
	{
		menuManager.ShowPopUpMenu(PopUpSelectLevel);
	}

	public void ButtonHomeClicked()
	{
		StopAllCoroutines();
//		LevelTransition.Instance.HideSceneAndLoadNext("HomeScene");
		BlockClicks.Instance.SetBlockAll(true);
		BlockClicks.Instance.SetBlockAllDelay(1f,false);
		SoundManager.instance.Play_ButtonClick();
	}

	public void ButtonReplayClicked()
	{
		StopAllCoroutines();
//		LevelTransition.Instance.HideSceneAndLoadNext("MiniGameConnectLines");
		BlockClicks.Instance.SetBlockAll(true);
		BlockClicks.Instance.SetBlockAllDelay(1f,false);
		SoundManager.instance.Play_ButtonClick();
	}



	public void ButtonNextClicked()
	{
		StopAllCoroutines();
//		LevelTransition.Instance.HideSceneAndLoadNext("HomeScene");
		BlockClicks.Instance.SetBlockAll(true);
		BlockClicks.Instance.SetBlockAllDelay(1f,false);
		SoundManager.instance.Play_ButtonClick();

		Debug.Log("MAP_INTERSTITIAL");
        //PAVLE reklama bila, vidi sa raskom
//		AdsManager.Instance.ShowInterstitial();
	}

}

