using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ShowLeaderboar : MonoBehaviour {
    public dreamloLeaderBoard help;
    List<dreamloLeaderBoard.Score> scores;
    public Text test;
    public Text score;
    //int place = Screen.width / 2 + 100;
   // int count = -1;
    bool draw = true;
    float bah = 0.8f;

	// Use this for initialization
	void Start () {
        //help.AddScore("bob", 50);
        help.LoadScores();
       // scores = new List<dreamloLeaderBoard.Score>();
        
       
       
	}

    
	// Update is called once per frame
    void Update()
    {
        bah -= Time.deltaTime;
        scores = help.ToListHighToLow();
       
        
        //bug.Log(help.ToListHighToLow().Count);
        Debug.Log(scores.Count);


        if (bah <= 0 && draw == true)
        {
            Method();
            draw = false;
        }
            
    }
    void Method()
    {
        for (int i = 0; i < scores.Count; i++)
        {
            test.text += scores[i].playerName + "\n";
            score.text += scores[i].score + "\n";


        }
        return;
    }

    //void OnGUI()
    //{
    //    if (scores == null)
    //    {
    //        Debug.Log("WUT");
    //    }
    //    else
    //    {

    //        for (int i = 0; i < scores.Count; i++ )
    //        {
                
                
                    
                
    //            GUIStyle h = new GUIStyle();
    //            h.fontSize = 18;
    //            if (i > 0)
    //            {
    //                GUI.Label(new Rect(place, (i + 1) * 80, 100, 100), , h);
    //            }
    //            else
    //            { 
    //                GUI.Label(new Rect(place, 80, 100, 100), scores[i].playerName + "                           " + scores[i].score, h);
    //            }
                
    //        }
    //    }
    //}
}
