using UnityEngine;
using System.Collections;
using System.Collections.Generic; 

public enum TweenDirection
{
	X,
	Y,
	Z
}
/*
 * Text Effects
 * Purpose: Provides some basic text effects on text meshes. (UNDOCUMENTED)
 * */
public class TextEffects : MonoBehaviour {
	
	public bool fade; 
	public float fadeStartTime; 
	public float fadeInterval; 
	public float startingAlpha = 1.0f; //A value between 0 and 1. 
	
	public bool expand; 
	public float expandRate; 
		
	public bool tweenBetweenColours;
	public bool randomiseColours; 
	public Color[] colors; 
	public float colourSpeed; 

	public bool tween; 
	public TweenDirection tweenDirection; 
	public float tweenSpeed; 
	public float tweenLength; 
	
	public bool shake; 
	public float shakeStrength; 


	public bool autoStart = false; 
	float start; 
	Queue<Color> colorQueue; 
	Color currentColor,nextColor; 
	TweenDirection shakeDirection = TweenDirection.X; 
	Vector3 startPosition; 
	float distanceMoved; 
	private TextMesh myTextMesh; 

	// Use this for initialization
	public void Start()
	{
		if(autoStart)
		{
			Enable ();
		}
	}


// If on a prefab that is created on destroy, uncomment this to ensure that it doesn't clutter editor space. 
//	[ExecuteInEditMode]
//	void FixedUpdate () {
//		if(!Application.isPlaying)
//		{
//			DestroyImmediate(this.gameObject);
//		}
//	}

	public void Enable()
	{
		this.enabled = true; 
		myTextMesh = GetComponent<TextMesh>(); 
		startPosition = transform.position; 
		if(fade)
		{
			start = startingAlpha; 
			startingAlpha = Mathf.Clamp (startingAlpha,0f,1.0f);
			InvokeRepeating("Fade",fadeStartTime,fadeInterval);
		}
		if(expand)
		{
			
		}
		if(tweenBetweenColours)
		{
			if(colors.Length > 1){
				currentColor = myTextMesh.color; 
				colorQueue = new Queue<Color>(); 
				if(!randomiseColours)
				{
					foreach(Color c in colors)
					{
						colorQueue.Enqueue(c);
					}
					nextColor = colorQueue.Dequeue();
				}
				else
				{
					nextColor = ColorHelper.RandomSolidColor();
				}
				InvokeRepeating ("TweenColors",colourSpeed,colourSpeed); 
			}
			else{
				if(!randomiseColours)
				{
					currentColor = myTextMesh.color; 
					nextColor = ColorHelper.RandomSolidColor();
					InvokeRepeating ("TweenColors",colourSpeed,colourSpeed); 
					Debug.LogWarning("No colors to tween between!");
				}
			}
			
		}
		if(shake)
		{
			InvokeRepeating("Shake",1f,0.1f);
		}
		if(tween)
		{
			distanceMoved = 0; 
			InvokeRepeating("TweenInDirection",0f,tweenSpeed);
		}
	}

	public void Disable()
	{
		CancelInvoke();
		this.enabled = false;
	}


	public void TweenColors()
	{
		if(!TimeManager.paused)
		{
			if(currentColor == nextColor)
			{
				if(randomiseColours)
				{
					nextColor = ColorHelper.RandomSolidColor();
				}
				else
				{
					colorQueue.Enqueue(nextColor);
				nextColor = colorQueue.Dequeue(); 
					
				}

			}
			currentColor.a = Mathf.MoveTowards(currentColor.a,nextColor.a,0.1f);
			currentColor.b = Mathf.MoveTowards (currentColor.b,nextColor.b,0.1f); 
			currentColor.g = Mathf.MoveTowards (currentColor.g,nextColor.g,0.1f);
			currentColor.r = Mathf.MoveTowards(currentColor.r,nextColor.r,0.1f); 
			myTextMesh.color = currentColor;
		}
	}
	public void Fade()
	{
		if(!TimeManager.paused)
		{
				Color c = myTextMesh.color;
				c.a = start;
				myTextMesh.color = c;
				start -= 0.1f;
				if(c.a <= 0.1f)
			{
				Destroy (this.gameObject);//EffectPool.ReturnEffect(this.gameObject,0);
			}
		}
	}

	public void TweenInDirection()
	{
		distanceMoved += 0.1f; 
		if(distanceMoved >= tweenLength)
		{
			return; 
		}
		else
		{
			Move (tweenDirection,0.1f); 
		}
	}

	public void Shake()
	{
		Move (shakeDirection, Random.Range (-shakeStrength,shakeStrength));
		shakeDirection = shakeDirection == TweenDirection.X ? TweenDirection.Z:TweenDirection.X;
		if(Vector3.Distance(startPosition,transform.position) > shakeStrength)
		{
			transform.position = startPosition; 
		}
	}

	public void Move(TweenDirection dir, float amount)
	{
		Vector3 position = transform.position;
		switch(dir)
		{
		case TweenDirection.X:
			position.x += amount; 
			break; 
		case TweenDirection.Z:
			position.z += amount; 
			break; 
		case TweenDirection.Y:
			position.y += amount; 
			break; 
		}
		transform.position = position;
	}
}