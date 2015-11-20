using UnityEngine;
using System.Collections;

public class AllObjects : MonoBehaviour {
    bool isDragging = true;
    bool isMoving = false;
    public int howMuch;
    public int pow;
    GameObject[] grounds;
    
	// Use this for initialization
	void Start () {
        MoneyManager.money -= howMuch;
        MoneyManager.power += pow;
        
        grounds = GameObject.FindGameObjectsWithTag("Ground");
        if (tag == "Oxygen")
        {
            MoneyManager.Oxygen = true;
            
        }
        
	
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(isMoving);
        if (isDragging == true)
        {
            Vector3 mouse = Input.mousePosition;
            mouse.z = 10;
            this.transform.position = Camera.main.ScreenToWorldPoint(mouse);

            if (Input.GetMouseButtonDown(0))
            {
                foreach (GameObject g in grounds)
                {
                    //if (GameObject.FindGameObjectWithTag("Ground").GetComponent<Collider2D>() == Physics2D.OverlapPoint(GetComponent<Collider2D>().bounds.min))
                    if (g.GetComponent<Collider2D>().bounds.Contains(GetComponent<Collider2D>().bounds.min) && isMoving == false)
                    {
                        Debug.Log("yes");
                        Debug.Log(GetComponent<Collider2D>().bounds.min);
                        isDragging = false;
                    }
                }
            }
        }
       
	
	}

    void OnMouseDown()
    {
        if (isDragging == false)
        {
            isDragging = true;
            isMoving = true;
        }
    }
    void OnMouseUp()
    {
        isMoving = false;
    }
}
