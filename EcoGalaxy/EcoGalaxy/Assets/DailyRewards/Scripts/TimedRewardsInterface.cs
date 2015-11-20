/***************************************************************************\
Project:      Mobile Interface Template
Copyright (c) Niobium Studios.
Author:       Guilherme Nunes Barbosa (gnunesb@gmail.com)
\***************************************************************************/
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;

/* 
 * Timed Rewards Canvas is the User interface to show Timed rewards
 */
public class TimedRewardsInterface : MonoBehaviour {

	public Image btnClaim;
	public DateTime lastRewardTime;		// The last time the user clicked in a reward
	public Text txtTimer;
	
	private TimeSpan timer;
	private bool canClaim;				// Checks if the user can claim the reward
	
	// Needed Constants
	private const float COINS_TIMER = 3600f; // How many seconds until the player can claim the reward
	private const string TIMED_REWARDS_TIME = "TimedRewardsTime";
	private const string FMT = "O";
	
	void Awake() {
		UpdateTimer ();
	}
	
	void FixedUpdate() {
		if (!canClaim) {
			timer = timer.Subtract(TimeSpan.FromSeconds(Time.deltaTime));

			if(txtTimer) {
				txtTimer.text = string.Format("{0:D2}:{1:D2}:{2:D2}", timer.Hours, timer.Minutes, timer.Seconds);
			}

			if(timer.TotalSeconds <= 0) {
				canClaim = true;
				btnClaim.enabled = true;
				txtTimer.text = "You prize is ready to be claimed!";
			} else {
				// I need to save the player time every tick. If the player exits the game the information keeps logged
				// For perfomance issues you can save this information when the player switches scenes or quits the application
				PlayerPrefs.SetString (TIMED_REWARDS_TIME, DateTime.Now.Add(timer - TimeSpan.FromSeconds(COINS_TIMER)).ToString (FMT));
			}
		}
	}

	// This where you add the player coins/cash/money/prize, etc.
	public void OnClaimClick() {
		Debug.Log("Reward Claimed!");
		PlayerPrefs.SetString (TIMED_REWARDS_TIME, DateTime.Now.ToString (FMT));
		UpdateTimer ();
	}
	
	// Updates the timer and the UI
	public void UpdateTimer() {
		string lastRewardTimeStr = PlayerPrefs.GetString (TIMED_REWARDS_TIME);
		
		if(!string.IsNullOrEmpty(lastRewardTimeStr)) {
			lastRewardTime = DateTime.ParseExact(lastRewardTimeStr, FMT, CultureInfo.InvariantCulture);
			
			timer = (lastRewardTime - DateTime.Now).Add(TimeSpan.FromSeconds(COINS_TIMER));
		} else {
			timer = TimeSpan.FromSeconds(COINS_TIMER);
		}

		btnClaim.enabled = false;
		
		canClaim = false;
	}

	// Resets player preferences. Debug Purposes
	public void OnResetClick() {
		PlayerPrefs.DeleteKey (TIMED_REWARDS_TIME);
		
		UpdateTimer ();
	}
	
}