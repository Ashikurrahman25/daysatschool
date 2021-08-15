using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindTheDifferenceGameManager : MonoBehaviour 
{
    public static FindTheDifferenceGameManager Instance;
    public FindTheDifferenceData m_data;
    public Sprite selectorCircleSprite;

    private Image leftPic,rightPic;
    private Transform leftDifferences,rightDifferences;

    private int foundDifferences=0;
    private int differencesToFound;
    private int index = 0;

    void Awake()
    {
        Instance = this;
        leftDifferences = transform.Find("AnimationHolder/BookHolder/AnimationHolder/LeftPicture/Differences");
        rightDifferences = transform.Find("AnimationHolder/BookHolder/AnimationHolder/RightPicture/Differences");
        leftPic = transform.Find("AnimationHolder/BookHolder/AnimationHolder/LeftPicture").GetComponent<Image>();
        rightPic = transform.Find("AnimationHolder/BookHolder/AnimationHolder/RightPicture").GetComponent<Image>();
    }

	void Start () 
    {
        int random = index % m_data.levels.Count;
        LoadLevel(random);
        random = (random + 1) % m_data.levels.Count;
        index = random;
    }

    public void Restart()
    {
        int random = index % m_data.levels.Count;
        LoadLevel(index);
        random = (random + 1) % m_data.levels.Count;
        index = random;
    }

    public void LoadLevel(int levelIndex)
    {
        transform.Find("ButtonHelp").GetComponent<Button>().interactable = true;
        FindTheDifferenceLevel level = m_data.levels[levelIndex];
        GameObject diff = new GameObject("Diff", typeof(RectTransform));
        leftPic.sprite = level.picture1;
        rightPic.sprite = level.picture2;
        for(int i=leftDifferences.childCount-1;i>=0;i--)
        {
            Destroy(leftDifferences.GetChild(i).gameObject);
        }

        for(int i=rightDifferences.childCount-1;i>=0;i--)
        {
            Destroy(rightDifferences.GetChild(i).gameObject);
        }

        GameObject instDiff;
        GameObject circle;

        foundDifferences = 0;
        differencesToFound = level.differences.Count;

        foreach (DifferenceObject difference in level.differences) {
            if(difference.pictureId==DifferencePicture.LeftPicture)
            {
                instDiff=GameObject.Instantiate(diff);
                instDiff.transform.SetParent(leftDifferences);
                instDiff.transform.localScale = Vector3.one;
                instDiff.GetComponent<RectTransform>().sizeDelta = difference.sizeDelta;
                instDiff.GetComponent<RectTransform>().anchorMax = difference.anchorMax;
                instDiff.GetComponent<RectTransform>().anchorMin = difference.anchorMin;
                instDiff.GetComponent<RectTransform>().anchoredPosition3D = difference.anchoredPosition3D;
                instDiff.AddComponent<Image>().color=new Color(1f,1f,1f,0f);
                instDiff.AddComponent<Difference>();

                circle = GameObject.Instantiate(diff);
                circle.transform.SetParent(instDiff.transform);
                circle.transform.localScale = Vector3.one;
                circle.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
                circle.AddComponent<Image>().preserveAspect = true;
                circle.GetComponent<Image>().sprite = selectorCircleSprite;
                circle.GetComponent<Image>().enabled = false;
                circle.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
                circle.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
                circle.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
                circle.GetComponent<RectTransform>().pivot = new Vector2(.5f, .5f);
                circle.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
            }
            else
            {
                instDiff=GameObject.Instantiate(diff);
                instDiff.transform.SetParent(rightDifferences); 
                instDiff.transform.localScale = Vector3.one;
                instDiff.GetComponent<RectTransform>().sizeDelta = difference.sizeDelta;
                instDiff.GetComponent<RectTransform>().anchorMax = difference.anchorMax;
                instDiff.GetComponent<RectTransform>().anchorMin = difference.anchorMin;
                instDiff.GetComponent<RectTransform>().anchoredPosition3D = difference.anchoredPosition3D;
                instDiff.AddComponent<Image>().color=new Color(1f,1f,1f,0f);
                instDiff.AddComponent<Difference>();

                circle = GameObject.Instantiate(diff);
                circle.transform.SetParent(instDiff.transform);
                circle.transform.localScale = Vector3.one;
                circle.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
                circle.AddComponent<Image>().preserveAspect = true;
                circle.GetComponent<Image>().sprite = selectorCircleSprite;
                circle.GetComponent<Image>().enabled = false;
                circle.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
                circle.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
                circle.GetComponent<RectTransform>().pivot = new Vector2(.5f, .5f);
                circle.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;

            }
        }
    }

    public void FoundDifference()
    {
        if (++foundDifferences == differencesToFound)
            Finished();
    }

    void Finished()
    {
        Debug.Log("Finished find the difference miniGame");
        SchoolTestGameManager.Instance.MiniGameFinished();
        transform.Find("ButtonHelp").GetComponent<Button>().interactable = false;
        StartCoroutine(FadeOutAndDisable());
    }

    public void Help()
    {
        StopAllCoroutines();
        foreach(Transform t in leftDifferences)
            if(t.GetComponent<Difference>().enabled)
            {
                StartCoroutine(Hint(t.GetChild(0)));
            }
        foreach (Transform t in rightDifferences)
            if (t.GetComponent<Difference>().enabled)
            {
                StartCoroutine(Hint(t.GetChild(0)));
            }
    }
    IEnumerator Hint(Transform t)
    {
        if (t.transform.parent.GetComponent<Difference>().enabled)
        {
            t.GetComponent<Image>().enabled = true;
            float a = 1f;
            while (a > 0)
            {
                a -= 0.02f;
                if (t.transform.parent.GetComponent<Difference>().enabled)
                    t.GetComponent<Image>().color = new Color(1, 1, 1, a);
                else
                    t.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                yield return new WaitForSeconds(0.01f);
            }
            if (t.transform.parent.GetComponent<Difference>().enabled)
                t.GetComponent<Image>().enabled = false;
        }
        else
            t.GetComponent<Image>().color = Color.white;

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

