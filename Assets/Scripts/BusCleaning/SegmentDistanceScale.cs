using UnityEngine;
using System.Collections;

public class SegmentDistanceScale : MonoBehaviour {

	HingeJoint2D hj;
	float dist ;

	void Start () {
		hj = transform.GetComponent<HingeJoint2D>();
	}
	

	void Update () {
		float dist = Vector3.Distance(transform.position, hj.connectedBody.transform.position);
		if (dist >0.3f)  transform.localScale = new Vector3(3,1,1);
		else  if(dist >0.1f   )  transform.localScale = new Vector3(dist*10,1,1);
		else transform.localScale =    Vector3.one;

		//if (dist >0.1f)  transform.position  =  hj.connectedBody.transform.position;
	}
}
