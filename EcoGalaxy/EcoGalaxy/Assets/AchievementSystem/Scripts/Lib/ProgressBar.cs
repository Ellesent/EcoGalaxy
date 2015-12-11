using UnityEngine;
using System.Collections;
/*
 * Progress Bar 
 * Purpose: Controls a progress bar. 
 * */
public class ProgressBar : MonoBehaviour {


	private float currentValue; 
	private float min = 0f; 
	private float max = 100;
	
	public void SetProgress(float value, float goal)
	{
		max = goal; 
		currentValue = (value - min) / (max - min);
		GetComponent<Renderer>().material.SetFloat ("_Progress", currentValue);
	}

	public void SetProgress(Vector2 v)
	{
		SetProgress(v.x,v.y);
	}
}
