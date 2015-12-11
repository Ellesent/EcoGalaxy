using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

/*
 * Achievement Editor
 * Purpose: An editor window that allows the creation/editing of achievements. 
 * 
 * Usage: Called by initialise which can be remapped to a shortcut as desired, make use of controls to handle achievements. 
 * */
public class AchievementEditor : EditorWindow{

	AchievementHolder holder; //The holder of achievements. 


	Vector2 scrollPos; //For the scroll bar.
	bool generateContainer;
	GameObject container = (GameObject)Resources.Load ("DefaultContainer"); 

	[MenuItem ("Window/Achievements/Achievement Editor")]
	static void Init()
	{
		AchievementEditor window = (AchievementEditor)EditorWindow.GetWindow<AchievementEditor>();
		window.Focus ();
		window.minSize = new Vector2(700,500);
	}

	void OnGUI()
	{
		holder = FindObjectOfType<AchievementHolder>();
		if(holder == null)
		{
			holder = new GameObject("AchievementHolder").AddComponent<AchievementHolder>(); 
			holder.listOfAchievements = new List<AchievementComponent>();
			//CreateBlankAchievement();
		}
		Undo.RecordObject (holder,"Achievement Holder");
		scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
		EditorGUILayout.BeginVertical();
		EditorGUILayout.LabelField ("Achievements", EditorStyles.boldLabel);
		for(int i = 0; i<holder.listOfAchievements.Count; i++)
		{

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.BeginVertical(); 
			holder[i].key = EditorGUILayout.TextField ("Key",holder[i].key);
			holder[i].myName = EditorGUILayout.TextField ("Name", holder[i].myName);
			EditorGUILayout.LabelField("Description");
			holder[i].description = EditorGUILayout.TextArea (holder[i].description);//EditorGUILayout.TextField("Description", holder[i].description);
			holder[i].isIncremental = EditorGUILayout.Toggle ("Incremental", holder[i].isIncremental);
			if(holder[i].isIncremental)
			{
				holder[i].goal = EditorGUILayout.IntField("Goal", holder[i].goal);
			}
			EditorGUILayout.EndVertical ();
			holder[i].lockedTexture = (Texture)EditorGUILayout.ObjectField("Locked Texture",holder[i].lockedTexture,typeof(Texture),true);
			holder[i].unlockedTexture = (Texture)EditorGUILayout.ObjectField("Unlocked Texture",holder[i].unlockedTexture,typeof(Texture),true);
			EditorGUILayout.EndHorizontal();
			if(GUILayout.Button ("Remove Achievement", GUILayout.MaxWidth (200f)))
			{
				GameObject o = holder[i].gameObject; 
				holder[i].KillMe() ;
				holder.listOfAchievements.Remove (holder[i]);
				holder.listOfAchievements.TrimExcess();
				DestroyImmediate(o);
			}
			EditorGUILayout.Space ();
			if(holder[i] == null)
			{
				return;
			}
			if(holder[i].nameTextMesh)
			{
				holder[i].nameTextMesh.text = holder[i].myName;
			}
			if(holder[i].descriptionTextMesh)
			{
				holder[i].descriptionTextMesh.text = holder[i].description;
			}
			if(holder[i].icon)
			{
				holder[i].icon.GetComponent<Renderer>().sharedMaterial = new Material(holder[i].icon.GetComponent<Renderer>().sharedMaterial);
				holder[i].icon.GetComponent<Renderer>().sharedMaterial.mainTexture = holder[i].lockedTexture;
			}
			if(holder[i].progress)
			{
				if(!holder[i].isIncremental)
				{
					holder[i].progress.GetComponent<Renderer>().enabled = false; 
				}
			}
			holder[i].CommitChanges();

		}
		EditorGUILayout.EndVertical();
		EditorGUILayout.BeginVertical();
		EditorGUILayout.BeginHorizontal();
		generateContainer = EditorGUILayout.Toggle ("Generate Container",generateContainer);
		if(generateContainer)
		{
			container = (GameObject)EditorGUILayout.ObjectField("Container",container,typeof(GameObject),true);
		}
		EditorGUILayout.EndHorizontal ();
		EditorGUILayout.BeginHorizontal();
		if(GUILayout.Button ("New Achievement"))
		{
			CreateBlankAchievement();
		}
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.EndVertical();
		EditorGUILayout.EndScrollView();
	}

	void CreateBlankAchievement()
	{
		if(!generateContainer)
		{
			holder.listOfAchievements.Add(new GameObject("Achievement" + holder.listOfAchievements.Count).AddComponent<AchievementComponent>());
			holder.listOfAchievements[holder.listOfAchievements.Count-1].transform.parent = holder.transform;

		}
		else
		{
		 	GameObject o = (GameObject)Instantiate(container);
			o.name = "Achievement" + holder.listOfAchievements.Count;

			if(o.GetComponent<AchievementComponent>() != null)
			{
				holder.listOfAchievements.Add (o.GetComponent<AchievementComponent>());
				
			}
			else
			{
				holder.listOfAchievements.Add (o.AddComponent<AchievementComponent>());
			}
			Refresh(o.GetComponent<AchievementComponent>());
		    o.transform.parent = holder.transform;
			o.transform.position = new Vector3(o.transform.position.x, o.transform.position.y + holder.listOfAchievements.Count,o.transform.position.z);
		}

	}

	void RefreshAll()
	{
		foreach(AchievementComponent a in holder.listOfAchievements)
		{
			a.Start();
		}
	}

	void Refresh(AchievementComponent a)
	{
		a.GetComponent<AchievementComponent>().Start();
	}

	void ForceSave()
	{
		holder.CommitToPlayerPrefs();
	}

}
