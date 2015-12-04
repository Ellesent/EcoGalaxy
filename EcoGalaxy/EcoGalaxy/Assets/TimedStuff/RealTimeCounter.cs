using UnityEngine;
using System.Collections;

public class RealTimeCounter : MonoBehaviour {

    //create timer
    public float timer;

	// Use this for initialization
	void Start () {
        timer = 200; 
        //init timer to starting amount
        //ResetClock();
        //update timer with real time
        timer -= TimeMaster.instance.CheckDate();
	
	}
	
	// Update is called once per frame
	void Update () {

        //make timer real time - each frame
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer = 0; 
        }
	
	}

    void OnGUI()
    {
        if (GUI.Button(new Rect(gameObject.transform.position.x, transform.position.y + 5, 50, 50), "Save Time"))
        {
            ResetClock();
        }

        GUI.Label(new Rect(gameObject.transform.position.x + 80, transform.position.y, 50, 50), timer.ToString());
    }

    void ResetClock()
    {
        TimeMaster.instance.SaveDate();
        timer = 200;
        timer -= TimeMaster.instance.CheckDate();
    }
}
