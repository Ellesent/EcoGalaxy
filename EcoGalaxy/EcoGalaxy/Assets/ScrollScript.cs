using UnityEngine;
using System.Collections;

public class ScrollScript : MonoBehaviour {

    public float speed = 0;

	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.A) && transform.position.x >= -5.6f)
        {
            transform.position -= new Vector3(speed, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(speed, 0);
        }
	}


}
