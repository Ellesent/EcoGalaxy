using UnityEngine;
using System.Collections;

public class RealTimeCounter : MonoBehaviour {

    //create timer
    public float timer;
    public static int count = 0;

  

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
        Vector2 point = Camera.main.WorldToScreenPoint(transform.position);
        int minutes = Mathf.FloorToInt(timer / 60F);
        int seconds = Mathf.FloorToInt(timer - minutes * 60);
        string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);

        if (timer == 0)
        {
            if (GUI.Button(new Rect(point.x - 30, point.y  - 120, 100, 50), "Save Time"))
            {
                ResetClock();
            }
        }

        else
        GUI.Label(new Rect(point.x - 30, point.y - 120, 100, 50), "Time until collection " + niceTime);
    }

    void ResetClock()
    {
        TimeMaster.instance.SaveDate();
        timer = 200;
        timer -= TimeMaster.instance.CheckDate();
    }
}
