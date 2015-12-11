using UnityEngine;
using System.Collections;

public class RealTimeCounter : MonoBehaviour {

    //create timer
    public float timer;
    float originalTime;
   // public static int count = 0;

  

	// Use this for initialization
	void Awake () {
        originalTime = timer;

        //timer = PlayerPrefs.("saveLocation");
       
        //init timer to starting amount
        //ResetClock();
        //update timer with real time
        if (GameControl.isLoaded ==false)
        {
        
            TimeMaster.instance.SaveDate();
        
        }
        timer -= TimeMaster.instance.CheckDate();
       
	
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("Timer" + timer);
        Debug.Log("Orig" + originalTime);
        

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
                if (tag == "SolarPanel")
                {
                    MoneyManager.power += 100;
                }
                if (tag == "WaterCollect")
                {
                    MoneyManager.water += 100;
                }
                if (tag == "Food")
                {
                    MoneyManager.food += 100;
                }
                ResetClock();
            }
        }

        else if (GameControl.isLoaded == false)
        {
            GUI.Label(new Rect(point.x - 30, point.y - 120, 100, 50), "Time until collection " + niceTime);
        }
    }

    void ResetClock()
    {
        TimeMaster.instance.SaveDate();
        timer = originalTime;
        timer -= TimeMaster.instance.CheckDate();
    }
}
