using UnityEngine;
using System.Collections;

public class SampleIncrement : MonoBehaviour {

	public string key;

	void Update()
	{
		if(AchievementManager.GetAchievement(key).IsLocked == false)
		{
			this.GetComponent<Renderer>().enabled = false; 
			this.GetComponent<Collider>().enabled = false; 
		}
		else
		{
			this.GetComponent<Renderer>().enabled = true; 
			this.GetComponent<Collider>().enabled = true; 
		}
	}
	void OnMouseDown()
	{
		AchievementManager.IncrementAchievement(key,true);
	}
}
