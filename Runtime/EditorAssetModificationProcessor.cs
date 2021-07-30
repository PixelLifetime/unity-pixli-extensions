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
public class EditorAssetModificationProcessor : UnityEditor.AssetModificationProcessor
{
	///// <summary>
	///// This is called by Unity when inspecting an asset to determine if an editor should be disabled.
	///// This can also be called by custom editor scripts.
	///// </summary>
	///// <param name="one"></param>
	///// <param name="two"></param>
	//private static void IsOpenForEdit(string one, string two)
	//{
	//}

	///// <summary>
	///// Unity calls this method when it is about to create an Asset you haven't imported (for example, .meta files).
	///// </summary>
	///// <param name="assetName"></param>
	//private static void OnWillCreateAsset(string assetName)
	//{
	//	Debug.Log("OnWillCreateAsset is being called with the following asset: " + assetName + ".");
	//}

	public static readonly HashSet<int> S_LockedFilePaths = new HashSet<int>();

	public static void LockAsset(Object @object) => S_LockedFilePaths.Add(@object.GetInstanceID());
	public static bool UnlockAsset(Object @object) => S_LockedFilePaths.Remove(@object.GetInstanceID());

	/// <summary>
	/// This is called by Unity when it is about to delete an asset from disk.
	/// If this is implemented, it allows you to delete the asset yourself.
	/// Deletion of a file can be prevented by returning AssetDeleteResult.FailedDelete.
	/// You should not call any Unity AssetDatabase api from within this callback, 
	/// preferably keep to file operations or VCS apis.
	/// </summary>
	/// <param name="asset"></param>
	/// <param name="removeAssetOptions"></param>
	/// <returns></returns>
	//private static AssetDeleteResult OnWillDeleteAsset(string asset, RemoveAssetOptions removeAssetOptions)
	//{
	//	int assetInstanceID = AssetDatabase.LoadAssetAtPath<Object>(asset).GetInstanceID();

	//	if (S_LockedFilePaths.Contains(assetInstanceID))
	//		return AssetDeleteResult.FailedDelete;

	//	return default;
	//}

	///// <summary>
	///// Unity calls this method when it is about to move an Asset on disk.
	///// Implement this method to customize the actions Unity performs when moving an Asset inside the Editor.
	///// This method allows you to move the Asset yourself but, if you do, please remember to return the correct enum.
	///// Alternatively, you can perform some processing and let Unity move the file.The moving of the asset can be prevented by returning AssetMoveResult.FailedMove.
	///// You should not call any Unity AssetDatabase API from within this callback, preferably restrict yourself to the usage of file operations or VCS APIs.
	///// </summary>
	///// <param name="sourcePath"></param>
	///// <param name="destinationPath"></param>
	///// <returns></returns>
	//private static AssetMoveResult OnWillMoveAsset(string sourcePath, string destinationPath)
	//{
	//	AssetMoveResult assetMoveResult = AssetMoveResult.DidMove;

	//	Debug.Log("Source path: " + sourcePath + ". Destination path: " + destinationPath + ".");

	//	return assetMoveResult;
	//}

	///// <summary>
	///// This is called by Unity when it is about to write serialized assets or Scene files to disk.
	///// If it is implemented, it allows you to override which files are written by returning an array containing a subset of the pathnames which have been passed by Unity.Note that this function is static.
	///// </summary>
	///// <param name="paths"></param>
	///// <returns></returns>
	//private static string[] OnWillSaveAssets(string[] paths)
	//{
	//	Debug.Log("OnWillSaveAssets");
	//	foreach (string path in paths)
	//		Debug.Log(path);
	//	return paths;
	//}
}
#endif