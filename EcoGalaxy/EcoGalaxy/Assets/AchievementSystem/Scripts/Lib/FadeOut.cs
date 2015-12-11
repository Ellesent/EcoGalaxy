using UnityEngine;
using System.Collections;
/*
 * Fade Out 
 * Purpose: Fades a game object out (provided the material on the object has an alpha(A) value. 
 * */
public class FadeOut: MonoBehaviour {
	
	
	
	float start = 1.0f; 
	public bool ignorePause = false; 
	public bool destroyWhenComplete = true; 
	// Use this for initialization
	void Start ()
	{
		InvokeRepeating("Fade", 1f, 0.1f);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(start <= 0f && destroyWhenComplete)
		{
			Destroy (gameObject);
		}
	}
	
 public void Fade()
	{
		if(!TimeManager.paused || ignorePause)
		{
							if(GetComponent<TextMesh>())
		{
			TextMesh t = GetComponent<TextMesh>(); 
			Color c = t.GetComponent<Renderer>().material.GetColor ("_Color"); 
			c.a = start;
			t.GetComponent<Renderer>().material.SetColor("_Color", c);
		}
		else
		{
			if(GetComponent<ParticleSystem>())
			{
				ParticleSystem t = GetComponent<ParticleSystem>(); 
							Color c = t.GetComponent<Renderer>().material.GetColor ("_TintColor"); 
			c.a = start;
			t.GetComponent<Renderer>().material.SetColor("_TintColor", c);
			}
			else
			{
									Color w = GetComponent<Renderer>().material.color;
		w.a = start;
		GetComponent<Renderer>().material.color = w; 
			}


		}
		start -= 0.1f;
		}
	}
	
}
