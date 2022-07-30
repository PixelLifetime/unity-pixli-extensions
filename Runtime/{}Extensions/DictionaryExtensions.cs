/* Created by Max.K.Kimo */

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace PixLi
{
	public static class DictionaryExtensions
	{
		public static void DebugLogDictionary<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
		{
			foreach (KeyValuePair<TKey, TValue> item in dictionary)
			{
				Debug.Log(item);
			}
		}

#if UNITY_EDITOR
#endif
	}
}