using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Branch : MonoBehaviour {
	RectTransform rect;
	public bool bCut = false;
	float speedx = 1;
	float speedy = 1;
	float gravity = -0.2f;
	// Use this for initialization
	void Start () {
		rect = transform.GetComponent<RectTransform>();

		speedx = Random.Range(-2,2);
		speedy = Random.Range(3,4);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(bCut)
		{
			speedy += gravity;
			rect.anchoredPosition += new Vector2(speedx,speedy);

			if(rect.anchoredPosition.y< -500) GameObject.Destroy(this.gameObject);
		}
	}
}
