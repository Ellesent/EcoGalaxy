/**
 * Author: Sander Homan
 * Copyright 2012
 **/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Achievement;

/// <summary>
/// The definition of an achievement.
/// </summary>
[System.Serializable]
public class AchievementDefinition
{
    /// <summary>
    /// The available types for the achievementVariables. Possible values are Bool, Int and Float.
    /// </summary>
    public enum Type
    {
        Bool,
        Int,
        Float
    }

    /// <summary>
    /// The achievement identifier
    /// </summary>
    public string name = "";
    /// <summary>
    /// The achievement title
    /// </summary>
    public string title = "";
    /// <summary>
    /// The completed desription
    /// </summary>
    public string description = "";
    /// <summary>
    /// The description when the achievement hasn't been completed yet
    /// </summary>
    public string incompleteDescription = "";
    /// <summary>
    /// Is the achievement hidden
    /// </summary>
    public bool hidden = false;
    /// <summary>
    /// see AchievementDefinition.Type
    /// </summary>
    public Type type = Type.Bool;

    /// <summary>
    /// The bool value used to check against
    /// </summary>
    public bool conditionBoolValue = true;
    /// <summary>
    /// The int value used to check against
    /// </summary>
    public int conditionIntValue = 1;
    /// <summary>
    /// The float value used to check against
    /// </summary>
    public float conditionFloatValue = 1;

    //public string parentAchievement = "";

    /// <summary>
    /// The progress needed to report progress
    /// </summary>
    public float progressChangeSize = 1;

    /// <summary>
    /// The category of the achievement
    /// </summary>
    public int categoryId = 0;
}

/// <summary>
/// A category
/// </summary>
[System.Serializable]
public class AchievementCategory
{
    public int id;
    public string name;
}

/// <summary>
/// The ScriptableObject that is used to store the definitions in the assets.
/// </summary>
public class AchievementDefinitions : ScriptableObject
{
    /// <summary>
    /// Holds all the definitions
    /// </summary>
    [HideInInspector]
    public List<AchievementDefinition> definitions;

    /// <summary>
    /// Holds the categories
    /// </summary>
    [HideInInspector]
    public List<AchievementCategory> categories;

    /// <summary>
    /// The maximum id in use
    /// </summary>
    [HideInInspector]
    public int maxCatId = 1;
}