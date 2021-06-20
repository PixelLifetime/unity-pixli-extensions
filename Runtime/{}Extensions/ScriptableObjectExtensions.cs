using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

public static class ScriptableObjectExtensions
{
#if UNITY_EDITOR
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T AddAssetAsChild<T>(this ScriptableObject scriptableObject, T asset, string name = null)
		where T : ScriptableObject => ScriptableObjectUtility.AddAssetAsChild<T>(scriptableObject, asset, name);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T CreateAssetAsChild<T>(this ScriptableObject scriptableObject, string name = null)
		where T : ScriptableObject => ScriptableObjectUtility.CreateAssetAsChild<T>(scriptableObject, name);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T GetChildAsset<T>(this ScriptableObject scriptableObject)
		where T : ScriptableObject => ScriptableObjectUtility.GetChildAsset<T>(scriptableObject);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static T GetChildAsset<T>(this ScriptableObject scriptableObject, string name)
		where T : ScriptableObject => ScriptableObjectUtility.GetChildAsset<T>(scriptableObject, name);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static List<T> GetChildrenAssets<T>(this ScriptableObject scriptableObject)
		where T : ScriptableObject => ScriptableObjectUtility.GetChildrenAssets<T>(scriptableObject);

	// [MethodImpl(MethodImplOptions.AggressiveInlining)]
	// public static T[] GetChildrenAssetsArray<T>(ScriptableObject scriptableObject)
	// 	where T : ScriptableObject => ScriptableObjectUtility.GetChildrenAssetsArray<T>(scriptableObject);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool RemoveChildAsset(this ScriptableObject scriptableObject, ScriptableObject childAsset) =>
		ScriptableObjectUtility.RemoveChildAsset(scriptableObject, childAsset);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool RemoveChildAsset<T>(this ScriptableObject scriptableObject)
		where T : ScriptableObject => ScriptableObjectUtility.RemoveChildAsset<T>(scriptableObject);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void RemoveChildrenAssets<T>(this ScriptableObject scriptableObject)
		where T : ScriptableObject => ScriptableObjectUtility.RemoveChildrenAssets<T>(scriptableObject);
#endif
}