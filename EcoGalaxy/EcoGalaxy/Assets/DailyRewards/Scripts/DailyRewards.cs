﻿/***************************************************************************\
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
 * Daily Rewards keeps track of the user daily rewards based on the time he last selected a reward
 */
public class DailyRewards : MonoBehaviour {
	
	// Needed controls
	public List<int> rewards;			// Rewards list 

	public DateTime timer;				// Today timer
	public DateTime lastRewardTime;		// The last time the user clicked in a reward
	public int availableReward;			// The available reward position the user can click
	public int lastReward;				// the last reward the user clicked
	private float t;					// Timer seconds ticker
	private bool isInitialized;			// Is the timer initialized?

	// Needed Constants
	private const string LAST_REWARD_TIME = "LastRewardTime";
	private const string LAST_REWARD = "LastReward";
	private const string FMT = "O";

	void Update() {
		t += Time.deltaTime;
		if(t >= 1) {
			timer = timer.AddSeconds(1);
			t = 0;
		}
	}

	private void Initialize() {
		timer = DateTime.Now;
		isInitialized = true;
	}

	// Check if the player have unclaimed prizes
	public void CheckRewards() {
		if(!isInitialized) {
			Initialize();
		}

		string lastClaimedTimeStr = PlayerPrefs.GetString (LAST_REWARD_TIME);
		lastReward = PlayerPrefs.GetInt(LAST_REWARD);

		// It is not the first time the user claimed.
		// I need to know if he can claim another reward or not
		if(!string.IsNullOrEmpty(lastClaimedTimeStr)) {
			lastRewardTime = DateTime.ParseExact(lastClaimedTimeStr, FMT, CultureInfo.InvariantCulture);

			TimeSpan diff = timer - lastRewardTime;
			Debug.Log("Last claim was " + (long)diff.TotalHours + " hours ago.");

			int days = (int)(Math.Abs(diff.TotalHours)/24);

			if(days == 0) {
				// No claim for you. Try tomorrow
				availableReward = 0;
				return;
			}

			// The player can only claim if he logs between the following day and the next.
			if(days >= 1 && days < 2) {
				// If reached the last reward, resets to the first restarting the cicle
				if(lastReward == rewards.Count) {
					availableReward = 1;
					lastReward = 0;
					return;
				}
				availableReward = lastReward + 1;

				Debug.Log("Player can claim prize " + availableReward);
				return;
			}

			if(days >= 2) {
				// The player loses the following day reward and resets the prize
				availableReward = 1;
				lastReward = 0;
				Debug.Log("Prize reset ");
			}
		} else {
			// Is this the first time? Shows only the first reward
			availableReward = 1;
		}
	}

	// Claims the prize and checks if the player can do it
	public void ClaimPrize(int day) {
		if(availableReward == day) {
			Debug.Log("Reward [" + rewards[day - 1] + "] Claimed!");
			PlayerPrefs.SetInt (LAST_REWARD, availableReward);
            MoneyManager.money += rewards[day - 1];
            Debug.Log(MoneyManager.money);

			string lastClaimedStr = timer.ToString (FMT);
			//Debug.Log("Setting date: " + lastClaimedStr);
			PlayerPrefs.SetString (LAST_REWARD_TIME, lastClaimedStr);

			CheckRewards();
		} else if(day <= lastReward) {
			Debug.Log("Reward already claimed. Try again tomorrow");
		} else {
			Debug.Log("Cannot Claim this reward! Can only claim reward #" + availableReward);
		}
	}

	public void Reset() {
		PlayerPrefs.DeleteKey (DailyRewards.LAST_REWARD);
		PlayerPrefs.DeleteKey (DailyRewards.LAST_REWARD_TIME);
	}

}
