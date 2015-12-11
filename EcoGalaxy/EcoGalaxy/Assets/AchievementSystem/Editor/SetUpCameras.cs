using UnityEngine;
using System.Collections;
using UnityEditor;
public class SetUpCameras : Editor {

	[MenuItem("Window/Achievements/Set Up Camera")]
	public static void SetUp()
	{
		GameObject cam = (GameObject)Resources.Load("DefaultGUICam");
		if(FindObjectOfType<AchievementHelper>() == null)
		{
			Instantiate (cam);
		}
	}

}
