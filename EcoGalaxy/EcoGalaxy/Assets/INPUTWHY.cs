using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class INPUTWHY : MonoBehaviour {
    InputField input;

	// Use this for initialization
	void Start () {
        input = gameObject.GetComponent<InputField>();
        input.Select();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
