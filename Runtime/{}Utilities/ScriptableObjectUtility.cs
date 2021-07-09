using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

#if UNITY_EDITOR
public static class ScriptableObjectUtility
{
	#region Create Asset
	/// <summary>
	///	This makes it easy to create, name and place unique new ScriptableObject asset files.
	/// </summary>
	public static T CreateAsset<T>(string extension = ".asset") where T : ScriptableObject
	{
		T asset = ScriptableObject.CreateInstance<T>();

		string path = AssetDatabase.GetAssetPath(Selection.activeObject);
		if (path == string.Empty)
		{
			path = "Assets";
		}
		else if (Path.GetExtension(path) != string.Empty)
		{
			path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), string.Empty);
		}

		string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/New " + typeof(T).ToString() + extension);

		AssetDatabase.CreateAsset(asset, assetPathAndName);

		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();

		EditorUtility.FocusProjectWindow();

		Selection.activeObject = asset;

		return asset;
	}

	/// <summary>
	///	This makes it easy to create, name and place unique new ScriptableObject asset files.
	/// </summary>
	public static T CreateAsset<T>(string path, string extension = ".asset") where T : ScriptableObject
	{
		T asset = ScriptableObject.CreateInstance<T>();

		if (path == string.Empty)
		{
			path = "Assets";
		}
		else if (Path.GetExtension(path) != string.Empty)
		{
			path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), string.Empty);
		}

		string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/New " + typeof(T).ToString() + extension);

		AssetDatabase.CreateAsset(asset, assetPathAndName);

		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();

		EditorUtility.FocusProjectWindow();

		Selection.activeObject = asset;

		return asset;
	}

	/// <summary>
	///	This makes it easy to create, name and place unique new ScriptableObject asset files.
	/// </summary>
	public static T CreateAsset<T>(string path, string name, string extension = ".asset") where T : ScriptableObject
	{
		T asset = ScriptableObject.CreateInstance<T>();

		if (path == string.Empty)
		{
			path = "Assets";
		}
		else if (Path.GetExtension(path) != string.Empty)
		{
			path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), string.Empty);
		}

		string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(Path.Combine(path, name + extension));

		AssetDatabase.CreateAsset(asset, assetPathAndName);

		//AssetDatabase.ImportAsset(assetPathAndName, ImportAssetOptions.ForceUpdate | ImportAssetOptions.ForceSynchronousImport);

		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();

		EditorUtility.FocusProjectWindow();

		Selection.activeObject = asset;

		return asset;
	}

	/// <summary>
	///	This makes it easy to create, name and place unique new ScriptableObject asset files.
	/// </summary>
	public static T CreateAsset<T>(string path, T prefab, string extension = ".asset") where T : ScriptableObject
	{
		T asset = Object.Instantiate(prefab);

		if (path == string.Empty)
		{
			path = "Assets";
		}
		else if (Path.GetExtension(path) != string.Empty)
		{
			path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), string.Empty);
		}

		string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/New " + typeof(T).ToString() + extension);

		AssetDatabase.CreateAsset(asset, assetPathAndName);

		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();

		EditorUtility.FocusProjectWindow();

		Selection.activeObject = asset;

		return asset;
	}
	#endregion

	#region Children Assets
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static T InlineAddAssetAsChild<T>(ScriptableObject scriptableObject, T asset, string name = null)
		where T : ScriptableObject
	{
		if (string.IsNullOrEmpty(name))
			name = $"[{typeof(T).ToString().ToDisplayValue()}]";

		asset.name = name;

		AssetDatabase.AddObjectToAsset(
			asset,
			scriptableObject
		);

		//TODO: remove this? When using this kind of thing in loop you might want to only call this upon finishing the loop.
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();

		return asset;
	}

	public static T AddAssetAsChild<T>(ScriptableObject scriptableObject, T asset, string name = null)
		where T : ScriptableObject => ScriptableObjectUtility.InlineAddAssetAsChild<T>(scriptableObject, asset, name);

	public static T CreateAssetAsChild<T>(ScriptableObject scriptableObject, string name = null)
		where T : ScriptableObject
	{
		T asset = ScriptableObject.CreateInstance<T>();

		return ScriptableObjectUtility.InlineAddAssetAsChild<T>(scriptableObject, asset, name);
	}

	public static T GetChildAsset<T>(ScriptableObject scriptableObject, string name)
		where T : ScriptableObject
	{
		//T childAsset = AssetDatabase.LoadAssetAtPath<T>(
		//	Path.Combine(Path.ChangeExtension(
		//			path: AssetDatabase.GetAssetPath(scriptableObject.GetInstanceID()),
		//			extension: null
		//		),
		//		$"{name}.asset"
		//	)
		//);

		//Debug.LogWarning(
		//	Path.Combine(Path.ChangeExtension(
		//			path: AssetDatabase.GetAssetPath(scriptableObject.GetInstanceID()),
		//			extension: null
		//		),
		//		$"{name}.asset"
		//	)
		//);

		//return childAsset;

		Object[] childrenAssets = AssetDatabase.LoadAllAssetRepresentationsAtPath(
			AssetDatabase.GetAssetPath(scriptableObject.GetInstanceID())
		);

		for (int a = 0; a < childrenAssets.Length; a++)
		{
			if (childrenAssets[a] is T child && child.name == name)
				return child;
		}

		return null;
	}

	public static T GetChildAsset<T>(ScriptableObject scriptableObject)
		where T : ScriptableObject
	{
		Object[] childrenAssets = AssetDatabase.LoadAllAssetRepresentationsAtPath(
			AssetDatabase.GetAssetPath(scriptableObject.GetInstanceID())
		);

		for (int a = 0; a < childrenAssets.Length; a++)
		{
			if (childrenAssets[a] is T child)
				return child;
		}

		return null;
	}

	public static List<T> GetChildrenAssets<T>(ScriptableObject scriptableObject)
		where T : ScriptableObject
	{
		Object[] childrenAssets = AssetDatabase.LoadAllAssetRepresentationsAtPath(
			AssetDatabase.GetAssetPath(scriptableObject.GetInstanceID())
		);

		List<T> assets = new List<T>(childrenAssets.Length);

		for (int a = 0; a < childrenAssets.Length; a++)
		{
			if (childrenAssets[a] is T child)
				assets.Add(child);
		}

		return assets;
	}

	// [MethodImpl(MethodImplOptions.AggressiveInlining)]
	// public static T[] GetChildrenAssetsArray<T>(ScriptableObject scriptableObject)
	// 	where T : ScriptableObject => GetChildrenAssets<T>(scriptableObject).ToArray();

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static bool InlineRemoveChildAsset(ScriptableObject childAsset)
	{
		Object.DestroyImmediate(childAsset, true);

		//AssetDatabase.RemoveObjectFromAsset(childrenAssets[a]);
		//AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(childAsset.GetInstanceID()));

		//TODO: result is always true, consider removing it.

		return true;
	}

	public static bool RemoveChildAsset(ScriptableObject scriptableObject, ScriptableObject childAsset)
	{
		bool result = ScriptableObjectUtility.InlineRemoveChildAsset(childAsset);

		AssetDatabase.SaveAssets();

		AssetDatabase.Refresh();

		return result;
	}

	public static bool RemoveChildAsset<T>(ScriptableObject scriptableObject)
		where T : ScriptableObject
	{
		bool result = ScriptableObjectUtility.InlineRemoveChildAsset(ScriptableObjectUtility.GetChildAsset<T>(scriptableObject));

		AssetDatabase.SaveAssets();

		AssetDatabase.Refresh();

		return result;
	}

	public static void RemoveChildrenAssets<T>(ScriptableObject scriptableObject)
		where T : ScriptableObject
	{
		List<T> childrenAssets = ScriptableObjectUtility.GetChildrenAssets<T>(scriptableObject);
		
		for (int a = 0; a < childrenAssets.Count; a++)
		{
			Object.DestroyImmediate(childrenAssets[a], true);
		}

		AssetDatabase.SaveAssets();

		AssetDatabase.Refresh();
	}
	#endregion
}
#endif