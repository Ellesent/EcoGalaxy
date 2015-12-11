/*! \mainpage Achievements
 *
 * \section intro_sec Introduction
 * Thank you for using the Achievements extension for the Unity 3D engine. The goal of this extension is to have an easy way to add achievements to your game or application.
 *
 * \section usage_sec Usage
 *
 * \subsection step1 Defining the achievements in Unity
 * After installing the extension, the window menu has an extra item called Achievements. This opens the achievements window.
 * 
 * \image html AchievementsWindow.jpg
 * If you have not yet defined any achievements yet, there will be a button to create a new achievements file. This file is used to store the information on the achievements in the current project.
 * After you have clicked the button, the window is divided into 2 parts. The left part shows all the defined achievements and the right part shows the information on the selected achievement.
 * To add a new achievement, click the "+" button and to remove the selected achievement, click the "-" button.
 * The right side of the window shows the fields of the achievements. Most fields are self-explanatory.
 * 
 * The most important field is the "Name" field. This field is used in the code to refer to the achievements. Other important fields are the "Type", "Condition Value" and "Progress Change" fields. Type can be Bool, Int or float. 
 * Condition Value is the value the code is checking against internally to see if the achievement as completed. For Bool the variable must be equal, but for Int and Float the variable must be equal or higher.
 * The progress change is used to determine when to notify the game of progress made. If the variable is divisable by this value, then the progress event will fire.
 * 
 * \subsection step2 Using the defined achievements
 * To use the achievements, you need to define a variable that uses the name identifier specified in unity.
 * For a Bool achievement, use the bool type. For an Int or Float, use int or float respectively.
 * \code
 * AchievementVariable<bool> variable = new AchievementVariable<bool>("AchievementName");
 * \endcode
 * To set the variable, set the Value property.
 * \code
 * variable.Value = true;
 * \endcode
 * You can also use the variable like a normal variable when just reading.
 * \code
 * bool test = variable;
 * \endcode
 * 
 * \subsection step3 Subscribing to the events
 * To know when an achievement made progress or was completed, you need to subscribe to the achievement events.
 * \code
 *  void Start()
 *  {
 *      AchievementManager.Instance.onComplete += AchievementComplete;
 *  }
 *  
 *  void AchievementComplete(AchievementDefinition def)
 *  {
 *      Debug.Log("Achievement Complete: " + def.title);
 *  }
 * \endcode
 * For progress, there are 2 events. One for Int achievements and one for Float achievements. These events get 2 more arguments with the actualValue and the progressValue. The progressValue is always a multiple of the defined "Progress Change" field.
 * \code
 *  void Start()
 *  {
 *      AchievementManager.Instance.onIntProgress += AchievementIntProgress;
 *  }
 *  
 *  void AchievementIntProgress(AchievementDefinition def, int actualValue, int progressValue)
 *  {
 *      Debug.Log("Achievement Progress: " + def.title + ":" + progressValue +"/" + def.conditionIntValue;
 *  }
 * \endcode
 * 
 * To see more examples on how to use this extension, see Achievement.AchievementManager.
 * 
 * \section updates_sec Updates
 * V1.1:
 *  -   Fixed a bug that caused in a corrupted window when code got recoompiled
 *  -   Left side of window can now be resized
 *  -   Achievements can now be put in categories
 * 
 * \author Sander Homan
 * \copyright Sander Homan 2012
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;

namespace Achievement
{
    /// <summary>
    /// A singleton class that manages all the achievements<br/>
    /// <br/>
    /// <example>
    /// Getting all achievement definitions:
    /// <code>List<AchievementDefinition> definitions = AchievementManager.Instance.Definitions;</code>
    /// </example>
    /// <example>
    /// Clearing all the set achievements:
    /// <code>AchievementManager.Instance.Clear();</code>
    /// </example>
    /// <example>
    /// Mark an achievement as completed(will not fire the completed event):
    /// <code>AchievementManager.Instance.MarkCompleted("AchievementOne");</code>
    /// </example>
    /// <example>
    /// Subscribing to the complete event using a lambda function:
    /// <code>
    /// AchievementManager.Instance.onComplete += (def) => {Debug.Log("achievement completed: " + def.title);};
    /// </code>
    /// </example>
    /// <example>
    /// Subscribing to the complete event using a method:
    /// <code>
    /// void Start()
    /// {
    ///     AchievementManager.Instance.onComplete += AchievementComplete;
    /// }
    /// void AchievementComplete(AchievementDefinition def)
    /// {
    ///     Debug.Log("achievement completed: " + def.title);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public class AchievementManager
    {
        /// <summary>
        /// The state of an achievement.
        /// </summary>
        public class AchievementState
        {
            /// <summary>
            /// Is the achievement still hidden
            /// </summary>
            public bool hidden;
            /// <summary>
            /// Has the achievement been completed
            /// </summary>
            public bool completed;
            /// <summary>
            /// The progress value. This value times the achievement progress change field is the last reported progress
            /// </summary>
            public int progressValue;
        }

        private static AchievementManager instance = null;
        /// <summary>
        /// The singleton instance property.
        /// </summary>
        public static AchievementManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new AchievementManager();
                return instance;
            }
        }

        //private Dictionary<string, List<object>> variables;

        private AchievementDefinitions definitions = null;
        /// <summary>
        /// All the achievement definitions defined.
        /// </summary>
        public List<AchievementDefinition> Definitions
        {
            get
            {
                if (definitions == null)
                    Init();
                return definitions.definitions;
            }
        }

        private Dictionary<string, AchievementDefinition> achievementDefinitions;
        private Dictionary<string, AchievementState> achievementStates;

        /// <summary>
        /// This event will fire when an achievement completes
        /// </summary>
        public event System.Action<AchievementDefinition> onComplete;
        /// <summary>
        /// This achievement will fire when a float achievement has progress.
        /// </summary>
        public event System.Action<AchievementDefinition, float, float> onFloatProgress;
        /// <summary>
        /// This achievement will fire when an int achievement has progress
        /// </summary>
        public event System.Action<AchievementDefinition, int, int> onIntProgress;

        private AchievementManager()
        {
            //variables = new Dictionary<string, List<object>>();
        }

        private void Init()
        {
            // load achievements
            definitions = (AchievementDefinitions)Resources.Load("achievements");
            achievementDefinitions = new Dictionary<string, AchievementDefinition>();
            achievementStates = new Dictionary<string, AchievementState>();
            foreach (AchievementDefinition definition in definitions.definitions)
            {
                achievementDefinitions.Add(definition.name, definition);
                AchievementState state = new AchievementState();
                state.hidden = definition.hidden;
                achievementStates.Add(definition.name, state);
            }

            // TODO build stuff for multi tier achievements
        }

        //public void registerAchievementVariable<T>(AchievementVariable<T> variable, string identifier) where T : IComparable
        //{
        //    if (!variables.ContainsKey(identifier))
        //        variables[identifier] = new List<object>();

        //    variables[identifier].Add(variable);

        //    Debug.Log("registered achievement variable with identifier: " + identifier);
        //}

        /// <summary>
        /// Automaticly called by an AchievementVariable to notify that the variable has changed. Will fire the progress and completed events if necesary.
        /// </summary>
        /// <typeparam name="T">The variable type</typeparam>
        /// <param name="variable">The AchievementVariable</param>
        /// <exception cref="System.ArgumentException">Thrown when there is no matching achievement for the given identifier</exception>
        public void VariableChanged<T>(AchievementVariable<T> variable) where T : IComparable
        {
            // check definitions
            if (definitions == null)
                Init();

            if (!achievementDefinitions.ContainsKey(variable.identifier))
                throw new ArgumentException("The identifier " + variable.identifier + " has no matching achievement");

            // get the associated achievement definition
            AchievementDefinition def = achievementDefinitions[variable.identifier];
            if (!achievementStates[variable.identifier].completed)
            {
                //Debug.Log("Checking Achievement " + def.title);
                bool completed = false;
                switch (def.type)
                {
                    case AchievementDefinition.Type.Bool:
                        if (variable.Value.Equals(def.conditionBoolValue))
                        {
                            completed = true;
                        }
                        break;
                    case AchievementDefinition.Type.Float:
                        {
                            AchievementVariable<float> tvar = (AchievementVariable<float>)(object)variable;
                            int pValue = Mathf.FloorToInt(tvar / def.progressChangeSize);
                            if (achievementStates[tvar.identifier].progressValue < pValue)
                                onFloatProgress(def, tvar, pValue * def.progressChangeSize);
                            achievementStates[tvar.identifier].progressValue = pValue; // always set it
                            if (variable.Value.CompareTo(def.conditionFloatValue) >= 0)
                            {
                                completed = true;
                            }
                        }
                        break;
                    case AchievementDefinition.Type.Int:
                        {
                            AchievementVariable<int> tvar = (AchievementVariable<int>)(object)variable;
                            int pValue = Mathf.FloorToInt(tvar / def.progressChangeSize);
                            if (achievementStates[tvar.identifier].progressValue < pValue)
                                onIntProgress(def, tvar, (int)(pValue * def.progressChangeSize));
                            achievementStates[tvar.identifier].progressValue = pValue; // always set it
                            if (variable.Value.CompareTo(def.conditionIntValue) >= 0)
                            {
                                completed = true;
                            }
                        }
                        break;
                }

                if (completed)
                {
                    //Debug.Log("Completed achievement " + def.title);
                    if (onComplete != null) onComplete(def);
                    achievementStates[variable.identifier].completed = true;
                    achievementStates[variable.identifier].hidden = false;
                }
            }
        }

        /// <summary>
        /// Mark the given achievement as completed. This will not cause an onComplete event.
        /// </summary>
        /// <param name="identifier">The achievement identifier</param>
        /// <exception cref="System.ArgumentException">Thrown when there is no matching achievement for the given identifier</exception>
        public void MarkCompleted(string identifier)
        {
            // check definitions
            if (definitions == null)
                Init();

            if (!achievementDefinitions.ContainsKey(identifier))
                throw new ArgumentException("The identifier " + identifier + " has no matching achievement");

            achievementStates[identifier].completed = true;
            achievementStates[identifier].hidden = false;
        }

        /// <summary>
        /// Returns the given achievement state.
        /// </summary>
        /// <param name="identifier">String identifying the achievement</param>
        /// <returns>The state of the achievement</returns>
        /// <exception cref="System.ArgumentException">Thrown when there is no matching achievement for the given identifier</exception>
        public AchievementState GetAchievementState(string identifier)
        {
            if (!achievementDefinitions.ContainsKey(identifier))
                throw new ArgumentException("The identifier " + identifier + " has no matching achievement");

            return achievementStates[identifier];
        }

        /// <summary>
        /// Resets all states to incomplete
        /// </summary>
        public void Clear()
        {
            foreach (KeyValuePair<string,AchievementState> state in achievementStates)
            {
                state.Value.completed = false;
                state.Value.hidden = achievementDefinitions[state.Key].hidden;
            }
        }

        /// <summary>
        /// Get the category definition with the given id
        /// </summary>
        /// <param name="id">The category id</param>
        /// <returns>The category</returns>
        public AchievementCategory getCategoryById(int id)
        {
            foreach (AchievementCategory category in definitions.categories)
            {
                if (category.id == id)
                    return category;
            }
            return null;
        }
    }
}
