using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

public static class GenericExtensions
{
    public static void InvokeStaticConstructor<T>(this T @this, params object[] parameters) => StaticUtility.InvokeStaticConstructor(typeof(T), parameters);

#if UNITY_EDITOR
#endif
}