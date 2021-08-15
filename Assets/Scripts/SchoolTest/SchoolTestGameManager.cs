using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SchoolTestGameManager : MonoBehaviour 
{
    public static SchoolTestGameManager Instance;

    public GameObject teacher;

    private SchoolTestMiniGame randomGame;

    private List<int> karakteri  =new List<int>(new int[]{0,1,2,3});

    private Transform canvas;

    private int miniGamesFinished = 0;

    void Awake()
    {
        Instance = this;
    }

    IEnumerator Start ()
    {
        canvas = GameObject.Find("Canvas").transform;
        yield return new WaitForSeconds(.5f);
        teacher.GetComponent<Animator>().Play("ShowStewardess");
        teacher.transform.Find("AnimationHolder").GetComponent<Animator>().Play("CharacterIdle_Pointing");
        yield return new WaitForSeconds(3f);
        teacher.GetComponent<Animator>().Play("HideStewardess");
        randomGame = (SchoolTestMiniGame)Random.Range(0, 3);
        BindRandomGameToChild();
    }


    void BindRandomGameToChild()
    {
        if (karakteri.Count > 0)
        {
            int randomChild = karakteri[Random.Range(0, karakteri.Count)];
            karakteri.Remove(randomChild);
            randomGame = (SchoolTestMiniGame)(((int)randomGame + 1) % 3);
            //randomGame = SchoolTestMiniGame.FindTheDifference;
            int index = randomChild;
            canvas.transform.Find("SceneGraphics/Characters/AnimationHolder").GetChild(randomChild).GetComponent<Button>().onClick.AddListener(delegate
                {
                    switch (randomGame)
                    {
                        case SchoolTestMiniGame.FindTheDifference:
                            canvas.Find("MiniGameFindTheDifference").gameObject.SetActive(true);
                            canvas.Find("MiniGameFindTheDifference").GetComponent<FindTheDifferenceGameManager>().Restart();
                            canvas.Find("MiniGameFindTheDifference").GetComponent<CanvasGroup>().alpha = 1;
                            canvas.transform.Find("SceneGraphics/Characters/AnimationHolder").GetChild(index).GetComponent<Button>().onClick.RemoveAllListeners();
                            canvas.transform.Find("SceneGraphics/Characters/AnimationHolder").GetChild(index).Find("AnimationHolder/Cloud").gameObject.SetActive(false);
                            break;

                        case SchoolTestMiniGame.MatchTheColor:
                            canvas.Find("MiniGameMatchTheColor").gameObject.SetActive(true);
                            canvas.Find("MiniGameMatchTheColor").GetComponent<MatchTheColorGameManager>().Restart();
                            canvas.Find("MiniGameMatchTheColor").GetComponent<CanvasGroup>().alpha = 1;
                            canvas.transform.Find("SceneGraphics/Characters/AnimationHolder").GetChild(index).GetComponent<Button>().onClick.RemoveAllListeners();
                            canvas.transform.Find("SceneGraphics/Characters/AnimationHolder").GetChild(index).Find("AnimationHolder/Cloud").gameObject.SetActive(false);
                            break;

                        case SchoolTestMiniGame.Puzzle:
                            canvas.Find("MiniGamePuzzle").gameObject.SetActive(true);
                            canvas.Find("MiniGamePuzzle").GetComponent<PuzzleGameManager>().StartNewGame();
                            canvas.Find("MiniGamePuzzle").GetComponent<CanvasGroup>().alpha = 1;
                            canvas.transform.Find("SceneGraphics/Characters/AnimationHolder").GetChild(index).GetComponent<Button>().onClick.RemoveAllListeners();
                            canvas.transform.Find("SceneGraphics/Characters/AnimationHolder").GetChild(index).Find("AnimationHolder/Cloud").gameObject.SetActive(false);
                            break;

                        default:
                            break;
                    }
                });
            canvas.transform.Find("SceneGraphics/Characters/AnimationHolder").GetChild(randomChild).Find("AnimationHolder/Cloud").gameObject.SetActive(true);
        }
    }
   

    public void MiniGameFinished()
    {
        miniGamesFinished++;
        BindRandomGameToChild();
        if (miniGamesFinished == 4) 
        {
            Debug.Log("PREBACI NA NAREDNU SCENU");
            canvas.transform.Find("TopUI/ButtonFinish").gameObject.SetActive(true);
        }
    }
}

[System.Serializable]
public enum SchoolTestMiniGame
{
    FindTheDifference,
    MatchTheColor,
    Puzzle
}
