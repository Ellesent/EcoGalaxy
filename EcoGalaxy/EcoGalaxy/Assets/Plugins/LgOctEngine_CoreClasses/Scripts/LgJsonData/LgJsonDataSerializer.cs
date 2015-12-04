﻿// <commonheader>
// ------------------------------------------------------------------------------------------------
// Copyright (c) 2013-14 Leviathan Games
// www.leviathangames.com
//
// This code is released under the "DO WHATEVER YOU WANT WITH IT EXCEPT DELETE THIS HEADER" policy.
// If anything bad happens as a result of using this code, don't blame us.
//
// If you want support, buy this code via the UnityAsset store.
//
// ------------------------------------------------------------------------------------------------
#pragma warning disable 0219 // unused assignment
#pragma warning disable 0168 // assigned not used
#pragma warning disable 0414 // unused variables

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

// </commonheader>

using LGMiniJSON;       // Our wrapped json import library
using System.Text;

namespace LgOctEngine.CoreClasses
{
    /// <summary>
    /// Implements a JSON format accessor class.
    /// </summary>
    public partial class LgJsonNode : LgBaseClass
    {
        public string Serialize()
        {
            CheckForNull();
            return Json.Serialize(json);
        }
        public void Print()
        {
            CheckForNull();
            // Format to more than 1 line?!
            Debug.Log(Json.Serialize(json));
        }
        /// <summary>
        /// Convert the input json to a pretty format for output.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string PrettyJson(string text)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;
            text = text.Replace(System.Environment.NewLine, string.Empty).Replace("\t", string.Empty);

            var offset = 0;
            var output = new StringBuilder();
            Action<StringBuilder, int> tabs = (sb, pos) => { for (var i = 0; i < pos; i++) { sb.Append("\t"); } };
            Func<string, int, Nullable<Char>> previousNotEmpty = (s, i) =>
            {
                if (string.IsNullOrEmpty(s) || i <= 0) return null;

                Nullable<Char> prev = null;

                while (i > 0 && prev == null)
                {
                    prev = s[i - 1];
                    if (prev.ToString() == " ") prev = null;
                    i--;
                }

                return prev;
            };
            Func<string, int, Nullable<Char>> nextNotEmpty = (s, i) =>
            {
                if (string.IsNullOrEmpty(s) || i >= (s.Length - 1)) return null;

                Nullable<Char> next = null;
                i++;

                while (i < (s.Length - 1) && next == null)
                {
                    next = s[i++];
                    if (next.ToString() == " ") next = null;
                }

                return next;
            };

            for (var i = 0; i < text.Length; i++)
            {
                var chr = text[i];

                if (chr.ToString() == "{")
                {
                    offset++;
                    output.Append(chr);
                    output.Append(System.Environment.NewLine);
                    tabs(output, offset);
                }
                else if (chr.ToString() == "}")
                {
                    offset--;
                    output.Append(System.Environment.NewLine);
                    tabs(output, offset);
                    output.Append(chr);

                }
                else if (chr.ToString() == ",")
                {
                    output.Append(chr);
                    output.Append(System.Environment.NewLine);
                    tabs(output, offset);
                }
                else if (chr.ToString() == "[")
                {
                    output.Append(chr);

                    var next = nextNotEmpty(text, i);

                    if (next != null && next.ToString() != "]")
                    {
                        offset++;
                        output.Append(System.Environment.NewLine);
                        tabs(output, offset);
                    }
                }
                else if (chr.ToString() == "]")
                {
                    var prev = previousNotEmpty(text, i);

                    if (prev != null && prev.ToString() != "[")
                    {
                        offset--;
                        output.Append(System.Environment.NewLine);
                        tabs(output, offset);
                    }

                    output.Append(chr);
                }
                else if (chr.ToString() == "\"")
                {
                    // copy characters until we get to an unescaped matching quote
                    output.Append(chr);
                    i++;
                    while (true)
                    {
                        var chrNext = text[i];
                        output.Append(chrNext);
                        if (chrNext.ToString() == "\"" && previousNotEmpty(text, i).ToString() != "\\")
                        { 
                            break; 
                        }

                        i++;
                    }
                }
                else
                    output.Append(chr);
            }

            return output.ToString().Trim();
        }
    }
} // namespace LgOctEngine.CoreClasses
