using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class MoneyManager : MonoBehaviour {

    public static int money = 150000;
    public static bool Oxygen = false;
    public static int food = 50;
    public static int water = 50;
    public static int buildMat = 50;
    public static int power = 0;
    public static int rating = 0;
    public static int pop = 0;
    public static int conquered = 0;
    public Text moneyText;
    public Text foodText;
    public Text waterText;
    public Text buildMatText;
    public Text powerText;
    public Text ratingText;
    public Text popText;
    
	// Use this for initialization
	void Start () {
	    //money = 150000;
        //food = 50;
        //water = 50;
        //buildMat = 50;
        //power = 0;
        //rating = 0;
        //conquered = 0;
        //help.LoadScores();
        //Achievements.first.Value = true;
        
	}
	
	// Update is called once per frame
	void Update () {
        moneyText.text = "Money: " + "$" + money.ToString("n0");
        foodText.text = "Food: " + food;
        waterText.text = "Water: " + water;
        buildMatText.text = "Build Materials: " + buildMat;
        powerText.text = "Power Left: " + power;
        ratingText.text = "Planet's rating: " + rating;
        popText.text = "Population: " + pop;
        //Debug.Log(Oxygen);

        if (Oxygen == false)
        {
            GameObject ox = GameObject.FindGameObjectWithTag("Oxygen");
            if (ox != null)
            {
                Oxygen = true;
            }
        }

        //Debug.Log(Oxygen);
        
    

        
	}

    //void OnGUI()
    //{
    //    if (scores == null)
    //    {
    //        Debug.Log("WUT");
    //    }
    //    foreach (dreamloLeaderBoard.Score currentScore in scores)
    //    {
    //        GUIStyle h = new GUIStyle();
    //        h.fontSize = 50;
            
    //        GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 100, 100), currentScore.playerName + "    " + currentScore.score,  h);
    //    }
    //}
}
