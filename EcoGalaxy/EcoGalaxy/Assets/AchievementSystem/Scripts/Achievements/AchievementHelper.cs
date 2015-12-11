using UnityEngine;
using System.Collections;
/*
 * Achievement Holder
 * Purpose: Helps with basic tasks related to achievements.  
 * 
 * Usage: Attach to an empty Game Object or camera at game start. 
 * */
public class AchievementHelper : MonoBehaviour {


	public GameObject popup; 						//The popup displayed when an achievement is unlocked.
	public Transform popupLocation; 				//The location the popup is created at. 
	public bool enableProgressBar; 					//Is the progress bar enabled on the popup by default. 
	public float distanceBetweenNotifications;		//The distance between stacking achievements. 
	public int maxConcurrentPopups;					//The max number of popups on screen at once. 

	//The majority of these static values are set on game start, with public values used to set their defaults. 

	static bool progressBarEnabled; 				//Is the progress bar enabled?
	static GameObject myGameObject; 				//A link to gameobject this is held in. 
	static GameObject defaultPopup;					//What is the default popup to be created.
	static AchievementPopup myPopup; 				//A link to a popup. 
	static Vector3 location; 						//The location for a popup.
	static Vector3 defaultLocation; 				//The original location for popup without editing. 
	static float distance; 							//The distance between notifications/achievements. 
	static GameObject current; 						//The current popup being displayed. 
	static int max; 								//The max number of popups.
	static int numberOut; 							//Popups out. 
	// Use this for initialization
	void Start () {

		if(popup)
		{
			progressBarEnabled = enableProgressBar;
			defaultPopup = popup; 

			myPopup = (defaultPopup.GetComponent<AchievementPopup>() != null) ? defaultPopup.GetComponent<AchievementPopup>():defaultPopup.AddComponent<AchievementPopup>();
			if(popupLocation)
			{
				location = popupLocation.position; 
				defaultLocation = popupLocation.transform.position;
			}
			else
			{
				location = FindObjectOfType<PopupMarker>() != null ? FindObjectOfType<PopupMarker>().transform.position:new Vector3(0,0,0);
				defaultLocation = location;
			}
			distance = distanceBetweenNotifications;
			max = maxConcurrentPopups;
			numberOut = 0; 
		}
		else
		{
			popup = (GameObject)Resources.Load ("DefaultPopup");
			distance = 1.5f; 
			max = 4; 
			if(popupLocation)
			{
				location = popupLocation.position; 
				defaultLocation = popupLocation.transform.position;
			}
			else
			{
				location = FindObjectOfType<PopupMarker>() != null ? FindObjectOfType<PopupMarker>().transform.position:new Vector3(0,0,0);
				defaultLocation = location;
			}
			numberOut = 0; 
		}
		myGameObject = gameObject;
	}
	public void ClearCurrent()
	{
			numberOut = 0;
			current = null; 
	}

	public static void DeadPopup()
	{
			//numberOut--;
	}
	public static void DisplayPopup(BasicAchievement a)
	{
		myGameObject.GetComponent<AchievementHelper>().CancelInvoke();
		myGameObject.GetComponent<AchievementHelper>().Invoke ("ClearCurrent", 1f);

		if(numberOut +1 > max)
		{
			myGameObject.GetComponent<AchievementHelper>().ClearCurrent();
		}
		if(current != null)
		{
			location = defaultLocation; 
			location.y -= (distance * numberOut); 
		}
		else
		{
			location = defaultLocation; 

		}

		current = (GameObject)Instantiate (defaultPopup,location,defaultPopup.transform.rotation); 
		numberOut++;
		myPopup = current.GetComponent<AchievementPopup>(); 
		if(a.IsIncremental && progressBarEnabled)
		{
			myPopup.SetValues(a.Name,a.Progress,a.Icon,progressBarEnabled); 
		}
		else
		{
			myPopup.SetValues("Achievement Unlocked",a.Name,a.Icon);
		}
	}
	
}
