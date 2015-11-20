/***************************************************************************\
Project:      Mobile Interface Template
Copyright (c) Niobium Studios.
Author:       Guilherme Nunes Barbosa (gnunesb@gmail.com)
\***************************************************************************/
using UnityEngine;
using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;

/* 
 * Daily Rewards GUI is the User interface to show Daily rewards
 */
public class DailyRewardsGUI : MonoBehaviour {

	private bool debug = true;			// Just to show the debug window
	private int selectedReward = -1;	// Control for the GUILayout.SelectionGrid

	private DailyRewards dailyRewards;

	void Awake() {
		dailyRewards = FindObjectOfType<DailyRewards> ();
		dailyRewards.CheckRewards ();
	}

	void FixedUpdate() {
		TimeSpan difference = (dailyRewards.lastRewardTime - dailyRewards.timer).Add(new TimeSpan (0, 24, 0, 0));
		
		// Is the counter below 0? There is a new reward then
		if(difference.TotalSeconds < 0) {
			dailyRewards.CheckRewards();
		}
	}

	void OnGUI() {
		GUIStyle btnStyle = GUI.skin.GetStyle("Button");
		btnStyle.fixedHeight = 50;

		GUILayout.BeginVertical(GUILayout.Width(150));

		debug = GUILayout.Toggle (debug, "Debug");

		if(debug) {
			GUILayout.Label("Now:\n" + dailyRewards.timer.ToUniversalTime());
			GUILayout.Label("Last Reward:\n" + dailyRewards.lastRewardTime.ToUniversalTime());

			if(GUILayout.Button("Reset")) {
				Reset();
			}

			if(GUILayout.Button("Advance 1 Day")) {
				AdvanceDay();
			}

			if(GUILayout.Button("Advance 1 Hour")) {
				AdvanceHour();
			}

			GUILayout.BeginHorizontal();
			GUILayout.Label("Rewards #");
			int newValue = (int) GUILayout.HorizontalSlider(dailyRewards.rewards.Count, 3, 15);
			if(newValue != dailyRewards.rewards.Count) {
				RearrangeRewardsArray(newValue);
			}
			GUILayout.EndHorizontal();
		}

		GUILayout.EndVertical();

		GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
		GUILayout.FlexibleSpace();
		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();

		string[] rewardsTxt = new string[dailyRewards.rewards.Count];
		for(int i = 0; i< dailyRewards.rewards.Count; i++) {
			int reward = i + 1;


			string btnTxt = "Day " + (i+1) + ": " + dailyRewards.rewards[i];
			if(reward == dailyRewards.availableReward) {
				btnTxt += "\n Claim now!";
			}
			
			if(reward <= dailyRewards.lastReward) {
				btnTxt += "\n Claimed!";
			}
			
			rewardsTxt[i] = btnTxt;
		}

		int selected = GUILayout.SelectionGrid (selectedReward, rewardsTxt, 3);
		if(selected != selectedReward) {
			dailyRewards.ClaimPrize (selected + 1);
			selectedReward = -1;
		}

		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();
		GUILayout.FlexibleSpace();
		GUILayout.EndArea();
	}

	// Reorganizes the Rewards Array
	private void RearrangeRewardsArray(int size) {
		dailyRewards.rewards = new List<int> (size);
		for(int i = 0; i< size; i++) {
			dailyRewards.rewards.Add(50 * (i+1));
		}
		
		Reset ();
	}

	// Resets player preferences
	public void Reset() {
		dailyRewards.Reset ();
		
		dailyRewards.lastRewardTime = DateTime.Now;
		
		dailyRewards.CheckRewards ();
	}
	
	// Simulates the next day
	public void AdvanceDay() {
		dailyRewards.timer = dailyRewards.timer.AddDays (1);
		dailyRewards.CheckRewards ();
	}
	
	// Simulates the next hour
	public void AdvanceHour() {
		dailyRewards.timer = dailyRewards.timer.AddHours (1);
		dailyRewards.CheckRewards ();
	}

}
