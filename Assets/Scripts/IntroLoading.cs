using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroLoading : MonoBehaviour {

    public string sceneToLoad;

	IEnumerator Start () {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(sceneToLoad);
	}
	
	
}
