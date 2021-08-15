using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/**
  * Scene:Splash
  * Object:Main Camera
  * Description: F-ja zaduzena za ucitavanje MainScene-e, kao i vizuelni prikaz inicijalizaije CrossPromotion-e i ucitavanja scene
  **/
public class SplashScene : MonoBehaviour {
	
	float myProgress=0;
	string sceneToLoad;
	// Use this for initialization
	void Start ()
	{
		sceneToLoad = "MapScene";
		

		StartCoroutine(LoadScene());
	}
	
	/// <summary>
	/// Coroutine koja ceka dok se ne inicijalizuje CrossPromotion, menja progres ucitavanja CrossPromotion-a, kao i progres ucitavanje scene, i taj progres se prikazuje u Update-u
	/// </summary>
	IEnumerator LoadScene()
    {


        while (myProgress < 0.99)
        {
            myProgress += 0.02f;

            yield return new WaitForSeconds(0.05f);
        }
        SceneManager.LoadScene(sceneToLoad);
    }		
}
