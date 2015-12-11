using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
/*
 * Achievement Holder
 * Purpose: To hold and ensure the achievements are serialised. This class is a workaround serialising static values.  
 * 
 * Usage: Attach to an empty Game Object at game start, you DO NOT need to use initialise when using this class. 
 * */
public class AchievementHolder : MonoBehaviour {
	
	public List<AchievementComponent> listOfAchievements; 

	public void Start()
	{
		foreach(AchievementComponent c in listOfAchievements)
		{
			c.Start ();
		}
	}

	public AchievementComponent this[int i]
	{
		set{listOfAchievements[i] = value;}
		get{if(i < listOfAchievements.Count)
			return listOfAchievements[i];
			else
				return null;}
	}

	public void CommitToPlayerPrefs()
	{
		foreach (AchievementComponent a in listOfAchievements)
		{
				a.CommitChanges();
		}
	}
}
