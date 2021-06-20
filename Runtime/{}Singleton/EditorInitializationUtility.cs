using System;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_EDITOR
[InitializeOnLoad]
public static class EditorInitializationUtility
{
	static EditorInitializationUtility()
	{
		// https://stackoverflow.com/questions/8645430/get-all-types-implementing-specific-open-generic-type
		//! This will return all types that inherit a generic base class. Not all types that inherit a generic interface.
		IEnumerable<Type> allTypesOfSOS =
			from x in Assembly.GetAssembly(typeof(ScriptableObjectSingleton<>)).GetTypes()
			let y = x.BaseType
			where !x.IsAbstract && !x.IsInterface &&
			y != null && y.IsGenericType &&
			y.GetGenericTypeDefinition() == typeof(ScriptableObjectSingleton<>)
			select x;

		foreach (Type type in allTypesOfSOS)
		{
			// Static constructor doesn't need instance.
			type.BaseType.GetConstructor(Type.EmptyTypes).Invoke(null);

			//! If multiple constructors are required to be invoked.
			//foreach (var item in type.BaseType.GetConstructors(BindingFlags.Static | BindingFlags.NonPublic))
			//{
			//	item.Invoke(null);
			//}

			//Debug.Log(type.BaseType.GetProperty("Instance", BindingFlags.Static | BindingFlags.Public));

			//? This didn't seem to work in Unity.
			//RuntimeHelpers.RunClassConstructor(type.TypeHandle);
		}
	}
}
#endif