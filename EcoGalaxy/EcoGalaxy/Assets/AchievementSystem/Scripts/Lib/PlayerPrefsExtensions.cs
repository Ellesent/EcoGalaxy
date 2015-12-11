using UnityEngine;
using System.Collections;


/*
 * Player Preference Extensions
 * Purpose: Extends functionality of PlayerPrefs to include boolean support. 
 * */
public static class PlayerPrefsExtensions
{
	public static void SetBool(this PlayerPrefs a,string n, bool v)
	{
		PlayerPrefs.SetInt (n, v?1:0);
	}
	
	public static bool GetBool(this PlayerPrefs a, string n)
	{
		return PlayerPrefs.GetInt(n) == 1 ? true:false; 
	}
	
	public static bool GetBool(this PlayerPrefs a, string n, bool d)
	{
		if(PlayerPrefs.HasKey(n))
		{
			return GetBool (a, n); 
		}
		
		return d; 
	}
}
