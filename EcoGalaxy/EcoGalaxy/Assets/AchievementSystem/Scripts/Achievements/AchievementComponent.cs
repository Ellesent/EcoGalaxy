using UnityEngine;
using System.Collections;

/*
 * Achievement Component
 * Purpose: Used to set-up and display an achievement. 
 * 
 * 
 */
public class AchievementComponent : MonoBehaviour {


	public string key;								//The key used to access/store the achievement.
	public string myName; 							//The title/name of the achievement.
	public string description; 						//The description of the achievement. 
		
	public bool isIncremental; 						//Whether the achievement is incremental.
	public int goal; 								//If it is incremental then what is the goal. 
	
	public Texture lockedTexture; 					//The texture when locked.	
	public Texture unlockedTexture; 				//The texture when unlocked. 

	public TextMesh nameTextMesh; 					//The textmesh used to represent the title. 
	public TextMesh descriptionTextMesh; 			//The textmesh used to represent the description.
	public GameObject icon; 						//The gameobject used to represent the icon.
	public GameObject progress;						//If a progress bar is wanted this should be set to that.

	BasicAchievement myAchievement;


	// Use this for initialization
	public void Start () 
	{
		myAchievement = (!isIncremental) ? 
							new BasicAchievement(key,myName,description,lockedTexture,unlockedTexture) : 
						 	new BasicAchievement(key,myName,description,lockedTexture,unlockedTexture,goal);
		if(string.IsNullOrEmpty(myAchievement.Name))
		{
			this.gameObject.SetActive(false);
			return; 
		}
		AchievementManager.AddAchievement(myAchievement); //Add Achievement to system. 
		Refresh (); 
	}
	

	public void Refresh()
	{
		if(nameTextMesh)
		{
			nameTextMesh.text = myAchievement.Name; 
		}
		if(descriptionTextMesh)
		{
			descriptionTextMesh.text = myAchievement.IsIncremental ?  (myAchievement.Progress.x + "/" + myAchievement.Progress.y):myAchievement.Description; 
		}
		if(icon)
		{

			icon.GetComponent<Renderer>().material.mainTexture = myAchievement.Icon; 
		}
		if(!isIncremental)
		{
			if(progress)
			{
				progress.GetComponent<Renderer>().enabled = false; 
			}
		}
		if(progress)
		{
			progress.GetComponentInChildren<ProgressBar>().SetProgress(myAchievement.Progress);
		}
	}

	public void KillMe()
	{
		if(myAchievement != null)
		{
			myAchievement.KillMe(); 
		}

	}

	public void CommitChanges()
	{
		if(myAchievement != null)
		{
			if(isIncremental)
			{
				myAchievement.CommitToPlayerPrefs(key, myName,description,lockedTexture,unlockedTexture,goal) ;
				
			}
			else
			{
				myAchievement.CommitToPlayerPrefs(key, myName,description,lockedTexture,unlockedTexture) ;
				
			}
		}
		else
		{
			myAchievement = (!isIncremental) ? 
				new BasicAchievement(key,myName,description,lockedTexture,unlockedTexture) : 
					new BasicAchievement(key,myName,description,lockedTexture,unlockedTexture,goal);
		}
	}
}
