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
 * Daily Rewards Canvas is the User interface to show Daily rewards using Unity 4.6
 */
public class DailyRewardsInterface : MonoBehaviour {

    public GameObject Daily;
    
    // Prefab containing the daily reward
	public GameObject dailyRewardPrefab;

	// Claim Button
	public Image btnClaim;

	// How long until next claim
	public Text txtTimeDue;

	// The Grid that contains the rewards
	public GridLayoutGroup dailyRewardsGroup;

	private DailyRewards dailyRewards;

	void Awake() {
		dailyRewards = FindObjectOfType<DailyRewards> ();
		dailyRewards.CheckRewards ();
		UpdateUI ();
	}

	// Clicked the claim button
	public void OnClaimClick() {
		dailyRewards.ClaimPrize (dailyRewards.availableReward);
		dailyRewards.CheckRewards ();
		UpdateUI ();
       // Daily.SetActive(false);
	}

	public void UpdateUI () {
		foreach(Transform child in dailyRewardsGroup.transform) {
			Destroy(child.gameObject);
		}

		bool isRewardAvailableNow = false;

		for(int i = 0; i< dailyRewards.rewards.Count; i++) {
			int reward = dailyRewards.rewards[i];
			int day = i+1;
			
			GameObject dailyRewardGo = GameObject.Instantiate(dailyRewardPrefab) as GameObject;
			
			DailyReward dailyReward = dailyRewardGo.GetComponent<DailyReward>();
			dailyReward.transform.SetParent(dailyRewardsGroup.transform);
			dailyRewardGo.transform.localScale = Vector2.one;
			
			dailyReward.day = day;
			dailyReward.reward = reward;
			
			dailyReward.isAvailable = day == dailyRewards.availableReward;
			dailyReward.isClaimed = day <= dailyRewards.lastReward;

			if(dailyReward.isAvailable) {
				isRewardAvailableNow = true;
			}
			
			dailyReward.Refresh();
		}

		btnClaim.gameObject.SetActive(isRewardAvailableNow);
		txtTimeDue.gameObject.SetActive(!isRewardAvailableNow);
	}

	void FixedUpdate() {
		if(txtTimeDue.IsActive()) {
			TimeSpan difference = (dailyRewards.lastRewardTime - dailyRewards.timer).Add(new TimeSpan (0, 24, 0, 0));

			// Is the counter below 0? There is a new reward then
			if(difference.TotalSeconds <= 0) {
				dailyRewards.CheckRewards();
				UpdateUI();
				return;
			}

			string formattedTs = string.Format("{0:D2}:{1:D2}:{2:D2}", difference.Hours, difference.Minutes, difference.Seconds);

			txtTimeDue.text = "Return in " + formattedTs + " to claim your reward";
		}
	}

	// Resets player preferences
	public void OnResetClick() {
		dailyRewards.Reset ();
		
		dailyRewards.lastRewardTime = System.DateTime.MinValue;
		
		dailyRewards.CheckRewards ();
		UpdateUI ();
	}
	
	// Simulates the next day
	public void OnAdvanceDayClick() {
		dailyRewards.timer = dailyRewards.timer.AddDays (1);
		dailyRewards.CheckRewards ();
		UpdateUI ();
	}
	
	// Simulates the next hour
	public void OnAdvanceHourClick() {
		dailyRewards.timer = dailyRewards.timer.AddHours (1);
		dailyRewards.CheckRewards ();
		UpdateUI ();
	}

}
