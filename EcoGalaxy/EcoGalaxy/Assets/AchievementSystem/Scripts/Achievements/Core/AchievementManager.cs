using UnityEngine;
using System.Collections;
using System.Collections.Generic; 


/*
 * Achievement Manager
 * Purpose: To manage and organise all created achievements. 
 * */
public static class AchievementManager {
		 
	//static bool enableGoogle = true; 
	static List<BasicAchievement> achievements; 				//List of achievements. 

	public static void Initialise()
	{
		achievements = new List<BasicAchievement>(); 
	}

	//Call this to add an achivement. 
	public static void AddAchievement(BasicAchievement a)
	{
		if(achievements == null)
		{
			achievements = new List<BasicAchievement>();
		}
		if(!achievements.Contains (a))
		{
			achievements.Add (a);  
		}
		else
		{
			achievements[achievements.IndexOf (a)] = a;
		}
	}


	//Returns an achievement based from it's key. 
	public static BasicAchievement GetAchievement(string key)
	{ 
		if(achievements == null)
		{
			return null;//throw new UnityException("Please Start From Main Menu!!"); //Add exception back if you want to, I found it annoying. 
		}
		for(int i = 0; i<achievements.Count; i++)
		{
			if(achievements[i].Key == key )
			{
				return achievements[i];
			}
		}
		throw new UnityException("No Achievement Found!");
	}


	//Increment achievement
	public static void IncrementAchievement(string key)
	{
		BasicAchievement a = GetAchievement(key); 
		if(a != null)
		{
			a.Increment(); 
		}
	}

	//Increment achievement and show progress. 
	public static void IncrementAchievement(string key, bool showPopup)
	{
		BasicAchievement a = GetAchievement(key); 
		if(a != null)
		{
			a.Increment(); 
		}
		if(showPopup && a.IsLocked)
		{
			DisplayPopup(a);
		}
	}


	public static void UnlockAchievement(string key)
	{
		BasicAchievement a = GetAchievement(key); 
		if(a.IsLocked)
		{
			a.Unlock(); 
			DisplayPopup (a);
//			if(enableGoogle)
//			{
//				GoogleAchievementHandler.UnlockGoogleAchievement(key); //Comment out if not using google. 
//			}
		}

	}

	public static void LockAchievement(string key)
	{
		BasicAchievement a = GetAchievement(key); 
		a.Lock(); 
	}

	public static void DisplayPopup(BasicAchievement a)
	{
		AchievementHelper.DisplayPopup(a);
	}
	


}
