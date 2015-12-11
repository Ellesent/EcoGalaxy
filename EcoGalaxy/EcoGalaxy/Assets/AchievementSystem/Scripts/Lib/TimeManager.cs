using UnityEngine;
using System.Collections;

/*
 * Time Manager
 * Purpose: Provides basic time management functions. 
 * */
public class TimeManager : MonoBehaviour
{
	
		public static int speedMultiplyer = 1;
		public static bool paused;

		void Start ()
		{
				paused = false; 
		}

		public static void Pause (bool pauseMe)
		{
				paused = pauseMe; 
		}
	
		public static void SetGameSpeed (int v)
		{
				Time.timeScale = (float)v;
		}
	
		public static void RestoreSpeed ()
		{
				Time.timeScale = 1f;
		}
}
