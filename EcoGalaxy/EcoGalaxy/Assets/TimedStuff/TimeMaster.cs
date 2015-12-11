using UnityEngine;
using System.Collections;
using System;

public class TimeMaster : MonoBehaviour {
    DateTime currentDate;
    DateTime oldDate;

    public string saveLocation;
    public static TimeMaster instance;


	// Use this for initialization
	void Awake () {

        //create instance of Timemaster script
        instance = this;

        //set playerprefs to save location
        saveLocation = "LastSavedDate1";

	
	}

    //checks current time against saved time
    //returns difference in seconds
    public float CheckDate()
    {
        string tempString = "";
        //store current time on start
        
        currentDate = System.DateTime.Now;
        if (PlayerPrefs.HasKey(saveLocation))
        {
            tempString = PlayerPrefs.GetString(saveLocation);
            //string tempString1 = PlayerPrefs.GetString("Save");
        }
        else
        {
            Debug.Log("error");
        }

        //grab old time from playerprefs
        long tempLong = Convert.ToInt64(tempString);

        //convert old time from binary to dateTime
        DateTime oldDate = DateTime.FromBinary(tempLong);
        Debug.Log("old date: " + oldDate);

        //subtraction -timestamp

        TimeSpan difference = currentDate.Subtract(oldDate);
        Debug.Log("Differece: " + difference );

        return (float)difference.TotalSeconds; 
    }

    //saves current time, necessary to accurately check difference
    public void SaveDate()
    {
        //PlayerPrefs.DeleteKey(saveLocation);
        //saves current system time
       // if (!PlayerPrefs.HasKey(saveLocation))
        //{
            PlayerPrefs.SetString(saveLocation, System.DateTime.Now.ToBinary().ToString());
        //}
        //PlayerPrefs.SetString("Save", System.DateTime.Now.ToBinary().ToString());
        Debug.Log("Saving this date to Player Prefs: " + System.DateTime.Now);
    }
	

	
}
