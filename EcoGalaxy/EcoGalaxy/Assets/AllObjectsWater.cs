using UnityEngine;
using System.Collections;

public class AllObjectsWater : MonoBehaviour {

    bool isDragging = true;
    bool isMoving = false;
    public int howMuch;
    public int pow;
    GameObject[] water;

	// Use this for initialization
	void Start () {

        MoneyManager.money -= howMuch;
        water = GameObject.FindGameObjectsWithTag("Water");
        MoneyManager.power += pow;
	}
	
	// Update is called once per frame
	void Update () {

        if (isDragging == true)
        {
            Vector3 mouse = Input.mousePosition;
            mouse.z = 10;
            this.transform.position = Camera.main.ScreenToWorldPoint(mouse);

            if (Input.GetMouseButtonDown(0))
            {
                foreach (GameObject w in water)
                {
                    //if (GameObject.FindGameObjectWithTag("Ground").GetComponent<Collider2D>() == Physics2D.OverlapPoint(GetComponent<Collider2D>().bounds.min))
                    if (w.GetComponent<Collider2D>().bounds.Contains(GetComponent<Collider2D>().bounds.center) && isMoving == false)
                    {
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
