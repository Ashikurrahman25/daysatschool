using UnityEngine;
using System.Collections;
 

public class MiniGame_DrawShapes : MonoBehaviour {
	RememberStateOfGrid rememberState;
 
	bool bCapture = false;
	public Transform TopLeft;
	public Transform BottomRight;
	public static Texture2D screenshot = null;


	void Start () {
		rememberState = gameObject.GetComponent<RememberStateOfGrid>();
		rememberState.SetGrid();
//		if( LevelTransition.Instance !=null ) LevelTransition.Instance.ShowScene();
	}
	
 
	public void ButtonFinishClicked()
	{
		bCapture = true;
		StartCoroutine("DelayButtonFinishClicked");

	}

	IEnumerator DelayButtonFinishClicked()
	{
		Gameplay.bReturnFromMiniGame = true;
		rememberState.SaveGrid();
		yield return new WaitForSeconds(.5f);
//		if( LevelTransition.Instance !=null ) LevelTransition.Instance.HideSceneAndLoadNext("Room2");

	}


	void OnPostRender() 
	{
		if (bCapture)
		{
			Vector2 tl = Camera.main.WorldToScreenPoint(TopLeft.position);
			Vector2 br =Camera.main.WorldToScreenPoint(BottomRight.position);


			Rect captureRect = new Rect(   Mathf.FloorToInt( tl.x) , Mathf.FloorToInt( tl.y),  Mathf.FloorToInt( Mathf.Abs( (br-tl).x)) , Mathf.FloorToInt( Mathf.Abs( (tl-br).y)) );
			bCapture = false;
			screenshot = new Texture2D ( (int) captureRect.width ,  (int) captureRect.height , TextureFormat.ARGB32, false);
			screenshot.ReadPixels (captureRect, 0, 0, false);
			screenshot.Apply ();
			 
		}
	}
}
