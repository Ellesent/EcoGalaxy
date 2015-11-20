using UnityEngine;
using System.Collections;

public class TwoPCharMover : MonoBehaviour {
    float speed = 0.1f;
    public GameObject empty; 
    //public Camera twoCam;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-speed, 0);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += new Vector3(speed, 0);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += new Vector3(0, -speed);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += new Vector3(0, speed);
        }
	
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Lake")
        {
            MoneyManager.water += 50;
            Instantiate(empty, coll.gameObject.transform.position, Quaternion.identity);
            Destroy(coll.gameObject);
            
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Carrot")
        {
            MoneyManager.food += 50;
            Destroy(coll.gameObject);
        }
    }
}
