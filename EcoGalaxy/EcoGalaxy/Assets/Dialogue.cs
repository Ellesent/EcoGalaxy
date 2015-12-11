using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour {
    public Text girl;
    public Text boy;
    public Text hboy;
    public Image go;
    public Text goT;
    int i;


    string[] dialogue = new string[10];
    bool next;

	// Use this for initialization
	void Start () {
        go.enabled = false;
        goT.enabled = false;
        dialogue[0] = "Hello Captain. engineer here!";
        dialogue[1] = "Tactician reporting for duty!";
        dialogue[2] = "Architect is present!";
        dialogue[3] = "We wanted to briefly discuss our mission again before we land. You remember why we are doing this right?";
        dialogue[4] = "Earth has been stripped of resources, and there aren't many people left because of it.";
        dialogue[5] = "Yeah and it's all because of those greedy Resource-Takers!";
        dialogue[6] = "Which is why we need to relocate to other planets. But we will be careful this time. We must use our resources wisely and only use renewable energy.";
        dialogue[7] = "This way, not only can we save, but we can prolong the human race!";
        dialogue[8] = "And we can save these planets before the Resource-Takers try to strip them.";
        dialogue[9] = "Our mission is crucial, and we must do the right thing! Are you ready captain?";

         i = 0;
    }

       


	
	// Update is called once per frame
	void Update () {

         if ( i < dialogue.Length)
        {
            if (i == 0 || i == 3 || i == 6 || i == 9)
            {
                girl.text = dialogue[i];
                boy.text = "";
                hboy.text = "";
            }
            else if (i == 1 || i == 4 || i == 7)
            {
                boy.text = dialogue[i];
                girl.text = "";
                hboy.text = "";
            }
            else if (i ==2 || i == 5 || i == 8)
            {
                hboy.text = dialogue[i];
                girl.text = "";
                boy.text = "";
            }

            if (next == true)
            {
                i += 1; 
            }

             if (i == 9)
             {
                 go.enabled = true;
                 goT.enabled = true;
             }
        }


        if (Input.GetKeyUp(KeyCode.Space))
        {
            next = true;
        }

        else//if (Input.GetKeyUp(KeyCode.Space))
        {
            next = false;
        }

	
	}
}
