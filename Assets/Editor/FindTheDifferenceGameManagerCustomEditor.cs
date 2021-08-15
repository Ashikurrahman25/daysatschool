using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

[CustomEditor(typeof(FindTheDifferenceGameManager))]
public class FindTheDifferenceGameManagerCustomEditor : Editor {

    SerializedProperty data;

    private GameObject gameObject;

    void OnEnable()
    {
//        serializedObject = new SerializedObject(target);
        data = serializedObject.FindProperty("m_data");
        gameObject = ((FindTheDifferenceGameManager)target).gameObject;
    }

    public override void OnInspectorGUI()
    {
        base.DrawDefaultInspector();
        serializedObject.Update();

        if(GUILayout.Button("Add Level To Data"))
        {
            FindTheDifferenceData dataSo = (FindTheDifferenceData)data.objectReferenceValue;
            FindTheDifferenceLevel levelToAdd = new FindTheDifferenceLevel();
            levelToAdd.picture1 = gameObject.transform.Find("AnimationHolder/BookHolder/AnimationHolder/LeftPicture").GetComponent<Image>().sprite;
            levelToAdd.picture2 = gameObject.transform.Find("AnimationHolder/BookHolder/AnimationHolder/RightPicture").GetComponent<Image>().sprite;
            foreach(Transform t in gameObject.transform.Find("AnimationHolder/BookHolder/AnimationHolder/LeftPicture/Differences"))
            {
                DifferenceObject difObj = new DifferenceObject();
                difObj.pictureId = DifferencePicture.LeftPicture;
                difObj.anchoredPosition3D = t.GetComponent<RectTransform>().anchoredPosition3D;
                difObj.anchorMax = t.GetComponent<RectTransform>().anchorMax;
                difObj.anchorMin = t.GetComponent<RectTransform>().anchorMin;
                difObj.sizeDelta = t.GetComponent<RectTransform>().sizeDelta;
                levelToAdd.differences.Add(difObj);
            }

            foreach(Transform t in gameObject.transform.Find("AnimationHolder/BookHolder/AnimationHolder/RightPicture/Differences"))
            {
                DifferenceObject difObj = new DifferenceObject();
                difObj.pictureId = DifferencePicture.RightPicture;
                difObj.anchoredPosition3D = t.GetComponent<RectTransform>().anchoredPosition3D;
                difObj.anchorMax = t.GetComponent<RectTransform>().anchorMax;
                difObj.anchorMin = t.GetComponent<RectTransform>().anchorMin;
                difObj.sizeDelta = t.GetComponent<RectTransform>().sizeDelta;
                levelToAdd.differences.Add(difObj);
            }

            for (int i = dataSo.levels.Count-1; i >=0; i--)
            {
                if (dataSo.levels[i].picture1 == levelToAdd.picture1)
                    dataSo.levels.RemoveAt(i);
            }

            dataSo.levels.Add(levelToAdd);

        }
    }
}
