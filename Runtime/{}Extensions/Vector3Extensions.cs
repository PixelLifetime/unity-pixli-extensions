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
	public static class Vector3Extensions
	{
		public static void SetX(this ref Vector3 vector, float value) => vector.Set(newX: value, newY: vector.y, newZ: vector.z);
		public static void SetY(this ref Vector3 vector, float value) => vector.Set(newX: vector.x, newY: value, newZ: vector.z);
		public static void SetZ(this ref Vector3 vector, float value) => vector.Set(newX: vector.x, newY: vector.y, newZ: value);

		public static void SetX(this ref Vector3 vector, int value) => vector.Set(newX: value, newY: vector.y, newZ: vector.z);
		public static void SetY(this ref Vector3 vector, int value) => vector.Set(newX: vector.x, newY: value, newZ: vector.z);
		public static void SetZ(this ref Vector3 vector, int value) => vector.Set(newX: vector.x, newY: vector.y, newZ: value);

#if UNITY_EDITOR
#endif
	}
}