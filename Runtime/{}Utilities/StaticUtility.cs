using System;
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

public static class StaticUtility
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void InvokeStaticConstructor(Type type, params object[] parameters)
	{
		// type.TypeInitializer.Invoke(null, null);
		// ConstructorInfo constructorInfo = type.BaseType.GetConstructor(Type.EmptyTypes);

		// constructorInfo.Invoke(parameters);

		throw new NotImplementedException("There is some kind of problem. Not a problem to call base class static constructor though. Weird.");
	}

#if UNITY_EDITOR
#endif
}