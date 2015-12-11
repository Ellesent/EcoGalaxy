using UnityEngine;
using System.Collections;

public static class ColorHelper {

	public static Color RandomSolidColor()
	{
		Color b = new Color(Random.Range (0f,1.0f),Random.Range (0f,1.0f), Random.Range (0f,1.0f),1.0f);
		return b; 
	}
}
