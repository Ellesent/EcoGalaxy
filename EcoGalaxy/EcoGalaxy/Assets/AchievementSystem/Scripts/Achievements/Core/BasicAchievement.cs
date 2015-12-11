using UnityEngine;
using System.Collections;

public class BasicAchievement {

	string aName; 									//The achievements name. 
	string myKey; 									//Key to access the achievement. 
	string myDescription;								//Description of achievement. 

	int currentValue;								//If incremental the current value. 
	int myGoal;										//If incremental the goal to reach. 

	bool incremental; 								//Whether the achievement is incremental (ie. 25/100 kills etc.) 
	bool unlocked; 									//Is the achievement unlocked? 

	Texture lockedIcon;								//The icon of the achievement when locked. 
	Texture unlockedIcon; 							//The icon of the achievement when unlocked. 


	public BasicAchievement(string key, string name, string description, Texture lockedTexture, Texture unlockedTexture)
	{
		incremental = false; 
		aName = name; 
		myKey = key;
		lockedIcon = lockedTexture; 
		unlockedIcon = unlockedTexture; 
		myDescription = description; 
		ReadPlayerPrefs(); 
	}

	public BasicAchievement(string key, string name, string description, Texture lockedTexture, Texture unlockedTexture, int goal)
	{
		aName = name; 
		myKey = key;
		myGoal = goal; 
		lockedIcon = lockedTexture; 
		unlockedIcon = unlockedTexture; 
		myDescription = description; 
		incremental = true; 
		ReadPlayerPrefs(); 
	}
	
	public string Name{
		get{return aName;}
	}

	public string Key{
		get{return myKey;}
	}

	public string Description{
		get{return myDescription;}
	}
	
	public bool IsLocked{
		get{return !unlocked;}
	}

	public bool IsIncremental{
		get{return incremental;}
	}

	public Texture Icon{
		get{
			if(unlocked)
			{
				return unlockedIcon;
			}
			else
			{
				return lockedIcon;
			}
		}
	}

	public Vector2 Progress{
		get{return new Vector2(currentValue,myGoal);}
	}
	public void Unlock()
	{
		unlocked = true; 
		(null as PlayerPrefs).SetBool (myKey, true); 
	}

	public void Lock()
	{
		unlocked = false; 
		currentValue = 0; 
		PlayerPrefs.SetInt(myKey+"count", currentValue);
		(null as PlayerPrefs).SetBool (myKey, true); 
	}

	public void Increment()
	{
		if(!unlocked)
		{
			currentValue++; 
			PlayerPrefs.SetInt (myKey+"count",currentValue);
			if(currentValue >= myGoal)
			{
				AchievementManager.UnlockAchievement(myKey);
			}
		}
		else
		{
			return;
		}

	}

	void ReadPlayerPrefs()
	{
		if(PlayerPrefs.HasKey (myKey))
		{
			incremental = (null as PlayerPrefs).GetBool (myKey + "incremental");
			unlocked = (null as PlayerPrefs).GetBool (myKey); 
			aName = PlayerPrefs.GetString (myKey + "name");
			myDescription = PlayerPrefs.GetString(myKey + "description");

			if(incremental)
			{
				myGoal = PlayerPrefs.GetInt (myKey + "goal");
				currentValue = PlayerPrefs.GetInt (myKey+"count");
			}
		}
		else
		{
			unlocked = false; 
			(null as PlayerPrefs).SetBool (myKey,unlocked); 
			PlayerPrefs.SetString (myKey + "name",aName);
			PlayerPrefs.SetString(myKey+ "description", myDescription);
			if(incremental)
			{
				currentValue = 0; 
				PlayerPrefs.SetInt (myKey+"goal",myGoal);
				PlayerPrefs.SetInt(myKey+"count",currentValue);
				(null as PlayerPrefs).SetBool(myKey+"incremental", true);
			}
		}
	}


	//Warning will reset values. 
	public void CommitToPlayerPrefs(string key, string name, string description, Texture lockedTexture, Texture unlockedTexture)
	{
			myKey = key; 
			aName = name; 
			myDescription = description;
			lockedIcon = lockedTexture; 
			unlockedIcon = unlockedTexture; 
			unlocked = false; 
			incremental = false;
		(null as PlayerPrefs).SetBool(myKey+"incremental", false);
			(null as PlayerPrefs).SetBool (myKey,unlocked); 
			PlayerPrefs.SetString (myKey + "name",aName);
			PlayerPrefs.SetString(myKey+ "description", myDescription);
		PlayerPrefs.Save ();
	}

	public void CommitToPlayerPrefs(string key, string name, string description, Texture lockedTexture, Texture unlockedTexture, int goal)
	{
		CommitToPlayerPrefs(key,name,description,lockedTexture,unlockedTexture);
		incremental = true; 
		currentValue = 0; 
		myGoal = goal;
		PlayerPrefs.SetInt (myKey+"goal",myGoal);
		PlayerPrefs.SetInt(myKey+"count",currentValue);
		(null as PlayerPrefs).SetBool(myKey+"incremental", true);
		PlayerPrefs.Save();
	}
	//If removing an acheivement call this to remove from playerprefs. 
	public void KillMe()
	{
		if(PlayerPrefs.HasKey(myKey))
		{
			PlayerPrefs.DeleteKey(myKey); 
			PlayerPrefs.DeleteKey(myKey + "incremental");
			PlayerPrefs.DeleteKey(myKey + "description");
		}
	}
	
}
