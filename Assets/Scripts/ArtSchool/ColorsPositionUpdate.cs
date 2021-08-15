using UnityEngine;
using System.Collections;

/// <summary>
/// <para>Scene: Gameplay</para>
/// <para>Object: ButtonLeft, ButtonRight</para>
/// <para>Description: Pomera scrollRect sa bojama klikom na strelice</para>
/// </summary>
public class ColorsPositionUpdate : MonoBehaviour {

	public static bool targetUpdated = false;
	public static Vector3 target;
	
	// Update is called once per frame
//	void Update () 
//	{
//		if(targetUpdated)
//		{
//			transform.localPosition = Vector3.Lerp(transform.localPosition, target, 0.5f);
//
//			if(Vector3.Distance (transform.localPosition,target) < 0.2f)
//			{
//				transform.localPosition = target;
//				targetUpdated = false;
//			}
//		}
//	}
}
