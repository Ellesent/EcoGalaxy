using UnityEngine;
using System.Collections;
/*
 * Achievement Popup
 * Purpose: A controller for an achievement popup. 
 * 
 * Usage: Attach to a popup that will display information about an achievement. 
 * */
public class AchievementPopup : MonoBehaviour {


	public TextMesh title; 				//TextMesh that displays the title. 
	public TextMesh message; 			//TextMesh that displays the message. 
	public GameObject icon; 			//GameObject that shows the icon.
	public GameObject progressBar; 		//The progress bar. 

	//Shader iconShader; 					//Used to read an icons shader <--NOT USED-->. 
	// Use this for initialization
	void Start () {
		//ProgressBarVisibility = false; 
	}

	void OnDestroy()
	{
		AchievementHelper.DeadPopup();
	}

	/// <summary>
	/// Sets the value of the achievement, along with the message and up to date image. </summary>
	/// <param name="title">T.</param>
	/// <param name="message">M.</param>
	/// <param name="texture">Tex.</param>
	public void SetValues(string t, string m, Texture tex)
	{
		title.text = t; 
		message.text = m; 
		icon.GetComponent<Renderer>().material.mainTexture = tex; 
	}

	/// <summary>
	/// Sets the value of the achievement, along with the message and up to date image. The vector2 contains
	/// min/max values related to an achievements goal. If p == true then show progress bar. </summary>
	/// <param name="t">T.</param>
	/// <param name="m">M.</param>
	/// <param name="tex">Tex.</param> 
	public void SetValues(string t,Vector2 v, Texture tex, bool p)
	{
		SetValues (t,"("+v.x+"/"+v.y+")",tex);
		if(p)
		{
			ProgressBarVisibility = true; 
			progressBar.GetComponentInChildren<ProgressBar>().SetProgress(v);
		}
		else
		{
			ProgressBarVisibility = false;
		}
	}

	//Property to determine a progress bars visibility. 
	public bool ProgressBarVisibility
	{
		set{progressBar.GetComponent<Renderer>().enabled = value;}
	}
}
