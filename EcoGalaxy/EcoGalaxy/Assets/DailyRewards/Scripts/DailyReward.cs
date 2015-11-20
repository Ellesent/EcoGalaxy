/***************************************************************************\
Project:      Mobile Interface Template
Copyright (c) Niobium Studios.
Author:       Guilherme Nunes Barbosa (gnunesb@gmail.com)
\***************************************************************************/
using UnityEngine;
using UnityEngine.UI;
using System;

/* 
 * Daily Reward Object
 */
public class DailyReward : MonoBehaviour {
	public Text txtDay;
	public Text txtReward;
	public Image imgBackground;

	public int day;
	public int reward;
	public bool isClaimed;
	public bool isAvailable;
	
	public Color availableColor;
	public Color claimedColor;

	public void Refresh() {
		txtDay.text = "Day " + day.ToString ();
		txtReward.text = reward.ToString ();

		if(isAvailable) {
			imgBackground.color = availableColor;
		}

		if(isClaimed) {
			imgBackground.color = claimedColor;
		}
	}


}
