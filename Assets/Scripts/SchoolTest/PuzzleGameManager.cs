using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleGameManager : MonoBehaviour {

    public static PuzzleGameManager Instance;

    public PuzzleData data;

    public Image darkenPuzzleImage;
    public GameObject puzzleHolder;
    public Transform piecesHolder;

    Puzzle randomPuzzle;

    void Awake()
    {
        Instance = this;
    }

	void Start () 
    {
        StartNewGame();
	}

    public void CheckForFinish()
    {
        bool finished = true;

        for(int i=0;i<=8;i++)
        {
            if(!puzzleHolder.transform.GetChild(i).GetComponent<PuzzleSlot>().RightSprite())
            {
                finished = false;
                break;
            }
        }

        if (finished)
            FinishedPuzzle();

        Debug.LogError("Game Ened " + finished);
    }

    void FinishedPuzzle()
    {
        Debug.Log("Puzzle Finished!");
        SchoolTestGameManager.Instance.MiniGameFinished();
        StartCoroutine(FadeOutAndDisable());
    }

    public void StartNewGame()
    {
        randomPuzzle = data.puzzles[Random.Range(0, data.puzzles.Count)];
        Restart();
    }

    public void Restart()
    {
        darkenPuzzleImage.sprite = randomPuzzle.fullImage;

        for(int i=1;i<=9;i++)
        {
            puzzleHolder.transform.Find("Part" + i.ToString()).GetComponent<PuzzleSlot>().desiredSprite = randomPuzzle.parts[i - 1];
            if (puzzleHolder.transform.Find("Part" + i.ToString()).childCount == 1)
            {
                puzzleHolder.transform.Find("Part" + i.ToString()).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(170, 170);
                puzzleHolder.transform.Find("Part" + i.ToString()).GetChild(0).SetParent(piecesHolder);
            }
        }
        foreach(Transform t in piecesHolder.transform)
        {
            t.GetComponent<ItemDrag>().enabled = true;
        }

        List<int> pieces=new List<int>(new int[]{1,2,3,4,5,6,7,8,9});
        int k=0;
        while(pieces.Count>0)
        {
            int random = Random.Range(0, pieces.Count);
            piecesHolder.Find("Piece" + pieces[random].ToString()).GetComponent<Image>().sprite = randomPuzzle.parts[k++];
            pieces.RemoveAt(random);
        }

    }
    IEnumerator FadeOutAndDisable()
    {
        float a = 1f;
        while(a>0)
        {
            a -= 0.01f;
            gameObject.GetComponent<CanvasGroup>().alpha = a;
            yield return new WaitForSeconds(0.01f);
        }
        this.gameObject.SetActive(false);

    }
	
}
