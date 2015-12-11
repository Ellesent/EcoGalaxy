using UnityEngine;
using System.Collections;

public class CheckParentIsHidden : MonoBehaviour {
	
	


	// Update is called once per frame
	void FixedUpdate () {
		if(GetComponent<Renderer>() && transform.parent.GetComponent<Renderer>())
		{
			GetComponent<Renderer>().enabled  = transform.parent.GetComponent<Renderer>().enabled ? true:false;
		}
	}
}
