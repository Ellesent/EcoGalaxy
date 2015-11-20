using UnityEngine;
using System.Collections;

public class Sound : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PlayAudio(AudioSource source)
    {
        //AudioSource.PlayClipAtPoint(sound, transform.position);
        source.Play();
    }
}
