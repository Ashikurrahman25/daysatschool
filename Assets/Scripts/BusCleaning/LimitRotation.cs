using UnityEngine;
using System.Collections;

public class LimitRotation : MonoBehaviour {

	HingeJoint2D hj;
	float dist ;
	
	void Start () {
		hj = transform.GetComponent<HingeJoint2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(transform.rotation.eulerAngles.z >200 )  transform.rotation = Quaternion.Euler(new Vector3(0,0, 200));
		if(transform.rotation.eulerAngles.z <160 )  transform.rotation = Quaternion.Euler(new Vector3(0,0, 160));

		float dist = Vector3.Distance(transform.position, hj.connectedBody.transform.position);
		if (dist >0.1f)  transform.position  =  hj.connectedBody.transform.position;//+=  (hj.connectedBody.transform.position - transform.position).normalized*0.01f;
 
		 
	}
}
