//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic; 
//using GooglePlayGames;
//using UnityEngine.SocialPlatforms;
//

/*
 * Google Achievement Handler
 * Purpose: Uncomment if using with Google Achievements. 
 * 
 * Usage: Place onto the first scene within your game.
 * 		  Set the each instance of keyreferences to match the keys specified in achievements that are set up, 
 * 		  match these with the Google IDs provided in the developer console. 
 * */
//public class GoogleAchievementHandler : MonoBehaviour {
//
//
//	public static Dictionary<string,string> keyreferences; 
//
//
//	//Hard Coded for now, may think of a better solution in the future. 
//	void Start () {
//		PlayGamesPlatform.DebugLogEnabled = true;
//		
//		// Activate the Google Play Games platform
//		PlayGamesPlatform.Activate();
//		Social.localUser.Authenticate((bool success) => {
//			if(success)
//			{
//				Application.LoadLevel ("Start");
//			}
//			else
//			{
//				Application.LoadLevel ("Start");
//			}
//		});
//		keyreferences = new Dictionary<string, string>(); 
//		keyreferences["al_one"] = ""; // <--Example Line-->
//	

//		// recommended for debugging:
//		PlayGamesPlatform.DebugLogEnabled = true;
//		
//		// Activate the Google Play Games platform
//		PlayGamesPlatform.Activate();
//	}
//
//	//Called to unlock an achievement on Google play services.
//	public static void UnlockGoogleAchievement(string key)
//	{
//		if(keyreferences != null)
//		{
//			Social.ReportProgress(keyreferences[key],100.0,(bool success) => {
//				
//			});
//		}
//
//	}
//
//	
//}
