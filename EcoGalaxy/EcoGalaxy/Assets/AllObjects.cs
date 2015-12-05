using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AllObjects : MonoBehaviour {
     bool isDragging = true;
    bool isMoving = false;
    public int howMuch;
    public int pow;
    List<GameObject> grounds = new List<GameObject>();
    
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
        
        if (GameControl.isLoaded == false)
        {
            MoneyManager.money -= howMuch;
            MoneyManager.power += pow;
        }
        else { GameControl.isLoaded = false; isDragging = false; }
        
        
        
        grounds.AddRange(GameObject.FindGameObjectsWithTag("Ground"));
        
        if (tag == "Oxygen")
        {
            MoneyManager.Oxygen = true;
            
        }
        if (!GameControl.objects.Contains(gameObject))
        {
            GameControl.objects.Add(gameObject);
            GameControl.control.ConvertObjects();
        }
        
	
	}
	
	// Update is called once per frame
	void Update () {
       // Debug.Log("is Dragging" + isDragging); 
        //Debug.Log("is Loaded " + GameControl.isLoaded);
        //Debug.Log(isMoving);
        if (isDragging == true && GameControl.isLoaded == false)
        {
            if (grounds.Count == 0)
            {
                grounds.AddRange(GameObject.FindGameObjectsWithTag("Ground"));
            }
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
                        //Debug.Log("yes");
                        //Debug.Log(GetComponent<Collider2D>().bounds.min);
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
