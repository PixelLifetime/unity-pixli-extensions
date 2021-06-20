using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

public static class ColorExtensions
{
	public static Color NormalBlend(this Color color, Color overlayColor)
	{
		return color * (1 - overlayColor.a) * color.a + overlayColor * overlayColor.a;
	}

	//TODO: move to extensions or utility.
	public static TValue[] ValuesToArray<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
	{
		Dictionary<TKey, TValue>.ValueCollection.Enumerator enumerator = dictionary.Values.GetEnumerator();

		TValue[] values = new TValue[dictionary.Count];

		int a = 0;
		while (enumerator.MoveNext())
		{
			values[a] = enumerator.Current;

			++a;
		}

		return values;
	}
}