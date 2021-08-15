using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizGameManager : MonoBehaviour {

    public int answersToDisplay;
    public QuestionContainer questionSprites;

    Vector3[] answerButtonLocalPositions;
    public Transform questionsPoolHolder;
    public Transform answersPoolHolder;

    List<Transform> answersToDisplayList;
    int[] answersIndices;
    int currentQuestion;

    int tempRand;
    int tempRand2;
    bool checking = false;

    void Awake()
    {
        // We map answer index to each question
        answersIndices = new int[questionsPoolHolder.childCount];
        for (int i = 0; i < questionsPoolHolder.childCount; i++)
        {
            tempRand = Random.Range(0,answersPoolHolder.childCount);
            tempRand2 = Random.Range(0,questionSprites.questions[tempRand].sprites.Length);
            answersIndices[i] = tempRand;
            questionsPoolHolder.GetChild(i).GetComponent<Image>().sprite = questionSprites.questions[tempRand].sprites[tempRand2];
        }
    }

    void Start()
    {
        // We take positions of first few buttons as default positions
        answerButtonLocalPositions = new Vector3[answersToDisplay];
        for (int i = 0; i < answersToDisplay; i++)
        {
            answerButtonLocalPositions[i] = answersPoolHolder.GetChild(i).localPosition;
        }

        answersToDisplayList = new List<Transform>();
        currentQuestion = 0;
        if (answersPoolHolder.childCount > answersToDisplay)
            SelectButtonsToDisplay(answersIndices[currentQuestion]);
    }

    void OnEnable()
    {
        Invoke("PlayFirstItemAnim", GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
    }

    void OnDisable()
    {
        CancelInvoke();
    }

    void PlayFirstItemAnim()
    {
        questionsPoolHolder.GetChild(0).GetComponent<Animator>().Play("QuestionShow",0,0);
    }

    void DoneChecking()
    {
        checking = false;
    }

    /// <summary>
    /// Checks the answer.
    /// </summary>
    /// <param name="index">Index.</param>
    public void CheckTheAnswer(int index)
    {
        if (checking || currentQuestion >= questionsPoolHolder.childCount)
            return;
        checking = true;

        // check for match
        if (answersIndices[currentQuestion] == index)
        {
            Debug.Log("BRAVO");
            SoundManagerEyeExamination.PlaySound("MiniGameCorrect");
            answersPoolHolder.GetChild(index).GetComponent<Animator>().Play("CorrectAnswer");
            Invoke("NextQuestion",answersPoolHolder.GetChild(index).GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        }
        else
        {
            Debug.Log("WRONG!");
            SoundManagerEyeExamination.PlaySound("MiniGameWrong");
            answersPoolHolder.GetChild(index).GetComponent<Animator>().Play("WrongAnswer");
            Invoke("DoneChecking",answersPoolHolder.GetChild(index).GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        }


    }

    void NextQuestion()
    {
        questionsPoolHolder.GetChild(currentQuestion).GetComponent<Animator>().Play("QuestionHide",0,0);
        currentQuestion++;
        // check for game over
        if (currentQuestion < questionsPoolHolder.childCount)
        {
            Debug.Log("Next question");
            if (answersPoolHolder.childCount > answersToDisplay)
                SelectButtonsToDisplay(answersIndices[currentQuestion]);

            questionsPoolHolder.GetChild(currentQuestion).GetComponent<Animator>().Play("QuestionShow",0,0);
        }
        else
        {
            Debug.Log("Quiz game over");
            GetComponent<ItemGame>().GameFinished();
            GameObject.Find("Canvas").GetComponent<MenuManager>().ClosePopUpMenu(gameObject);
        }

        checking = false;
    }

    /// <summary>
    /// Selects the buttons to display if needed.
    /// </summary>
    /// <param name="index">Index of the correct answer button.</param>
    public void SelectButtonsToDisplay(int index)
    {
        // clear the list
        answersToDisplayList.Clear();

        //deactivate all answers
        foreach (Transform answer in answersPoolHolder)
        {
            answer.gameObject.SetActive(false);
        }

        // fill in the list with random answers
        //TODO add specific rules to selection so we can have more options, not jus random

        for (int i = 0; i < answersToDisplay; i++)
        {
            tempRand = Random.Range(0,answersPoolHolder.childCount);

            if (answersToDisplayList.Contains(answersPoolHolder.GetChild(tempRand)))
            {
                while (answersToDisplayList.Contains(answersPoolHolder.GetChild(tempRand)))
                {
                    tempRand = Random.Range(0,answersPoolHolder.childCount);
                }
            }
            answersToDisplayList.Add(answersPoolHolder.GetChild(tempRand));
        }
        // if correct answer is not in the list, replace it with random wrong answer
        if (!answersToDisplayList.Contains(answersPoolHolder.GetChild(answersIndices[currentQuestion])))
        {
            answersToDisplayList[Random.Range(0,answersToDisplayList.Count)] = answersPoolHolder.GetChild(answersIndices[currentQuestion]);
        }

        foreach (Transform answer in answersToDisplayList)
        {
            answer.gameObject.SetActive(true);
        }

        // position the buttons
        for (int i = 0; i < answersToDisplay; i++)
        {
            answersToDisplayList[i].localPosition = answerButtonLocalPositions[i];
        }
    }
}
