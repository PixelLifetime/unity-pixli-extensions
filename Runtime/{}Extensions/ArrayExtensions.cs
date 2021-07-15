using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public static class ArrayExtensions
{
	public static bool Contains<T>(this T[] array, T value)
		where T : IEquatable<T>
	{
		for (int a = 0; a < array.Length; a++)
		{
			if (array[a].Equals(value))
				return true;
		}

		return false;
	}

	public static void StepForward<T>(this T[] array, int steps)
	{
		for (int a = array.Length - 1; a >= steps; a--)
			array[a] = array[a - steps];

		for (int a = 0; a < steps; a++)
			array[a] = default;
	}
}