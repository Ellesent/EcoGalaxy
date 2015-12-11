using UnityEngine;
using System.Collections;

public class SampleReset : MonoBehaviour {

void OnMouseDown()
	{
		foreach(AchievementComponent a in FindObjectsOfType<AchievementComponent>())
		{
			AchievementManager.LockAchievement(a.key);
			a.Refresh();
		}
	}
}
