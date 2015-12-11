using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Achievement
{
    /// <summary>
    /// A variable linked to an achievement. At the moment, there are 3 types supported: bool, int and float.<br/>
    /// <br/>
    /// Creation:<br/>
    /// AchievementVariable&lt;bool&gt; variable = new AchievementVariable&lt;bool&gt;();<br/>
    /// <br/>
    /// Setting:<br/>
    /// variable.Value = bool;<br/>
    /// <br/>
    /// Getting:<br/>
    /// bool b = variable;
    /// </summary>
    /// <typeparam name="T">bool, int or float</typeparam>
    public class AchievementVariable<T> where T : IComparable
    {
        /// <summary>
        /// The achievement identifier/name
        /// </summary>
        public string identifier;

        private T data;

        /// <summary>
        /// The stored value in the variable. Setting it will update the achievement manager.
        /// </summary>
        public T Value
        {
            get { return data; }
            set
            {
                data = value;
                // send manager a message this variable updated
                AchievementManager.Instance.VariableChanged(this);
            }
        }

        /// <summary>
        /// Creates a new achievement variable.
        /// </summary>
        /// <param name="identifier">The achievement identifier/name</param>
        public AchievementVariable(string identifier)
        {
            this.identifier = identifier;

            // get manager and assign this variable as an achievement
            //AchievementManager.Instance.registerAchievementVariable(this, identifier);
        }

        /// <summary>
        /// Implicit cast too given type T to make the variable easier to use. Because of this, you can use it as a normal variable when reading it. Only when setting the variable, the Value property is needed.
        /// </summary>
        /// <param name="var">The AchievementVariable</param>
        /// <returns>The internal value of AchievementVariable</returns>
        public static implicit operator T(AchievementVariable<T> var)
        {
            return var.data;
        }

        /// <summary>
        /// Returns the internal value.ToString
        /// </summary>
        /// <returns>The internal value.ToString</returns>
        public override string ToString()
        {
            return data.ToString();
        }

        /// <summary>
        /// Returns the internal value.Equals
        /// </summary>
        /// <param name="obj">Object to compare against</param>
        /// <returns>The internal value.Equals</returns>
        public override bool Equals(object obj)
        {
            return data.Equals(obj);
        }

        /// <summary>
        /// Returns the internal value.GetHashCode()
        /// </summary>
        /// <returns>The internal value.GetHashCode</returns>
        public override int GetHashCode()
        {
            return data.GetHashCode();
        }
    }
}