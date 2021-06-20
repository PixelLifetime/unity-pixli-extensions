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

#if UNITY_EDITOR
public class CustomAssetPostprocessor : AssetPostprocessor
{
	private static void OnPostprocessAllAssets(
		string[] importedAssets, 
		string[] deletedAssets, 
		string[] movedAssets, 
		string[] movedFromAssetPaths)
	{
		//Debug.Log(EditorApplication.isCompiling);
	}
}
#endif